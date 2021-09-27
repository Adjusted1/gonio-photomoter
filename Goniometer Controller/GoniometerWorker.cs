using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;

using Goniometer_Controller.Models;
using Goniometer_Controller.Motors;
using Goniometer_Controller.Sensors;

namespace Goniometer_Controller
{
    public class GoniometerWorker
    {
        private BackgroundWorker _worker;

        private List<BaseSensor> _sensors;

        private double[] _hRange;
        private double[] _vRange;
        private MeasurementCollection _data;

        private DateTime _startTime;
        private DateTime _stopTime;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hRange">horizontal testing range</param>
        /// <param name="vRange">vertical testing range</param>
        /// <param name="sensors">sensors to measure from</param>
        /// <param name="data">dataset to add data to</param>
        public GoniometerWorker(double[] hRange, double[] vRange, IEnumerable<BaseSensor> sensors, MeasurementCollection data)
        {
            _hRange = hRange;
            _vRange = vRange;
            _data = data;

            _worker = new BackgroundWorker();
            _worker.WorkerReportsProgress = true;
            _worker.WorkerSupportsCancellation = true;
            _worker.DoWork             += DoWork;
            _worker.ProgressChanged    += OnProgressChanged;
            _worker.RunWorkerCompleted += OnRunWorkerCompleted;

            _sensors = sensors.ToList();
        }

        #region state methods
        private bool _running = false;
        private bool _paused = false;

        /// <summary>
        /// Requests termination of goniometer process
        /// </summary>
        public void CancelAsync()
        {
            _worker.CancelAsync();
        }

        /// <summary>
        /// Requests temporary pause of goniometer process
        /// </summary>
        public void PauseAsync()
        {
            _paused = true;
            //TODO: Pause motor movement
        }

        /// <summary>
        /// Resumes temporary pause of goniometer process
        /// </summary>
        public void UnPauseAsync()
        {
            _paused = false;
        }

        /// <summary>
        /// Starts execution of goniometer
        /// </summary>
        public void RunAsync()
        {
            if (_running)
                throw new Exception("Goniometer is already running a test");

            _startTime = DateTime.Now;
            _running = true;

            _worker.RunWorkerAsync();
        }
        #endregion

        #region worker methods
        private void DoWork(object sender, DoWorkEventArgs e)
        {
            int progress;

            try
            {
                _worker.ReportProgress(0, "Test Started");

                for (int v = 0; v < _vRange.Length; v++)
                {
                    try
                    {
                        //update progress, move vertical arm
                        progress = (int)100 * (v / _vRange.Length);
                        _worker.ReportProgress(progress, String.Format("Preparing Vertical Angle: {0}", _vRange[v]));
                        MotorController.SetVerticalAngleAndWait(_vRange[v]);

                        //log vertical difference
                        double vertDiff = MotorController.GetVerticalEncoderPosition() - MotorController.GetVerticalMotorPosition();
                        _worker.ReportProgress(progress, String.Format("Vertical Diff: {0}", vertDiff));
                    }
                    catch (Exception ex)
                    {
                        var args = new GonioErrorEventArgs(ex);
                        OnError(this, args);

                        if (args.Stop)
                        {
                            //halt test
                            e.Cancel = true;
                            return;
                        }
                        else if (!args.Skip)
                        {
                            //go back one step and start over
                            v--;
                            continue; //vertical for loop
                        }
                    }

                    for (int h = 0; h < _hRange.Length; h++)
                    {
                        try
                        {
                            //update progress, move horizontal motor
                            progress = (int) 100 * ((v / _vRange.Length) + (h / _hRange.Length) * (1 / _vRange.Length));
                            _worker.ReportProgress(progress, String.Format("Preparing Horizontal Angle: {0}", _hRange[h]));
                            MotorController.SetHorizontalAngleAndWait(_hRange[h]);

                            //log horizontal difference
                            double horzDiff = MotorController.GetHorizontalEncoderPosition() - MotorController.GetHorizontalMotorPosition();
                            _worker.ReportProgress(progress, String.Format("Horizontal Diff: {0}", horzDiff));

                            //loop if paused
                            do
                            {
                                //check for cancel
                                Thread.Sleep(100);
                                if (_worker.CancellationPending)
                                {
                                    e.Cancel = true;
                                    return;
                                }

                            } while (_paused);

                            //collect measurements
                            _worker.ReportProgress(progress, "Taking Measurements");

                            double theta = _hRange[h];
                            double phi = _vRange[v];
                            double exactTheta = MotorController.GetHorizontalEncoderPosition();
                            double exactPhi = MotorController.GetVerticalEncoderPosition();

                            var measurements = _sensors
                                .SelectMany(s => s.CollectMeasurements(theta, phi, exactTheta, exactPhi))
                                .ToList();                                                          //evaluate now

                            //validate measurements
                            measurements.ForEach(m => _data.AssertUniqueValue(m));

                            //add measurements to collection
                            measurements.ForEach(m => _data.Add(m));

                            //notify subscribes about measurements
                            var measureArgs = new MeasurementEventArgs(measurements);
                            OnMeasurementTaken(this, measureArgs);
                        }
                        catch (Exception ex)
                        {
                            var args = new GonioErrorEventArgs(ex);
                            OnError(this, args);

                            if (args.Stop)
                            {
                                //halt test
                                e.Cancel = true;
                                return;
                            }
                            else if (!args.Skip)
                            {
                                //go back one step and start over
                                h--;
                                continue; //vertical for loop
                            }
                        }
                    } //vertical for loop
                } //horizontal for loop

                //return mirror to start point, do in small increments
                MotorController.SetVerticalAngleAndWait(90);
                MotorController.SetVerticalAngleAndWait(45);
                MotorController.SetVerticalAngleAndWait(0);

                //return lamp to start point
                MotorController.SetHorizontalAngleAndWait(0);

                _worker.ReportProgress(100, "Test Complete");

                e.Result = _data;
            }
            catch (Exception ex)
            {
                var args = new GonioErrorEventArgs(ex);
                OnError(this, args);
            }
            finally
            {
                MotorController.SetVerticalAngle(0);
                MotorController.SetHorizontalAngle(0);
            }
        }

        /// <summary>
        /// Receive updates on progress. 
        /// 
        /// e.UserState is a string with status information.
        /// </summary>
        public event ProgressChangedEventHandler ProgressChanged;
        protected virtual void OnProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var temp = ProgressChanged;
            if (temp != null)
                temp(this, e);
        }

        /// <summary>
        /// Receive notification of completion. 
        /// 
        /// e.Cancelled indicates success/failure.
        /// e.Result contains List of Tuples(double,double,double): hAngle,vAngle,reading
        /// e.Error contains any errors that may have been encountered
        /// </summary>
        public event RunWorkerCompletedEventHandler RunWorkerCompleted;
        protected virtual void OnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _running = false;
            _stopTime = DateTime.Now;

            var temp = RunWorkerCompleted;
            if (temp != null)
                temp(this, e);
        }
        
        /// <summary>
        /// Receive notification of error.
        /// 
        /// set e.stop to indicate if this error should be considered critical
        /// set e.skip to indicate if we should skip or retry the previous angle
        /// </summary>
        public event EventHandler<GonioErrorEventArgs> Error;
        protected virtual void OnError(object sender, GonioErrorEventArgs e)
        {
            var temp = Error;
            if (temp != null)
                temp(this, e);
        }

        /// <summary>
        /// Receive notification that successful measurement occured
        /// </summary>
        public event EventHandler<MeasurementEventArgs> MeasurementTaken;
        protected virtual void OnMeasurementTaken(object sender, MeasurementEventArgs e)
        {
            var temp = MeasurementTaken;
            if (temp != null)
                temp(this, e);
        }
        #endregion

        public MeasurementCollection GetResults()
        {
            if (_running)
                throw new InvalidOperationException("Progress not complete.");

            return _data;
        }

        public class GonioErrorEventArgs : EventArgs
        {
            /// <summary>
            /// indicates that failure is unrecoverable and the test should halt
            /// set to false if test should continue
            /// </summary>
            public bool Stop = false;

            /// <summary>
            /// indicates that failure is recoverable and the test should continue at next datapoint
            /// set to true if datapoint should recollect
            /// </summary>
            public bool Skip = false;

            /// <summary>
            /// contains the exception caught during Gonio process
            /// </summary>
            public Exception Exception;

            public GonioErrorEventArgs(Exception exception)
            {
                this.Exception = exception;
            }
        }

        public class MeasurementEventArgs : EventArgs
        {
            /// <summary>
            /// list of measurements collected during this event
            /// </summary>
            public List<MeasurementBase> Measurements;

            public MeasurementEventArgs(IEnumerable<MeasurementBase> measurements)
                : base()
            {
                this.Measurements = measurements.ToList();
            }
        }
    }
}

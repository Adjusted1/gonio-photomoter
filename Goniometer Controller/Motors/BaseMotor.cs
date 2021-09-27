using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Goniometer_Controller.Motors
{
    internal class BaseMotor
    {
        protected readonly double _accuracy = 0.35; //scaled units
        //protected readonly double _accuracy = 0.15; //scaled units

        protected short _axisNumber;
        protected double _scale;

        protected MotionSpeed _speed;

        /// <summary>
        /// controls the max accerlation and velocity for the motor
        /// </summary>
        public MotionSpeed Speed
        { 
            get { return _speed; }
            set { _speed = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="axisNumber">zero indexed</param>
        /// <param name="scale"></param>
        public BaseMotor(short axisNumber, double scale)
        {
            this._axisNumber = axisNumber;
            this._scale = scale;
            this._speed = MotionSpeed.Normal;
        }

        public virtual void Initialize()
        {

        }

        public virtual double GetMotorPosition()
        {
            short axis = (short) (_axisNumber + 1);
            int pos = MotorSocketProvider.GetMotorPosition(axis);
            return pos / _scale;
        }

        public virtual double GetEncoderPosition()
        {
            short axis = (short)(_axisNumber + 1);
            int pos = MotorSocketProvider.GetEncoderPosition(axis);
            return pos / _scale;
        }

        /// <summary>
        /// Set motor encoder offset Value to zero
        /// </summary>
        public virtual void ZeroMotor()
        {
            string cmd = "pset";
            cmd += ','.Multiply(_axisNumber);
            cmd += "0:";

            MotorSocketProvider.Write(cmd); 
        }

        #region initialize commands

        #endregion

        #region motion commands
        /// <summary>
        /// Instruct Motor to move
        /// This method makes no guarantee that the motor will reach its destination
        /// in a timely manner (or at all)
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="velocity"></param>
        /// <param name="acceleration"></param>
        public virtual void Move(double distance, double velocity, double acceleration)
        {
            //adjust for scale direction
            if (_scale < 0)
                distance *= -1;

            //apply speed factors
            velocity /= (int) _speed;
            acceleration /= (int)_speed;

            string cmd = "";

            //enable drive
            cmd += "drive";
            cmd += 'x'.Multiply(_axisNumber);
            cmd += "1:";

            //distance
            cmd += "d";
            cmd += ','.Multiply(_axisNumber);
            cmd += distance + ":";

            //velocity
            cmd += "v";
            cmd += ','.Multiply(_axisNumber);
            cmd += velocity + ":";

            //acceleration
            cmd += "a";
            cmd += ','.Multiply(_axisNumber);
            cmd += acceleration + ":";

            //average acceleration
            cmd += "aa";
            cmd += ','.Multiply(_axisNumber);
            cmd += acceleration + ":";

            //deceleration
            cmd += "ad";
            cmd += ','.Multiply(_axisNumber);
            cmd += acceleration + ":";

            //average deceleration
            cmd += "ada";
            cmd += ','.Multiply(_axisNumber);
            cmd += acceleration + ":";

            //execute
            cmd += "!go";
            cmd += 'x'.Multiply(_axisNumber);
            cmd += "1:";

            MotorSocketProvider.Write(cmd); 
        }

        /// <summary>
        /// Instruct Motor to move, return when motion stopped
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="velocity"></param>
        /// <param name="acceleration"></param>
        /// <exception cref="TimeoutException">Throw if the motor does not reach it's destination in a timely manner</exception>
        public virtual void MoveAndWait(double distance, double velocity, double acceleration)
        {
            //is motion even required?
            if (Math.Abs(GetMotorPosition() - distance) < _accuracy)
                return;

            //set maximum time for a full movement
            TimeSpan timeout = new TimeSpan(0, 4, 0);
            DateTime startTime = DateTime.Now;

            //record current position
            double lastPosition = GetMotorPosition();
            
            //command motor to move, then wait some time
            Move(distance, velocity, acceleration);
            Thread.Sleep(3000);
            
            //has the motor stopped moving?
            while (Math.Abs(GetMotorPosition() - lastPosition) > 0.15)
            {
                //we've exceeded our alloted time
                if (DateTime.Now - startTime > timeout)
                    throw new TimeoutException("The motor hasn't stabilized in a reasonable time");

                //record new current position
                lastPosition = GetMotorPosition();

                //sleep then check again
                Thread.Sleep(1000);
            }

            //halt motion at destination
            StopMotion();
        }

        public void StopMotion()
        {
            string cmd = "s";
            cmd += '0'.Multiply(_axisNumber);
            cmd += "1:";

            MotorSocketProvider.Write(cmd);
        }

        public void EmergencyStop()
        {
            string cmd = "!k";
            cmd += 'x'.Multiply(_axisNumber);
            cmd += "1:";

            MotorSocketProvider.Write(cmd); 
        }
        #endregion

        public void SendCommand(string cmd, string data)
        {
            cmd += ','.Multiply(_axisNumber);
            cmd += data + ":";

            MotorSocketProvider.Write(cmd); 
        }

        /// <summary>
        /// indicates that the motor has not moved when movement was expected
        /// </summary>
        public class MotorStoppedException : Exception
        {
        }

        /// <summary>
        /// indicates that the motor has moved when movement wasn't expected
        /// </summary>
        public class MotorUnstableException : Exception
        {
        }
    }
}

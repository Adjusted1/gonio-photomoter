using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading;
using System.Net;

namespace Goniometer_Controller.Motors
{
    /* SO yeah... you might be asking yourself: is the static/singleton really necessary here?
     * Pretty much. I'd like to have multiple consumers of the MotorController (readers and writers),
     * but I also need to support: 1) the ability to reset the connection without requiring everyone to
     * fetch new instances from a factory. Additionally, 2) some methods require a write-wait-read pattern
     * that should lock the socket for the duration of that command.
     */

    public static class MotorController
    {
        private static HorizontalMotor _horizontalMotor;
        private static VerticalMotor _verticalMotor;

        /// <summary>
        /// controls the max accerlation and velocity for the horizontal motor
        /// </summary>
        public static MotionSpeed HorizontalSpeed
        {
            get { return _horizontalMotor.Speed; }
            set { _horizontalMotor.Speed = value; }
        }

        /// <summary>
        /// controls the max accerlation and velocity for the vertical motor
        /// </summary>
        public static MotionSpeed VerticalSpeed
        {
            get { return _verticalMotor.Speed; }
            set { _verticalMotor.Speed = value; }
        }

        #region configuration and initialization
        static MotorController()
        {
            _verticalMotor   = new VerticalMotor();
            _horizontalMotor = new HorizontalMotor();
        }

        public static void Connect(IPAddress address)
        {
            //halt all motion during configuration process
            EmergencyStop();

            MotorSocketProvider.SetIPAddress(address);

            _verticalMotor.Initialize();
            _horizontalMotor.Initialize();

            //Global Controller Commands
            MotorSocketProvider.Write("scale1");    //set all axis to scale mode
            MotorSocketProvider.Write("!comexc1:"); //set continuous execution mode
            MotorSocketProvider.Write("!comexs0:"); //stop execution on stop command

            //Default not zeroing!
            ExitZeroingMode();
        }
        #endregion

        #region get angles
        public static double GetHorizontalMotorPosition()
        {
            return _horizontalMotor.GetMotorPosition();
        }

        public static double GetHorizontalEncoderPosition()
        {
            return _horizontalMotor.GetEncoderPosition();
        }

        public static double GetVerticalMotorPosition()
        {
            return _verticalMotor.GetMotorPosition();
        }

        public static double GetVerticalEncoderPosition()
        {
            return _verticalMotor.GetEncoderPosition();
        }
        #endregion

        #region set angles
        public static void SetHorizontalAngle(double angle)
        {
            _horizontalMotor.Move(angle);
        }

        public static void SetVerticalAngle(double angle)
        {
            _verticalMotor.Move(angle);
        }

        public static void SetHorizontalAngleAndWait(double angle)
        {
            _horizontalMotor.MoveAndWait(angle);
        }

        public static void SetVerticalAngleAndWait(double angle)
        {
            _verticalMotor.MoveAndWait(angle);
        }
        #endregion

        #region zero motors
        private static bool _zeroing;        
        public static void EnterZeroingMode()
        {
            //set all axis to incremental mode (not absolute)
            MotorSocketProvider.Write("ma0000:");     
            _zeroing = true;
        }

        public static void ZeroHorizontalMotor()
        {
            if (!_zeroing)
                throw new InvalidOperationException("Call EnterZeroingMode before calling Zero Methods");

            _horizontalMotor.ZeroMotor();
        }

        public static void ZeroVerticalMotor()
        {
            if (!_zeroing)
                throw new InvalidOperationException("Call EnterZeroingMode before calling Zero Methods");

            _verticalMotor.ZeroMotor();
        }

        public static void ExitZeroingMode()
        {
            //set all axis to absolute mode (not incremental)
            MotorSocketProvider.Write("ma1111:");     
            _zeroing = false;
        }
        #endregion

        #region error checking
        public static void CheckErrorStatus()
        {
            string cmd = "ter!:";
            string result = MotorSocketProvider.WriteForResponse(cmd);

            //parse result;
        }

        public static void CheckMotorEnabled()
        {
            string cmd = "tino!:";
            string result = MotorSocketProvider.WriteForResponse(cmd);
        
            //parse result;
        }
        #endregion

        public static void EmergencyStop()
        {
            try
            {
                MotorSocketProvider.Write("!k:");
            }
            catch(Exception)
            {
                //omnomnom
            }
        }
    }
}

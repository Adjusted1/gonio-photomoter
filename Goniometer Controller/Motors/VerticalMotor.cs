using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goniometer_Controller.Motors
{
    internal class VerticalMotor : BaseMotor
    {
        private static double _min = 0;
        private static double _max = 180;
                
        private static double _velocity = 5;
        private static double _accerlation = 3;
                
        private static short _motorAxis = 0;
        private static short _encoderAxis = 3;

        private static double _motorScale = -9102.2;
        private static double _encoderScale = 27.777;

        //motor encoder resolution * 4 = 8192 counts per rev
        //load  encoder resolution * 4 = 10000 counts per rev

        //scale factors:
        //motor scale factor: 400:1 gear reducer = 3,276,800 counts per rev / 360deg = 9102.2... counts per degree
        //load  scale factor:         no reducer =    10,000 counts per rev / 360deg = 27.7...

        public VerticalMotor()
            : base(_motorAxis, _motorScale)
        {

        }

        public override void Initialize()
        {
            base.Initialize();

            this.SendCommand("eres",  "8192");        //encoder resolution * 4 = 8192 counts per rev
            this.SendCommand("lh",    "0");           //Hardware End-of-Travel Limit — Enable Checking: Disable negative-direction limit; Disable positive-direction limit: i = 0

            //scale factors: 400:1 gear reducer = 3,276,800 counts per rev / 360deg = 9102.222 counts per degree
            this.SendCommand("scla",  "9102");        //Acceleration Scale Factor [rev/sec/sec]
            this.SendCommand("sclv",  "9102");        //Velocity Scale Factor     [rev/sec]
            this.SendCommand("scld",  "9102");        //Distance Scale Factor     [rev]

            //feedback values
            this.SendCommand("smper", "4");           //Maximum Allowable Position Error
            this.SendCommand("sgp",   "20");          //Proportional Feedback Gain [microvolts/step]
            this.SendCommand("sgv",   "3");           //Velocity Feedback Gain     [microvolts/step/sec]
            this.SendCommand("sgi",   "0");           //
        }

        public void Move(double angle)
        {
            this.Move(angle, _velocity, _accerlation);
        }

        public void MoveAndWait(double angle)
        {
            this.MoveAndWait(angle, _velocity, _accerlation);
        }

        public override double GetEncoderPosition()
        {
            short axis = (short)(_encoderAxis + 1);
            int pos = MotorSocketProvider.GetEncoderPosition(axis);
            return pos / _encoderScale;
        }

        public override void ZeroMotor()
        {
            //zero motor
            base.ZeroMotor();

            //zero encoder
            string cmd = "pset";
            cmd += ','.Multiply(_encoderAxis);
            cmd += "0:";

            MotorSocketProvider.Write(cmd);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goniometer_Controller.Motors
{
    internal class HorizontalMotor : DualFeedbackMotor
    {
        private static double _min = 0;
        private static double _max = 360;

        private static double _velocity = 3;
        //private static double _velocity = 5;
        private static double _accerlation = 1;
        //private static double _accerlation = 3;

        private static short _motorAxis = 1;
        private static short _encoderAxis = 2;

        private static double _motorScale = 2222.2;
        private static double _encoderScale = -13.888;

        //motor encoder resolution * 4 = 8000 counts per rev
        //load  encoder resolution * 4 = 5000 counts per rev

        //scale factors:
        //motor scale factor: 100:1 gear reducer = 800,000 counts per rev / 360deg = 2222.222 counts per degree
        //load  scale factor:         no reducer =    5000 counts per rev / 360deg = 13.8...

        //load encoder runs negative numbers

        public HorizontalMotor()
            : base(_motorAxis, _motorScale, _encoderAxis, _encoderScale)
        {

        }

        public override void Initialize()
        {
            base.Initialize();

            //encoder resolution not necessary?
            this.SendCommand("eres", "8000");    //encoder resolution * 4 = 8000 counts per rev
            this.SendCommand("lh",   "0");       //Hardware End-of-Travel Limit — Enable Checking: Disable negative-direction limit; Disable positive-direction limit: i = 0

            //scale factors: 100:1 gear reducer = 800,000 counts per rev / 360deg = 2222.222 counts per degree
            this.SendCommand("scla", "2222");    //Acceleration Scale Factor [rev/sec/sec]
            this.SendCommand("sclv", "2222");    //Velocity Scale Factor     [rev/sec]
            this.SendCommand("scld", "2222");    //Distance Scale Factor     [rev]

            //feedback values
            this.SendCommand("smper", "15");      //Maximum Allowable Position Error
            this.SendCommand("sgp",   "1");       //Proportional Feeback Gain [microvolts/step]
            this.SendCommand("sgv",   "15");      //Velocity Feedback Gain    [microvolts/step/sec]
        }

        public void Move(double angle)
        {
            this.Move(angle, _velocity, _accerlation);
        }

        public void MoveAndWait(double angle)
        {
            this.MoveAndWait(angle, _velocity, _accerlation);
        }
    }
}

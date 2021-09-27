using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goniometer_Controller.Motors
{
    /// <summary>
    /// UnidirectionMotor should always be run in absolute mode!!!
    /// </summary>
    internal class UnidirectionalMotor : BaseMotor
    {
        protected Direction _direction;
        protected double _circumference;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="axisNumber"></param>
        /// <param name="scale"></param>
        /// <param name="direction"></param>
        /// <param name="circumference"></param>
        public UnidirectionalMotor(short axisNumber, double scale, Direction direction, double circumference)
            : base(axisNumber, scale)
        {
            this._direction = direction;
            this._circumference = circumference;
        }

        public override double GetMotorPosition()
        {
            //always report angle as the mod for the max angle (circumference)
            double value = base.GetMotorPosition();
            return value % _circumference;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="distance">desired angle</param>
        /// <param name="velocity"></param>
        /// <param name="acceleration"></param>
        public override void Move(double distance, double velocity, double acceleration)
        {
            //check current location (mod adjusted)
            double current = this.GetMotorPosition();

            //calculate incremental movement
            distance -= current;

            //is move too small to care?
            if (Math.Abs(distance) < _accuracy)
                return;

            if (_direction == Direction.Clockwise)
            {
                //do we need to move all the way around?
                if (distance < 0)
                {
                    distance += _circumference;
                }
            }
            else
            {
                //do we need to move all the way around?
                if (distance > 0)
                {
                    distance -= _circumference;
                }
            }

            base.Move(distance, velocity, acceleration);
        } 

        public enum Direction : int
        {
            Clockwise = 1,
            CounterClockwise = -1
        }
    }
}

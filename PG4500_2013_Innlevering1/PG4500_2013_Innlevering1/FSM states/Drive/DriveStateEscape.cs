using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robocode;

namespace PG4500_2013_Innlevering1
{
    class DriveStateEscape : State
    {
        public DriveStateEscape(ref RoboData rData) 
        {
            this.rData = rData;
            steeringBehavior = new SteeringBehavior();
        }

        public override void Update()
        {
            //Steeringbehavior flee and wall avoidence
        }

        public override int GetNewState()
        {
            //If more energy; change to RAM
            //If the same energy; change to AVOID
            return base.GetNewState();
        }
    }
}

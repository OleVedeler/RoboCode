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
        SteeringBehavior steeringBehavior;

        public DriveStateEscape() 
        {
            steeringBehavior = new SteeringBehavior();
        }

        public override void Update()
        {
            //Steeringbehavior flee and wall avoidence
            ahead(steeringBehavior.Flee());
        }

        public override int GetNewState()
        {
            //If more energy; change to RAM
            //If the same energy; change to Avoid
            return base.GetNewState();
        }
    }
}

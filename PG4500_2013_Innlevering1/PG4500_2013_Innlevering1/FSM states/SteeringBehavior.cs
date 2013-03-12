using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Santom;
using Robocode;

namespace PG4500_2013_Innlevering1
{
    class SteeringBehavior
    {
        RoboData rData;
        EnemyData eData;

        public SteeringBehavior(ref RoboData rData, ref EnemyData eData) 
        {
            this.rData = rData;
            this.eData = eData;
        }

        public Vector2D Flee(Vector2D targetPos, Vector2D vehiclePos, double MAX_SPEED, Vector2D velocity)
        {


            Vector2D desiredVelociyNormalized = targetPos - vehiclePos;
            desiredVelociyNormalized.Normalize();
            Vector2D desiredVelocity = MAX_SPEED * desiredVelociyNormalized;
            return desiredVelocity - velocity;
        }

        public Point2D Evade(Point2D bulletPos, Point2D vehiclePos) 
        {
            return null;
        }

        public Point2D Pursuit(Point2D targetPos, Point2D vehiclePos)
        {
            return null;
        }

        public Point2D WallAvoidance(Point2D wallPos, Point2D vehiclePos)
        {
            return null;
        }

        public Point2D OffsetPursuit(Point2D targetPos, Point2D vehiclePos)
        {
            return null;
        }
    }
}

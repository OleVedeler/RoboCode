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
        EnemyData eData;

        public SteeringBehavior(ref EnemyData eData)
        {
            this.eData = eData;
        }

        public Vector2D Flee(Point2D position)
        {
            //RoboHelpers.CalculateTargetVector(

            Vector2D newDirection = new Vector2D((eData.Position - position).X, (eData.Position - position).Y);
            newDirection.Normalize();
            newDirection *= Rules.MAX_VELOCITY;

            return newDirection;
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
			double distanceToWall = 10;
			double lookAhead = 20;

            return null;
        }

        public Point2D OffsetPursuit(Point2D targetPos, Point2D vehiclePos)
        {
            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Santom;
using Robocode;
using Robocode.Util;

namespace PG4500_2013_Innlevering1
{
    class SteeringBehavior
    {
        EnemyData eData;
		Vedole_Joroiv_TheAntSmasher robo;
        public SteeringBehavior(ref EnemyData eData, Vedole_Joroiv_TheAntSmasher robo)
        {
			this.robo = robo;
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

        public void Pursuit()
        {
			double time = eData.Distance / Rules.MAX_VELOCITY;

			Vector2D newOffset = new Vector2D();
			newOffset.X = time * eData.Velocity * Math.Cos(eData.Heading);
			newOffset.Y = time * eData.Velocity * Math.Sin(eData.Heading);

			double angle = Math.Tan((newOffset.Length() / eData.Distance));

			Seek(angle);
        }

		public void Seek(double turnAngle)
		{
			double angle = Utils.NormalRelativeAngleDegrees(turnAngle);
			robo.SetTurnRight(eData.Bearing + angle);
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

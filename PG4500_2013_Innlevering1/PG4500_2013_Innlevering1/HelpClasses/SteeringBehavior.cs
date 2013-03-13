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


        public Vector2D Pursuit()
        {
            
            Vector2D newPos = new Vector2D(eData.Position.X,eData.Position.Y);
            
            for (int i = 0; i < (int)eData.Distance / Rules.MAX_VELOCITY; i++)
			{
			    
                newPos += new Vector2D(eData.Velocity * Math.Sin(Utils.ToRadians(eData.Heading)),eData.Velocity * Math.Cos(Utils.ToRadians(eData.Heading)));
			}

            Vector2D relativeVector = newPos - robo.Position;

            double enemyPosHeading = Utils.ToDegrees(Math.Atan2(relativeVector.X, relativeVector.Y));
            
            double deltaAngle = enemyPosHeading - robo.Heading;

            Seek(deltaAngle);

            return newPos;
        }

		public void Seek(double turnAngle)
		{
			double angle = Utils.NormalRelativeAngleDegrees(turnAngle);
			robo.SetTurnRight(angle);
		}

        public Point2D WallAvoidance(Point2D wallPos, Point2D vehiclePos)
        {
			double distanceToWall = 10;
			double lookAhead = 20;

            return null;
        }

        public void OffsetPursuit()
        {
            if (eData.Distance > 40)
                Pursuit();
        }
    }


    /*
     double time = eData.Distance / eData.Velocity;

            Vector2D ePos = new Vector2D(eData.Position.X, eData.Position.Y);

            ePos.X = (eData.Velocity * Math.Sin(Utils.ToRadians(Utils.NormalRelativeAngleDegrees(eData.Heading))));
            ePos.Y = (eData.Velocity * Math.Cos(Utils.ToRadians(Utils.NormalRelativeAngleDegrees(eData.Heading))));
            ePos = Vector2D.Normalize(ePos);

            Vector2D newPosition = new Vector2D(robo.Position.X, robo.Position.Y);
            newPosition.X += ePos.X * time;
            newPosition.Y += ePos.Y * time;

            double angle = Math.Tan(new Vector2D(newPosition.X - robo.Position.X, newPosition.Y - robo.Position.Y).Length() / eData.Distance);

            /*
            double time = eData.Distance / Rules.MAX_VELOCITY;

            Vector2D vel = new Vector2D();
            vel.X = (eData.Velocity * Math.Sin(RoboHelpers.DegreesToRadians(eData.Heading)));
            vel.Y = (eData.Velocity * Math.Cos(RoboHelpers.DegreesToRadians(eData.Heading)));

			Vector2D newOffset = new Vector2D();
			newOffset.X = (time * vel.X) * Math.Sin(RoboHelpers.DegreesToRadians(eData.Heading));
            newOffset.Y = (time * vel.Y) * Math.Cos(RoboHelpers.DegreesToRadians(eData.Heading));

            //Denne formelen er riktig
			double angle = Math.Tan(newOffset.Length() / eData.Distance);
            */
}

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
        public Vector2D leftHorn, midHorn, rightHorn;

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

        public void WallAvoidance()
        {
            int nodeSize = 80; //Pixels
            //Boolean hasHitWall = false;

            leftHorn = new Vector2D(Math.Sin(Utils.ToRadians(robo.Heading - 45)) * (nodeSize),
                Math.Cos(Utils.ToRadians(robo.Heading - 45)) * (nodeSize));
            
            midHorn = new Vector2D(Math.Sin(Utils.ToRadians(robo.Heading)) * nodeSize, 
                Math.Cos(Utils.ToRadians(robo.Heading)) * nodeSize);
            
            rightHorn = new Vector2D(Math.Sin(Utils.ToRadians(robo.Heading + 45)) * (nodeSize),
                Math.Cos(Utils.ToRadians(robo.Heading + 45)) * (nodeSize));

            /*
            middleFeeler = RobotHelpers.GetHeadingVector(robot.HeadingRadians) * feelerLength;
            leftFeeler = RobotHelpers.GetHeadingVector(robot.HeadingRadians - Math.PI / 5) * sideFeelerLength;
            rightFeeler = RobotHelpers.GetHeadingVector(robot.HeadingRadians + Math.PI / 5) * sideFeelerLength;
            */

            leftHorn += robo.Position;
            midHorn += robo.Position;
            rightHorn += robo.Position;

            if (midHorn.X < 0 || midHorn.X > robo.BattleFieldWidth ||
                midHorn.Y < 0 || midHorn.Y > robo.BattleFieldHeight)
            {
                robo.SetTurnLeft(10);
            }

            if (leftHorn.X < 0 || leftHorn.X > robo.BattleFieldWidth ||
                leftHorn.Y < 0 || leftHorn.Y > robo.BattleFieldHeight)
            {
                robo.SetTurnRight(10);

                if (midHorn.X < 0 || midHorn.X > robo.BattleFieldWidth ||
                midHorn.Y < 0 || midHorn.Y > robo.BattleFieldHeight)
                {
                    robo.SetTurnRight(20);
                }
            }

            if (rightHorn.X < 0 || rightHorn.X > robo.BattleFieldWidth ||
                rightHorn.Y < 0 || rightHorn.Y > robo.BattleFieldHeight)
            {
                robo.SetTurnLeft(10);

                if (midHorn.X < 0 || midHorn.X > robo.BattleFieldWidth ||
                midHorn.Y < 0 || midHorn.Y > robo.BattleFieldHeight)
                {
                    robo.SetTurnLeft(20);
                }
            }
        }

        public void OffsetPursuit()
        {
            if (eData.Distance > 40)
                Pursuit();
        }

		public Vector2D AimInFront()
		{
			Vector2D newPos = new Vector2D(eData.Position.X, eData.Position.Y);
			Vector2D BulletPos = new Vector2D(robo.Position.X, robo.Position.Y);
			int lengthPerTurn = (int)Math.Round(eData.Distance / (Rules.MAX_VELOCITY + (20 - 3 * Rules.MIN_BULLET_POWER)));
			
			for (int i = 0; BulletPos == newPos; i++)
			{
				newPos += new Vector2D(eData.Velocity * Math.Sin(Utils.ToRadians(eData.Heading)), eData.Velocity * Math.Cos(Utils.ToRadians(eData.Heading)));
				BulletPos += new Vector2D((20 - 3 * Rules.MIN_BULLET_POWER) * Math.Sin(Utils.ToRadians(robo.Heading)), (20 - 3 * Rules.MIN_BULLET_POWER)  * Math.Cos(Utils.ToRadians(robo.Heading)));
			}

			Vector2D relativeVector = newPos - robo.Position;

            double enemyPosHeading = Utils.ToDegrees(Math.Atan2(relativeVector.X, relativeVector.Y));
            
            double deltaAngle = enemyPosHeading - robo.GunHeading;

			robo.SetTurnGunRight(Utils.NormalRelativeAngleDegrees(deltaAngle));
			
			return newPos;
		}
    }
}

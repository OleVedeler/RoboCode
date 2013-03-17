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
		double oldEnemyHeading;
        public SteeringBehavior(ref EnemyData eData, Vedole_Joroiv_TheAntSmasher robo)
        {
			this.robo = robo;
            this.eData = eData;
			oldEnemyHeading = eData.Heading;
        }
		// kjører i motsatt retning av fienden
        public void Flee()
        {
			robo.SetTurnRight(Utils.NormalRelativeAngleDegrees(eData.Bearing - 180));
        }

		double PreEnergy = 100;
		// prøver å holde seg på en 90 graders vinkel av fiendens heading
		// sjekker om fienden mister energi,
 		// skifter rettning hvis den mister mer en 3 energi
        public void Evade() 
        {
			//Setter vår robot 90 grader på hans så det er lettere å evade sidelangs
			robo.SetTurnRight(eData.Bearing + 90);
			if (PreEnergy != eData.Energy)
			{
				if(PreEnergy - eData.Energy <= 3)
					robo.moveDir *= -1;
				PreEnergy = eData.Energy;
			}
        }

		// Finner hvor tanken befinner seg
 		// kjører mot det punktet
		// returnerer en vector2D for å tegne boksen du kjører mot
        public Vector2D Pursuit()
        {    
            Vector2D newPos = new Vector2D(eData.Position.X,eData.Position.Y);
            
            for (int i = 0; i < (int)(eData.Distance / Rules.MAX_VELOCITY); i++)
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
			robo.SetTurnRight(angle * robo.moveDir);
		}
		
		// Setter noder i kjøreretningen 
		// sjekker om nodene treffer veggen
		// svinger vekk fra veggen
        public void WallAvoidance()
        {
            int nodeSize = 100; //Pixels

            leftHorn = new Vector2D(Math.Sin(Utils.ToRadians(robo.Heading - 45)) * (nodeSize) * robo.moveDir,
                Math.Cos(Utils.ToRadians(robo.Heading - 45)) * (nodeSize) * robo.moveDir);

			midHorn = new Vector2D(Math.Sin(Utils.ToRadians(robo.Heading)) * nodeSize * robo.moveDir,
				Math.Cos(Utils.ToRadians(robo.Heading)) * nodeSize * robo.moveDir);

			rightHorn = new Vector2D(Math.Sin(Utils.ToRadians(robo.Heading + 45)) * (nodeSize) * robo.moveDir,
				Math.Cos(Utils.ToRadians(robo.Heading + 45)) * (nodeSize) * robo.moveDir);

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

		// følger etter fienden så lenge det er på avstand
        public void OffsetPursuit()
        {
			if (eData.Distance > 600)
				Pursuit();
			else
				Flee();
        }

		// Finner farten og tiden det tar for kullen å gå avstanden
		// Finner hvor tanken vil være i samme tidsrom
		// Finner vinkelen du må skyte for å treffe tanksen
		// returnerer en vector for å kunne tegne boksen.
		public Vector2D AimInFront(double Firepower)
		{			
			double bulletSpeed = (20 - Firepower * 3);
			long time = (long)(eData.Distance / bulletSpeed);

			Vector2D FuturePos = new Vector2D(
				eData.Position.X + Math.Sin(Utils.ToRadians(eData.Heading)) * eData.Velocity * time, 
				eData.Position.Y + Math.Cos(Utils.ToRadians(eData.Heading)) * eData.Velocity * time);

			double enemyHeadingChange = eData.Heading - oldEnemyHeading;
			oldEnemyHeading = eData.Heading;

			if (Math.Abs(eData.TurnRate) > 0)
			{
				FuturePos = new Vector2D(
					eData.Position.X + Math.Sin(Utils.ToRadians(eData.Heading + enemyHeadingChange)) * eData.Velocity * time,
					eData.Position.Y + Math.Cos(Utils.ToRadians(eData.Heading + enemyHeadingChange)) * eData.Velocity * time);
			}

            double enemyPosHeading = Utils.ToDegrees(Math.Atan2(FuturePos.X - robo.Position.X, FuturePos.Y - robo.Position.Y));
			
            double deltaAngle = enemyPosHeading - robo.GunHeading;

			robo.SetTurnGunRight(Utils.NormalRelativeAngleDegrees(deltaAngle));

			return FuturePos;
		}
    }
}

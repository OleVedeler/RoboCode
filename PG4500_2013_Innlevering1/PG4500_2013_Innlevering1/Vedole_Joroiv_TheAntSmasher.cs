using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robocode;
using Robocode.Util;
using System.Drawing;
using Santom;


namespace PG4500_2013_Innlevering1 
{
    //Enum for all the drivestates
    public enum DriveState
    {
        RAM,
        ESCAPE,
        AVOID
    };

    //Forskjellige Statene for enum maskin
    public enum TurretState
    {
        SCAN,
        ATTACK,
        SAVEENERGY
    }

	class Vedole_Joroiv_TheAntSmasher : AdvancedRobot
	{
        public Vector2D Position
        {
            get { return new Vector2D(this.X, this.Y); }
        }

        RoboData rData = new RoboData();
        EnemyData eData = new EnemyData();
        TurretState currentTurretState = 0;
        DriveState currentDriveState = DriveState.AVOID;
        SteeringBehavior sB;
        Vector2D driveVector;
		Vector2D shootVector;
		double bulletStrength = 0;
		public int moveDir = 1;

		bool isOnTarget = false;

        public override void Run()
        {
            IsAdjustGunForRobotTurn = true;
            IsAdjustRadarForRobotTurn = true;
            IsAdjustRadarForGunTurn = true;
            SetColors(Color.Black, Color.Gray, Color.Black);

            sB = new SteeringBehavior(ref eData, this);
			shootVector = new Vector2D();
			driveVector = new Vector2D();
            SetTurnRadarLeft(360);
			
			// used first in paint, so fast init it
			sB.WallAvoidance();
            while (true)
            {
                getDriveState();

				if (currentDriveState == DriveState.RAM)
                {
					Console.WriteLine("DriveState.RAM");
					driveVector = sB.Pursuit();
                }
                else if (currentDriveState == DriveState.ESCAPE)
                {
                    //FLEE
					Console.WriteLine("DriveState.ESCAPE");
					sB.OffsetPursuit();   
				}
                else if (currentDriveState == DriveState.AVOID)
                {
					Console.WriteLine("DriveState.AVOID");
					sB.Evade();
                }

				sB.WallAvoidance();
				SetAhead(100 * moveDir);
				Execute();

				getTurretState();

				if (currentTurretState == TurretState.ATTACK)
				{
					bulletStrength = Math.Min(400 / eData.Distance, 3);
					Console.WriteLine("TurretState.ATTACK");
					shootVector = sB.AimInFront(bulletStrength);
				}
				else if(currentTurretState == TurretState.SAVEENERGY)
				{

					Console.WriteLine("TurretState.SAVEENERGY");
					bulletStrength = Math.Min( 200 / eData.Distance, 3);
					shootVector = sB.AimInFront(bulletStrength);
				}
				else if(currentTurretState == TurretState.SCAN)
				{
					Console.WriteLine("TurretState.SCAN");
					SetTurnRadarRight(180);
				}
				Console.WriteLine("bulletStrength: " + bulletStrength);

				if (Math.Abs(GunTurnRemaining) < 1)
					SetFire(bulletStrength);

                Execute();
				
				isOnTarget = false;
				Scan();
				
            }
        }

        public override void OnScannedRobot(ScannedRobotEvent e)
        {
			isOnTarget = true;
            double offSetX = e.Distance * (Math.Sin(RadarHeadingRadians));
            double offSetY = e.Distance * (Math.Cos(RadarHeadingRadians));

            Vector2D enPos = Position + new Vector2D(offSetX, offSetY);
            eData.SetEnemyData(Time, e, new Vector2D(offSetX, offSetY), new Point2D(enPos.X, enPos.Y));

            double radarTurn = Heading + e.Bearing - RadarHeading;
            double radar = 1.9 * Utils.NormalRelativeAngleDegrees(radarTurn);
            SetTurnRadarRight(radar);
        }


		
        public override void OnPaint(IGraphics graphics)
        {
            RoboHelpers.DrawBulletTarget(graphics, Color.Orange, new Point2D(Position.X, Position.Y), new Point2D(sB.leftHorn.X, sB.leftHorn.Y));
            RoboHelpers.DrawBulletTarget(graphics, Color.Purple, new Point2D(Position.X, Position.Y), new Point2D(sB.midHorn.X, sB.midHorn.Y));
            RoboHelpers.DrawBulletTarget(graphics, Color.Yellow, new Point2D(Position.X, Position.Y), new Point2D(sB.rightHorn.X, sB.rightHorn.Y));
            RoboHelpers.DrawBulletTarget(graphics, Color.Yellow, new Point2D(Position.X, Position.Y), new Point2D(driveVector.X, driveVector.Y));
			RoboHelpers.DrawBulletTarget(graphics, Color.Green, new Point2D(Position.X, Position.Y), new Point2D(shootVector.X, shootVector.Y));
        }

        public void getTurretState()
        {
            if (currentTurretState == TurretState.ATTACK)
            {
                //10% under
                if (this.Energy + (this.Energy / 20) < eData.Energy)
                    currentTurretState= TurretState.SAVEENERGY;
                else if (!isOnTarget)
                    currentTurretState= TurretState.SCAN;
     
            }
            else if (currentTurretState == TurretState.SAVEENERGY)
            {
                //if enemy has a set portion less health then you: attack
                if (this.Energy + (this.Energy / 20) > eData.Energy)
                    currentTurretState= TurretState.ATTACK;
                else if (!isOnTarget)
                    currentTurretState= TurretState.SCAN;
            }
            else if (currentTurretState == TurretState.SCAN)
            {
                if (isOnTarget)
                {
                    if (this.Energy + (this.Energy / 20) < eData.Energy)
                        currentTurretState= TurretState.SAVEENERGY;
                    if (this.Energy + (this.Energy / 20) >= eData.Energy)
                        currentTurretState= TurretState.ATTACK;
                }
            }
        }

        public void getDriveState()
        {
            if (Energy + ((Energy / 100) * 35) < eData.Energy) 
            {
                currentDriveState = DriveState.ESCAPE;
            }
			else if (Energy > eData.Energy)
            {
                currentDriveState = DriveState.RAM;
            }
            else
            {
                currentDriveState = DriveState.AVOID;
            }
        }
	}
}

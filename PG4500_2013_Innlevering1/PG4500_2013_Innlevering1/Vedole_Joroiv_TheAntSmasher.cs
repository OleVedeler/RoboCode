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

		bool isOnTarget = false;

        public override void Run()
        {
            IsAdjustGunForRobotTurn = true;
            IsAdjustRadarForRobotTurn = true;
            IsAdjustRadarForGunTurn = true;
            SetColors(Color.Black, Color.Gray, Color.Black);

            sB = new SteeringBehavior(ref eData, this);

            SetTurnRadarLeft(360);

            while (true)
            {
                currentDriveState = DriveState.AVOID;
                    //getDriveState();

                if (currentDriveState == DriveState.RAM)
                {
					driveVector = sB.Pursuit();
                    sB.WallAvoidance();
                    SetAhead(100);
                }
                else if (currentDriveState == DriveState.ESCAPE)
                {
                    //FLEE
                    double angle = Utils.NormalRelativeAngleDegrees(eData.Bearing + 180);
                    SetTurnRight(angle);
                    SetAhead(Rules.MAX_VELOCITY);
                    
                    //ADD WALLAVOIDANCE
                }
                else if (currentDriveState == DriveState.AVOID)
                {
                    //Random shit
                    sB.WallAvoidance();

                    SetAhead(Rules.MAX_VELOCITY);

                }
                
				getTurretState();

				if (currentTurretState == TurretState.ATTACK)
				{
					SetTurnRadarRight(RoboHelpers.RadarToTargetAngleDegrees(Heading, RadarHeading, eData.Bearing));
					SetTurnGunRight(RoboHelpers.GunToTargetAngleDegrees(Heading,GunHeading, eData.Bearing));
					SetFire(1);
				}
				else if(currentTurretState == TurretState.SAVEENERGY)
				{
					SetTurnRadarRight(RoboHelpers.RadarToTargetAngleDegrees(Heading, RadarHeading, eData.Bearing));
					SetTurnGunRight(RoboHelpers.GunToTargetAngleDegrees(Heading, GunHeading, eData.Bearing));
					// Do nothing?
				}
				else if(currentTurretState == TurretState.SCAN)
				{
					SetTurnRadarRight(180);
				}

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
            //RoboHelpers.DrawBulletTarget(graphics, Color.Yellow, new Point2D(Position.X, Position.Y), new Point2D(driveVector.X, driveVector.Y));
            RoboHelpers.DrawBulletTarget(graphics, Color.Orange, new Point2D(Position.X, Position.Y), new Point2D(sB.leftHorn.X, sB.leftHorn.Y));
            RoboHelpers.DrawBulletTarget(graphics, Color.Purple, new Point2D(Position.X, Position.Y), new Point2D(sB.midHorn.X, sB.midHorn.Y));
            RoboHelpers.DrawBulletTarget(graphics, Color.Yellow, new Point2D(Position.X, Position.Y), new Point2D(sB.rightHorn.X, sB.rightHorn.Y));
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

        public DriveState getDriveState()
        {
            DriveState ret;


            if (Energy + ((Energy / 100) * 35) < eData.Energy) 
            {
                ret = DriveState.ESCAPE;
            }
            else if (Energy + ((Energy / 100) * 50) > eData.Energy)
            {
                ret = DriveState.RAM;
            }
            else
            {
                ret = DriveState.AVOID;
            }
            /*
            if (currentDriveState == DriveState.ESCAPE)
            {
                //If more energy; change to RAM
                if (this.Energy > eData.Energy) 
                    ret = DriveState.RAM;

                //If the same energy; change to AVOID
                else if (this.Energy == eData.Energy) 
                    ret = DriveState.AVOID;
            }
            else if (currentDriveState == DriveState.AVOID)
            {
                //If lower life change to ESCAPE
                if (this.Energy > eData.Energy)
                    ret = DriveState.RAM;

                //If the same energy; change to AVOID
                else if (this.Energy < eData.Energy)
                    ret = DriveState.ESCAPE;
            }
            else if (currentDriveState == DriveState.RAM)
            {
                //If lower life change to ESCAPE
                if (this.Energy < eData.Energy)
                    ret = DriveState.ESCAPE;

                //If the same energy; change to AVOID
                else if (this.Energy == eData.Energy)
                    ret = DriveState.AVOID;
            }
            */
            return ret;
        }
	}
}

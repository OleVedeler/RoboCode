using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robocode;
using Robocode.Util;
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
        DriveState currentDriveState = 0;
        SteeringBehavior sB;

		bool isOnTarget = false;

        public override void Run()
        {
            IsAdjustGunForRobotTurn = true;
            IsAdjustRadarForRobotTurn = true;
            IsAdjustRadarForGunTurn = true;

            sB = new SteeringBehavior(ref eData);

            SetTurnRadarLeft(360);

            while (true)
            {
                currentDriveState = DriveState.ESCAPE;

                if (currentDriveState == DriveState.ESCAPE)
                {
                    //Vector2D way = RoboHelpers.CalculateTargetVector(HeadingRadians, eData.Bearing, eData.Distance);
                    //way.Normalize();
                    //way *= Rules.MAX_VELOCITY;
                    
                    //double heading = Math.Atan2(way.Y, way.X);
                    //if (heading < 0)
                    //    heading += 180;

                    SetTurnRight(eData.Bearing);


                    SetAhead(100);
                }
                else if (currentDriveState == DriveState.RAM)
                {
                    
                }
                else if (currentDriveState == DriveState.AVOID)
                {
                    
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
					// Do nothing?
				}
				else if(currentTurretState == TurretState.SCAN)
				{
					SetTurnRadarRight(10);
				}

                Execute();
            }
        }

        public override void OnScannedRobot(ScannedRobotEvent e)
        {
            double radarTurn = Heading + e.Bearing - RadarHeading;
            double radar = 1.9 * Utils.NormalRelativeAngleDegrees(radarTurn);
            SetTurnRadarRight(radar);

			isOnTarget = true;
            double offSetX = e.Distance * (Math.Cos(RadarHeading));
            double offSetY = e.Distance * (Math.Sin(RadarHeading));

            Vector2D enPos = this.Position + new Vector2D(offSetX, offSetY);
            eData.SetEnemyData(Time, e, RoboHelpers.CalculateTargetVector(HeadingRadians,e.BearingRadians,e.Distance), new Point2D(enPos.X, enPos.Y));
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
            DriveState ret = currentDriveState;

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

            return ret;
        }
	}
}

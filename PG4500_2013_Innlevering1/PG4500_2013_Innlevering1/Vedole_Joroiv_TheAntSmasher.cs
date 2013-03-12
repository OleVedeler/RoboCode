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

        public override void Run()
        {
            SetTurnRadarRight(360);

            DriveStateEscape esc = new DriveStateEscape(ref rData);

            while (true)
            {
                
                //esc.Update();

                //Ahead(rData.ahead);

                Ahead(100);

                Execute();
            }
        }

        public override void OnScannedRobot(ScannedRobotEvent e)
        {
            double offSetX = e.Distance * (Math.Cos(RadarHeading));
            double offSetY = e.Distance * (Math.Sin(RadarHeading));

            Vector2D enPos = this.Position + new Vector2D(offSetX, offSetY);

            eData.SetEnemyData(Time, e, new Vector2D(offSetX, offSetY), new Point2D(enPos.X, enPos.Y));


        }

        public TurretState getTurretState()
        {
            TurretState ret = currentTurretState;
            
            if (currentTurretState == TurretState.ATTACK)
            {
                //10% under 
                if (this.Energy + (this.Energy / 10) < eData.Energy)
                    ret = TurretState.SAVEENERGY;
                else if (!rData.isOnTarget)
                    ret = TurretState.SCAN;
     
            }
            else if (currentTurretState == TurretState.SAVEENERGY)
            {
                //if enemy has a set portion less health then you: attack
                if (this.Energy + (this.Energy / 10) > eData.Energy)
                    ret = TurretState.ATTACK;
                else if (!rData.isOnTarget)
                    ret = TurretState.SCAN;
            }
            else if (currentTurretState == TurretState.SCAN)
            {
                if (rData.isOnTarget)
                {
                    if (this.Energy + (this.Energy / 10) < eData.Energy)
                        ret = TurretState.SAVEENERGY;
                    if (this.Energy + (this.Energy / 10) >= eData.Energy)
                        ret = TurretState.ATTACK;
                }
            }

            return ret;
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

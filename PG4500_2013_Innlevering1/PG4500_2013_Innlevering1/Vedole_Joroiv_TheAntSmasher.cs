﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robocode;
using Robocode.Util;
using Santom;


namespace PG4500_2013_Innlevering1 
{
	class Vedole_Joroiv_TheAntSmasher : AdvancedRobot
	{
        RoboData rData = new RoboData();
        EnemyData eData = new EnemyData();

        public override void Run()
        {
            SetTurnRadarRight(360);
            SetAdjustRadarForGunTurn(true);

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
            double radarTurn = Heading + e.Bearing - RadarHeading;

            double radar = 1.9 * Utils.NormalRelativeAngleDegrees(radarTurn);

            SetTurnRadarRight(radar);

            SetTurnGunRight(radar);
        }
	}
}

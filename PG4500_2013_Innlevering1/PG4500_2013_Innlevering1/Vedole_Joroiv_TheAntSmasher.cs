using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robocode;


namespace PG4500_2013_Innlevering1 
{
	class Vedole_Joroiv_TheAntSmasher : AdvancedRobot
	{
		public override void Run()
		{
 			base.Run();

			int ActiveSate = 0;
			List<State> TurretFSM = new List<State>();
			//TurretFSM.Add(new TurretStateWin());


			//activestate = states[activState].GetState();
			//states[ActiveState].Update();

		}
	}
}

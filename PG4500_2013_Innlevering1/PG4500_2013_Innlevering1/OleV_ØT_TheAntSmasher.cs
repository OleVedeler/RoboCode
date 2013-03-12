using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robocode;


namespace PG4500_2013_Innlevering1 
{
	class Vedole_Jorøiv_TheAntSmasher : AdvancedRobot
	{

		public override void Run()
		{
 			base.Run();

			List<State> states;



			activestate = states[activState].GetState();
			states[ActiveState].Update();

		}
	}
}

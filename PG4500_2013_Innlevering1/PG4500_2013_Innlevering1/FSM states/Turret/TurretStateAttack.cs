using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Santom;

namespace PG4500_2013_Innlevering1.FSM_states.Turret
{
	class TurretStateAttack : State
	{
		public TurretStateAttack()
		{

		}

		public List<string> GetAction()
		{

			/*List<string> stringlist = new List<string>();
			//10% under
			if (this.Energy + (this.Energy / 20) < eData.Energy)
				stringlist.Add("SAVEENERGY");
			else if (!isOnTarget)
				currentTurretState = TurretState.SCAN;
			*/

			return null;
		}

		public string GetEntryAction()
		{
			throw new NotImplementedException();
		}

		public string GetExitAction()
		{
			throw new NotImplementedException();
		}

		public Transition[] GetTransitions()
		{
			throw new NotImplementedException();
		}
	}
}

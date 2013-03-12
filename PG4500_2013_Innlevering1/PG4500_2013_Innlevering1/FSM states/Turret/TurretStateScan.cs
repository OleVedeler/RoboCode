using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG4500_2013_Innlevering1.FSM_states.Turret
{
	class TurretStateScan : State
	{
		public override void Update()
		{

			//search for the enemy until it is picked up on the radar
			base.Update();
		}

		public override int GetNewState()
		{

			if(rData.isOnTarget){
				if (rData.energy + (rData.energy / 10) < eData.Energy)
					return (int) TurretState.SAVEENERGY;
				if (rData.energy + (rData.energy / 10) >= eData.Energy)
					return (int)TurretState.ATTACK;
			}
			return (int) TurretState.SCAN;
		}
	}
}

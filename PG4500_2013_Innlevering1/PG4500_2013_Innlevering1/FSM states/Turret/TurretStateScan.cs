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
			//if enemy found && enemy has a set portion less health then you: attack
			//if enemy found && enemy has a set portion more health then you : save energy
			//else return this.state
			return base.GetNewState();
		}
	}
}

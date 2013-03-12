using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG4500_2013_Innlevering1.FSM_states.Turret
{
	class TurretStateAttack : State
	{
		public override void Update()
		{

			//scan og sikt forran fienden og skyt der du tror den skal være
			base.Update();
		}

		public override int GetNewState()
		{
			//if enemy lost : scan
			//if enemy found && enemy has a set portion more health then you : save energy
			//else return this.state
			return base.GetNewState();
		}
	}
}

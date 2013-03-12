using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Santom;
namespace PG4500_2013_Innlevering1.FSM_states.Turret
{
	class TurretSaveEnergy : State
	{
		public TurretSaveEnergy(ref EnemyData data)
		{
			this.Data = data;
		}

		public override void Update()
		{
			base.Update();
		}

		public override int GetNewState()
		{
			//if enemy has a set portion less health then you: attack
			//if enemy lost : Scan
			//else return this.state
			return base.GetNewState();
		}
	}
}

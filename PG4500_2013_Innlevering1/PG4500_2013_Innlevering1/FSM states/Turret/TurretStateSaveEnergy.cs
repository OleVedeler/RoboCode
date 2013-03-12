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
		public TurretSaveEnergy(ref RoboData rData, ref EnemyData eData)
		{
			this.eData = eData;
			this.rData = rData;
		}

		public override void Update()
		{
			base.Update();
		}

		public override int GetNewState()
		{
			//if enemy has a set portion less health then you: attack
			if (rData.energy + rData.energy / 10 > eData.Energy)
				return (int)TurretState.ATTACK;
			else if (!rData.isOnTarget)
				return (int)TurretState.SCAN;
			else
				return (int)TurretState.SAVEENERGY;
		}
	}
}

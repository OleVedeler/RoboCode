using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Santom;

namespace PG4500_2013_Innlevering1.FSM_states.Turret
{
	class TurretStateScan : State
	{
		public TurretStateScan(ref RoboData rData, ref EnemyData eData)
		{
			this.rData = rData;
			this.eData = eData;
		}

		public override void Update()
		{
			rData.rotationRadarLeft = 45;
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

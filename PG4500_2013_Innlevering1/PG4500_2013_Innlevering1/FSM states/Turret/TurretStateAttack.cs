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
		public TurretStateAttack(ref RoboData rData, ref EnemyData eData)
		{
			this.rData = rData;
			this.eData = eData;
		}

		public override void Update()
		{

			//scan og sikt forran fienden og skyt der du tror den skal være
			rData.rotationRadarLeft = RoboHelpers.RadarToTargetAngleDegrees(rData.Robotheading, rData.RadarHeading, eData.Bearing);

			/*eData.Velocity;
			eData.Heading;
			eData.Time;
			eData.Distance;
		*/
			base.Update();
		}

		public override int GetNewState()
		{
			if (!rData.isOnTarget)
				return (int)TurretState.SCAN;
			// 10% under
			else if (rData.energy + (rData.energy / 10) < eData.Energy)
				return (int)TurretState.SAVEENERGY;
			else
				return (int)TurretState.ATTACK;
		}
	}
}

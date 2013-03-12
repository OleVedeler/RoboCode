using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robocode;
using Santom;


namespace PG4500_2013_Innlevering1
{

	//Enum for all the drivestates
	public enum DriveState
	{
		RAM,
		ESCAPE,
		AVOID
	};
	
	//Forskjellige Statene for enum maskin
	public enum TurretState
	{
		SCAN,
		ATTACK,
		SAVEENERGY
	}

	abstract class State : AdvancedRobot
	{
		protected EnemyData eData;
        protected RoboData rData;
        protected SteeringBehavior steeringBehavior;

		public virtual void Update()
		{
			
		}

		public virtual int GetNewState()
		{
            return 0;
		}
	}
}

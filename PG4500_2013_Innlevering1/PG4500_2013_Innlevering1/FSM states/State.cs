using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robocode;
using Santom;


namespace PG4500_2013_Innlevering1
{

	

	abstract class State
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

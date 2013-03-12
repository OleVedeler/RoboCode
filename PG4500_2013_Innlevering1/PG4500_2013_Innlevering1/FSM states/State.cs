using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robocode;

namespace PG4500_2013_Innlevering1
{
	abstract class State
	{
		public virtual void Update()
		{
			
		}

		public virtual int GetNewState()
		{
			return 0;
		}
	}
}

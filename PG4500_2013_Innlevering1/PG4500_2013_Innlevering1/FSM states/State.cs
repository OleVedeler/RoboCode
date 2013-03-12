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

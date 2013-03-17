using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG4500_2013_Innlevering1
{
	interface Transition
	{
		bool isTriggered();
		State getTargetState();
		string getAction();
	}
}

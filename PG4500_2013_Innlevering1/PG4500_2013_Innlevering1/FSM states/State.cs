﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robocode;
using Santom;


namespace PG4500_2013_Innlevering1
{
	interface State
	{
		List<String> GetAction();
		string GetEntryAction();
		string GetExitAction();
		Transition[] GetTransitions();
	}
}

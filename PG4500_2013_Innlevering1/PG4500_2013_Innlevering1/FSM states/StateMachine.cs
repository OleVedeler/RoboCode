using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG4500_2013_Innlevering1.FSM_states
{
	class StateMachine
	{

		List<State> States; 
		State initialState;
		State currentState;

		public StateMachine(){
			States = new List<State>();
			
			
			
			initialState = null;
			currentState = initialState;
		}

		public List<String> Update()
		{
			Transition triggerdTransition = null;


			foreach(Transition transition in currentState.GetTransitions())
			{
				if (transition.isTriggered())
				{
					triggerdTransition = transition;
					break;
				}
			}

			if (null != triggerdTransition)
			{
				State targetState = triggerdTransition.getTargetState();

				List<String> Actions = new List<string>();

				Actions.Add(currentState.GetExitAction());
				Actions.Add(triggerdTransition.getAction());
				Actions.Add(targetState.GetEntryAction());

				currentState = targetState;
				return Actions;
			}
			else return currentState.GetAction();
		}

	}
}

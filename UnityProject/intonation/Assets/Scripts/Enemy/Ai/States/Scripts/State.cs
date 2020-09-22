using EvilOwl.Enemy.Ai.Transitions;
using MyBox;
using UnityEngine;
using Action = EvilOwl.Enemy.Ai.Actions.Action;

namespace EvilOwl.Enemy.Ai.States
{
	[CreateAssetMenu(fileName = "_State", menuName = "Ai/State")] 
	public class State : ScriptableObject
	{
#pragma warning disable CS0649
		/*****************************
		 *         Variables         *
		 *****************************/
		[SerializeField] public string stateName;
		
		[Separator] [Space(-15)] [Header("Actions")]
		
		[SerializeField] private bool hasEnterStateAction;
		[ConditionalField(nameof(hasEnterStateAction))]
		[SerializeField] private Action enterStateAction;
		
		[SerializeField] private Action[] actions;
		
		[SerializeField] private bool hasExitStateAction;
		[ConditionalField(nameof(hasExitStateAction))] 
		[SerializeField] private Action exitStateAction;

		[Separator] [Space(-15)] [Header("Transitions")]
		[SerializeField] private Transition[] transitions;
		
#pragma warning restore CS0649
		/*****************************
		 *          Methods          *
		 *****************************/
		public void UpdateState(AiStateController controller)
		{
			if(CheckForTransitions(controller)) ExecuteActions(controller);
		}

		private void EnterState(AiStateController controller)
		{
			if(hasEnterStateAction) enterStateAction.Act(controller);
		}

		private void ExitState(AiStateController controller)
		{
			if(hasExitStateAction) exitStateAction.Act(controller);
		}

		private void ExecuteActions(AiStateController controller)
		{
			foreach (var action in actions)
			{
				action.Act(controller);
			}
		}

		private bool CheckForTransitions(AiStateController controller)
		{
			foreach (var transition in transitions)
			{
				if (!transition.decision.Evaluate(controller)) continue;

				controller.currentState.ExitState(controller);
				transition.nextState.EnterState(controller);
				
				controller.currentState = transition.nextState;
				controller.OnStateChanged();
				return false;
			}

			return true;
		}
	}
}

using UnityEngine;

namespace EvilOwl.Enemy.Ai.Decisions
{
	[CreateAssetMenu(fileName = "TargetIsGoneDecision", menuName = "Ai/Decisions/TargetIsGoneDecision")]
	public class TargetIsGoneDecision : Decision
	{
#pragma warning disable CS0649
		/*****************************
		 *         Variables         *
		 *****************************/
		
#pragma warning restore CS0649
		/*****************************
		 *           Init            *
		 *****************************/
		
		/*****************************
		 *          Update           *
		 *****************************/
		
		/*****************************
		 *          Methods          *
		 *****************************/

		public override bool Evaluate(AiStateController controller)
		{
			return controller.Target == null;
		}
	}
}

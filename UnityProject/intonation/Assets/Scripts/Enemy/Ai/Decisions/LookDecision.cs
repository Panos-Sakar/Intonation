using UnityEngine;

namespace EvilOwl.Enemy.Ai.Decisions
{
	[CreateAssetMenu(fileName = "LookDecision", menuName = "Ai/Decisions/LookDecision")]
	public class LookDecision : Decision
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
			return controller.SpellChainMaxed;
		}
	}
}

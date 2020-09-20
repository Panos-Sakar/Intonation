using UnityEngine;

namespace EvilOwl.Enemy.Ai.Actions
{
	[CreateAssetMenu(fileName = "FireSpellAction", menuName = "Ai/Actions/FireSpellAction")]
	public class FireSpellAction : Action
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

		public override void Act(AiStateController controller)
		{
			controller.DestroySpell();
		}
	}
}

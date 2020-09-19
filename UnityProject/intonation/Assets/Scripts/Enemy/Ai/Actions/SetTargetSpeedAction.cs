using UnityEngine;

namespace EvilOwl.Enemy.Ai.Actions
{
	[CreateAssetMenu(fileName = "SetTargetSpeedAction", menuName = "Ai/Actions/SetTargetSpeedAction")]
	public class SetTargetSpeedAction : Action
	{
#pragma warning disable CS0649
		/*****************************
		 *         Variables         *
		 *****************************/
		public Vector2 targetSpeed;
		
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
			controller.TargetSpeed = targetSpeed;
		}
	}
}

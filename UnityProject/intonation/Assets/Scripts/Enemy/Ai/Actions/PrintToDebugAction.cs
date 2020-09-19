using UnityEngine;

namespace EvilOwl.Enemy.Ai.Actions
{
	[CreateAssetMenu(fileName = "PrintToDebugAction", menuName = "Ai/Actions/PrintToDebugAction")]
	public class PrintToDebugAction : Action
	{
#pragma warning disable CS0649
		/*****************************
		 *         Variables         *
		 *****************************/
		public string message;
		
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
			Debug.Log(message,controller.gameObject);
		}
	}
}

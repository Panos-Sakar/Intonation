using UnityEngine;

namespace EvilOwl.Enemy.Ai.Actions
{
	public abstract class Action : ScriptableObject
	{
#pragma warning disable CS0649
		/*****************************
		 *         Variables         *
		 *****************************/
		public abstract void Act(AiStateController controller);

#pragma warning restore CS0649
		/*****************************
		 *          Methods          *
		 *****************************/

	}
}

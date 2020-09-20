using UnityEngine;

namespace EvilOwl.Enemy.Ai.Decisions
{
	public abstract class Decision : ScriptableObject
	{
#pragma warning disable CS0649
		/*****************************
		 *         Variables         *
		 *****************************/
		
#pragma warning restore CS0649
		/*****************************
		 *          Methods          *
		 *****************************/
		public abstract bool Evaluate(AiStateController controller);
	}
}

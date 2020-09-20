using UnityEngine;

namespace EvilOwl.Core.Stats
{
	[CreateAssetMenu(fileName = "BaseEnemySharedStats", menuName = "Stats/EnemySharedStats")]
	public class EnemySharedStats : ScriptableObject
	{
#pragma warning disable CS0649
		/*****************************
		 *         Variables         *
		 *****************************/
		public float speed;
		public float spellTimerCooldown;
		public float addSpellCooldown;
		public float fireSpellDelay;
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

	}
}

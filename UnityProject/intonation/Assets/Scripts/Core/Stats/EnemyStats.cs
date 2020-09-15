namespace EvilOwl.Core.Stats
{
	public class EnemyStats : EntityStats
	{
#pragma warning disable CS0649
		/*****************************
		 *         Variables         *
		 *****************************/
		public float spellTimerCooldown;
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

		public override void Kill()
		{
			Destroy(gameObject);
		}
	}
}

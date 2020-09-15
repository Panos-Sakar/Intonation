using UnityEngine;

namespace EvilOwl.Core.Stats
{
	public class PlayerStats : EntityStats
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

		public override void Kill()
		{
			Debug.Log("Player Death");
		}
	}
}

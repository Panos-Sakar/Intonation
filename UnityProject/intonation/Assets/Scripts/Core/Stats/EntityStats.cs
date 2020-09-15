using UnityEngine;

namespace EvilOwl.Core
{
	public abstract class EntityStats : MonoBehaviour
	{
#pragma warning disable CS0649
		/*****************************
		 *         Variables         *
		 *****************************/
		public float life;
		
#pragma warning restore CS0649

		/*****************************
		 *          Methods          *
		 *****************************/
		public abstract void Kill();

	}
}

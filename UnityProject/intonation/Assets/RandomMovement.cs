using UnityEngine;
using Random = UnityEngine.Random;

namespace EvilOwl
{
	public class RandomMovement : MonoBehaviour
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
		private void Update()
		{
			var rand = Random.Range(-3,9);
			transform.position += new Vector3(rand * Time.deltaTime,0,0); 
		}
		/*****************************
		 *          Methods          *
		 *****************************/
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (!other.gameObject.CompareTag("KillZone")) return;
			transform.position = new Vector3(0,0,0);
		}
	}
}

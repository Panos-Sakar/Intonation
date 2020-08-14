using UnityEngine;

namespace EvilOwl.Player
{
	public class AutoRotate : MonoBehaviour
	{
#pragma warning disable CS0649
		/*****************************
		 *         Variables         *
		 *****************************/
		
		[SerializeField] private float rotateSpeed;

#pragma warning restore CS0649
		/*****************************
		 *           Init            *
		 *****************************/

		/*****************************
		 *          Update           *
		 *****************************/
		private void Update()
		{
			transform.Rotate(0,0,rotateSpeed*Time.deltaTime);
		}
		/*****************************
		 *          Methods          *
		 *****************************/
	}
}

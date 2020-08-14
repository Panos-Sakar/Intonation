using UnityEngine;

namespace EvilOwl.Environment
{
	public class ParallaxObject : MonoBehaviour
	{
#pragma warning disable CS0649
		/*****************************
		 *         Variables         *
		 *****************************/

		[SerializeField] private GameObject followCamera;
		[SerializeField] private float effectStrength;

		private Vector3 _startPosition;

#pragma warning restore CS0649
		/*****************************
		 *           Init            *
		 *****************************/
		private void Start()
		{
			_startPosition = transform.position;
		}
		/*****************************
		 *          Update           *
		 *****************************/
		private void FixedUpdate()
		{
			var myTransform = transform;
			var myPosition = myTransform.position;
			var deltaDistance = followCamera.transform.position.x * effectStrength;

			myTransform.position = new Vector3(_startPosition.x + deltaDistance, myPosition.y, myPosition.z);
		}
		/*****************************
		 *          Methods          *
		 *****************************/

	}
}

using UnityEngine;

namespace EvilOwl.Enemy.Ai.Actions
{
	[CreateAssetMenu(fileName = "KeepDistanceAction", menuName = "Ai/Actions/KeepDistanceAction")]
	public class KeepDistanceAction : Action
	{
#pragma warning disable CS0649
		/*****************************
		 *         Variables         *
		 *****************************/
		public float minDistance;
		public float maxDistance;
		
		private float _distance;
		private Vector2 _direction;

		private Vector3 _targetPosition;
		private Vector3 _objectPosition;
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
			_objectPosition = controller.transform.position;
			_targetPosition = controller.Target.transform.position;
			_distance = Vector3.Distance(_objectPosition,_targetPosition);
			
			if (_distance < minDistance)
			{
				_direction = ((_objectPosition.x - _targetPosition.x) < 0) ? Vector3.left : Vector3.right;
				controller.TargetSpeed = _direction * controller.Speed;
				controller.SetGfxDirectionX = _direction.x;
			}
			else if (_distance > maxDistance)
			{
				_direction = ((_objectPosition.x - _targetPosition.x) < 0) ? Vector3.right : Vector3.left;
				controller.TargetSpeed = _direction * controller.Speed;
				controller.SetGfxDirectionX = _direction.x;
			}
			else
			{
				_direction = ((_objectPosition.x - _targetPosition.x) < 0) ? Vector3.right : Vector3.left;
				controller.TargetSpeed = Vector2.zero;
				controller.SetGfxDirectionX = _direction.x;
			}
			
		}
	}
}

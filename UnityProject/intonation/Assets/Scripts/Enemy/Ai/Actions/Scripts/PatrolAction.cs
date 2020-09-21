using UnityEngine;

namespace EvilOwl.Enemy.Ai.Actions
{
	[CreateAssetMenu(fileName = "PatrolAction", menuName = "Ai/Actions/PatrolAction")]
	public class PatrolAction : Action
	{
#pragma warning disable CS0649
		/*****************************
		 *         Variables         *
		 *****************************/
		
		private float _distance;
		private Vector3 _direction;
		private Vector3 _objectPosition;
		private Vector3 _targetPosition;
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
			_objectPosition = controller.gameObject.transform.position;
			_targetPosition = controller.TargetPosition;
			_distance = Vector3.Distance(_objectPosition, _targetPosition);
			
			_direction = ((_objectPosition.x - _targetPosition.x) < 0) ? Vector3.right : Vector3.left;

			if (controller.HasCollidedWithOtherEnemy || _distance<1) 
				controller.TargetPosition = controller.GetNextPatrolPoint;

			controller.TargetSpeed = _direction * controller.Speed;
			controller.SetGfxDirectionX = _direction.x;
		}
	}
}

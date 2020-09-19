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
			_distance = Vector3.Distance(_objectPosition, controller.targetPosition);
			
			_direction = ((_objectPosition.x - controller.targetPosition.x) < 0) ? Vector3.right : Vector3.left;

			if (_distance<1)
			{
				controller.targetPosition = controller.GetNextPatrolPoint;
			}
			controller.TargetSpeed = _direction * controller.Speed;
		}
	}
}

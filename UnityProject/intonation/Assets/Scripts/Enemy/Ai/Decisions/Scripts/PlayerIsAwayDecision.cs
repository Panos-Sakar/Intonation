using UnityEngine;

namespace EvilOwl.Enemy.Ai.Decisions
{
	[CreateAssetMenu(fileName = "PlayerIsAwayDecision", menuName = "Ai/Decisions/PlayerIsAwayDecision")]
	public class PlayerIsAwayDecision : Decision
	{
#pragma warning disable CS0649
		/*****************************
		 *         Variables         *
		 *****************************/
		[SerializeField] private float looseAggroDistance;
		
		private float _distance;
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

		public override bool Evaluate(AiStateController controller)
		{
			_objectPosition = controller.transform.position;
			_targetPosition = controller.Target.transform.position;
			_distance = Vector3.Distance(_objectPosition,_targetPosition);

			if (!(_distance > looseAggroDistance)) return false;
			
			controller.Target = null;
			return true;
		}
	}
}

using UnityEngine;

namespace EvilOwl.Enemy.Ai.Decisions
{
	[CreateAssetMenu(fileName = "LookDecision", menuName = "Ai/Decisions/LookDecision")]
	public class LookDecision : Decision
	{
#pragma warning disable CS0649
		/*****************************
		 *         Variables         *
		 *****************************/
		[SerializeField] private float searchRadius;
		[SerializeField] private LayerMask searchLayerMask;
		
		private readonly Collider2D[] _hitColliders = new Collider2D[10];
		
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
			var  numColliders = Physics2D.OverlapCircleNonAlloc(controller.Position, searchRadius,
				_hitColliders, searchLayerMask);
			var minDist = 100000f;

			if ( numColliders <= 0) return false;

			for (var index = 0; index < _hitColliders.Length; index++)
			{
				var hitCollider = _hitColliders[index];
				_hitColliders[index] = null;
				
				if (hitCollider == null ||
				    controller.gameObject.transform.GetInstanceID() == hitCollider.transform.GetInstanceID()) continue;

				var offset = controller.Position - hitCollider.transform.position;
				var dist = Vector3.SqrMagnitude(offset);

				if (!(dist < minDist)) continue;

				controller.Target = hitCollider.gameObject;
				minDist = dist;
			}
			
			return controller.Target != null;
		}
	}
}

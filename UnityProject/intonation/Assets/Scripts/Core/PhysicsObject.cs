using System.Collections.Generic;
using UnityEngine;

namespace EvilOwl.Core
{
	public class PhysicsObject : MonoBehaviour
	{
#pragma warning disable CS0649
		/*****************************
		 *         Variables         *
		 *****************************/
		[Header("References")] 
		[SerializeField] private Rigidbody2D rb2D;
		
		[Header("Properties")]
		[SerializeField] private float gravityModifier = 1f;
		[SerializeField] private float minGroundNormalY = 0.65f;

		private ContactFilter2D _contactFilter;
		private const float MinMoveDistance = 0.001f;
		private const float ShellRadius = 0.01f;
		private Vector2 _groundNormal;
		private RaycastHit2D[] _raycastHits;
		private List<RaycastHit2D> _raycastHitsList;

		public bool isGrounded;
		public bool isRunning;
		public Vector2 velocity;
		public Vector2 targetVelocity;
		
#pragma warning restore CS0649
		/*****************************
		 *           Init            *
		 *****************************/
		private void Start()
		{
			_raycastHits = new RaycastHit2D[32];
			_raycastHitsList = new List<RaycastHit2D>();
			_contactFilter.useTriggers = true;
			_contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer)); // or 
			_contactFilter.useLayerMask = true;
		}
		/*****************************
		 *          Update           *
		 *****************************/

		private void FixedUpdate()
		{
			velocity += Physics2D.gravity * (gravityModifier * Time.deltaTime);
			velocity.x = targetVelocity.x;
			isGrounded = false;
			
			var deltaPosition = velocity * Time.deltaTime;
			
			var moveAlongGround = new Vector2(_groundNormal.y, -_groundNormal.x);
			var move = moveAlongGround * deltaPosition.x;
			Movement(move, false);
			
			move = Vector2.up * deltaPosition.y;
			Movement(move, true);
		}
		/*****************************
		 *          Methods          *
		 *****************************/
		private void Movement(Vector2 move , bool yMovement)
		{
			var distance = move.magnitude;

			if (distance > MinMoveDistance)
			{
				var hitCount = rb2D.Cast(move, _contactFilter, _raycastHits, distance + ShellRadius);
				_raycastHitsList.Clear();
				for (var i = 0; i < hitCount; i++)
				{
					_raycastHitsList.Add(_raycastHits[i]);
				}

				foreach (var raycastHit in _raycastHitsList)
				{
					var hitNormal = raycastHit.normal;
					if (hitNormal.y > minGroundNormalY)
					{
						isGrounded = true;
						
						if (yMovement)
						{
							_groundNormal = hitNormal;
							hitNormal.x = 0;
						}
					}

					var projection = Vector2.Dot(velocity, hitNormal);
					if (projection < 0)
					{
						velocity -= projection * hitNormal;
					}

					var modifiedDistance = raycastHit.distance - ShellRadius;
					distance = modifiedDistance < distance ? modifiedDistance : distance;
				}
			}
			rb2D.position += move.normalized * distance;
		}	
	}
}

using System;
using MyBox;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EvilOwl.Player
{
	public class PlayerMovementRb2d : MonoBehaviour
	{
#pragma warning disable CS0649
		/*****************************
		 *         Variables         *
		 *****************************/
		[Header("Properties")]
		[SerializeField] private float speed;
		[SerializeField] private float runMultiplier;
		[SerializeField] private float jumpForce;
		
		[Header("References")]
		[SerializeField] private Rigidbody2D myRigidbody;
		[SerializeField] private GameObject characterContainer;
		
		[SerializeField] private bool updateAnimator;
		[ConditionalField(nameof(updateAnimator))] 
		[SerializeField] private AnimatorEvents animator;
		
		private int _moveDirection;
		private float _runMultiplier = 1;
		
		private bool _grounded;
		
		//Animation Ids


#pragma warning restore CS0649
		/*****************************
		 *           Init            *
		 *****************************/

		/*****************************
		 *          Update           *
		 *****************************/
		private void Update()
		{
			
			if (_moveDirection != 0)
			{
				transform.position += new Vector3(_moveDirection * speed * _runMultiplier * Time.deltaTime, 0,0);
			}
		}

		/*****************************
		 *          Methods          *
		 *****************************/
		public void MovePlayer(InputAction.CallbackContext context)
		{
			var value = context.ReadValue<float>();
			
			if ( value > 0)
			{
				_moveDirection = 1;
				
				var currentScale = characterContainer.transform.localScale;
				characterContainer.transform.localScale = new Vector3(Math.Abs(currentScale.x),currentScale.y,currentScale.z);
				
				if(updateAnimator) animator.MoveState(Math.Abs(transform.position.x));
			}
			else if(value < 0)
			{
				_moveDirection = -1;
				
				var currentScale = transform.localScale;
				characterContainer.transform.localScale = new Vector3(-1 * Math.Abs(currentScale.x),currentScale.y,currentScale.z);
				
				if(updateAnimator) animator.MoveState(Math.Abs(transform.position.x));
			}
		}

		public void StopPlayer()
		{
			_moveDirection = 0;
			if(updateAnimator) animator.MoveState(0);
			
		}

		public void Jump()
		{
			if (!_grounded) return;
			
			myRigidbody.AddForce(Vector2.up * (jumpForce * 10));
			if(updateAnimator) animator.JumpState(true);
			
			_grounded = false;

		}

		public void Run()
		{
			_runMultiplier = runMultiplier;
			if(updateAnimator) animator.SprintState(true);
		}

		public void StopRun()
		{
			_runMultiplier = 1;
			if(updateAnimator) animator.SprintState(false);
		}

		private void OnCollisionEnter2D()
		{
			_grounded = true;
			if(updateAnimator) animator.JumpState(false);
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (!other.gameObject.CompareTag("KillZone")) return;
			
			print("Killed");
			transform.position = new Vector3(0,0,0);
		}
	}
}

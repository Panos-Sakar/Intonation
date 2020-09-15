using System;
using EvilOwl.Core;
using EvilOwl.Player.Animation;
using MyBox;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EvilOwl.Player.Input_System
{
	public class PlayerMovementPhObj : MonoBehaviour
	{
#pragma warning disable CS0649
		/*****************************
		 *         Variables         *
		 *****************************/
		
		[Header("References")]
		[SerializeField] private PhysicsObject physicsObject;
		
		[SerializeField] private bool updateAnimator;
		[ConditionalField(nameof(updateAnimator))] 
		[SerializeField] private AnimatorEvents animator;

		[Header("Movement attributes")]
		[SerializeField] private float jumpForce;
		[SerializeField] private float normalSpeed;
		[SerializeField] private float runSpeed;
		
		private float _inputVelocity;
		
#pragma warning restore CS0649
		/*****************************
		 *           Init            *
		 *****************************/
		private void FixedUpdate()
		{
			if(updateAnimator && Math.Abs(physicsObject.velocity.y) < 0.01) animator.JumpState(false);
		}
		/*****************************
		 *          Update           *
		 *****************************/
		
		/*****************************
		 *          Methods          *
		 *****************************/
		public void Move(InputAction.CallbackContext context)
		{
			_inputVelocity = context.ReadValue<float>();
			
			var velocityX = physicsObject.isRunning ? _inputVelocity * runSpeed : _inputVelocity * normalSpeed;
			physicsObject.targetVelocity.x = velocityX;

			var transform1 = transform;
			var localScale = transform1.localScale;
			localScale *= new Vector2(((_inputVelocity * localScale.x) < 0) ? -1 : 1,1);
			transform1.localScale = localScale;
			if(updateAnimator) animator.MoveState(Math.Abs(velocityX));
		}
		public void StopMove()
		{
			physicsObject.targetVelocity.x = 0;
			_inputVelocity = 0;
			if(updateAnimator) animator.MoveState(0);
		}
		public void Run()
		{
			if (!physicsObject.isGrounded) return;
			
			// ReSharper disable once CompareOfFloatsByEqualityOperator
			if(_inputVelocity != 0) physicsObject.targetVelocity.x = _inputVelocity * runSpeed;
			physicsObject.isRunning = true;
			if(updateAnimator) animator.SprintState(true);
		}
		public void StopRun()
		{
			if (!physicsObject.isGrounded) return;
			
			physicsObject.isRunning = false;
			if(updateAnimator) animator.SprintState(false);
		}
		public void Jump()
		{
			if (!physicsObject.isGrounded) return;
			
			physicsObject.velocity.y = jumpForce;
			if(updateAnimator) animator.JumpState(true);
		}
		public void StopJump()
		{
			if (!(physicsObject.velocity.y > 0)) return;
			physicsObject.velocity.y *= 0.5f;
		}		
		
	}
}

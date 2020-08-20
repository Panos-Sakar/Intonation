using EvilOwl.Core;
using EvilOwl.Player.Input_System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EvilOwl.Player
{
	public class PlayerInputManager : MonoBehaviour
	{
#pragma warning disable CS0649
		/*****************************
		 *         Variables         *
		 *****************************/
		[Header("References")]
		[SerializeField] private PhysicsObject physicsObject;
		[SerializeField] private SpellManager spellManager;
		
		[Header("Movement attributes")]
		[SerializeField] private float jumpForce;
		[SerializeField] private float normalSpeed;
		[SerializeField] private float runSpeed;

		private MainControls _controls;
		private float _inputVelocity;
		
#pragma warning restore CS0649
		/*****************************
		 *           Init            *
		 *****************************/
		private void Awake()
		{
			InitializeInput();
		}

		private void OnEnable()
		{
			_controls.Enable();
		}

		private void OnDisable()
		{
			_controls.Disable();
		}
		/*****************************
		 *          Update           *
		 *****************************/
		
		/*****************************
		 *          Methods          *
		 *****************************/

		private void InitializeInput()
		{
			_controls = new MainControls();
			
			//Move
			_controls.Player.Move.performed += Move;
			_controls.Player.Move.performed += spellManager.Move;
			_controls.Player.Move.canceled += StopMove;
			_controls.Player.Move.performed += spellManager.StopMove;
			
			//Sprint
			_controls.Player.Sprint.performed += Run;
			_controls.Player.Sprint.canceled += StopRun;
			
			//Jump
			_controls.Player.Jump.performed += Jump;
			_controls.Player.Jump.canceled += StopJump;
			
			//Fire
			_controls.Player.Fire.performed += spellManager.Fire;

			//Spells
			_controls.Player.RedSpell.performed += spellManager.Red;
			_controls.Player.GreenSpell.performed += spellManager.Green;
			_controls.Player.BlueSpell.performed += spellManager.Blue;
			_controls.Player.YellowSpell.performed += spellManager.Yellow;
		}
		private void Move(InputAction.CallbackContext context)
		{
			_inputVelocity = context.ReadValue<float>();
			
			var velocityX = physicsObject.isRunning ? _inputVelocity * runSpeed : _inputVelocity * normalSpeed;
			physicsObject.targetVelocity.x = velocityX;

			var transform1 = transform;
			var localScale = transform1.localScale;
			localScale *= new Vector2(((_inputVelocity * localScale.x) < 0) ? -1 : 1,1);
			transform1.localScale = localScale;
		}
		private void StopMove(InputAction.CallbackContext context)
		{
			physicsObject.targetVelocity.x = 0;
			_inputVelocity = 0;
		}
		private void Run(InputAction.CallbackContext context)
		{

			if (!physicsObject.isGrounded) return;
			
			// ReSharper disable once CompareOfFloatsByEqualityOperator
			if(_inputVelocity != 0) physicsObject.targetVelocity.x = _inputVelocity * runSpeed;
			physicsObject.isRunning = true;
		}
		private void StopRun(InputAction.CallbackContext context)
		{
			if (!physicsObject.isGrounded) return;
			
			physicsObject.isRunning = false;
		}
		private void Jump(InputAction.CallbackContext context)
		{
			if (physicsObject.isGrounded) physicsObject.velocity.y = jumpForce;
		}
		private void StopJump(InputAction.CallbackContext context)
		{
			if (physicsObject.velocity.y > 0) physicsObject.velocity.y *= 0.5f;
		}		

	}
}

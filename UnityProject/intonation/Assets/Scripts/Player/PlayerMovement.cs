using UnityEngine;
using UnityEngine.InputSystem;
using EvilOwl.Player.Input_System;

namespace EvilOwl.Player
{
	public class PlayerMovement : MonoBehaviour
	{
#pragma warning disable CS0649
		/*****************************
		 *         Variables         *
		 *****************************/
		[SerializeField] private float speed;
		[SerializeField] private float runMultiplier;
		[SerializeField] private float jumpForce;
		[SerializeField] private Rigidbody2D myRigidbody;
		
		private MainControls _controls;
		private int _moveDirection;
		private float _runMultiplier = 1;
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
		private void InitializeInput()
		{
			_controls = new MainControls();
			
			//Move
			_controls.Player.Move.performed += MovePlayer;
			_controls.Player.Move.canceled += StopPlayer;
			
			//Jump
			_controls.Player.Jump.performed += Jump;
			
			//Fire
			_controls.Player.Fire.performed += Fire;
			
			//
			_controls.Player.Sprint.performed += Run;
			_controls.Player.Sprint.canceled += StopRun;
		}
		private void MovePlayer(InputAction.CallbackContext context)
		{
			var value = context.ReadValue<float>();
			
			if ( value > 0)
			{
				_moveDirection = 1;
			}
			else if(value < 0)
			{
				_moveDirection = -1;
			}
		}

		private void StopPlayer(InputAction.CallbackContext context)
		{
			_moveDirection = 0;
		}

		private void Jump(InputAction.CallbackContext context)
		{
			myRigidbody.AddForce(Vector2.up * (jumpForce * 10));
		}

		private void Fire(InputAction.CallbackContext context)
		{ 
			print("Fire!");
		}

		private void Run(InputAction.CallbackContext context)
		{
			_runMultiplier = runMultiplier;
		}

		private void StopRun(InputAction.CallbackContext context)
		{
			_runMultiplier = 1;
		}
	}
}

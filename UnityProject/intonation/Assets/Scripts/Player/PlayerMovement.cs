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
		[SerializeField] private Rigidbody2D myRigidbody;
		
		private MainControls _controls;
		private int _moveDirection;
		private Transform _myTransform;
		private Vector3 _myTransformPosition;
		
#pragma warning restore CS0649
		/*****************************
		 *           Init            *
		 *****************************/
		private void Awake()
		{
			_myTransform = transform;
			_myTransformPosition = _myTransform.position;
			
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
			print(_moveDirection);
			if (_moveDirection != 0)
			{
				_myTransformPosition.x += _moveDirection * speed;
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
			else
			{
				print("Useless");
			}
		}

		private void StopPlayer(InputAction.CallbackContext context)
		{
			_moveDirection = 0;
		}

		private void Jump(InputAction.CallbackContext context)
		{
			print("Jump");
			myRigidbody.AddForce(Vector2.up);
		}

		private void Fire(InputAction.CallbackContext context)
		{ 
			print("Fire!");
		}
	}
}

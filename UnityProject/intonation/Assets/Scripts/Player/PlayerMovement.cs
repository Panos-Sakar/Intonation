using System;
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
		[Header("Properties")]
		[SerializeField] private float speed;
		[SerializeField] private float runMultiplier;
		[SerializeField] private float jumpForce;
		
		[Header("References")]
		[SerializeField] private Rigidbody2D myRigidbody;
		
		public Animator MyAnimator;
		private MainControls _controls;
		private int _moveDirection;
		private float _runMultiplier = 1;
		
		private bool _grounded;
		private static readonly int Animspeed = Animator.StringToHash("animspeed");
		private static readonly int Isjumping = Animator.StringToHash("isjumping");
		private static readonly int Isruning = Animator.StringToHash("isruning");


		//Animation Ids
		
	

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

			//Sprint
			_controls.Player.Sprint.performed += Run;
			_controls.Player.Sprint.canceled += StopRun;
		}
		private void MovePlayer(InputAction.CallbackContext context)
		{
			var value = context.ReadValue<float>();
			
			if ( value > 0)
			{
				_moveDirection = 1;
				var currentScale = transform.localScale;
				transform.localScale = new Vector3(Math.Abs(currentScale.x),currentScale.y,currentScale.z);
				MyAnimator.SetFloat (Animspeed , Math.Abs(currentScale.x));
			}
			else if(value < 0)
			{
				_moveDirection = -1;
				
				var currentScale = transform.localScale;
				transform.localScale = new Vector3(-1 * Math.Abs(currentScale.x),currentScale.y,currentScale.z);
				MyAnimator.SetFloat (Animspeed , Math.Abs(currentScale.x));
				
			}
		}

		private void StopPlayer(InputAction.CallbackContext context)
		{
			_moveDirection = 0;
			MyAnimator.SetFloat (Animspeed , 0);
			
		}

		private void Jump(InputAction.CallbackContext context)
		{
			if (!_grounded) return;
			
			myRigidbody.AddForce(Vector2.up * (jumpForce * 10));
			MyAnimator.SetBool (Isjumping , true);
			
			_grounded = false;

		}

		private void Run(InputAction.CallbackContext context)
		{
			_runMultiplier = runMultiplier;
			MyAnimator.SetBool (Isruning , true);
		}

		private void StopRun(InputAction.CallbackContext context)
		{
			_runMultiplier = 1;
			MyAnimator.SetBool (Isruning , false);
		}

		private void OnCollisionEnter2D()
		{
			_grounded = true;
			MyAnimator.SetBool (Isjumping , false);
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (!other.gameObject.CompareTag("KillZone")) return;
			
			print("Killed");
			transform.position = new Vector3(0,0,0);
		}
	}
}

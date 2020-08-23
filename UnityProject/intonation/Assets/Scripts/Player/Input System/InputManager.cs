using System;
using EvilOwl.Core;
using MyBox;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

namespace EvilOwl.Player.Input_System
{
	public class InputManager : MonoBehaviour
	{
#pragma warning disable CS0649
		/*****************************
		 *         Variables         *
		 *****************************/
#pragma warning disable CS0649
		
		[Separator()] [Space(-15)] [Header("User")] 
		[SerializeField] private int deviceId;
		//[SerializeField]  private InputControlScheme controlScheme;
		
		[Separator()] [Space(-15)] [Header("Animation")]
		
		[SerializeField] private bool triggerAnimator;
		[ConditionalField(nameof(triggerAnimator))] 
		[SerializeField] private AnimatorEvents animatorEvents;

		[Separator()] [Space(-15)] [Header("Soundwave Sprite")]
		
		[SerializeField] private bool triggerSpriteColor;
		[ConditionalField(nameof(triggerSpriteColor))] 
		[SerializeField] private SpriteRenderer soundwaveSprite;
		[ConditionalField(nameof(triggerSpriteColor))] 
		[SerializeField] private SpellColors spellColors;

		[Separator()] [Space(-15)] [Header("Spells")]
		
		[SerializeField] private bool triggerSpells;
		[ConditionalField(nameof(triggerSpells))] 
		[SerializeField] private SpellManagerV2 spellManager;
		[ConditionalField(nameof(triggerSpells))] 
		[SerializeField] private PlayerSpellChainCollider chainCollider;
		
		[Separator()] [Space(-15)] [Header("Movement")]
		
		[SerializeField] private MovementHandler triggerInput;
		
		[ConditionalField(nameof(triggerInput), false, MovementHandler.PhysicsObject)]
		[SerializeField] private PhysicsObject physicsObject;
		[ConditionalField(nameof(triggerInput), false, MovementHandler.PhysicsObject)] 
		[SerializeField] private PlayerMovementPhObj playerMovementPhObj;

		[ConditionalField(nameof(triggerInput), false, MovementHandler.RidgedBody2D)] 
		[SerializeField] private Rigidbody2D rigidBody2D;
		[ConditionalField(nameof(triggerInput), false, MovementHandler.RidgedBody2D)] 
		[SerializeField] private PlayerMovementRb2d playerMovementRb2d;

		[Separator()] [Space(-15)] [Header("Vfx")]
		
		[SerializeField] private bool triggerVfx;
		[ConditionalField(nameof(triggerVfx))] 
		[SerializeField] private PlayerVfxManager vfxManager;


		private User _user;

#pragma warning restore CS0649
		/*****************************
		 *           Init            *
		 *****************************/
		private void Awake()
		{
			InitializeInput();
		}
		
		private void InitializeInput()
		{
			
			
			_user = new User
			{
				Controls = new MainControls(),
				Device = InputSystem.devices[deviceId],
			};
			
			_user.InputUser = InputUser.PerformPairingWithDevice(device: _user.Device);
			_user.InputUser.AssociateActionsWithUser(_user.Controls);
			_user.Actions = _user.Controls.Main;
			InputUser.PerformPairingWithDevice(device: InputSystem.devices[2],_user.InputUser);
			
			InitialiseSpells();

			switch (triggerInput)
			{
				case MovementHandler.None:
					break;
				case MovementHandler.RidgedBody2D:
					InitialiseMovementWithRigidbody();
					break;
				case MovementHandler.PhysicsObject:
					InitialiseMovementWithPhysicsObject();
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
		
		private void InitialiseSpells()
		{
			//Spells
			
			_user.Actions.RedSpell.performed += ctx=> StartSpellSeq(SpellType.Red, spellColors.red);

			_user.Actions.GreenSpell.performed += ctx=> StartSpellSeq(SpellType.Green, spellColors.green);
			
			_user.Actions.BlueSpell.performed += ctx => StartSpellSeq(SpellType.Blue, spellColors.blue);
			
			_user.Actions.YellowSpell.performed += ctx => StartSpellSeq(SpellType.Yellow, spellColors.yellow);
			
			//Fire 
			_user.Actions.Fire.performed += (context) =>
			{
				if (triggerSpells) spellManager.Fire();
				if(triggerSpells && !spellManager.spellChainMaxed) chainCollider.ResetSpellChain();
				if(triggerVfx) vfxManager.StopVfx(VfxName.SpellOrb);
			};
			
			//Move
			_user.Actions.Move.performed += spellManager.Move;
			_user.Actions.Move.canceled += spellManager.StopMove;

		}

		private void StartSpellSeq(SpellType spellType, Color spriteColor)
		{
			if(spellManager.spellChainMaxed) return;

			if (triggerSpells)
			{
				spellManager.CreateSpell(spellType);
				chainCollider.AddSpell(spellType);
			}
			 
			if(triggerSpriteColor) soundwaveSprite.color = spriteColor;
			
			if(triggerVfx) vfxManager.StartVfx(VfxName.SpellOrb);
			
			if(triggerAnimator) animatorEvents.StartSoundwaveAnimation();
		}

		private void InitialiseMovementWithRigidbody()
		{
			rigidBody2D.bodyType = RigidbodyType2D.Dynamic;
			playerMovementRb2d.enabled = true;
			
			//Move
			_user.Actions.Move.performed += ctx => playerMovementRb2d.MovePlayer(ctx);
			_user.Actions.Move.canceled += ctx => playerMovementRb2d.StopPlayer();
			
			//Sprint
			_user.Actions.Sprint.performed += ctx => playerMovementRb2d.Run();
			_user.Actions.Move.canceled += ctx => playerMovementRb2d.StopRun();
			
			//jump
			_user.Actions.Jump.performed += ctx => playerMovementRb2d.Jump();

		}

		private void InitialiseMovementWithPhysicsObject()
		{
			rigidBody2D.bodyType = RigidbodyType2D.Kinematic;
			playerMovementPhObj.enabled = true;
			physicsObject.enabled = true;
			
			//Move
			_user.Actions.Move.performed += ctx => playerMovementPhObj.Move(ctx);
			_user.Actions.Move.canceled += ctx => playerMovementPhObj.StopMove();
			
			//Sprint
			_user.Actions.Sprint.performed += ctx => playerMovementPhObj.Run();
			_user.Actions.Sprint.canceled += ctx => playerMovementPhObj.StopRun();
			
			//jump
			_user.Actions.Jump.performed += ctx => playerMovementPhObj.Jump();
			_user.Actions.Jump.canceled += ctx => playerMovementPhObj.StopJump();
			
		}

		private void OnEnable()
		{
			_user.Controls.Enable();
		}

		private void OnDisable()
		{
			_user.Controls.Disable();
		}
	}
}

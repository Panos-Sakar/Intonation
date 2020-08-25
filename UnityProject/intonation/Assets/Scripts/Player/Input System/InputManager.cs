using System;
using EvilOwl.Core;
using EvilOwl.Player.Animation;
using EvilOwl.Player.Spell_System;
using EvilOwl.Player.User_Management;
using EvilOwl.Player.Vfx;
using MyBox;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EvilOwl.Player.Input_System
{
	public class InputManager : MonoBehaviour
	{
#pragma warning disable CS0649
		/*****************************
		 *         Variables         *
		 *****************************/


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


		[HideInInspector] public User user;
		private MainControls.MainActions _actions;
		private int _playerId;

#pragma warning restore CS0649
		/*****************************
		 *           Init            *
		 *****************************/
		
		public void InitializeInput(InputDevice device)
		{
			user = new User(device);
			_actions = user.Controls.Main;
			
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
			user.Controls.Enable();
		}
		
		private void InitialiseSpells()
		{
			//Spells
			
			_actions.RedSpell.performed += ctx=> StartSpellSeq(SpellType.Red, spellColors.red);

			_actions.GreenSpell.performed += ctx=> StartSpellSeq(SpellType.Green, spellColors.green);
			
			_actions.BlueSpell.performed += ctx => StartSpellSeq(SpellType.Blue, spellColors.blue);
			
			_actions.YellowSpell.performed += ctx => StartSpellSeq(SpellType.Yellow, spellColors.yellow);
			
			//Fire 
			_actions.Fire.performed += (context) =>
			{
				if (triggerSpells) spellManager.Fire();
				if(triggerSpells && !spellManager.spellChainMaxed) chainCollider.ResetSpellChain();
				if(triggerVfx) vfxManager.StopVfx(VfxName.SpellOrb);
			};
			
			//Move
			_actions.Move.performed += spellManager.Move;
			_actions.Move.canceled += spellManager.StopMove;

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

			if (triggerVfx)
			{
				vfxManager.AddGradientColor(spriteColor);
				vfxManager.StartVfx(VfxName.SpellOrb);
			}

			if(triggerAnimator) animatorEvents.StartSoundwaveAnimation();
		}

		private void InitialiseMovementWithRigidbody()
		{
			rigidBody2D.bodyType = RigidbodyType2D.Dynamic;
			playerMovementRb2d.enabled = true;
			
			//Move
			_actions.Move.performed += ctx => playerMovementRb2d.MovePlayer(ctx);
			_actions.Move.canceled += ctx => playerMovementRb2d.StopPlayer();
			
			//Sprint
			_actions.Sprint.performed += ctx => playerMovementRb2d.Run();
			_actions.Move.canceled += ctx => playerMovementRb2d.StopRun();
			
			//jump
			_actions.Jump.performed += ctx => playerMovementRb2d.Jump();

		}

		private void InitialiseMovementWithPhysicsObject()
		{
			rigidBody2D.bodyType = RigidbodyType2D.Kinematic;
			playerMovementPhObj.enabled = true;
			physicsObject.enabled = true;
			
			//Move
			_actions.Move.performed += ctx => playerMovementPhObj.Move(ctx);
			_actions.Move.canceled += ctx => playerMovementPhObj.StopMove();
			
			//Sprint
			_actions.Sprint.performed += ctx => playerMovementPhObj.Run();
			_actions.Sprint.canceled += ctx => playerMovementPhObj.StopRun();
			
			//jump
			_actions.Jump.performed += ctx => playerMovementPhObj.Jump();
			_actions.Jump.canceled += ctx => playerMovementPhObj.StopJump();
			
		}

		private void OnEnable()
		{
			user?.Controls.Enable();
		}

		private void OnDisable()
		{
			user?.Controls.Disable();
		}
	}
}

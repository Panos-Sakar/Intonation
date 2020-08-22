using System;
using EvilOwl.Core;
using MyBox;
using UnityEngine;

namespace EvilOwl.Player.Input_System
{
	public class InputManager : MonoBehaviour
	{
#pragma warning disable CS0649
		/*****************************
		 *         Variables         *
		 *****************************/

		[Separator()]
		[Space(-15)]
		[Header("Animation")]
		[SerializeField] private bool triggerAnimator;
		[ConditionalField(nameof(triggerAnimator))] 
		[SerializeField] private Animator animator;
		[ConditionalField(nameof(triggerAnimator))] 
		[SerializeField] private AnimatorEvents animatorEvents;

		[Separator()]
		[Space(-15)]
		[Header("Soundwave Sprite")]
		[SerializeField] private bool triggerSpriteColor;
		[ConditionalField(nameof(triggerSpriteColor))] 
		[SerializeField] private SpriteRenderer soundwaveSprite;
		[ConditionalField(nameof(triggerSpriteColor))] 
		[SerializeField] private SpellColors spellColors;

		[Separator()]
		[Space(-15)]
		[Header("Spells")]
		[SerializeField] private bool triggerSpells;
		[ConditionalField(nameof(triggerSpells))] 
		[SerializeField] private SpellManagerV2 spellManager;
		[ConditionalField(nameof(triggerSpells))] 
		[SerializeField] private PlayerSpellChainCollider chainCollider;
		
		[Separator()]
		[Space(-15)]
		[Header("Movement")]
		[SerializeField] private MovementHandler triggerInput;

		[ConditionalField(nameof(triggerInput), false, MovementHandler.PhysicsObject)] 
		[SerializeField] private PhysicsObject physicsObject;

		[ConditionalField(nameof(triggerInput), false, MovementHandler.RidgedBody2D)] 
		[SerializeField] private Rigidbody2D rigidBody2D;

		[Separator()]
		[Space(-15)]
		[Header("Vfx")]
		[SerializeField] private bool triggerVfx;
		[ConditionalField(nameof(triggerVfx))] 
		[SerializeField] private PlayerVfxManager vfxManager;
		
		
		private MainControls _controls;
		
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
			_controls = new MainControls();

			InitialiseSpells();
			
			if (triggerAnimator) InitialiseAnimation();

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

		private void InitialiseAnimation()
		{
			
		}
		private void InitialiseSpells()
		{
			//Spells
			_controls.Player.RedSpell.performed += (context) =>
			{
				if (triggerSpells) spellManager.CreateSpell(SpellType.Red);
				if(!spellManager.spellChainMaxed) chainCollider.AddSpell(SpellType.Red);
				if(triggerSpriteColor) soundwaveSprite.color = spellColors.redEffectColour;
				if(triggerVfx) vfxManager.StartVfx(VfxName.SpellOrb);
			};
			
			_controls.Player.GreenSpell.performed += (context) =>
			{
				if (triggerSpells) spellManager.CreateSpell(SpellType.Green);
				if(!spellManager.spellChainMaxed) chainCollider.AddSpell(SpellType.Green);
				if(triggerSpriteColor) soundwaveSprite.color = spellColors.greenEffectColour;
				if(triggerVfx) vfxManager.StartVfx(VfxName.SpellOrb);
			};
			
			_controls.Player.BlueSpell.performed += (context) =>
			{
				if (triggerSpells) spellManager.CreateSpell(SpellType.Blue);
				if(!spellManager.spellChainMaxed) chainCollider.AddSpell(SpellType.Blue);
				if(triggerSpriteColor) soundwaveSprite.color = spellColors.blueEffectColour;
				if(triggerVfx) vfxManager.StartVfx(VfxName.SpellOrb);
			};
			
			_controls.Player.YellowSpell.performed += (context) =>
			{
				if (triggerSpells) spellManager.CreateSpell(SpellType.Yellow);
				if(!spellManager.spellChainMaxed) chainCollider.AddSpell(SpellType.Yellow);
				if(triggerSpriteColor) soundwaveSprite.color = spellColors.yellowEffectColour;
				if(triggerVfx) vfxManager.StartVfx(VfxName.SpellOrb);
			};
			
			//Fire 
			_controls.Player.Fire.performed += (context) =>
			{
				if (triggerSpells) spellManager.Fire();
				if(!spellManager.spellChainMaxed) chainCollider.ResetSpellChain();
				if(triggerVfx) vfxManager.StopVfx(VfxName.SpellOrb);
			};
			
			//Move
			_controls.Player.Move.performed += spellManager.Move;
			_controls.Player.Move.canceled += spellManager.StopMove;

		}
		private void InitialiseMovementWithRigidbody()
		{
			
		}
		private void InitialiseMovementWithPhysicsObject()
		{
			
		}

		private void OnEnable()
		{
			_controls.Enable();
		}

		private void OnDisable()
		{
			_controls.Disable();
		}
	}
}

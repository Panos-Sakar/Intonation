using UnityEngine;
using UnityEngine.InputSystem;
using EvilOwl.Player.Input_System;

namespace EvilOwl.Player
{
	public class TileColorChangeTest : MonoBehaviour
	{
#pragma warning disable CS0649
		/*****************************
		 *         Variables         *
		 *****************************/
		[SerializeField] private SpriteRenderer sprite;
		[SerializeField] private Animator animator;
		
		[Header("Colors")] 
		[SerializeField] private Color defaultColor;
		[SerializeField] private SpellColors spellColors;

		private MainControls _controls;
		private static readonly int StartSoundwave = Animator.StringToHash("StartSoundwave");

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
			
			//Spells
			_controls.Player.RedSpell.performed += Red;
			_controls.Player.GreenSpell.performed += Green;
			_controls.Player.BlueSpell.performed += Blue;
			_controls.Player.YellowSpell.performed += Yellow;
		}
		
		private void Red(InputAction.CallbackContext context)
		{
			sprite.color = spellColors.redEffectColour;
			animator.SetTrigger(StartSoundwave);
		}
		
		private void Green(InputAction.CallbackContext context)
		{
			sprite.color = spellColors.greenEffectColour;
			animator.SetTrigger(StartSoundwave);
		}
		
		private void Blue(InputAction.CallbackContext context)
		{
			sprite.color = spellColors.blueEffectColour;
			animator.SetTrigger(StartSoundwave);
		}
		
		private void Yellow(InputAction.CallbackContext context)
		{
			sprite.color = spellColors.yellowEffectColour;
			animator.SetTrigger(StartSoundwave);
		}

		public void ResetColor()
		{
			sprite.color = defaultColor;
		}
	}
}

using System.Collections.Generic;
using EvilOwl.Player.Input_System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

namespace EvilOwl.Player
{
	public class SpellManager : MonoBehaviour
	{
#pragma warning disable CS0649
		/*****************************
		 *         Variables         *
		 *****************************/
		[Header("Properties")]
		[SerializeField] private int maxSpells;
		[SerializeField] private float spellSpacing;
		[SerializeField] private float spellCircleRadius;
		[SerializeField] private float spellCircleOffset;

		[Header("References")] 
		[SerializeField] private GameObject spellsParent;
		[SerializeField] private GameObject spellPrefab;
		[SerializeField] private SpellColors spellColors;
		[SerializeField] private SpriteRenderer soundwaveSprite;
		[SerializeField] private Animator playerAnimator;
		[SerializeField] private VisualEffect vfx;
		
		private MainControls _controls;
		private List<GameObject> _spells;

		private bool _vfxIsActive;
		
		private static readonly int StartSoundwave = Animator.StringToHash("StartSoundwave");
		
#pragma warning restore CS0649
		/*****************************
		 *           Init            *
		 *****************************/
		private void Awake()
		{
			InitializeInput();
			_spells = new List<GameObject>();
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
			if (_spells.Count >= maxSpells) return;
			
			soundwaveSprite.color = spellColors.redEffectColour;
			playerAnimator.SetTrigger(StartSoundwave);
			CreateSpell(SpellType.Red);
		}
		
		private void Green(InputAction.CallbackContext context)
		{
			if (_spells.Count >= maxSpells) return;
			
			soundwaveSprite.color = spellColors.greenEffectColour;
			playerAnimator.SetTrigger(StartSoundwave);
			CreateSpell(SpellType.Green);
		}
		
		private void Blue(InputAction.CallbackContext context)
		{
			if (_spells.Count >= maxSpells) return;
			
			soundwaveSprite.color = spellColors.blueEffectColour;
			playerAnimator.SetTrigger(StartSoundwave);
			CreateSpell(SpellType.Blue);
		}
		
		private void Yellow(InputAction.CallbackContext context)
		{
			if (_spells.Count >= maxSpells) return;
			
			soundwaveSprite.color = spellColors.yellowEffectColour;
			playerAnimator.SetTrigger(StartSoundwave);
			CreateSpell(SpellType.Yellow);
		}

		private void CreateSpell(SpellType type)
		{
			var newSpell = Instantiate(spellPrefab, spellsParent.transform);
			_spells.Add(newSpell);

			var newSpellScript = newSpell.GetComponent<Spell>();
			newSpellScript.Initialise(_spells.Count, spellSpacing, type);
			newSpellScript.PlaceSpellAroundCircle(spellsParent.transform.localPosition, spellCircleRadius , spellCircleOffset);


			if(_vfxIsActive) return;
			
			vfx.SendEvent("Start");
			_vfxIsActive = true;
		}
	}
}

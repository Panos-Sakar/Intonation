using System.Collections.Generic;
using EvilOwl.Player.Input_System;
using UnityEngine;
using UnityEngine.InputSystem;

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
		
		[Header("References")]
		[SerializeField] private GameObject spellPrefab;

		
		private MainControls _controls;
		private List<GameObject> _spells;

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
			
			//Fire
			_controls.Player.RedSpell.performed += Red;
			_controls.Player.GreenSpell.performed += Green;
			_controls.Player.BlueSpell.performed += Blue;
			_controls.Player.YellowSpell.performed += Yellow;
		}
		
		private void Red(InputAction.CallbackContext context)
		{
			if (_spells.Count < maxSpells) CreateSpell(SpellType.Red);
		}
		
		private void Green(InputAction.CallbackContext context)
		{
			if (_spells.Count < maxSpells) CreateSpell(SpellType.Green);
		}
		
		private void Blue(InputAction.CallbackContext context)
		{
			if (_spells.Count < maxSpells) CreateSpell(SpellType.Blue);
		}
		
		private void Yellow(InputAction.CallbackContext context)
		{
			if (_spells.Count < maxSpells) CreateSpell(SpellType.Yellow);
		}

		private void CreateSpell(SpellType type)
		{
			var newSpell = Instantiate(spellPrefab, transform);
			
			
			newSpell.GetComponent<Spell>().Initialise(_spells.Count, spellSpacing, type);
			
			_spells.Add(newSpell);
			
		}
	}
}

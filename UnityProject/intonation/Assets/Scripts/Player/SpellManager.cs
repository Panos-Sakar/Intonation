using System.Collections.Generic;
using EvilOwl.Core;
using EvilOwl.Player.Input_System;
using Pathfinding;
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
		[Header("General")]
		[SerializeField] private int maxSpells;
		[SerializeField] private bool useJoints;
		
		[Header("Positioning")]
		[SerializeField] private bool spawnAtCenterOfParent;
		[SerializeField] private float spellSpacing;
		[SerializeField] private float spellCircleRadius;
		[SerializeField] private float spellCircleOffset;
		
		[Header("Targeting")] 
		[SerializeField] private GameObject targetAtPlayer;
		[SerializeField] private float defaultSpeed;
		[SerializeField] private bool increaseSpeedWhenSeeking;
		[SerializeField] private float seekingSpeed;
		[SerializeField] private bool increaseSpeedWhenWalking;
		[SerializeField] private float walkingSpeed;

		[Header("References")] 
		[SerializeField] private GameObject spellsParent;
		[SerializeField] private GameObject spellPrefab;
		[SerializeField] private SpellColors spellColors;
		[SerializeField] private SpriteRenderer soundwaveSprite;
		[SerializeField] private Animator playerAnimator;
		[SerializeField] private ParticleSystem vfx;

		private MainControls _controls;
		private List<GameObject> _spells;

		private bool _vfxIsActive;
		private bool _speedIncreased;
		
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
			
			//Move
			_controls.Player.Move.performed += Move;
			_controls.Player.Move.canceled += StopMove;
			//Spells
			_controls.Player.RedSpell.performed += Red;
			_controls.Player.GreenSpell.performed += Green;
			_controls.Player.BlueSpell.performed += Blue;
			_controls.Player.YellowSpell.performed += Yellow;
			
			//Fire
			_controls.Player.Fire.performed += Fire;
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

		private void Fire(InputAction.CallbackContext context)
		{
			if(_spells.Count == 0) return;

			var enemy = GameObject.FindWithTag("Enemy"); // TODO: Implement ray cast to find enemy

			if (enemy == null)
			{
				_spells[0].GetComponent<Spell>().SelfDestroy();
			}
			else
			{
				if(increaseSpeedWhenSeeking) SetSpellChainSpeed(seekingSpeed);
				SetSpellLight(3.5f);
				ActivateSeekTarget(enemy);
				
			}
			
			StopSpellVFx();
			_spells.Clear();
		}

		private void Move(InputAction.CallbackContext context)
		{
			if (!increaseSpeedWhenWalking) return;
			
			SetSpellChainSpeed(walkingSpeed);
			_speedIncreased = true;

		}		
		private void StopMove(InputAction.CallbackContext context)
		{
			if (!_speedIncreased) return;
			
			SetSpellChainSpeed(defaultSpeed);
			_speedIncreased = false;
			//TODO: Find a more appropriate time to restore speed 
		}

		private void CreateSpell(SpellType type)
		{
			var newSpell = InstantiateSpell();

			var newSpellScript = InitialiseSpell(newSpell, type);
			
			if(_spells.Count == 1) newSpellScript.MakeSpellLeader();
			
			PositionSpell(newSpellScript);

			ChainSpells(newSpell, newSpellScript);

			SetSpellChainSpeed(defaultSpeed);
			
			newSpellScript.SetSpellTarget(targetAtPlayer);
			
			StartSpellVfx();
		}

		private GameObject InstantiateSpell()
		{
			var newSpell = Instantiate(spellPrefab, spellsParent.transform);
			_spells.Add(newSpell);
			newSpell.name = $"Spell_No_{_spells.Count}";
			return newSpell;
		}

		private Spell InitialiseSpell(GameObject newSpell, SpellType type)
		{
			var newSpellScript = newSpell.GetComponent<Spell>();
			newSpellScript.Initialise(_spells.Count, spellSpacing, type, useJoints);
			return newSpellScript;
		}

		private void ChainSpells(GameObject newSpell, Spell newSpellScript)
		{
			newSpellScript.SetPreviousSpell((_spells.Count > 1) ? _spells[_spells.Count - 2] : null);
			
			if(_spells.Count > 1) _spells[_spells.Count - 2].GetComponent<Spell>().SetNextSpell(newSpell);
			
			_spells[0].GetComponent<SpellLeader>()?.AddSpell(newSpellScript.type);
		}
		
		private void PositionSpell(Spell spellScript)
		{
			if (spawnAtCenterOfParent)
			{
				spellScript.gameObject.transform.position = gameObject.transform.position;
			}
			else
			{
				spellScript.PlaceSpellAroundCircle(spellsParent.transform.localPosition, spellCircleRadius, spellCircleOffset);
			}
		}
		
		private void StartSpellVfx()
		{
			if(_vfxIsActive) return;
			
			vfx.Play();
			_vfxIsActive = true;
		}

		private  void StopSpellVFx()
		{
			vfx.Stop();
			_vfxIsActive = false;
		}

		private void SetSpellChainSpeed(float speed)
		{
			foreach (var spell in _spells) 
			{
				spell.GetComponent<AIPath>().maxSpeed = speed;
				
			}
		}

		private void SetSpellLight(float strength)
		{
			foreach (var spell in _spells) 
			{
				spell.GetComponent<Spell>().LerpLight(strength);
			}
		}
		
		private void ActivateSeekTarget(GameObject seekTarget)
		{
			var leaderDestSetter = _spells[0].GetComponent<AIDestinationSetter>();
			var leaderCollider = _spells[0].GetComponent<CircleCollider2D>();
			
			leaderCollider.enabled = true;
			leaderDestSetter.target = seekTarget.transform;
		}
	}
}

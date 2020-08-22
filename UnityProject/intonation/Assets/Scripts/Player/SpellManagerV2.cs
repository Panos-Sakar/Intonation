using System.Collections.Generic;
using EvilOwl.Core;
using MyBox;
using Pathfinding;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EvilOwl.Player
{
	public class SpellManagerV2 : MonoBehaviour
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
		[ConditionalField(nameof(spawnAtCenterOfParent),true)] 
		[SerializeField] private float spellSpacing;
		[ConditionalField(nameof(spawnAtCenterOfParent),true)] 
		[SerializeField] private float spellCircleRadius;
		[ConditionalField(nameof(spawnAtCenterOfParent),true)] 
		[SerializeField] private float spellCircleOffset;
		
		[Header("Targeting")] 
		[SerializeField] private GameObject targetAtPlayer;
		[SerializeField] private float defaultSpeed;
		[SerializeField] private bool increaseSpeedWhenSeeking;
		[ConditionalField(nameof(increaseSpeedWhenSeeking))]
		[SerializeField] private float seekingSpeed;
		[SerializeField] private bool increaseSpeedWhenWalking;
		[ConditionalField(nameof(increaseSpeedWhenWalking))]
		[SerializeField] private float walkingSpeed;

		[Header("References")] 
		[SerializeField] private GameObject spellsParent;
		[SerializeField] private GameObject spellPrefab;

		private List<GameObject> _spells;
		public bool spellChainMaxed;
		
		private bool _speedIncreased;

#pragma warning restore CS0649
		/*****************************
		 *           Init            *
		 *****************************/
		private void Awake()
		{
			_spells = new List<GameObject>();
			spellChainMaxed = false;
		}

		/*****************************
		 *          Update           *
		 *****************************/

		/*****************************
		 *          Methods          *
		 *****************************/

		public void CreateSpell(SpellType type)
		{
			if (_spells.Count >= maxSpells) {
				spellChainMaxed = true;
				return;
			}
			
			var newSpell = InstantiateSpell();

			var newSpellScript = InitialiseSpell(newSpell, type);
			
			if(_spells.Count == 1) newSpellScript.MakeSpellLeader();
			
			PositionSpell(newSpellScript);

			ChainSpells(newSpell, newSpellScript);

			SetSpellChainSpeed(defaultSpeed);
			
			newSpellScript.SetSpellTarget(targetAtPlayer);
		}

		private GameObject InstantiateSpell()
		{
			var newSpell = Instantiate(spellPrefab, spellsParent.transform);
			_spells.Add(newSpell);
			newSpell.name = $"Spell_No_{_spells.Count}";
			newSpell.tag = gameObject.tag;
			return newSpell;
		}

		private Spell InitialiseSpell(GameObject newSpell, SpellType type)
		{
			var newSpellScript = newSpell.GetComponent<Spell>();
			newSpellScript.Initialise(_spells.Count, spellSpacing, type, useJoints);
			return newSpellScript;
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

		private void ChainSpells(GameObject newSpell, Spell newSpellScript)
		{
			newSpellScript.SetPreviousSpell((_spells.Count > 1) ? _spells[_spells.Count - 2] : null);
			
			if(_spells.Count > 1) _spells[_spells.Count - 2].GetComponent<Spell>().SetNextSpell(newSpell);
			
			_spells[0].GetComponent<SpellLeader>()?.AddSpell(newSpellScript.type);
		}

		public void Fire()
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

			spellChainMaxed = false;
			_spells.Clear();
		}

		public void Move(InputAction.CallbackContext context)
		{
			if (!increaseSpeedWhenWalking) return;
			
			SetSpellChainSpeed(walkingSpeed);
			_speedIncreased = true;

		}

		public void StopMove(InputAction.CallbackContext context)
		{
			if (!_speedIncreased) return;
			
			SetSpellChainSpeed(defaultSpeed);
			_speedIncreased = false;
			//TODO: Find a more appropriate time to restore speed 
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

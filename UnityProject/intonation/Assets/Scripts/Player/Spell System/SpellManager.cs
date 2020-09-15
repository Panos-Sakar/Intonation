using System.Collections.Generic;
using EvilOwl.Core;
using MyBox;
using Pathfinding;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EvilOwl.Player.Spell_System
{
	public class SpellManager : MonoBehaviour
	{
#pragma warning disable CS0649
		/*****************************
		 *         Variables         *
		 *****************************/
		[Separator]
		[Space(-15)]
		[Header("General")]
		[SerializeField] private int maxSpells;
		[SerializeField] private bool useJoints;
		[SerializeField][PositiveValueOnly] private float searchRadius;
		[SerializeField] private LayerMask searchLayerMask;

		[Separator]
		[Space(-15)]
		[Header("Positioning")]
		[SerializeField] private bool spawnAtCenterOfParent;
		[ConditionalField(nameof(spawnAtCenterOfParent),true)] 
		[SerializeField] private float spellSpacing;
		[ConditionalField(nameof(spawnAtCenterOfParent),true)] 
		[SerializeField] private float spellCircleRadius;
		[ConditionalField(nameof(spawnAtCenterOfParent),true)] 
		[SerializeField] private float spellCircleOffset;
		
		[Separator]
		[Space(-15)]
		[Header("Targeting")] 
		[SerializeField] private GameObject targetAtPlayer;
		
		[Separator]
		[Space(-15)]
		[Header("Speed")] 
		[SerializeField] private float defaultSpeed = 15;
		[Space(15)]
		[SerializeField] private bool increaseSpeedWhenSeeking;
		[ConditionalField(nameof(increaseSpeedWhenSeeking))]
		[SerializeField] private float seekingSpeed = 120;
		[Space(15)]
		[SerializeField] private bool increaseSpeedWhenWalking;
		[ConditionalField(nameof(increaseSpeedWhenWalking))]
		[SerializeField] private float walkingSpeed = 70;

		[Separator]
		[Space(-15)]
		[Header("References")] 
		[SerializeField] private GameObject spellsParent;
		[SerializeField] private GameObject spellPrefab;

		private List<GameObject> _spells;
		[HideInInspector]
		public bool spellChainMaxed;

		private Collider2D[] _hitColliders;
		private bool _speedIncreased;
		private Vector3 _playerPos;
#pragma warning restore CS0649
		/*****************************
		 *           Init            *
		 *****************************/
		private void Awake()
		{
			_hitColliders = new Collider2D[10];
			_spells = new List<GameObject>();
			spellChainMaxed = false;
			if (spellsParent == null) spellsParent = GameObject.FindGameObjectWithTag("SpellsParent");
			
		}

		/*****************************
		 *          Update           *
		 *****************************/

		/*****************************
		 *          Methods          *
		 *****************************/

		public void CreateSpell(SpellType type)
		{
			if (_spells.Count >= maxSpells) return;

			var newSpell = InstantiateSpell();

			var newSpellScript = InitialiseSpell(newSpell, type);
			
			if(_spells.Count == 1) newSpellScript.MakeSpellLeader();
			
			PositionSpell(newSpellScript);

			ChainSpells(newSpell, newSpellScript);

			SetSpellChainSpeed(defaultSpeed);
			
			newSpellScript.SetSpellTarget(targetAtPlayer);
			
			if(_spells.Count == maxSpells) spellChainMaxed = true;
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
			var ignoreId = gameObject.transform.parent.GetInstanceID();
			newSpellScript.Initialise(_spells.Count, spellSpacing, type, useJoints, ignoreId);
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

			//var target = GameObject.FindWithTag("Enemy"); // TODO: Implement ray cast to find enemy
			GameObject target = null;
			
			_playerPos = gameObject.transform.position;
			var  numColliders = Physics2D.OverlapCircleNonAlloc(_playerPos, searchRadius, _hitColliders, searchLayerMask);
			var minDist = 100000f;

			if ( numColliders <= 0)
			{
				//_spells[0].GetComponent<Spell>().SelfDestroy();
				return;
			}

			for (var index = 0; index < _hitColliders.Length; index++)
			{
				var hitCollider = _hitColliders[index];
				_hitColliders[index] = null;
				
				if (hitCollider == null ||
				    gameObject.transform.parent.GetInstanceID() == hitCollider.transform.GetInstanceID()) continue;

				var offset = _playerPos - hitCollider.transform.position;
				var dist = Vector3.SqrMagnitude(offset);

				if (!(dist < minDist)) continue;

				target = hitCollider.gameObject;
				minDist = dist;
			}

			if (target == null) return;
			
			if(increaseSpeedWhenSeeking) SetSpellChainSpeed(seekingSpeed);
			SetSpellLight(3.5f);
			ActivateSeekTarget(target);

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

		private void OnDrawGizmosSelected()
		{
			Gizmos.DrawWireSphere(gameObject.transform.position, searchRadius);
		}
	}
}

using System;
using EvilOwl.Core;
using EvilOwl.Core.Spell_System;
using EvilOwl.Core.Stats;
using EvilOwl.Enemy.Ai.States;
using JetBrains.Annotations;
using MyBox;
using TMPro;
using UnityEngine;
using Random = System.Random;

namespace EvilOwl.Enemy.Ai
{
	public class AiStateController : MonoBehaviour
	{
#pragma warning disable CS0649
		/*****************************
		 *         Variables         *
		 *****************************/
		[Header("References")]
		
		[SerializeField] private SpellChainCollider spellChainCollider;
		[SerializeField] private SpellManager spellManager;
		// ReSharper disable once NotAccessedField.Local
		[SerializeField] private EnemyStats stats;
		[SerializeField] private EnemySharedStats sharedStats;
		[SerializeField] private PhysicsObject physicsObject;
		[SerializeField] private GameObject gfx;
		[SerializeField] private TextMeshProUGUI debugStateTmp;

		[Header("Ai")]
		
		[SerializeField] private bool aiIsActive;
		[SerializeField] public State currentState;

		[Header("Settings")] 
		[SerializeField] private Transform[] patrolPositions;
		
		//Public
		public int MaxSpellLength => spellManager.MaxSpellLength;
		public float Speed => sharedStats.speed;
		public int SpellLength { set; get; }
		public Vector3 Position => gameObject.transform.position;
		public GameObject Target { get; set; }
		public Vector3 TargetPosition  { get; set; }
		public SpellCastingState CastSpellState { set; get; }
		public Vector2 TargetSpeed
		{
			get => physicsObject.targetVelocity;
			set => physicsObject.targetVelocity = value;
		}
		public float SetGfxDirectionX
		{
			get => gfx.transform.localScale.x;
			set => gfx.transform.localScale = new Vector3(value<0? -1f:1f,1,1);
		}

		public Vector3 GetNextPatrolPoint
		{
			get
			{
				var ret = patrolPositions[_nextPatrolPointIndex];
				_nextPatrolPointIndex++;
				if (_nextPatrolPointIndex >= patrolPositions.Length) _nextPatrolPointIndex = 0;
				
				return ret.position;
			}
		}
		public bool HasCollidedWithOtherEnemy
		{
			get
			{
				if (!_collisionWithOtherEnemy) return false;
				
				_collisionWithOtherEnemy = false;
				return true;

			}
			set => _collisionWithOtherEnemy = value;
		}

		//Privates 
		private float _castSpellTimerAcc;
		private float _addSpellTimerAcc;
		private float _fireSpellTimerAcc;
		private int _nextPatrolPointIndex;
		private bool _collisionWithOtherEnemy;
		
		private Array _spellTypeValues;
		private Random _random;
		
#pragma warning restore CS0649
		/*****************************
		 *           Init            *
		 *****************************/

		private void Awake()
		{
			_spellTypeValues = Enum.GetValues(typeof(SpellType));
			_random = new Random(gameObject.GetInstanceID());
			
			TargetPosition = patrolPositions[0].transform.position;
			
			debugStateTmp.text = currentState.stateName;
			
			_castSpellTimerAcc = Time.time;
			_addSpellTimerAcc = _castSpellTimerAcc;
			_fireSpellTimerAcc = _castSpellTimerAcc;
			
			CastSpellState = SpellCastingState.Waiting;
		}

		/*****************************
		 *          Update           *
		 *****************************/
		private void Update()
		{
			if(aiIsActive) currentState.UpdateState(this);
		}

		/*****************************
		 *          Methods          *
		 *****************************/

		public void AddRandomSpell()
		{
			if (spellManager.spellChainMaxed) return;
			
			var randomBar = (SpellType)_spellTypeValues.GetValue(_random.Next(_spellTypeValues.Length));
			
			AddSpell(randomBar);
			
		}

		public bool EnemyIsReadyToCastSpell()
		{
			if (Time.time - _castSpellTimerAcc < sharedStats.spellTimerCooldown) return false;

			_castSpellTimerAcc = Time.time;
			return true;
		}
		
		public bool EnemyIsReadyToAddSpell()
		{
			if (Time.time - _addSpellTimerAcc < sharedStats.addSpellCooldown) return false;

			_addSpellTimerAcc = Time.time;
			return true;
		}

		public bool EnemyIsReadyToFire()
		{
			if (Time.time - _fireSpellTimerAcc < sharedStats.fireSpellDelay) return false;

			_fireSpellTimerAcc = Time.time;
			return true;
		}

		private void AddSpell(SpellType type)
		{
			if(spellManager.spellChainMaxed) return;
			
			spellManager.CreateSpell(type);
			spellChainCollider.AddSpell(type);
		}
		
		[ButtonMethod()]
		[UsedImplicitly]
		public void FireSpell()
		{
			spellManager.Fire(findTarget: Target == null, target: Target);
			spellChainCollider.ResetSpellChain();
		}

		public void DestroySpell()
		{
			//TODO: Create function to destroy spell in spellManager
			FireSpell();
		}

		public void OnStateChanged()
		{
			debugStateTmp.text = currentState.stateName;
		}

		// private void OnTriggerEnter2D(Collider2D other)
		// {
		//TODO: Why dis tho?
		// 	if (gameObject.CompareTag(other.gameObject.tag)) _collisionWithOtherEnemy = true;
		// }

		private void OnTriggerStay2D(Collider2D other)
		{
			if (gameObject.CompareTag(other.gameObject.tag)) _collisionWithOtherEnemy = true;
		}
	}
}

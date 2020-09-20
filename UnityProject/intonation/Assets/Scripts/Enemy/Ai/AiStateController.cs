using EvilOwl.Core;
using EvilOwl.Core.Spell_System;
using EvilOwl.Core.Stats;
using EvilOwl.Enemy.Ai.States;
using JetBrains.Annotations;
using MyBox;
using TMPro;
using UnityEngine;

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

		[SerializeField] private TextMeshProUGUI debugStateTmp;

		[Header("Ai")]
		
		[SerializeField] private bool aiIsActive;
		[SerializeField] public State currentState;

		[Header("Settings")] 
		[SerializeField] private Transform[] patrolPositions;
		
		//Public

		public bool SpellChainMaxed => spellManager.spellChainMaxed;
		public float Speed => sharedStats.speed;
		public Vector3 Position => gameObject.transform.position;
		public GameObject Target { get; set; }
		public Vector2 TargetSpeed
		{
			get => physicsObject.targetVelocity;
			set => physicsObject.targetVelocity = value;
		}
		public Vector3 TargetPosition  { get; set; }
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
		
		private float _spellTimer;
		private int _nextPatrolPointIndex;
		private bool _collisionWithOtherEnemy;
#pragma warning restore CS0649
		/*****************************
		 *           Init            *
		 *****************************/

		private void Awake()
		{
			TargetPosition = patrolPositions[0].transform.position;
			debugStateTmp.text = currentState.stateName;
			_spellTimer = Time.time;
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

		public void CastRandomSpell()
		{
			if ((Time.time - _spellTimer < sharedStats.spellTimerCooldown) || (spellManager.spellChainMaxed)) return;
			
			CreateSpell(SpellType.Blue);
			_spellTimer = Time.time;
		}
		
		[ButtonMethod()]
		[UsedImplicitly]
		public void CreateRedSpell()
		{
			CreateSpell(SpellType.Red);
		}

		[ButtonMethod()]
		[UsedImplicitly]
		public void CreateGreenSpell()
		{
			CreateSpell(SpellType.Green);
		}

		[ButtonMethod()]
		[UsedImplicitly]
		public void CreateBlueSpell()
		{
			CreateSpell(SpellType.Blue);
		}
		
		[ButtonMethod()]
		[UsedImplicitly]
		public void CreateYellowSpell()
		{
			CreateSpell(SpellType.Yellow);
		}

		private void CreateSpell(SpellType type)
		{
			if(spellManager.spellChainMaxed) return;
			
			spellManager.CreateSpell(type);
			spellChainCollider.AddSpell(type);
		}
		
		[ButtonMethod()]
		[UsedImplicitly]
		private void FireSpell()
		{
			spellManager.Fire();
			spellChainCollider.ResetSpellChain();
		}

		public void OnStateChanged()
		{
			debugStateTmp.text = currentState.stateName;
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (gameObject.CompareTag(other.gameObject.tag)) _collisionWithOtherEnemy = true;
		}
	}
}

using EvilOwl.Core;
using EvilOwl.Core.Spell_System;
using EvilOwl.Core.Stats;
using EvilOwl.Enemy.Ai.States;
using JetBrains.Annotations;
using MyBox;
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
		[SerializeField] 
		private SpellChainCollider spellChainCollider;
		[SerializeField] 
		private SpellManager spellManager;
		[SerializeField] 
		private EnemyStats stats;

		[Header("Ai")] 
		[SerializeField] 
		private bool aiIsActive;
		[SerializeField]
		public State currentState;
		
		private float _spellTimer;

		public bool SpellChainMaxed => spellManager.spellChainMaxed;
		
#pragma warning restore CS0649
		/*****************************
		 *           Init            *
		 *****************************/

		private void Awake()
		{
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
			if ((Time.time - _spellTimer < stats.spellTimerCooldown) || (spellManager.spellChainMaxed)) return;
			
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
	}
}

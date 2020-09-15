using System.Collections.Generic;
using System.Linq;
using EvilOwl.Core.Interfaces;
using EvilOwl.Core.Stats;
using UnityEngine;

namespace EvilOwl.Core
{
	public class SpellChainCollider : MonoBehaviour , IDamageable
	{
#pragma warning disable CS0649
		/*****************************
		 *         Variables         *
		 *****************************/

		public List<SpellType> spellChain;
		public EntityStats stats;
		
#pragma warning restore CS0649
		/*****************************
		 *           Init            *
		 *****************************/
		private void Awake()
		{
			spellChain = new List<SpellType>();
		}
		/*****************************
		 *          Update           *
		 *****************************/
		
		/*****************************
		 *          Methods          *
		 *****************************/

		public bool Deflected(List<SpellType> othersSpellChain)
		{
			if (spellChain.Count != othersSpellChain.Count) return false;
			
			if (spellChain.Where((t, index) => othersSpellChain[index] != t).Any()) return false;

			print("Ha deflect");
			return true;
		}

		public void Damage(float damageAmount)
		{
			stats.life -= damageAmount;
			if(stats.life <= 0) stats.Kill();
		}

		public void AddSpell(SpellType spellType)
		{
			spellChain.Add(spellType);
		}

		public void ResetSpellChain()
		{
			spellChain.Clear();
		}
	}
}

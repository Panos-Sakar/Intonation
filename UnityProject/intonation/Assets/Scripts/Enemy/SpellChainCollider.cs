using System.Collections.Generic;
using EvilOwl.Core;
using EvilOwl.Core.Interfaces;
using UnityEngine;

namespace EvilOwl.Enemy
{
	public class SpellChainCollider : MonoBehaviour , IDamageable
	{
#pragma warning disable CS0649
		/*****************************
		 *         Variables         *
		 *****************************/

		public List<SpellType> spellChain;
		public float life;
		
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
			
			for (var index = 0 ; index < spellChain.Count; index++)
			{
				print($"Spells at position {index}: {spellChain[index]} other {othersSpellChain[index]}");
				if (othersSpellChain[index] != spellChain[index]) return false;
			}
			print("Ha deflect");
			return true;
		}

		public void Damage(float damageAmount)
		{
			life -= damageAmount;
			if(life <= 0) Destroy(gameObject);
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

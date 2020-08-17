using System.Collections.Generic;
using EvilOwl.Core;
using EvilOwl.Core.Interfaces;
using UnityEngine;

namespace EvilOwl.Player
{
	public class SpellLeader : MonoBehaviour
	{
#pragma warning disable CS0649
		/*****************************
		 *         Variables         *
		 *****************************/
		public Spell spellObject;
		public float damageMultiplier = 1;
		
		private List<SpellType> _spellChain;
		private float _spellChainDamage;
		
#pragma warning restore CS0649
		/*****************************
		 *           Init            *
		 *****************************/
		private void Awake()
		{
			_spellChain = new List<SpellType>();
		}
		/*****************************
		 *          Update           *
		 *****************************/

		/*****************************
		 *          Methods          *
		 *****************************/
		public void AddSpell(SpellType type)
		{
			_spellChain.Add(type);
			_spellChainDamage += _spellChain.Count * damageMultiplier;
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			var otherObject = other.GetComponent<IDamageable>();

			if (otherObject == null) return;
			
			if (otherObject.Deflected(_spellChain))
			{
				spellObject.SelfDestroy();
			}
			else
			{
				otherObject.Damage(_spellChainDamage);
				spellObject.SelfDestroy();
			}
		}
	}
}

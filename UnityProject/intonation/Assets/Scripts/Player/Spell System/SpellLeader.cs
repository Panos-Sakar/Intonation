using System.Collections.Generic;
using EvilOwl.Core;
using EvilOwl.Core.Interfaces;
using UnityEngine;

namespace EvilOwl.Player.Spell_System
{
	public class SpellLeader : MonoBehaviour
	{
#pragma warning disable CS0649
		/*****************************
		 *         Variables         *
		 *****************************/
		public float damageMultiplier = 1;
		
		private Spell _spellObject;
		private List<SpellType> _spellChain;
		private float _spellChainDamage;
		private int _ignoreInstanceId;
		
#pragma warning restore CS0649
		/*****************************
		 *           Init            *
		 *****************************/
		
		/*****************************
		 *          Update           *
		 *****************************/

		/*****************************
		 *          Methods          *
		 *****************************/
		public void Initialise(Spell spellObject, int ignoreInstanceId)
		{
			_spellChain = new List<SpellType>();
			_spellObject = spellObject;
			_spellChainDamage = 0;
			_ignoreInstanceId = ignoreInstanceId;
		}
		public void AddSpell(SpellType type)
		{
			_spellChain.Add(type);
			_spellChainDamage += _spellChain.Count * damageMultiplier;
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			var otherObject = other.GetComponent<IDamageable>();
			if (otherObject == null || other.gameObject.transform.GetInstanceID() == _ignoreInstanceId)
			{
				return;
			}
			if (otherObject.Deflected(_spellChain))
			{
				_spellObject.SelfDestroy();
			}
			else
			{
				otherObject.Damage(_spellChainDamage);
				_spellObject.SelfDestroy();
			}
		}
	}
}

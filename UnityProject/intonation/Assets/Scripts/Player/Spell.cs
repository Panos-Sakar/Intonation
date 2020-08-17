using System;
using EvilOwl.Core;
using Pathfinding;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace EvilOwl.Player
{
	public class Spell : MonoBehaviour
	{
#pragma warning disable CS0649
		/*****************************
		 *         Variables         *
		 *****************************/
		[SerializeField] private SpriteRenderer spellSprite;
		[SerializeField] private SpellColors effectColours;
		[SerializeField] private Light2D pointLight;
		[SerializeField] private AIDestinationSetter aStarDestinationSetter;

		private GameObject _parent;

		private int _position;
		private float _spacing;
		public SpellType type;
		
		private bool _isSpellLeader;
		private GameObject _previousSpell;
		private GameObject _nextSpell;


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
		public void Initialise(GameObject parent, int position, float spacing, SpellType typeOfSpell)
		{
			_parent = parent;
			_position = position;
			_spacing = spacing;
			type = typeOfSpell;

			switch (type)
			{
				case SpellType.Red:
					spellSprite.color = effectColours.redEffectColour;
					pointLight.color = effectColours.redEffectColour;
					break;
				case SpellType.Green:
					spellSprite.color = effectColours.greenEffectColour;
					pointLight.color = effectColours.greenEffectColour;
					break;
				case SpellType.Blue:
					spellSprite.color = effectColours.blueEffectColour;
					pointLight.color = effectColours.blueEffectColour;
					break;
				case SpellType.Yellow:
					spellSprite.color = effectColours.yellowEffectColour;
					pointLight.color = effectColours.yellowEffectColour;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
		
		public void PlaceSpellAroundCircle (Vector3 center, float radius, float offset){
			
			var radians = 2 * Math.PI + (_position * (_spacing - 1) + offset);
			var vertical = (float) Math.Sin(radians);
			var horizontal = (float) Math.Cos(radians); 
	         
			var spawnDir = new Vector3 (horizontal, vertical, 0);
			
			transform.localPosition = center + spawnDir * radius;
			
		}

		public void PlaceSpellAtParent()
		{
			transform.localPosition = _parent.transform.localPosition;
		}
		
		public void SetNextSpell(GameObject nextSpell)
		{
			_nextSpell = nextSpell;
		}

		public void SetPreviousSpell(GameObject previousSpell)
		{
			_previousSpell = previousSpell;
		}

		public void MakeSpellLeader()
		{
			_isSpellLeader = true;
			var spellLeader = gameObject.AddComponent(typeof(SpellLeader)) as SpellLeader;
			if (spellLeader != null) spellLeader.spellObject = this;
		}

		public void SetSpellTarget(GameObject target)
		{
			aStarDestinationSetter.target = _isSpellLeader ? target.transform : _previousSpell.transform;
		}

		public void SelfDestroy()
		{
			if(_nextSpell != null) _nextSpell.GetComponent<Spell>().SelfDestroy();
			Destroy(gameObject);
			
			//TODO: Set next spell in chain to follow enemy?
			
		}
	}
}

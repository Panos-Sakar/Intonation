using System;
using UnityEngine;

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

		private GameObject _parent;
		private Spell _nextSpell;
		private Spell _previousSpell;
		private int _position;
		private float _spacing;
		private SpellType _type;

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
		public void Initialise(int position, float spacing, SpellType type)
		{
			_position = position;
			_type = type;
			_spacing = spacing;

			switch (_type)
			{
				case SpellType.Red:
					spellSprite.color = effectColours.redEffectColour;
					break;
				case SpellType.Green:
					spellSprite.color = effectColours.greenEffectColour;
					break;
				case SpellType.Blue:
					spellSprite.color = effectColours.blueEffectColour;
					break;
				case SpellType.Yellow:
					spellSprite.color = effectColours.yellowEffectColour;
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
	}

	public enum SpellType
	{
		Red = 1,
		Green = 2,
		Blue = 3,
		Yellow = 4
	}
}

using System;
using UnityEngine;
using UnityEngine.VFX;

namespace EvilOwl.Player
{
	public class Spell : MonoBehaviour
	{
#pragma warning disable CS0649
		/*****************************
		 *         Variables         *
		 *****************************/
		[SerializeField] private VisualEffect vfx;
		[SerializeField] private Color redEffectColour;
		[SerializeField] private Color greenEffectColour;
		[SerializeField] private Color blueEffectColour;
		[SerializeField] private Color yellowEffectColour;
			
		private SpellManager _parent;
		private Spell _nextSpell;
		private Spell _previousSpell;
		private int _position;
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
			transform.localPosition = new Vector3(_position*spacing,0,0);

			switch (_type)
			{
				case SpellType.Red:
					vfx.SetVector4("BaseColor",redEffectColour);
					break;
				case SpellType.Green:
					vfx.SetVector4("BaseColor",greenEffectColour);
					break;
				case SpellType.Blue:
					vfx.SetVector4("BaseColor",blueEffectColour);
					break;
				case SpellType.Yellow:
					vfx.SetVector4("BaseColor",yellowEffectColour);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
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

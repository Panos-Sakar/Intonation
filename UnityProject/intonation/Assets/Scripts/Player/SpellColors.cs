using UnityEngine;

namespace EvilOwl.Player
{
	[CreateAssetMenu(fileName = "EffectColours", menuName ="Game Data/EffectColours")]
	public class SpellColors : ScriptableObject
	{
		public Color redEffectColour;
		public Color greenEffectColour;
		public Color blueEffectColour;
		public Color yellowEffectColour;
	}
}

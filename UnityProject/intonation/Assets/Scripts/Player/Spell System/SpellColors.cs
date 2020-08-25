using UnityEngine;

namespace EvilOwl.Player.Spell_System
{
	[CreateAssetMenu(fileName = "EffectColours", menuName ="Game Data/EffectColours")]
	public class SpellColors : ScriptableObject
	{
		public Color red;
		public Color green;
		public Color blue;
		public Color yellow;
	}
}

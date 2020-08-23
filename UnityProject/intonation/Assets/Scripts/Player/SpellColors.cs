using UnityEngine;

namespace EvilOwl.Player
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

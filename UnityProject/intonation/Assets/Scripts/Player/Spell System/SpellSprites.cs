using UnityEngine;

namespace EvilOwl.Player.Spell_System
{
	[CreateAssetMenu(fileName = "SpellSprites", menuName ="Game Data/SpellSprites")]
	public class SpellSprites : ScriptableObject
	{
		public Sprite red;
		public Sprite green;
		public Sprite blue;
		public Sprite yellow;
	}
}

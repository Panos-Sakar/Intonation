using JetBrains.Annotations;
using UnityEngine;

namespace EvilOwl.Player
{
	public class AnimatorEvents : MonoBehaviour
	{
#pragma warning disable CS0649
		
		[SerializeField] private SpriteRenderer sprite;
		[SerializeField] private Color defaultColor;

#pragma warning restore CS0649

		[UsedImplicitly]
		public void ResetColor()
		{
			sprite.color = defaultColor;
		}
	}
}

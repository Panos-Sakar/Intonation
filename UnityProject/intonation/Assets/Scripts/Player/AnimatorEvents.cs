using JetBrains.Annotations;
using UnityEngine;

namespace EvilOwl.Player
{
	public class AnimatorEvents : MonoBehaviour
	{
#pragma warning disable CS0649
		
		[SerializeField] private SpriteRenderer sprite;
		[SerializeField] private Color defaultColor;
		[SerializeField] private Animator animator;
		
		private static readonly int StartSoundwave = Animator.StringToHash("StartSoundwave");
		private static readonly int MoveSpeed = Animator.StringToHash("moveSpeed");
		private static readonly int IsJumping = Animator.StringToHash("isJumping");
		private static readonly int IsRunning = Animator.StringToHash("isRunning");
		
#pragma warning restore CS0649

		[UsedImplicitly]
		public void ResetColor()
		{
			sprite.color = defaultColor;
		}

		public void StartSoundwaveAnimation()
		{
			animator.SetTrigger(StartSoundwave);
		}

		public void MoveState(float speed)
		{
			animator.SetFloat (MoveSpeed , speed);
		}

		public void StopMoving()
		{
			animator.SetFloat (MoveSpeed , 0);
		}

		public void JumpState(bool isJumping)
		{
			animator.SetBool (IsJumping , isJumping);
		}

		public void SprintState(bool isSprinting)
		{
			animator.SetBool (IsRunning , isSprinting);
		}
	}
}

using System;
using EvilOwl.Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EvilOwl.Enemy.Ai.Actions
{
	[CreateAssetMenu(fileName = "CastSpellAction", menuName = "Ai/Actions/CastSpellAction")]
	public class CastSpellAction : Action
	{
#pragma warning disable CS0649
		/*****************************
		 *         Variables         *
		 *****************************/
		
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

		public override void Act(AiStateController controller)
		{
			switch (controller.CastSpellState)
			{
				case SpellCastingState.Waiting:
					
					if (controller.EnemyIsReadyToCastSpell())
					{
						controller.CastSpellState = SpellCastingState.Casting;
						controller.SpellLength = Random.Range(0, controller.MaxSpellLength) + 1;
					}
					
					break;
				case SpellCastingState.Casting:
					
					if (controller.SpellLength > 0 && controller.EnemyIsReadyToAddSpell())
					{
						controller.AddRandomSpell();
						controller.SpellLength--;
					}
					
					if(controller.SpellLength == 0) controller.CastSpellState = SpellCastingState.Firing;
					
					break;
				case SpellCastingState.Firing:
					
					if (controller.EnemyIsReadyToFire())
					{
						controller.FireSpell();
						controller.CastSpellState = SpellCastingState.Waiting;
					}
					
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
			
		}
	}
}

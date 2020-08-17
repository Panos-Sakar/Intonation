using System.Collections.Generic;

namespace EvilOwl.Core.Interfaces
{
	public interface IDamageable
	{
		bool Deflected(List<SpellType> spellChain);
		void Damage(float damageAmount);
	}
}

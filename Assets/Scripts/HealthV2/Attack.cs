using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthV2
{
	public class Attack
	{
		public float Damage { get; private set; }
		public AttackType Type { get; private set; }

		public Attack(float damage)
		{
			Damage = damage;
			if(Damage <= 5)
			{
				Type = AttackType.Light;
			}
			else if (Damage > 5)
			{
				Type = AttackType.Heavy;
			}
			else
			{
				Type = AttackType.Light;
			}
		}

		public Attack(AttackType type)
		{
			Type = type;
			Damage = type switch
			{
				AttackType.Light => 5,
				AttackType.Heavy => 10,
				_ => 0,
			};
		}

		public Attack(float damage, AttackType type)
		{
			Damage = damage;
			Type = type;
		}

		public Attack Scale(float scale)
		{
			Damage *= scale;
			return this;
		}

		public enum AttackType
		{
			None,
			Light,
			Heavy
		}

		public static Attack operator *(Attack a, float b)
		{
			return new Attack(a.Damage * b, a.Type);
		}

		public static Attack operator *(float a, Attack b)
		{
			return new Attack(b.Damage * a, b.Type);
		}

		public static bool operator >=(Attack a, Attack b)
		{
			return a.Type >= b.Type && a.Damage >= b.Damage;
		}

		public static bool operator <=(Attack a, Attack b)
		{
			return a.Type <= b.Type && a.Damage <= b.Damage;
		}
		
		public static bool operator >(Attack a, Attack b)
		{
			return a.Type > b.Type && a.Damage > b.Damage;
		}

		public static bool operator <(Attack a, Attack b)
		{
			return a.Type < b.Type && a.Damage < b.Damage;
		}
	}
}

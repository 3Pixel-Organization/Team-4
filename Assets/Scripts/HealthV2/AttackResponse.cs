using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthV2
{
	public class AttackResponse
	{
		public float DamageTaken;
		public HitResult HitType;

		public AttackResponse(float damageTaken)
		{
			DamageTaken = damageTaken;
		}

		public AttackResponse(float damageTaken, HitResult hitResult)
		{
			DamageTaken = damageTaken;
			HitType = hitResult;
		}

		public AttackResponse(Attack attack)
		{
			DamageTaken = attack.Damage;
			HitType = (HitResult)attack.Type;
		}

		public enum HitResult
		{
			None,
			Light,
			Heavy,
			BrokeGuard,
			Death,
			InstaDeath,
			Blocked
		}

		public static AttackResponse Blocked { get => new AttackResponse(0, HitResult.Blocked); }
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthV2
{
	interface IDamageable
	{
		AttackResponse Damage(Attack attack);
	}
}

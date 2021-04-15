

namespace HealthV2
{
	public static class Delegates
	{
		public delegate void HealthEvent(float currentHealth, float maxHealth, float healthDelta);
		public delegate void AttackEvent(Attack attack);
		public delegate void StateEvent();
		public delegate AttackResponse AttackResponseEvent(Attack attack);
	}
}

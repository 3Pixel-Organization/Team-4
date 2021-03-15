

namespace HealthV2
{
	public static class Delegates
	{
		public delegate void HealthEvent(float currentHealth, float maxHealth, float healthDelta);
		public delegate void StateEvent();
	}
}

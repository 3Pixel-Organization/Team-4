namespace Health
{
    public static class Delegates
    {
   
    
        public delegate void HealthEvent(int currentHealth, int maxHealth, int healthDelta);
        public delegate void StateEvent();

    }
}
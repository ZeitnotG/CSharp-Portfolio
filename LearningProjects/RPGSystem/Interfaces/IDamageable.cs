namespace RPGSystem
{
    public interface IDamageable
    {
        string Name { get; }
        int Health {  get; }
        int Defense {  get; }
        int TakeDamage(int amount);
        int XPReward { get; }
        bool IsAlive {  get; }
    }
}


namespace RPGSystem
{
    public interface IDamageable
    {
        string Name { get; }
        int Health {  get; }
        int Defense {  get; }
        int TakeDamage(int amount);
        bool IsAlive {  get; }
    }
}


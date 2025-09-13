namespace RPGSystem.Interfaces
{
    internal interface IAttacker
    {
        int AttackPower { get; }
        void Attack(IDamageable target);
    }
}

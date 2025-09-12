namespace RPGSystem
{
    public enum ItemType
    {
        Weapon,
        Potion,
        Equipment,
        Other
    }
    public class Item
    {
        public string Name { get; set; }
        public double Weight { get; set; }
        public ItemType Type { get; set; }

        public int AttackBonus { get; set; }
        public int DefenseBonus { get; set; } 
        public int HealAmount { get; set; }

        public Item(string name, ItemType type, double weight, int attackBonus = 0, int defenseBonus = 0, int healAmount = 0)
        {
            Name = name;
            Weight = weight;
            Type = type;
            AttackBonus = attackBonus;
            DefenseBonus = defenseBonus;
            HealAmount = healAmount;
        }
    }
}


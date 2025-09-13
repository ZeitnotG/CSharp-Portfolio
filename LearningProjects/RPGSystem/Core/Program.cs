using System;

namespace RPGSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Item knife = new Item("Knife", ItemType.Weapon, 3, attackBonus:10);
            Item armor = new Item("Armor", ItemType.Equipment, 10, defenseBonus:5);
            Item potion = new Item("Health potion", ItemType.Potion, 1, healAmount: 20);

            Player player = new Player("Ivan", 30, 50, 10, 3);
            Player player1 = new Player("Lisa", 20, 40, 5, 6);

            player.AddItemToInventory(knife);
            player1.AddItemToInventory(knife);
            player1.AddItemToInventory(armor);

            player.EquipItem(armor);
            player1.EquipItem(knife);

            player1.Inventory.ShowItems();
            player.Inventory.ShowItems();

            Console.WriteLine(player);
            Console.WriteLine(player1);

            BattleService.Battle(player1, player);

            player.EquipItem(potion);

            Enemy zombie = new Enemy("Zombie", 15, 3, 1, 6, 40);
            BattleService.Battle(player, zombie);
        }
        
    }
}


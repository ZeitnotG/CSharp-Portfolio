using System;
using System.Collections.Generic;

namespace StaticNotStatic
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /* Dog dog = new Dog();
             Dog.Info();
             dog.Name = "Tuzik";
             dog.Bark();*/

            BankAccount.BankInfo();

            BankAccount bankAccount = new BankAccount("Ivan", 5000);
            BankAccount bankAccount1 = new BankAccount("Lisa", 0);
            bankAccount.Deposit(10000);
            bankAccount.Withdraw(1000);
            bankAccount.Withdraw(10000);
            bankAccount.Transfer(bankAccount1, 1000);
            bankAccount1.Withdraw(1000);
            bankAccount.ShowTransactionsHistory();
        }
    }
    public class Dog
    {
        public string Name {get;set;}

        public void Bark() 
        {
            Console.WriteLine($"Dog named {Name}" + " barks");
        }
        public static void Info()
        {
            Console.WriteLine("All dogs loves people");
        }


    }

    public class BankAccount
    {
        public string Owner { get;set;}
        public decimal Balance {get; private set;}
        private List<string> transactionsHistory = new List<string>();

        public BankAccount( string owner, decimal balance)
        {
            Owner = owner;
            Balance = balance;
        }
        public static void BankInfo()
        {
            Console.WriteLine("T-bank is the best bank in Russia");
        }
        public void Deposit(decimal amount)
        {
            if (!CheckAmount(amount))
                return;

                Balance += amount;
                ShowBalance();
            transactionsHistory.Add($"Deposit {amount}");
            
        }
        public void Withdraw(decimal amount)
        {
            if (!CheckAmount(amount))
                return;
            
                if (Balance >= amount)
                {
                    Balance -= amount;
                transactionsHistory.Add($"Withdraw {amount}");
                ShowBalance();
            }
                else
                {
                    Console.WriteLine("Sorry, you dont have enough money, try entering a smaller amount ");
                }
            
        }
        private void ShowBalance()
        {
            Console.WriteLine($"Dear {Owner}, your balance now: {Balance} $");
        }
        private bool CheckAmount(decimal amount)
        {
            bool isChecked = amount > 0;
            if (!isChecked)
            {
                Console.WriteLine("Amount must be positive!");
            }
            return isChecked;
        }
        public void Transfer(BankAccount recipient, decimal amount)
        {
            if (!CheckAmount(amount))
                return;
            if (Balance >= amount)
            {
                Balance -= amount;
                Console.WriteLine($"Transfer {amount} $ from {Owner} to {recipient.Owner} completed");
                recipient.Deposit(amount);
                ShowBalance();
                transactionsHistory.Add($"Transfer to {recipient.Owner} {amount}");

            }
            else
            {
                Console.WriteLine("Sorry, you dont have enough money, try entering a smaller amount ");
            }
        }
        public void ShowTransactionsHistory()
        {
            Console.WriteLine( $" {Owner}, Your transaction history: ");
            foreach (var transaction in transactionsHistory)
            {
                Console.WriteLine(transaction.ToString());
            }
        }
    }
}

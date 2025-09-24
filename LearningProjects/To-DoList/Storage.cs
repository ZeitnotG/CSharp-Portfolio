using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace To_DoList
{
    internal class Storage
    {
        TaskRepository repo = new TaskRepository();
        public List<TodoItem> tasks {  get;private set; }

        public Storage() => tasks = new List<TodoItem>();
        
        public void AddTask(TodoItem item) => tasks.Add(item);
        

        public void RemoveTask(TodoItem item) =>  tasks.Remove(item);  

        public void ShowAllTasks()
        {
            foreach (TodoItem item in tasks)
            {
                Console.WriteLine($"Id: {item.Id}, Title: {item.Title} , Description: {item.Description}, Date: {item.DueDate}, Status: {item.IsCompleted}");
            }
        }

        public void EditTask(TodoItem item)
        {
            Console.WriteLine("What do you want to edit?");
            Console.WriteLine("1. Title");
            Console.WriteLine("2. Description");
            Console.WriteLine("3. Date");
            Console.WriteLine("4. Status");
            Console.WriteLine("5. All of the above");
            string? input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    Console.WriteLine($"Title: {item.Title}. Input new title: ");
                    string title = Console.ReadLine();
                    item.Title = title;
                    break;
                case "2":
                    Console.WriteLine($"Description: {item.Description}. Input new description: ");
                    string description = Console.ReadLine();
                    item.Description = description;
                    break;
                case "3":
                    Console.WriteLine($"Deadline: {item.DueDate}. Input new deadline (yyyy-mm-dd): ");
                    string inputDate = Console.ReadLine();
                    if (DateTime.TryParse(inputDate, out DateTime date))
                    {
                        Console.WriteLine($"Deadline date has been successfully changed. {date}");
                        item.DueDate = date;
                    }
                    else
                        Console.WriteLine("Invalid date format");
                    break;
                case "4":
                    item.IsCompleted = true;
                    Console.WriteLine($"Task {item.Title} status changed to completed");
                    break;
                case "5":
                    Console.WriteLine("Input new title: ");
                    string? t = Console.ReadLine();
                    if (t != null)
                        item.Title = t;

                    Console.WriteLine("Input new description: ");
                    string? d = Console.ReadLine();
                    if (d != null)
                        item.Description = d;

                    Console.WriteLine("Input new deadline (yyyy-mm-dd): ");
                    string dt = Console.ReadLine();
                    if (DateTime.TryParse(dt, out DateTime dateTime))
                    {
                        Console.WriteLine($"Deadline date has been successfully changed. {dateTime}");
                        item.DueDate = dateTime;
                    }
                    else
                        Console.WriteLine("Invalid date format");

                    item.IsCompleted = true;
                    Console.WriteLine($"Task {item.Title} status changed to completed");
                    break;
                default:
                    Console.WriteLine("Unknown command");
                    break;
            }
        }

        public TodoItem? FindById(string id) 
        {
            return tasks.FirstOrDefault(t => t.Id.ToString() == id); 
        }

        public void Save() => repo.Save(tasks);
        public void Load() => tasks = repo.Load();
    }
}

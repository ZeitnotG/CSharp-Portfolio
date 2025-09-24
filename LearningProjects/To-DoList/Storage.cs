using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace To_DoList
{
    internal class Storage
    {
       public List<TodoItem> tasks {  get; private set; }

        public Storage() 
        {
            tasks = new List<TodoItem>();
        }
        public void AddTask(TodoItem item)
        {
            tasks.Add(item);
        }

        public void RemoveTask(TodoItem item)
        {
            tasks.Remove(item);
        }

        public void ShowAllTasks()
        {
            foreach (TodoItem item in tasks)
            {
                Console.WriteLine($"Id: {item.Id}, Title: {item.Title} , Description: {item.Description}, Date: {item.DueDate}, Status: {item.IsCompleted}");
            }
        }

        public void UpdateStatus(TodoItem item)
        {
            item.IsCompleted = true;
            Console.WriteLine($"Task {item.Title} status changed to completed");
        }

        public TodoItem? FindById(string id) 
        {
            return tasks.FirstOrDefault(t => t.Id.ToString() == id); 
        }
    }
}

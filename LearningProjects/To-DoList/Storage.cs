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

        public void UpdateTitle(TodoItem item, string title) => item.Title = title;

        public void UpdateDescription(TodoItem item, string description) => item.Description = description;

        public void UpdateDueDate(TodoItem item, DateTime inputDate) => item.DueDate = inputDate;

        public void ToggleStatus(TodoItem item) => item.IsCompleted = !item.IsCompleted;

        public TodoItem? FindById(string id) 
        {
            return tasks.FirstOrDefault(t => t.Id.ToString() == id); 
        }

        public void Save() => repo.Save(tasks);
        public void Load() => tasks = repo.Load();
    }
}

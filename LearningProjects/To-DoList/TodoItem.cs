using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace To_DoList
{
    internal class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate {  get; set; }
        public bool IsCompleted {  get; set; }
        public TodoItem(int id, string title, string description)
        {
            Id = id;
            Title = title;
            Description = description;
            DateTime duedate = DateTime.Now.AddDays(7);
            IsCompleted = false;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Id: {Id}, Title: {Title} , Description: {Description}, Date: {DueDate}, Status: {IsCompleted}");
        }
    }
}

using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using To_DoList;

namespace To_DoList
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Storage storage = new Storage();
            bool isRunning = true;

            int nextId = 1;

            while (isRunning)
            {
                Console.WriteLine("\n--- To-Do List ---");
                Console.WriteLine("1. Add task");
                Console.WriteLine("2. Show all tasks");
                Console.WriteLine("3. Edit task");
                Console.WriteLine("4. Remove task");
                Console.WriteLine("5. Save Todo List");
                Console.WriteLine("6. Load Todo List");
                Console.WriteLine("0. Exit");

                string? input = Prompt("Choose option: ");

                switch (input)
                {
                    case "1":
                        string? title = Prompt("Enter title ");
                        string? description = Prompt("Enter description ");
                        TodoItem? newTask = new TodoItem(nextId, title, description);
                        storage.AddTask(newTask);
                        nextId++;
                        break;

                    case "2":
                        storage.ShowAllTasks();
                        FilterTasks(storage);
                        break;

                    case "3":
                        string? idToComplete = Prompt("Enter task Id: ");
                        var taskToComplete = storage.FindById(idToComplete);
                        if (taskToComplete != null)
                            EditTaskInteractive(taskToComplete, storage);
                        else
                            Console.WriteLine("Task not found!");
                        break;

                    case "4":
                        string? idToRemove = Prompt("Enter task Id: ");
                        var taskToRemove = storage.FindById(idToRemove);
                        if (taskToRemove != null)
                            storage.RemoveTask(taskToRemove);
                        else
                            Console.WriteLine("Task not found!");
                        break;
                    case "5":
                        storage.Save();
                        break;
                    case "6":
                        storage.Load();
                        break;
                    case "0":
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }
        private static void EditTaskInteractive(TodoItem item, Storage storage)
        {
            Console.WriteLine("1 - Title, 2 - Description, 3 - Deadline, 4 - Status , 5 - All of the above");
            Console.WriteLine("What to you want to edit? (1-5)");
            string? inputEditTask = Console.ReadLine();
            switch (inputEditTask)
            {
                case "1":
                    EditTitle(item, storage);
                    break;
                case "2":
                    EditDescription(item, storage);
                    break;
                case "3":
                    EditDueTime(item, storage);
                    break;
                case "4":
                    EditStatus(item, storage);
                    break;
                case "5":
                    Action<TodoItem, Storage>[] edits = { EditTitle, EditDescription, EditDueTime, EditStatus };
                    foreach (var edit in edits)
                        edit(item, storage);
                    break;
                default:
                    Console.WriteLine("Unknown command");
                    break;
            }
        }

        private static void EditTitle(TodoItem item, Storage storage)
        {
            string? title = Prompt($"Title: {item.Title}. Input new title: ");
            if (!string.IsNullOrWhiteSpace(title))
                storage.UpdateTitle(item, title);
        }

        private static void EditDescription(TodoItem item, Storage storage)
        {
            string? description = Prompt($"Description: {item.Description}. Input new description: ");
            if (!string.IsNullOrWhiteSpace(description))
                storage.UpdateDescription(item, description);
        }

        private static void EditDueTime(TodoItem item, Storage storage)
        {
            string? inputDate = Prompt($"Deadline: {item.DueDate}. Input new deadline (yyyy-mm-dd): ");
            if (DateTime.TryParse(inputDate, out DateTime date))
            {
                storage.UpdateDueDate(item, date);
                Console.WriteLine($"Deadline date has been successfully changed. {date}");
            }
            else
                Console.WriteLine("Invalid date format");
        }

        private static void EditStatus(TodoItem item, Storage storage)
        {
            storage.ToggleStatus(item);
            Console.WriteLine($"Task {item.Title} status changed");
        }

        private static string? Prompt(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }

        private static void FilterTasks(Storage storage)
        {
            Console.WriteLine("1. Show active tasks");
            Console.WriteLine("2. Show completed tasks");
            Console.WriteLine("3. Show tasks with a deadline for today");
            Console.WriteLine("4. Show overdue tasks");
            string? input = Prompt("Select filter:");
            IEnumerable <TodoItem> result = Enumerable.Empty<TodoItem>();
            switch (input)
            {
                case "1":
                    result = storage.GetTasks(task => !task.IsCompleted);
                    break;
                case "2":
                    result = storage.GetTasks(task => task.IsCompleted);
                    break;
                case "3":
                    var start = DateTime.Today;
                    var end = start.AddDays(1);
                    result = storage.GetTasks(task => task.DueDate >= start && task.DueDate < end);
                    break;
                case "4":
                    result = storage.GetTasks(task => task.DueDate.Date < DateTime.Today);
                    break;
                default:
                    Console.WriteLine("Invalid operation");
                    return;
            }
            PrintTasks(result);
        }
        private static void PrintTasks(IEnumerable<TodoItem> tasks)
        {
            foreach (var task in tasks)
            {
                task.ShowInfo();
            }
        }
    }
}
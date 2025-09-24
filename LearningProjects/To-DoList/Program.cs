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
                Console.Write("Choose option: ");

                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.Write("Enter title ");
                        string title = Console.ReadLine();
                        Console.Write("Enter description ");
                        string description = Console.ReadLine();
                        TodoItem newTask = new TodoItem(nextId, title, description);
                        storage.AddTask(newTask);
                        nextId++;
                        break;

                    case "2":
                        storage.ShowAllTasks();
                        break;

                    case "3":
                        Console.Write("Enter task Id: ");
                        string idToComplete = Console.ReadLine();
                        var taskToComplete = storage.FindById(idToComplete);
                        if(taskToComplete != null)
                        storage.EditTask(taskToComplete);
                        else
                            Console.WriteLine("Task not found!");
                        break;

                    case "4":
                        Console.Write("Enter task Id: ");
                        string idToRemove = Console.ReadLine();
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
    }
}

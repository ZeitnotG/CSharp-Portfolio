using System.Text.Json;

namespace To_DoList
{
    internal class TaskRepository
    {
        private const string FilePath = "tasks.json";

        public void Save(List<TodoItem> tasks)
        {
            string json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(FilePath, json);
            Console.WriteLine("✅ Tasks are stored in tasks.json");
        }

        public List<TodoItem> Load()
        {
            if (!File.Exists(FilePath))
            {
                Console.WriteLine("File not found.Returning an empty list.");
                return new List<TodoItem>();
            }
            
            string json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<TodoItem>>(json) ?? new List<TodoItem>();
        }
    }
}

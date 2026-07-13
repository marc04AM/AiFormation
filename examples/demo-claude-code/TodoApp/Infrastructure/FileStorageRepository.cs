namespace TodoApp.Infrastructure;

using System.Text.Json;
using TodoApp.Application;
using TodoApp.Domain;

public class FileStorageRepository : IStorageRepository
{
    private readonly string _filePath;

    public FileStorageRepository()
    {
        var homeDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        var todosDir = Path.Combine(homeDir, ".todos");
        _filePath = Path.Combine(todosDir, "todos.json");

        Directory.CreateDirectory(todosDir);
    }

    public async Task<IReadOnlyList<TodoItem>> LoadTodosAsync()
    {
        try
        {
            if (!File.Exists(_filePath))
                return [];

            var json = await File.ReadAllTextAsync(_filePath);
            if (string.IsNullOrWhiteSpace(json))
                return [];

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var dtos = JsonSerializer.Deserialize<List<TodoDto>>(json, options) ?? [];

            return dtos
                .Select(dto => new { dto, todo = TodoItem.Create(
                    TodoId.From(dto.Id),
                    dto.Description ?? "",
                    Priority.FromString(dto.Priority ?? "normal")) })
                .Select(x => x.dto.Status == "done" ? x.todo.MarkComplete() : x.todo)
                .ToList();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to load todos: {ex.Message}", ex);
        }
    }

    public async Task SaveTodosAsync(IReadOnlyList<TodoItem> todos)
    {
        try
        {
            var dtos = todos
                .Select(todo => new TodoDto
                {
                    Id = todo.Id.Value,
                    Description = todo.Description,
                    Status = todo.Status.ToString(),
                    Priority = todo.Priority.ToString()
                })
                .ToList();

            var json = JsonSerializer.Serialize(dtos, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_filePath, json);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to save todos: {ex.Message}", ex);
        }
    }

    private class TodoDto
    {
        public int Id { get; set; }
        public string Description { get; set; } = "";
        public string Status { get; set; } = "pending";
        public string Priority { get; set; } = "normal";
    }
}

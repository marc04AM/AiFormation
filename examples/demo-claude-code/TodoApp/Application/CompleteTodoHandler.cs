namespace TodoApp.Application;

using TodoApp.Domain;

public class CompleteTodoHandler
{
    private readonly IStorageRepository _repository;

    public CompleteTodoHandler(IStorageRepository repository)
    {
        _repository = repository;
    }

    public async Task<TodoItem> HandleAsync(TodoId id)
    {
        var todos = await _repository.LoadTodosAsync();
        var todo = todos.FirstOrDefault(t => t.Id.Value == id.Value)
            ?? throw new InvalidOperationException($"Todo with ID {id.Value} not found.");

        var completed = todo.MarkComplete();
        var updated = todos
            .Where(t => t.Id.Value != id.Value)
            .Append(completed)
            .ToList();

        await _repository.SaveTodosAsync(updated);
        return completed;
    }
}

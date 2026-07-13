namespace TodoApp.Application;

using TodoApp.Domain;

public class UpdateTodoHandler
{
    private readonly IStorageRepository _repository;

    public UpdateTodoHandler(IStorageRepository repository)
    {
        _repository = repository;
    }

    public async Task<TodoItem> HandleAsync(
        TodoId id,
        string? newDescription = null,
        Priority? newPriority = null)
    {
        var todos = await _repository.LoadTodosAsync();
        var todo = todos.FirstOrDefault(t => t.Id.Value == id.Value)
            ?? throw new InvalidOperationException($"Todo with ID {id.Value} not found.");

        var updated = todo;
        if (newDescription != null)
            updated = updated.WithDescription(newDescription);
        if (newPriority != null)
            updated = updated.WithPriority(newPriority);

        var all = todos
            .Where(t => t.Id.Value != id.Value)
            .Append(updated)
            .ToList();

        await _repository.SaveTodosAsync(all);
        return updated;
    }
}

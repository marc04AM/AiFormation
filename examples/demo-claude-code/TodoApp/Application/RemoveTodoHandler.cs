namespace TodoApp.Application;

using TodoApp.Domain;

public class RemoveTodoHandler
{
    private readonly IStorageRepository _repository;

    public RemoveTodoHandler(IStorageRepository repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(TodoId id)
    {
        var todos = await _repository.LoadTodosAsync();
        var todo = todos.FirstOrDefault(t => t.Id.Value == id.Value)
            ?? throw new InvalidOperationException($"Todo with ID {id.Value} not found.");

        var updated = todos.Where(t => t.Id.Value != id.Value).ToList();
        await _repository.SaveTodosAsync(updated);
    }
}

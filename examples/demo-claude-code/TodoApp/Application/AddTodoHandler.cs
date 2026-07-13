namespace TodoApp.Application;

using TodoApp.Domain;

public class AddTodoHandler
{
    private readonly IStorageRepository _repository;

    public AddTodoHandler(IStorageRepository repository)
    {
        _repository = repository;
    }

    public async Task<TodoItem> HandleAsync(string description, Priority priority)
    {
        var todos = await _repository.LoadTodosAsync();
        var nextId = todos.Count == 0 ? 1 : todos.Max(t => t.Id.Value) + 1;
        var newTodo = TodoItem.Create(TodoId.From(nextId), description, priority);

        var updated = todos.Append(newTodo).ToList();
        await _repository.SaveTodosAsync(updated);

        return newTodo;
    }
}

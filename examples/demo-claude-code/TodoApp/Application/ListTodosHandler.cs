namespace TodoApp.Application;

using TodoApp.Domain;

public class ListTodosHandler
{
    private readonly IStorageRepository _repository;

    public ListTodosHandler(IStorageRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<TodoItem>> HandleAsync(
        TodoStatus? statusFilter = null,
        Priority? priorityFilter = null)
    {
        var todos = await _repository.LoadTodosAsync();

        var filtered = todos.AsEnumerable();

        if (statusFilter != null)
            filtered = filtered.Where(t => t.Status.ToString() == statusFilter.ToString());

        if (priorityFilter != null)
            filtered = filtered.Where(t => t.Priority.ToString() == priorityFilter.ToString());

        return filtered
            .OrderBy(t => t.Status.IsDone)
            .ThenBy(t => t.Priority.SortOrder)
            .ToList();
    }
}

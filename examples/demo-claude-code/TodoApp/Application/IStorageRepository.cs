namespace TodoApp.Application;

using TodoApp.Domain;

public interface IStorageRepository
{
    Task<IReadOnlyList<TodoItem>> LoadTodosAsync();
    Task SaveTodosAsync(IReadOnlyList<TodoItem> todos);
}

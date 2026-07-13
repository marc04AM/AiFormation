using TodoApp.Application;
using TodoApp.Domain;
using Xunit;

namespace TodoApp.Tests;

public class MockStorageRepository : IStorageRepository
{
    private List<TodoItem> _todos = [];

    public Task<IReadOnlyList<TodoItem>> LoadTodosAsync() => Task.FromResult<IReadOnlyList<TodoItem>>(_todos);

    public Task SaveTodosAsync(IReadOnlyList<TodoItem> todos)
    {
        _todos = todos.ToList();
        return Task.CompletedTask;
    }
}

public class AddTodoHandlerTests
{
    [Fact]
    public async Task HandleAsync_AddsNewTodo()
    {
        var repository = new MockStorageRepository();
        var handler = new AddTodoHandler(repository);

        var todo = await handler.HandleAsync("Buy milk", Priority.Normal());

        Assert.NotNull(todo);
        Assert.Equal(1, todo.Id.Value);
        Assert.Equal("Buy milk", todo.Description);

        var todos = await repository.LoadTodosAsync();
        Assert.Single(todos);
    }

    [Fact]
    public async Task HandleAsync_MultipleAdds_IncrementIds()
    {
        var repository = new MockStorageRepository();
        var handler = new AddTodoHandler(repository);

        var todo1 = await handler.HandleAsync("First", Priority.High());
        var todo2 = await handler.HandleAsync("Second", Priority.Low());

        Assert.Equal(1, todo1.Id.Value);
        Assert.Equal(2, todo2.Id.Value);
    }

    [Fact]
    public async Task HandleAsync_InvalidDescription_ThrowsException()
    {
        var repository = new MockStorageRepository();
        var handler = new AddTodoHandler(repository);

        await Assert.ThrowsAsync<ArgumentException>(() => handler.HandleAsync("", Priority.Normal()));
    }
}

public class ListTodosHandlerTests
{
    [Fact]
    public async Task HandleAsync_EmptyList_ReturnsEmpty()
    {
        var repository = new MockStorageRepository();
        var handler = new ListTodosHandler(repository);

        var todos = await handler.HandleAsync();

        Assert.Empty(todos);
    }

    [Fact]
    public async Task HandleAsync_ReturnsSortedByPriority()
    {
        var repository = new MockStorageRepository();
        await repository.SaveTodosAsync(new[]
        {
            TodoItem.Create(TodoId.From(1), "Low", Priority.Low()),
            TodoItem.Create(TodoId.From(2), "High", Priority.High()),
            TodoItem.Create(TodoId.From(3), "Normal", Priority.Normal())
        });

        var handler = new ListTodosHandler(repository);
        var todos = await handler.HandleAsync();

        Assert.Equal(3, todos.Count);
        Assert.Equal("High", todos[0].Description);
        Assert.Equal("Normal", todos[1].Description);
        Assert.Equal("Low", todos[2].Description);
    }

    [Fact]
    public async Task HandleAsync_FilterByStatus()
    {
        var repository = new MockStorageRepository();
        var pending = TodoItem.Create(TodoId.From(1), "Pending", Priority.Normal());
        var done = TodoItem.Create(TodoId.From(2), "Done", Priority.Normal()).MarkComplete();
        await repository.SaveTodosAsync(new[] { pending, done });

        var handler = new ListTodosHandler(repository);
        var pendingTodos = await handler.HandleAsync(TodoStatus.Pending());

        Assert.Single(pendingTodos);
        Assert.Equal("Pending", pendingTodos[0].Description);
    }

    [Fact]
    public async Task HandleAsync_FilterByPriority()
    {
        var repository = new MockStorageRepository();
        await repository.SaveTodosAsync(new[]
        {
            TodoItem.Create(TodoId.From(1), "Low", Priority.Low()),
            TodoItem.Create(TodoId.From(2), "High", Priority.High())
        });

        var handler = new ListTodosHandler(repository);
        var highTodos = await handler.HandleAsync(priorityFilter: Priority.High());

        Assert.Single(highTodos);
        Assert.Equal("High", highTodos[0].Description);
    }

    [Fact]
    public async Task HandleAsync_SortsPendingBeforeDone()
    {
        var repository = new MockStorageRepository();
        var done = TodoItem.Create(TodoId.From(1), "Done", Priority.Normal()).MarkComplete();
        var pending = TodoItem.Create(TodoId.From(2), "Pending", Priority.Normal());
        await repository.SaveTodosAsync(new[] { done, pending });

        var handler = new ListTodosHandler(repository);
        var todos = await handler.HandleAsync();

        Assert.Equal(2, todos.Count);
        Assert.True(todos[0].Status.IsPending);
        Assert.True(todos[1].Status.IsDone);
    }
}

public class CompleteTodoHandlerTests
{
    [Fact]
    public async Task HandleAsync_MarksTodoAsDone()
    {
        var repository = new MockStorageRepository();
        var todo = TodoItem.Create(TodoId.From(1), "Task", Priority.Normal());
        await repository.SaveTodosAsync(new[] { todo });

        var handler = new CompleteTodoHandler(repository);
        var completed = await handler.HandleAsync(TodoId.From(1));

        Assert.True(completed.Status.IsDone);

        var todos = await repository.LoadTodosAsync();
        Assert.Single(todos);
        Assert.True(todos[0].Status.IsDone);
    }

    [Fact]
    public async Task HandleAsync_InvalidId_ThrowsException()
    {
        var repository = new MockStorageRepository();
        var handler = new CompleteTodoHandler(repository);

        await Assert.ThrowsAsync<InvalidOperationException>(() => handler.HandleAsync(TodoId.From(999)));
    }
}

public class RemoveTodoHandlerTests
{
    [Fact]
    public async Task HandleAsync_RemovesTodo()
    {
        var repository = new MockStorageRepository();
        var todo = TodoItem.Create(TodoId.From(1), "Task", Priority.Normal());
        await repository.SaveTodosAsync(new[] { todo });

        var handler = new RemoveTodoHandler(repository);
        await handler.HandleAsync(TodoId.From(1));

        var todos = await repository.LoadTodosAsync();
        Assert.Empty(todos);
    }

    [Fact]
    public async Task HandleAsync_InvalidId_ThrowsException()
    {
        var repository = new MockStorageRepository();
        var handler = new RemoveTodoHandler(repository);

        await Assert.ThrowsAsync<InvalidOperationException>(() => handler.HandleAsync(TodoId.From(999)));
    }

    [Fact]
    public async Task HandleAsync_RemovesOnlySpecificTodo()
    {
        var repository = new MockStorageRepository();
        var todo1 = TodoItem.Create(TodoId.From(1), "First", Priority.Normal());
        var todo2 = TodoItem.Create(TodoId.From(2), "Second", Priority.Normal());
        await repository.SaveTodosAsync(new[] { todo1, todo2 });

        var handler = new RemoveTodoHandler(repository);
        await handler.HandleAsync(TodoId.From(1));

        var todos = await repository.LoadTodosAsync();
        Assert.Single(todos);
        Assert.Equal(2, todos[0].Id.Value);
    }
}

public class UpdateTodoHandlerTests
{
    [Fact]
    public async Task HandleAsync_UpdatesDescription()
    {
        var repository = new MockStorageRepository();
        var todo = TodoItem.Create(TodoId.From(1), "Old", Priority.Normal());
        await repository.SaveTodosAsync(new[] { todo });

        var handler = new UpdateTodoHandler(repository);
        var updated = await handler.HandleAsync(TodoId.From(1), "New");

        Assert.Equal("New", updated.Description);

        var todos = await repository.LoadTodosAsync();
        Assert.Equal("New", todos[0].Description);
    }

    [Fact]
    public async Task HandleAsync_UpdatesPriority()
    {
        var repository = new MockStorageRepository();
        var todo = TodoItem.Create(TodoId.From(1), "Task", Priority.Low());
        await repository.SaveTodosAsync(new[] { todo });

        var handler = new UpdateTodoHandler(repository);
        var updated = await handler.HandleAsync(TodoId.From(1), newPriority: Priority.High());

        Assert.Equal("high", updated.Priority.ToString());
    }

    [Fact]
    public async Task HandleAsync_UpdatesBoth()
    {
        var repository = new MockStorageRepository();
        var todo = TodoItem.Create(TodoId.From(1), "Old", Priority.Low());
        await repository.SaveTodosAsync(new[] { todo });

        var handler = new UpdateTodoHandler(repository);
        var updated = await handler.HandleAsync(TodoId.From(1), "New", Priority.High());

        Assert.Equal("New", updated.Description);
        Assert.Equal("high", updated.Priority.ToString());
    }

    [Fact]
    public async Task HandleAsync_InvalidId_ThrowsException()
    {
        var repository = new MockStorageRepository();
        var handler = new UpdateTodoHandler(repository);

        await Assert.ThrowsAsync<InvalidOperationException>(() => handler.HandleAsync(TodoId.From(999)));
    }
}

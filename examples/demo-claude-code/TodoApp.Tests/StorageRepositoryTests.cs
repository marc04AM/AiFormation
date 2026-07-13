using TodoApp.Application;
using TodoApp.Domain;
using TodoApp.Infrastructure;
using Xunit;

namespace TodoApp.Tests;

public class FileStorageRepositoryTests : IDisposable
{
    private readonly string _todoDir;
    private readonly string _todoPath;

    public FileStorageRepositoryTests()
    {
        _todoDir = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".todos");
        _todoPath = Path.Combine(_todoDir, "todos.json");

        if (File.Exists(_todoPath))
            File.Delete(_todoPath);
    }

    public void Dispose()
    {
        if (File.Exists(_todoPath))
            File.Delete(_todoPath);
    }

    private FileStorageRepository CreateRepository() => new();

    [Fact]
    public async Task LoadTodosAsync_EmptyFile_ReturnsEmpty()
    {
        if (File.Exists(_todoPath))
            File.Delete(_todoPath);

        var repository = CreateRepository();
        var todos = await repository.LoadTodosAsync();

        Assert.NotNull(todos);
        Assert.Empty(todos);
    }

    [Fact]
    public async Task SaveAndLoadTodosAsync_PersistsData()
    {
        if (File.Exists(_todoPath))
            File.Delete(_todoPath);

        var repository = CreateRepository();
        var todo = TodoItem.Create(TodoId.From(1), "Test task", Priority.High());

        await repository.SaveTodosAsync(new[] { todo });
        var loaded = await repository.LoadTodosAsync();

        Assert.Single(loaded);
        Assert.Equal(1, loaded[0].Id.Value);
        Assert.Equal("Test task", loaded[0].Description);
        Assert.Equal("high", loaded[0].Priority.ToString());
    }

    [Fact]
    public async Task SaveTodosAsync_CreatesDirectoryIfMissing()
    {
        if (File.Exists(_todoPath))
            File.Delete(_todoPath);
        if (Directory.Exists(_todoDir))
            Directory.Delete(_todoDir, recursive: true);

        var repository = CreateRepository();
        var todo = TodoItem.Create(TodoId.From(1), "Task", Priority.Normal());

        await repository.SaveTodosAsync(new[] { todo });

        Assert.True(Directory.Exists(_todoDir));
    }

    [Fact]
    public async Task LoadTodosAsync_PreservesStatus()
    {
        if (File.Exists(_todoPath))
            File.Delete(_todoPath);

        var repository = CreateRepository();
        var pending = TodoItem.Create(TodoId.From(1), "Pending", Priority.Normal());
        var done = TodoItem.Create(TodoId.From(2), "Done", Priority.Normal()).MarkComplete();

        await repository.SaveTodosAsync(new[] { pending, done });
        var loaded = await repository.LoadTodosAsync();

        Assert.Equal(2, loaded.Count);
        Assert.True(loaded[0].Status.IsPending);
        Assert.True(loaded[1].Status.IsDone);
    }

    [Fact]
    public async Task LoadTodosAsync_CorruptedJson_ThrowsException()
    {
        Directory.CreateDirectory(_todoDir);
        await File.WriteAllTextAsync(_todoPath, "{ invalid json");

        var repository = CreateRepository();
        await Assert.ThrowsAsync<InvalidOperationException>(() => repository.LoadTodosAsync());

        File.Delete(_todoPath);
    }

    [Fact]
    public async Task LoadTodosAsync_MissingFields_UsesDefaults()
    {
        Directory.CreateDirectory(_todoDir);
        var json = @"[{ ""id"": 1, ""description"": ""Task"" }]";
        await File.WriteAllTextAsync(_todoPath, json);

        var repository = CreateRepository();
        var todos = await repository.LoadTodosAsync();

        Assert.Single(todos);
        Assert.Equal("normal", todos[0].Priority.ToString());
        Assert.True(todos[0].Status.IsPending);

        File.Delete(_todoPath);
    }
}

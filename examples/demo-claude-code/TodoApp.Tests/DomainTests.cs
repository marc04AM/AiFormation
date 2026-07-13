using TodoApp.Domain;
using Xunit;

namespace TodoApp.Tests;

public class TodoStatusTests
{
    [Fact]
    public void Pending_CreatesCorrectStatus()
    {
        var status = TodoStatus.Pending();
        Assert.True(status.IsPending);
        Assert.False(status.IsDone);
        Assert.Equal("pending", status.ToString());
    }

    [Fact]
    public void Done_CreatesCorrectStatus()
    {
        var status = TodoStatus.Done();
        Assert.False(status.IsPending);
        Assert.True(status.IsDone);
        Assert.Equal("done", status.ToString());
    }

    [Theory]
    [InlineData("pending")]
    [InlineData("done")]
    public void FromString_CreatesValidStatus(string value)
    {
        var status = TodoStatus.FromString(value);
        Assert.Equal(value, status.ToString());
    }

    [Fact]
    public void FromString_InvalidValue_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => TodoStatus.FromString("invalid"));
    }
}

public class PriorityTests
{
    [Fact]
    public void Low_CreatesCorrectPriority()
    {
        var priority = Priority.Low();
        Assert.Equal("low", priority.ToString());
        Assert.Equal(2, priority.SortOrder);
    }

    [Fact]
    public void Normal_CreatesCorrectPriority()
    {
        var priority = Priority.Normal();
        Assert.Equal("normal", priority.ToString());
        Assert.Equal(1, priority.SortOrder);
    }

    [Fact]
    public void High_CreatesCorrectPriority()
    {
        var priority = Priority.High();
        Assert.Equal("high", priority.ToString());
        Assert.Equal(0, priority.SortOrder);
    }

    [Theory]
    [InlineData("low")]
    [InlineData("normal")]
    [InlineData("high")]
    public void FromString_CreatesValidPriority(string value)
    {
        var priority = Priority.FromString(value);
        Assert.Equal(value, priority.ToString());
    }

    [Fact]
    public void FromString_InvalidValue_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => Priority.FromString("invalid"));
    }

    [Fact]
    public void SortOrder_HighBeforeNormalBeforeLow()
    {
        var high = Priority.High();
        var normal = Priority.Normal();
        var low = Priority.Low();

        Assert.True(high.SortOrder < normal.SortOrder);
        Assert.True(normal.SortOrder < low.SortOrder);
    }
}

public class TodoIdTests
{
    [Fact]
    public void From_CreatesTodoId()
    {
        var id = TodoId.From(42);
        Assert.Equal(42, id.Value);
    }

    [Fact]
    public void Equality_SameValue_AreEqual()
    {
        var id1 = TodoId.From(1);
        var id2 = TodoId.From(1);

        Assert.Equal(id1, id2);
    }

    [Fact]
    public void Inequality_DifferentValues_AreNotEqual()
    {
        var id1 = TodoId.From(1);
        var id2 = TodoId.From(2);

        Assert.NotEqual(id1, id2);
    }
}

public class TodoItemTests
{
    [Fact]
    public void Create_ValidInput_CreatesPendingTodo()
    {
        var id = TodoId.From(1);
        var description = "Buy groceries";
        var priority = Priority.High();

        var todo = TodoItem.Create(id, description, priority);

        Assert.Equal(id, todo.Id);
        Assert.Equal(description, todo.Description);
        Assert.Equal(priority.ToString(), todo.Priority.ToString());
        Assert.True(todo.Status.IsPending);
    }

    [Fact]
    public void Create_EmptyDescription_ThrowsException()
    {
        var id = TodoId.From(1);
        var priority = Priority.Normal();

        Assert.Throws<ArgumentException>(() => TodoItem.Create(id, "", priority));
    }

    [Fact]
    public void Create_NullDescription_ThrowsException()
    {
        var id = TodoId.From(1);
        var priority = Priority.Normal();

        Assert.Throws<ArgumentException>(() => TodoItem.Create(id, null!, priority));
    }

    [Fact]
    public void WithDescription_ReturnsNewInstance()
    {
        var original = TodoItem.Create(TodoId.From(1), "Original", Priority.Normal());
        var updated = original.WithDescription("Updated");

        Assert.Equal("Original", original.Description);
        Assert.Equal("Updated", updated.Description);
        Assert.Equal(original.Id, updated.Id);
    }

    [Fact]
    public void WithDescription_EmptyDescription_ThrowsException()
    {
        var todo = TodoItem.Create(TodoId.From(1), "Original", Priority.Normal());

        Assert.Throws<ArgumentException>(() => todo.WithDescription(""));
    }

    [Fact]
    public void WithPriority_ReturnsNewInstance()
    {
        var original = TodoItem.Create(TodoId.From(1), "Task", Priority.Low());
        var updated = original.WithPriority(Priority.High());

        Assert.Equal("low", original.Priority.ToString());
        Assert.Equal("high", updated.Priority.ToString());
        Assert.Equal(original.Id, updated.Id);
    }

    [Fact]
    public void MarkComplete_ReturnsDoneTodo()
    {
        var todo = TodoItem.Create(TodoId.From(1), "Task", Priority.Normal());
        var completed = todo.MarkComplete();

        Assert.True(todo.Status.IsPending);
        Assert.True(completed.Status.IsDone);
        Assert.Equal(todo.Id, completed.Id);
    }

    [Fact]
    public void MarkPending_ReturnsPendingTodo()
    {
        var todo = TodoItem.Create(TodoId.From(1), "Task", Priority.Normal()).MarkComplete();
        var pending = todo.MarkPending();

        Assert.True(todo.Status.IsDone);
        Assert.True(pending.Status.IsPending);
    }
}

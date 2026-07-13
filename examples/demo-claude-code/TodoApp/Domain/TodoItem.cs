namespace TodoApp.Domain;

public class TodoItem
{
    public TodoId Id { get; }
    public string Description { get; private set; }
    public TodoStatus Status { get; private set; }
    public Priority Priority { get; private set; }

    private TodoItem(TodoId id, string description, TodoStatus status, Priority priority)
    {
        Id = id;
        Description = description;
        Status = status;
        Priority = priority;
    }

    public static TodoItem Create(TodoId id, string description, Priority priority)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be empty.");

        return new TodoItem(id, description, TodoStatus.Pending(), priority);
    }

    public TodoItem WithDescription(string newDescription)
    {
        if (string.IsNullOrWhiteSpace(newDescription))
            throw new ArgumentException("Description cannot be empty.");

        return new TodoItem(Id, newDescription, Status, Priority);
    }

    public TodoItem WithPriority(Priority newPriority) =>
        new(Id, Description, Status, newPriority);

    public TodoItem MarkComplete() =>
        new(Id, Description, TodoStatus.Done(), Priority);

    public TodoItem MarkPending() =>
        new(Id, Description, TodoStatus.Pending(), Priority);
}

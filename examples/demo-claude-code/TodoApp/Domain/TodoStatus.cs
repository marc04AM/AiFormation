namespace TodoApp.Domain;

public record TodoStatus
{
    private readonly string _value;

    private TodoStatus(string value)
    {
        _value = value;
    }

    public static TodoStatus Pending() => new("pending");
    public static TodoStatus Done() => new("done");

    public static TodoStatus FromString(string value) =>
        value switch
        {
            "pending" => Pending(),
            "done" => Done(),
            _ => throw new ArgumentException($"Invalid status: {value}")
        };

    public bool IsPending => _value == "pending";
    public bool IsDone => _value == "done";

    public override string ToString() => _value;
}

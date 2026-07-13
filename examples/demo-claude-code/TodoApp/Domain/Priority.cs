namespace TodoApp.Domain;

public record Priority
{
    private readonly string _value;

    private Priority(string value)
    {
        _value = value;
    }

    public static Priority Low() => new("low");
    public static Priority Normal() => new("normal");
    public static Priority High() => new("high");

    public static Priority FromString(string value) =>
        value switch
        {
            "low" => Low(),
            "normal" => Normal(),
            "high" => High(),
            _ => throw new ArgumentException($"Invalid priority: {value}")
        };

    public int SortOrder => _value switch
    {
        "high" => 0,
        "normal" => 1,
        "low" => 2,
        _ => 3
    };

    public override string ToString() => _value;
}

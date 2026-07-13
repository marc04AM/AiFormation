namespace TodoApp.Domain;

public record TodoId(int Value)
{
    public static TodoId From(int id) => new(id);
}

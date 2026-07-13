using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using TodoApp.Application;
using TodoApp.Domain;
using TodoApp.Infrastructure;

var services = new ServiceCollection()
    .AddSingleton<IStorageRepository, FileStorageRepository>()
    .AddSingleton<AddTodoHandler>()
    .AddSingleton<ListTodosHandler>()
    .AddSingleton<CompleteTodoHandler>()
    .AddSingleton<RemoveTodoHandler>()
    .AddSingleton<UpdateTodoHandler>()
    .BuildServiceProvider();

if (args.Length == 0 || args[0] == "help" || args[0] == "--help" || args[0] == "-h")
{
    PrintHelp();
    return;
}

try
{
    var command = args[0];
    switch (command)
    {
        case "add":
            await HandleAdd(services, args);
            break;
        case "list":
            await HandleList(services, args);
            break;
        case "complete":
            await HandleComplete(services, args);
            break;
        case "remove":
            await HandleRemove(services, args);
            break;
        case "update":
            await HandleUpdate(services, args);
            break;
        default:
            AnsiConsole.MarkupLine($"[red]Error: Unknown command '{command}'[/]");
            AnsiConsole.MarkupLine("[yellow]Use 'todo help' for usage information.[/]");
            Environment.Exit(1);
            break;
    }
}
catch (Exception ex)
{
    AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
    Environment.Exit(1);
}

async Task HandleAdd(IServiceProvider provider, string[] args)
{
    if (args.Length < 2)
    {
        AnsiConsole.MarkupLine("[red]Error: Missing required argument 'description'[/]");
        Console.WriteLine("Usage: todo add \"description\" [--priority low|normal|high]");
        Environment.Exit(1);
    }

    var description = args[1];
    var priority = Priority.Normal();

    for (int i = 2; i < args.Length; i++)
    {
        if (args[i] == "--priority" && i + 1 < args.Length)
        {
            try
            {
                priority = Priority.FromString(args[i + 1]);
            }
            catch
            {
                AnsiConsole.MarkupLine($"[red]Error: Invalid priority '{args[i + 1]}'[/]");
                Environment.Exit(1);
            }
            i++;
        }
    }

    var handler = provider.GetRequiredService<AddTodoHandler>();
    var todo = await handler.HandleAsync(description, priority);

    AnsiConsole.MarkupLine($"[green]✓ Added:[/] {todo.Description} [dim]({todo.Priority})[/]");
}

async Task HandleList(IServiceProvider provider, string[] args)
{
    TodoStatus? statusFilter = null;
    Priority? priorityFilter = null;

    for (int i = 1; i < args.Length; i++)
    {
        if (args[i] == "--status" && i + 1 < args.Length)
        {
            try
            {
                statusFilter = TodoStatus.FromString(args[i + 1]);
            }
            catch
            {
                AnsiConsole.MarkupLine($"[red]Error: Invalid status '{args[i + 1]}'[/]");
                Environment.Exit(1);
            }
            i++;
        }
        else if (args[i] == "--priority" && i + 1 < args.Length)
        {
            try
            {
                priorityFilter = Priority.FromString(args[i + 1]);
            }
            catch
            {
                AnsiConsole.MarkupLine($"[red]Error: Invalid priority '{args[i + 1]}'[/]");
                Environment.Exit(1);
            }
            i++;
        }
    }

    var handler = provider.GetRequiredService<ListTodosHandler>();
    var todos = await handler.HandleAsync(statusFilter, priorityFilter);

    if (todos.Count == 0)
    {
        AnsiConsole.MarkupLine("[yellow]No todos found.[/]");
        return;
    }

    var table = new Table()
        .AddColumn("ID")
        .AddColumn("Description")
        .AddColumn("Status")
        .AddColumn("Priority");

    foreach (var todo in todos)
    {
        var statusColor = todo.Status.IsDone ? "green" : "yellow";
        var priorityColor = todo.Priority.ToString() switch
        {
            "high" => "red",
            "normal" => "yellow",
            "low" => "green",
            _ => "white"
        };

        table.AddRow(
            todo.Id.Value.ToString(),
            todo.Description,
            $"[{statusColor}]{todo.Status}[/]",
            $"[{priorityColor}]{todo.Priority}[/]");
    }

    AnsiConsole.Write(table);
}

async Task HandleComplete(IServiceProvider provider, string[] args)
{
    if (args.Length < 2)
    {
        AnsiConsole.MarkupLine("[red]Error: Missing required argument 'id'[/]");
        Console.WriteLine("Usage: todo complete <id>");
        Environment.Exit(1);
    }

    if (!int.TryParse(args[1], out var id))
    {
        AnsiConsole.MarkupLine($"[red]Error: Invalid ID '{args[1]}'[/]");
        Environment.Exit(1);
    }

    var handler = provider.GetRequiredService<CompleteTodoHandler>();
    var todo = await handler.HandleAsync(TodoId.From(id));

    AnsiConsole.MarkupLine($"[green]✓ Marked as done:[/] {todo.Description}");
}

async Task HandleRemove(IServiceProvider provider, string[] args)
{
    if (args.Length < 2)
    {
        AnsiConsole.MarkupLine("[red]Error: Missing required argument 'id'[/]");
        Console.WriteLine("Usage: todo remove <id>");
        Environment.Exit(1);
    }

    if (!int.TryParse(args[1], out var id))
    {
        AnsiConsole.MarkupLine($"[red]Error: Invalid ID '{args[1]}'[/]");
        Environment.Exit(1);
    }

    var handler = provider.GetRequiredService<RemoveTodoHandler>();
    await handler.HandleAsync(TodoId.From(id));

    AnsiConsole.MarkupLine($"[green]✓ Removed:[/] Todo #{id}");
}

async Task HandleUpdate(IServiceProvider provider, string[] args)
{
    if (args.Length < 2)
    {
        AnsiConsole.MarkupLine("[red]Error: Missing required argument 'id'[/]");
        Console.WriteLine("Usage: todo update <id> [--description \"new desc\"] [--priority level]");
        Environment.Exit(1);
    }

    if (!int.TryParse(args[1], out var id))
    {
        AnsiConsole.MarkupLine($"[red]Error: Invalid ID '{args[1]}'[/]");
        Environment.Exit(1);
    }

    string? newDescription = null;
    Priority? newPriority = null;

    for (int i = 2; i < args.Length; i++)
    {
        if (args[i] == "--description" && i + 1 < args.Length)
        {
            newDescription = args[i + 1];
            i++;
        }
        else if (args[i] == "--priority" && i + 1 < args.Length)
        {
            try
            {
                newPriority = Priority.FromString(args[i + 1]);
            }
            catch
            {
                AnsiConsole.MarkupLine($"[red]Error: Invalid priority '{args[i + 1]}'[/]");
                Environment.Exit(1);
            }
            i++;
        }
    }

    if (newDescription == null && newPriority == null)
    {
        AnsiConsole.MarkupLine("[red]Error: Must specify at least --description or --priority[/]");
        Environment.Exit(1);
    }

    var handler = provider.GetRequiredService<UpdateTodoHandler>();
    var todo = await handler.HandleAsync(TodoId.From(id), newDescription, newPriority);

    AnsiConsole.MarkupLine($"[green]✓ Updated:[/] {todo.Description} [dim]({todo.Priority})[/]");
}

void PrintHelp()
{
    AnsiConsole.MarkupLine("[bold blue]Todo CLI[/]");
    AnsiConsole.MarkupLine("[yellow]Commands:[/]");
    Console.WriteLine("  add \"description\" [--priority low|normal|high]     Add a new todo");
    Console.WriteLine("  list [--status pending|done] [--priority low|normal|high]  List todos");
    Console.WriteLine("  complete <id>                                Mark todo as done");
    Console.WriteLine("  remove <id>                                  Remove a todo");
    Console.WriteLine("  update <id> [--description \"new\"] [--priority level]  Update a todo");
    Console.WriteLine("  help                                         Show this help");
}

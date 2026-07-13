## Why

CLI-based productivity tools are essential for developers who spend most of their time in the terminal. A lightweight C# .NET 8 todo app provides a testable, scriptable alternative to GUI-based task managers while demonstrating modern .NET development patterns (async/await, dependency injection, structured logging).

## What Changes

- New CLI tool with commands for task CRUD operations
- Persistent local JSON storage for todos
- Support for task status (pending/completed), priority levels, and basic filtering
- Structured logging and error handling throughout

## Capabilities

### New Capabilities
- `task-management`: Add, remove, list, and update todo items with descriptions and priorities
- `task-persistence`: Read/write todos to local JSON file with serialization
- `task-filtering`: Filter and display todos by status (pending/done) or priority level
- `cli-interface`: Command-line argument parsing and user-friendly output formatting

### Modified Capabilities
<!-- No existing capabilities being modified -->

## Impact

- New standalone console application project in the repo
- No changes to existing projects or dependencies
- Uses Spectre.Console for output formatting and System.Text.Json for persistence

## 1. Project Setup

- [x] 1.1 Create new console application project `TodoApp.csproj` targeting .NET 8
- [x] 1.2 Add NuGet dependencies: Spectre.Console, Microsoft.Extensions.DependencyInjection
- [x] 1.3 Create folder structure: `Domain/`, `Application/`, `Infrastructure/`, `Presentation/`

## 2. Domain Model Implementation

- [x] 2.1 Create `TodoStatus` value object record with `Pending` and `Done` factory methods
- [x] 2.2 Create `Priority` value object record with `Low`, `Normal`, `High` factory methods
- [x] 2.3 Create `TodoId` value object record for type-safe todo identifiers
- [x] 2.4 Create `TodoItem` aggregate root class with private constructor and `Create()` factory method
- [x] 2.5 Implement TodoItem methods: `WithDescription()`, `WithPriority()`, `MarkComplete()`, `MarkPending()`
- [x] 2.6 Add validation for empty descriptions in TodoItem.Create()

## 3. Application Layer

- [x] 3.1 Create `IStorageRepository` interface for todo persistence
- [x] 3.2 Create `AddTodoHandler` application service accepting description and priority
- [x] 3.3 Create `ListTodosHandler` application service returning filtered todos
- [x] 3.4 Create `CompleteTodoHandler` application service accepting todo ID
- [x] 3.5 Create `RemoveTodoHandler` application service accepting todo ID
- [x] 3.6 Create `UpdateTodoHandler` application service accepting ID and new description/priority
- [x] 3.7 Add filter support to ListTodosHandler: status and priority filters
- [x] 3.8 Implement sorting: todos sorted by priority (high → normal → low) within status groups

## 4. Infrastructure - Storage

- [x] 4.1 Create `FileStorageRepository` implementing `IStorageRepository`
- [x] 4.2 Define JSON schema: `{ "id": int, "description": string, "status": "pending"|"done", "priority": "low"|"normal"|"high" }`
- [x] 4.3 Implement `LoadTodos()` method reading from `~/.todos/todos.json`
- [x] 4.4 Implement `SaveTodos()` method persisting to `~/.todos/todos.json` using System.Text.Json
- [x] 4.5 Ensure auto-creation of `~/.todos/` directory if missing
- [x] 4.6 Add error handling for file I/O failures with descriptive error messages
- [x] 4.7 Implement defensive JSON parsing to handle missing fields with defaults

## 5. CLI Interface

- [x] 5.1 Create command dispatcher in `Program.cs` to route `todo <command>` arguments
- [x] 5.2 Implement `add` command: `todo add "description" [--priority low|normal|high]`
- [x] 5.3 Implement `list` command: `todo list [--status pending|done] [--priority low|normal|high]`
- [x] 5.4 Implement `complete` command: `todo complete <id>`
- [x] 5.5 Implement `remove` command: `todo remove <id>`
- [x] 5.6 Implement `update` command: `todo update <id> [--description "new desc"] [--priority level]`
- [x] 5.7 Implement `help` and `--help` command support with usage documentation
- [x] 5.8 Format todo list output as Spectre.Console table with columns: ID, Description, Status, Priority
- [x] 5.9 Add color coding: high priority (red), normal (yellow), low (green)
- [x] 5.10 Add success feedback messages: "✓ Added: ...", "✓ Marked as done: ...", "✓ Removed: ..."
- [x] 5.11 Add error handling for unknown commands with helpful message
- [x] 5.12 Add validation for missing required arguments with usage hints

## 6. Testing

- [x] 6.1 Create xUnit test project `TodoApp.Tests`
- [x] 6.2 Write domain model tests for TodoItem creation and validation
- [x] 6.3 Write tests for Priority and TodoStatus value objects
- [x] 6.4 Write application handler tests (add, list, complete, remove, update)
- [x] 6.5 Write storage repository tests with mock file system
- [x] 6.6 Write integration tests for command parsing and execution
- [x] 6.7 Test error scenarios: invalid commands, missing arguments, corrupted JSON
- [x] 6.8 Test filtering and sorting: by status and priority

## 7. Integration & Verification

- [x] 7.1 Wire up dependency injection in `Program.cs` for all handlers and repository
- [x] 7.2 Test manual workflow: add, list, complete, remove operations end-to-end
- [x] 7.3 Verify file persistence: todos survive application restart
- [x] 7.4 Test help and error messages for usability
- [x] 7.5 Build solution without errors
- [x] 7.6 Run all tests: 100% pass rate required
- [x] 7.7 Test edge cases: empty todo list, invalid ID, corrupted storage file

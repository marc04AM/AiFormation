## Context

Building a standalone CLI todo application in C# .NET 8. This is a greenfield project with no existing codebase integration. Primary users are developers who prefer terminal-based workflows.

## Goals / Non-Goals

**Goals:**
- Deliver a fully functional CLI todo manager with CRUD operations
- Demonstrate clean architecture with layered separation (domain, application, infrastructure, presentation)
- Support persistent local storage with zero external service dependencies
- Use modern .NET 8 async patterns throughout
- Provide a foundation for testing and extension

**Non-Goals:**
- Multi-user collaboration or cloud sync
- Mobile/web interfaces
- GUI-based clients
- Authentication or authorization mechanisms

## Decisions

### 1. Architecture: Layered with DDD
**Decision**: Domain-Driven Design with four layers: Domain → Application → Infrastructure → Presentation

**Rationale**: Ensures testability, maintainability, and clear separation of concerns. Domain logic (todo state, validation) is isolated from CLI mechanics.

**Alternatives considered**:
- Monolithic script: Low maintainability, hard to test
- Microservices: Over-engineered for a CLI tool

### 2. Storage: JSON with file system
**Decision**: Persist todos as JSON in a local `~/.todos/todos.json` file using System.Text.Json

**Rationale**: Zero external dependencies, human-readable format, sufficient for single-user CLI. Easy to backup and version-control todos.

**Alternatives considered**:
- SQLite: Adds dependency, overkill for simple flat file
- Environment variables: Impractical for large todo lists

### 3. CLI Framework: Manual argument parsing with Spectre.Console
**Decision**: Use Spectre.Console for output formatting (tables, colors, progress). Parse arguments manually via `string[] args`.

**Rationale**: Spectre.Console is lightweight, gives rich output without framework bloat. Manual parsing keeps dependencies minimal.

**Alternatives considered**:
- System.CommandLine: Heavier dependency
- No formatting: Ugly output

### 4. Domain Model: Value Objects + Aggregate
**Decision**: `TodoItem` is an aggregate root. `Priority` and `TodoStatus` are value objects using `record` with private constructors and factory methods.

**Rationale**: Encapsulates domain rules (e.g., only valid priorities). Immutable value objects prevent bugs.

**Alternatives considered**:
- Anemic DTOs with static helpers: Violates OOP, no encapsulation
- Mutable classes: Harder to reason about state

### 5. Application Layer: Command Handler pattern
**Decision**: Each CLI command (add, list, complete, remove) maps to an application service handler. Handlers are injected with `IStorageRepository` (interface).

**Rationale**: Decouples business logic from CLI parsing. Easy to test handlers without CLI. Supports future extensions (web API, API tests).

**Alternatives considered**:
- Direct method calls: Mixes concerns, hard to test
- Event-driven CQRS: Over-engineered for this scope

### 6. Dependency Injection: Microsoft.Extensions.DependencyInjection
**Decision**: Use native .NET DI in `Program.cs`. Register domain services, repositories, command handlers.

**Rationale**: Built into .NET 8, no external DI framework needed. Enforces constructor injection, no service locator anti-pattern.

## Risks / Trade-offs

- **File access contention** → Mitigated by single-user design; no multi-process locking implemented. Document as limitation.
- **JSON format fragility** → Mitigated by versioning schema and defensive parsing (e.g., default values for missing fields).
- **No real-time sync** → Acceptable; todos are read/written per command, not streamed.
- **Learning curve for DDD** → Mitigated by clear separation: domain logic in `TodoItem`, application in handlers, CLI in `Program.cs`.

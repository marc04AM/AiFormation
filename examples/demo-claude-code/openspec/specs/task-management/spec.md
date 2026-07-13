## Purpose

Support core CRUD operations for todo items with validation, status management, and error handling for non-existent items.

## Requirements

### Requirement: Add a new todo item
The system SHALL allow users to add a new todo item with a description and optional priority level.

#### Scenario: Add todo with description only
- **WHEN** user runs `todo add "Buy groceries"`
- **THEN** system creates a new todo with status "pending" and default priority "normal"

#### Scenario: Add todo with priority
- **WHEN** user runs `todo add "Fix bug" --priority high`
- **THEN** system creates a new todo with specified priority level

#### Scenario: Add todo with empty description
- **WHEN** user runs `todo add ""`
- **THEN** system rejects the command with error message "Description cannot be empty"

### Requirement: List all todo items
The system SHALL display all todos in a table format with ID, description, status, and priority.

#### Scenario: List all todos
- **WHEN** user runs `todo list`
- **THEN** system displays all todos in a formatted table

#### Scenario: List todos when empty
- **WHEN** user runs `todo list` and no todos exist
- **THEN** system displays message "No todos found"

### Requirement: Complete a todo item
The system SHALL mark a todo as completed by ID.

#### Scenario: Mark todo as completed
- **WHEN** user runs `todo complete 1`
- **THEN** system updates the todo status to "done"

#### Scenario: Complete non-existent todo
- **WHEN** user runs `todo complete 999`
- **THEN** system returns error "Todo with ID 999 not found"

### Requirement: Remove a todo item
The system SHALL delete a todo by ID.

#### Scenario: Remove todo
- **WHEN** user runs `todo remove 1`
- **THEN** system deletes the todo with ID 1

#### Scenario: Remove non-existent todo
- **WHEN** user runs `todo remove 999`
- **THEN** system returns error "Todo with ID 999 not found"

### Requirement: Update a todo item
The system SHALL allow updating description or priority of an existing todo by ID.

#### Scenario: Update description
- **WHEN** user runs `todo update 1 --description "New description"`
- **THEN** system updates the todo description

#### Scenario: Update priority
- **WHEN** user runs `todo update 1 --priority high`
- **THEN** system updates the todo priority

#### Scenario: Update non-existent todo
- **WHEN** user runs `todo update 999 --description "Test"`
- **THEN** system returns error "Todo with ID 999 not found"

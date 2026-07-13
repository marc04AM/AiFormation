## Purpose

Persist todo data to local JSON storage and reload on application startup with graceful error handling for file I/O issues.

## Requirements

### Requirement: Persist todos to local file
The system SHALL save all todos to a JSON file at `~/.todos/todos.json`.

#### Scenario: Save todos on add
- **WHEN** user runs `todo add "Buy groceries"`
- **THEN** system persists the new todo to `~/.todos/todos.json`

#### Scenario: Save todos on completion
- **WHEN** user runs `todo complete 1`
- **THEN** system persists the updated todo to `~/.todos/todos.json`

#### Scenario: Save todos on removal
- **WHEN** user runs `todo remove 1`
- **THEN** system persists the deletion to `~/.todos/todos.json`

### Requirement: Load todos from file on startup
The system SHALL read existing todos from the JSON file when the application starts.

#### Scenario: Load existing todos
- **WHEN** application starts and `~/.todos/todos.json` exists
- **THEN** system loads all todos from file into memory

#### Scenario: Initialize empty todos
- **WHEN** application starts and `~/.todos/todos.json` does not exist
- **THEN** system creates the file with an empty list

### Requirement: Handle file I/O errors gracefully
The system SHALL report file I/O errors to the user without crashing.

#### Scenario: Handle missing data directory
- **WHEN** `~/.todos/` directory does not exist
- **THEN** system creates the directory automatically

#### Scenario: Handle corrupted JSON
- **WHEN** `~/.todos/todos.json` contains invalid JSON
- **THEN** system displays error "Could not read todos file" and exits

#### Scenario: Handle file access denied
- **WHEN** system cannot read or write to `~/.todos/todos.json` due to permissions
- **THEN** system displays error "Permission denied: cannot access todos file"

### Requirement: JSON storage format
The system SHALL serialize todos to JSON with the following schema: `[{ "id": 1, "description": "...", "status": "pending"|"done", "priority": "low"|"normal"|"high" }]`

#### Scenario: Valid JSON structure
- **WHEN** system saves todos
- **THEN** JSON contains required fields: id, description, status, priority

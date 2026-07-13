## ADDED Requirements

### Requirement: Filter todos by status
The system SHALL allow filtering todos to show only pending or completed items.

#### Scenario: Show pending todos
- **WHEN** user runs `todo list --status pending`
- **THEN** system displays only todos with status "pending"

#### Scenario: Show completed todos
- **WHEN** user runs `todo list --status done`
- **THEN** system displays only todos with status "done"

#### Scenario: List with invalid status filter
- **WHEN** user runs `todo list --status invalid`
- **THEN** system returns error "Invalid status: valid values are 'pending' or 'done'"

### Requirement: Filter todos by priority
The system SHALL allow filtering todos to show only items of a specific priority level.

#### Scenario: Show high priority todos
- **WHEN** user runs `todo list --priority high`
- **THEN** system displays only todos with priority "high"

#### Scenario: Show normal priority todos
- **WHEN** user runs `todo list --priority normal`
- **THEN** system displays only todos with priority "normal"

#### Scenario: Show low priority todos
- **WHEN** user runs `todo list --priority low`
- **THEN** system displays only todos with priority "low"

#### Scenario: List with invalid priority filter
- **WHEN** user runs `todo list --priority invalid`
- **THEN** system returns error "Invalid priority: valid values are 'low', 'normal', or 'high'"

### Requirement: Combine status and priority filters
The system SHALL allow filtering by both status and priority simultaneously.

#### Scenario: Filter by status and priority
- **WHEN** user runs `todo list --status pending --priority high`
- **THEN** system displays only pending todos with high priority

### Requirement: Sort todos by priority
The system SHALL display todos sorted by priority (high → normal → low) within each status group.

#### Scenario: Sort by priority
- **WHEN** user runs `todo list`
- **THEN** system displays pending todos sorted by priority (high first), followed by completed todos sorted by priority

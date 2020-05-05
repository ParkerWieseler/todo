USE [ToDo]

GO

CREATE TABLE [SubItems](
    [ID] UNIQUEIDENTIFIER PRIMARY KEY,
    [ListItemID] UNIQUEIDENTIFIER,
    [Name] VARCHAR(50),
    [Completed] BIT NOT NULL DEFAULT(0)

    FOREIGN KEY (ListItemID) REFERENCES TodoListItems (ID) ON DELETE CASCADE
)

GO
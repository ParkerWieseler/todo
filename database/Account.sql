USE [ToDo]

GO

Create Table [Accounts](
    [ID] UNIQUEIDENTIFIER PRIMARY KEY,
    [FullName] VARCHAR(50),
    [PictureUrl] VARCHAR(255),
	[Email] VARCHAR(50) NOT NULL,
    [PlanID] INT NOT NULL,
    FOREIGN KEY ([PlanID]) REFERENCES Plans([ID])
)

GO
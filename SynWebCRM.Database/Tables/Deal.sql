CREATE TABLE [dbo].[Deal]
(
	[DealId] INT NOT NULL PRIMARY KEY IDENTITY,
	[CreationDate] DATETIME NOT NULL DEFAULT GETDATE(),
	[Sum] DECIMAL(18,2) NULL,
	[Profit] DECIMAL(18,2) NULL,
	[CustomerId] INT NOT NULL,
	[Name] NVARCHAR(200) NULL,
	[Description] NVARCHAR(MAX) NULL, 
    [Type] int NOT NULL, 
	[DealStateId] INT NOT NULL DEFAULT 1,
	[NeedsAttention] bit NOT NULL DEFAULT 0,
	[Creator] NVARCHAR(256) NULL,
	[ServiceTypeId] INT NULL,
    CONSTRAINT [FK_Deal_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [Customer]([CustomerId]) ON DELETE CASCADE, 
    CONSTRAINT [FK_Deal_State] FOREIGN KEY ([DealStateId]) REFERENCES [DealState]([DealStateId]), 
    CONSTRAINT [FK_Deal_ServiceType] FOREIGN KEY ([ServiceTypeId]) REFERENCES [ServiceType]([ServiceTypeId]),
)

GO

CREATE INDEX [IX_Deal_Date] ON [dbo].[Deal] ([CreationDate] DESC)

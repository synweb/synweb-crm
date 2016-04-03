CREATE TABLE [dbo].[Customer]
(
	[CustomerId] INT NOT NULL PRIMARY KEY IDENTITY,
	[CreationDate] DATETIME NOT NULL DEFAULT GETDATE(),
	[Name] NVARCHAR(300) NOT NULL,
	[Source] int NOT NULL,
	[Description] NVARCHAR(MAX) NULL,
	[Phone] NVARCHAR(100) NULL,
	[Email] VARCHAR(100) NULL,
	[VkId] VARCHAR(50) NULL,
	[Creator] NVARCHAR(256) NULL,
)

GO

CREATE INDEX [IX_Customer_Date] ON [dbo].[Customer] ([CreationDate] DESC)

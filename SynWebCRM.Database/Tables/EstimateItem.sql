CREATE TABLE [dbo].[EstimateItem]
(
	[ItemId] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[CreationDate] DATETIME NOT NULL DEFAULT GETDATE(),
	[EstimateId] INT NOT NULL,
	[Name] NVARCHAR(200) NOT NULL,
	[Description] NVARCHAR(max) NULL,
	[Price] DECIMAL(18,2) NOT NULL,
	[DevelopmentHours] FLOAT NULL,
	[PerMonth] BIT NOT NULL DEFAULT 0,
	[IsOptional] BIT NOT NULL DEFAULT 0,
	[SortOrder] INT NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_EstimateItem_Estimate] FOREIGN KEY ([EstimateId]) REFERENCES [Estimate]([EstimateId])
)

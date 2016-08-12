CREATE TABLE [dbo].[Website]
(
	[WebsiteId] INT NOT NULL PRIMARY KEY IDENTITY,
	[CreationDate] DATETIME NOT NULL DEFAULT GETDATE(),
	[OwnerId] INT NOT NULL,
	[Domain] NVARCHAR(200) NOT NULL,
	[HostingEndingDate] DATETIME NULL,
	[DomainEndingDate] DATETIME NULL,
	[HostingPrice] DECIMAL(18,2) NULL, 
	[IsActive] BIT NOT NULL DEFAULT 1,
	[Creator] NVARCHAR(256) NULL,
    CONSTRAINT [FK_Website_Customer] FOREIGN KEY ([OwnerId]) REFERENCES [Customer]([CustomerId])
)

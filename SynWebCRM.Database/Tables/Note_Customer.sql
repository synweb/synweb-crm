CREATE TABLE [dbo].[Note_Customer]
(
	[CustomerId] INT NOT NULL,
	[NoteId] INT NOT NULL, 
    CONSTRAINT [PK_Customer_Note] PRIMARY KEY ([CustomerId], [NoteId]), 
    CONSTRAINT [FK_Customer_Note_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [Customer]([CustomerId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Customer_Note_Note] FOREIGN KEY ([NoteId]) REFERENCES [Note]([NoteId]) ON DELETE CASCADE,

)

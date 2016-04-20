CREATE TABLE [dbo].[Note_Deal]
(
	[DealId] INT NOT NULL,
	[NoteId] INT NOT NULL, 
    CONSTRAINT [PK_Deal_Note] PRIMARY KEY ([DealId], [NoteId]), 
    CONSTRAINT [FK_Deal_Note_Customer] FOREIGN KEY ([DealId]) REFERENCES [Deal]([DealId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Deal_Note_Note] FOREIGN KEY ([NoteId]) REFERENCES [Note]([NoteId]) ON DELETE CASCADE,
)

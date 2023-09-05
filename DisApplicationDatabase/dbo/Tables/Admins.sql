CREATE TABLE [dbo].[Admins] (
    [PIN]             NCHAR (11) NOT NULL,
    [AdminTypeCode]   NCHAR (3)  NOT NULL,
    [FirstName]       TEXT       NOT NULL,
    [LastName]        TEXT       NOT NULL,
    [EmailAddress]    TEXT       NOT NULL,
    [Password]        TEXT       NOT NULL,
    [AssessmentScore] INT        NOT NULL,
    CONSTRAINT [PK_Admins] PRIMARY KEY CLUSTERED ([PIN] ASC),
    CONSTRAINT [FK_Admins_AdminType] FOREIGN KEY ([AdminTypeCode]) REFERENCES [dbo].[AdminType] ([ID])
);


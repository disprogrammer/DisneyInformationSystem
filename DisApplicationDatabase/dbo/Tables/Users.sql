CREATE TABLE [dbo].[Users] (
    [PIN]          NCHAR (11) NOT NULL,
    [FirstName]    TEXT       NOT NULL,
    [LastName]     TEXT       NOT NULL,
    [PhoneNumber]  TEXT       NOT NULL,
    [EmailAddress] TEXT       NOT NULL,
    [Password]     TEXT       NOT NULL,
    [HomeAddress]  TEXT       NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([PIN] ASC)
);


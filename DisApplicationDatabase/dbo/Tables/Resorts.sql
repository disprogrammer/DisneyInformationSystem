CREATE TABLE [dbo].[Resorts] (
    [PIN]                         NCHAR (3) NOT NULL,
    [ResortName]                  TEXT      NOT NULL,
    [AddressOfResort]             TEXT      NOT NULL,
    [Phone]                       TEXT      NOT NULL,
    [NumberOfThemeParks]          INT       NOT NULL,
    [NumberOfResortHotels]        INT       NOT NULL,
    [NumberOfPartnerHotels]       INT       NOT NULL,
    [NumberOfWaterParks]          INT       NOT NULL,
    [NumberOfEntertainmentVenues] INT       NOT NULL,
    [Operating]                   BIT       NOT NULL,
    [OpeningDate]                 DATE      NOT NULL,
    [ClosingDate]                 DATE      NOT NULL,
    CONSTRAINT [PK_Resorts] PRIMARY KEY CLUSTERED ([PIN] ASC)
);


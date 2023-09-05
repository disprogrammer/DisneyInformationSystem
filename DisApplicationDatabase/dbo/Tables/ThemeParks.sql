CREATE TABLE [dbo].[ThemeParks] (
    [PIN]                 NCHAR (3) NOT NULL,
    [ResortID]            NCHAR (3) NOT NULL,
    [ParkName]            TEXT      NOT NULL,
    [AddressOfPark]       TEXT      NOT NULL,
    [Phone]               TEXT      NOT NULL,
    [Transportation]      TEXT      NOT NULL,
    [NumberOfLands]       INT       NOT NULL,
    [NumberOfAttractions] INT       NOT NULL,
    [NumberOfShops]       INT       NOT NULL,
    [NumberOfRestaurants] INT       NOT NULL,
    [NumberOfTours]       INT       NOT NULL,
    [NumberOfRestrooms]   INT       NOT NULL,
    [Operating]           BIT       NOT NULL,
    [OpeningDate]         DATE      NOT NULL,
    [ClosingDate]         DATE      NOT NULL,
    CONSTRAINT [PK_ThemeParks] PRIMARY KEY CLUSTERED ([PIN] ASC),
    CONSTRAINT [FK_ThemeParks_Resorts] FOREIGN KEY ([ResortID]) REFERENCES [dbo].[Resorts] ([PIN])
);


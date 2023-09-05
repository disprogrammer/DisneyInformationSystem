﻿CREATE TABLE [dbo].[ResortHotels] (
    [PIN]                 NVARCHAR (3)  NOT NULL,
    [ResortID]            NCHAR (3)     NOT NULL,
    [ResortHotelName]     VARCHAR (MAX) NOT NULL,
    [ResortType]          VARCHAR (50)  NULL,
    [Area]                VARCHAR (MAX) NULL,
    [Theme]               VARCHAR (MAX) NOT NULL,
    [Description]         TEXT          NOT NULL,
    [Address]             VARCHAR (MAX) NOT NULL,
    [Phone]               VARCHAR (12)  NOT NULL,
    [NumberOfRooms]       INT           NOT NULL,
    [CheckInTime]         TIME (7)      NOT NULL,
    [CheckOutTime]        TIME (7)      NOT NULL,
    [RoomTypes]           XML           NOT NULL,
    [Transportation]      VARCHAR (MAX) NOT NULL,
    [NumberOfBusStops]    INT           NOT NULL,
    [ParkingCost]         MONEY         NOT NULL,
    [ValetCost]           MONEY         NOT NULL,
    [HasInRoomDining]     BIT           NOT NULL,
    [HasBeach]            BIT           NOT NULL,
    [HasPetService]       BIT           NOT NULL,
    [HasFishing]          BIT           NOT NULL,
    [HasCampfire]         BIT           NOT NULL,
    [HasShoppingDelivery] BIT           NOT NULL,
    [HasChildCare]        BIT           NOT NULL,
    [IsConventionResort]  BIT           NOT NULL,
    [OpeningDate]         DATETIME      NOT NULL,
    [ClosingDate]         DATETIME      NOT NULL,
    [Operating]           BIT           NOT NULL,
    CONSTRAINT [PK_ResortHotels] PRIMARY KEY CLUSTERED ([PIN] ASC),
    CONSTRAINT [FK_ResortHotels_Resorts] FOREIGN KEY ([ResortID]) REFERENCES [dbo].[Resorts] ([PIN])
);


-- =============================================
-- Author:		James McKinney
-- Create date: 2023-01-15
-- Description:	Inserts a new resort hotel into the table.
-- =============================================
CREATE PROCEDURE InsertNewResortHotel
	(@PIN NVARCHAR(3),
	@ResortID NCHAR(3),
	@ResortHotelName VARCHAR(MAX),
	@ResortType VARCHAR(50),
	@Area VARCHAR(MAX),
	@Theme VARCHAR(MAX),
	@Description TEXT,
	@Address VARCHAR(MAX),
	@Phone VARCHAR(12),
	@NumberOfRooms INT,
	@CheckInTime TIME(7),
	@CheckOutTime TIME(7),
	@RoomTypes XML,
	@Transportation VARCHAR(MAX),
	@NumberOfBusStops INT,
	@ParkingCost MONEY,
	@ValetCost MONEY,
	@HasInRoomDining BIT,
	@HasBeach BIT,
	@HasPetService BIT,
	@HasFishing BIT,
	@HasCampfire BIT,
	@HasShoppingDelivery BIT,
	@HasChildCare BIT,
	@IsConventionResort BIT,
	@OpeningDate DATETIME,
	@ClosingDate DATETIME,
	@Operating BIT)
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO ResortHotels VALUES
	(@PIN,
	@ResortID,
	@ResortHotelName,
	@ResortType,
	@Area,
	@Theme,
	@Description,
	@Address,
	@Phone,
	@NumberOfRooms,
	@CheckInTime,
	@CheckOutTime,
	@RoomTypes,
	@Transportation,
	@NumberOfBusStops,
	@ParkingCost,
	@ValetCost,
	@HasInRoomDining,
	@HasBeach,
	@HasPetService,
	@HasFishing,
	@HasCampfire,
	@HasShoppingDelivery,
	@HasChildCare,
	@IsConventionResort,
	@OpeningDate,
	@ClosingDate,
	@Operating)
END

-- =============================================
-- Author:		James McKinney
-- Create date: 2023-01-15
-- Description:	Updates a particular resort hotel.
-- =============================================
CREATE PROCEDURE UpdateResortHotel
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
	UPDATE ResortHotels
	SET
	ResortID = @ResortID,
	ResortHotelName = @ResortHotelName,
	ResortType = @ResortType,
	Area = @Area,
	Theme = @Theme,
	Description = @Description,
	Address = @Address,
	Phone = @Phone,
	NumberOfRooms = @NumberOfRooms,
	CheckInTime = @CheckInTime,
	CheckOutTime = @CheckOutTime,
	RoomTypes = @RoomTypes,
	Transportation = @Transportation,
	NumberOfBusStops = @NumberOfBusStops,
	ParkingCost = @ParkingCost,
	ValetCost = @ValetCost,
	HasInRoomDining = @HasInRoomDining,
	HasBeach = @HasBeach,
	HasPetService = @HasPetService,
	HasFishing = @HasFishing,
	HasCampfire = @HasCampfire,
	HasShoppingDelivery = @HasShoppingDelivery,
	HasChildCare = @HasChildCare,
	IsConventionResort = @IsConventionResort,
	OpeningDate = @OpeningDate,
	ClosingDate = @ClosingDate,
	Operating = @Operating
	WHERE
	PIN = @PIN
END

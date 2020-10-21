CREATE PROCEDURE SP_InsertOrder
	@CustomerID INT = NULL,
	@AdminID INT = NULL,
	@ShippingType VARCHAR(15) = NULL,
	@ShippingStatus VARCHAR(15) = NULL,
	@ShippingAddress VARCHAR(50) = NULL,
	@SaleDate DATE = NULL,
	@UpdatedBy VARCHAR(20) = NULL,
	@ShippingPrice MONEY = NULL,
	@SubTotal MONEY = NULL,
	@PST MONEY = NULL,
	@HST MONEY = NULL,
	@GST MONEY = NULL,
	@Total MONEY = NULL
AS
DECLARE @ReturnCode INT
SET @ReturnCode = 0

IF @CustomerID IS NULL
	RAISERROR('SP_InsertOrder: Customer ID cannot be null', 16, 1)
	IF @ShippingType IS NULL
		RAISERROR('SP_InsertOrder: Shipping Type cannot be null', 16, 1)
		IF @ShippingAddress IS NULL
			RAISERROR('SP_InsertOrder: Shipping Address cannot be null', 16, 1)
			IF @SaleDate IS NULL
				RAISERROR('SP_InsertOrder: Sale Date cannot be null', 16, 1)
				IF @SubTotal IS NULL
					RAISERROR('SP_InsertOrder: Sub Total cannot be null', 16, 1)
					IF @Total IS NULL 
						RAISERROR('SP_InsertOrder: Total cannot be nul', 16, 1)
ELSE
	BEGIN
		BEGIN TRANSACTION
			INSERT [Order] (CustomerID, AdminID, ShippingType, ShippingStatus, ShippingAddress, SaleDate, UpdatedBy, ShippingPrice, SubTotal, PST, HST, GST, Total)
			Values (@CustomerID, @AdminID, @ShippingType, @ShippingStatus, @ShippingAddress, @SaleDate, @UpdatedBy, @ShippingPrice, @SubTotal, @PST, @HST, @GST, @Total)
			IF @@ERROR = 0
				BEGIN
					SET @ReturnCode = 0
					COMMIT TRANSACTION
				END
			ELSE
				BEGIN
					SET @ReturnCode = 1
					RAISERROR('SP_InsertOrder: Failed to insert the new order', 16, 1)
					ROLLBACK TRANSACTION
				END
	END
RETURN @ReturnCode

GO
CREATE PROCEDURE SP_UpdateOrder
	@OrderID INT = NULL,
	@CustomerID INT = NULL,
	@AdminID INT = NULL,
	@ShippingType VARCHAR(15) = NULL,
	@ShippingStatus VARCHAR(15) = NULL,
	@ShippingAddress VARCHAR(50) = NULL,
	@SaleDate DATE = NULL,
	@UpdatedBy VARCHAR(20) = NULL,
	@ShippingPrice MONEY = NULL,
	@SubTotal MONEY = NULL,
	@PST MONEY = NULL,
	@HST MONEY = NULL,
	@GST MONEY = NULL,
	@Total MONEY = NULL
AS
DECLARE @ReturnCode INT
SET @ReturnCode = 0

IF @OrderID IS NULL
	RAISERROR('SP_UpdateOrder: Order ID cannot be null', 16, 1)
	IF @CustomerID IS NULL
		RAISERROR('SP_UpdateOrder: Customer ID cannot be null', 16, 1)
		IF @ShippingType IS NULL
			RAISERROR('SP_UpdateOrder: Shipping Type cannot be null', 16, 1)
			IF @ShippingAddress IS NULL
				RAISERROR('SP_UpdateOrder: Shipping Address cannot be null', 16, 1)
				IF @SaleDate IS NULL
					RAISERROR('SP_UpdateOrder: Sale Date cannot be null', 16, 1)
					IF @SubTotal IS NULL
						RAISERROR('SP_UpdateOrder: Sub Total cannot be null', 16, 1)
						IF @Total IS NULL 
							RAISERROR('SP_UpdateOrder: Total cannot be nul', 16, 1)
ELSE
	BEGIN
		BEGIN TRANSACTION
			UPDATE [Order]
			SET
				CustomerID = @CustomerID,
				AdminID = @AdminID,
				ShippingType = @ShippingType,
				ShippingStatus = @ShippingStatus,
				ShippingAddress = @ShippingAddress,
				SaleDate = @SaleDate,
				UpdatedBy = @UpdatedBy,
				ShippingPrice = @ShippingPrice,
				PST = @PST,
				HST = @HST,
				GST = @GST,
				SubTotal = @SubTotal,
				Total = @Total
			WHERE OrderID = @OrderID
			IF @@ERROR = 0
				BEGIN
					SET @ReturnCode = 0
					COMMIT TRANSACTION
				END
			ELSE
				BEGIN
					SET @ReturnCode = 1
					RAISERROR('SP_UpdateOrder: Failed to update the order', 16, 1)
					ROLLBACK TRANSACTION
				END
	END
RETURN @ReturnCode

--Orders cannot be deleted. They will get a status called cancelled
GO
CREATE PROCEDURE SP_DeleteOrder 
	@OrderID INT = NULL,
	@CustomerID INT = NULL,
	@AdminID INT = NULL,
	@ShippingStatus VARCHAR(15) = NULL,
	@UpdatedBy VARCHAR(20) = NULL
AS

DECLARE @ReturnCode INT
SET @ReturnCode = 0

IF @OrderID IS NULL
	RAISERROR('SP_DeleteOrder: Order ID cannot be null', 16, 1)
	IF @CustomerID IS NULL
		RAISERROR('SP_DeleteOrder: Customer ID cannot be null', 16, 1)
		IF @ShippingStatus IS NULL
			RAISERROR('SP_DeleteOrder: Shipping Status cannot be null', 16, 1)
ELSE
	BEGIN
		BEGIN TRANSACTION
			Update [Order]
			SET
				ShippingStatus = @ShippingStatus,
				UpdatedBy = @UpdatedBy,
				AdminID = @AdminID
			WHERE OrderID = @OrderID
			IF @@ERROR = 0
				BEGIN
					SET @ReturnCode = 0
					COMMIT TRANSACTION
				END
			ELSE
				BEGIN
					SET @ReturnCode = 1
					RAISERROR('SP_DeleteOrder: Failed to delete the order of the provided ID', 16, 1)
					ROLLBACK TRANSACTION
				END
	END
RETURN @ReturnCode

GO
CREATE PROCEDURE SP_GetAllOrdersByCustomer
	@CustomerID INT = NULL
AS

DECLARE @ReturnCode INT
SET @ReturnCode = 0

IF @CustomerID IS NULL
	RAISERROR('SP_GetAllOrdersByCustomer: Customer ID cannot be null', 16, 1)
ELSE
	BEGIN
		SELECT
		[Order].OrderID, CustomerID, ShippingType, ShippingStatus, ShippingAddress, SaleDate, ShippingPrice, SubTotal, PST, HST, GST, Total,
		OrderQuantity, OrderItemPrice,
		CardName, CardSetNumber, CardRarity,
		SealedProductName
		FROM [Order]
		INNER JOIN OrderItems
		ON [Order].OrderID = OrderItems.OrderID
		INNER JOIN [Card]
		ON OrderItems.CardID = [Card].CardID
		INNER JOIN SealedProduct
		ON OrderItems.SealedProductID = SealedProduct.SealedProductID
		WHERE CustomerID = @CustomerID
		IF @@ERROR = 0
			SET @ReturnCode = 0
		ELSE
			BEGIN
				SET @ReturnCode = 1
				RAISERROR('SP_GetAllOrdersByCustomer: Failed to find any results for the provided customer', 16, 1)
			END
	END
RETURN @ReturnCode

GO
CREATE PROCEDURE SP_GetAllOrders
AS

DECLARE @ReturnCode INT
SET @ReturnCode = 0

BEGIN
	SELECT
		[Order].OrderID, CustomerID, ShippingType, ShippingStatus, ShippingAddress, SaleDate, ShippingPrice, SubTotal, PST, HST, GST, Total,
		OrderQuantity, OrderItemPrice,
		CardName, CardSetNumber, CardRarity,
		SealedProductName
		FROM [Order]
		INNER JOIN OrderItems
		ON [Order].OrderID = OrderItems.OrderID
		INNER JOIN [Card]
		ON OrderItems.CardID = [Card].CardID
		INNER JOIN SealedProduct
		ON OrderItems.SealedProductID = SealedProduct.SealedProductID
		IF @@ERROR = 0
			SET @ReturnCode = 0
		ELSE
			BEGIN
				SET @ReturnCode = 1
				RAISERROR('SP_GetAllOrders: Failed to find any results', 16, 1)
			END

END
RETURN @ReturnCode
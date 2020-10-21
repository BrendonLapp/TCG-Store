CREATE PROCEDURE SP_InsertIntoOrderItems
	@OrderID INT = NULL,
	@SealedProductID INT = NULL,
	@CardID INT = NULL,
	@OrderQuantity INT = NULL,
	@OrderItemPrice INT = NULL
AS

DECLARE @ReturnCode INT
SET @ReturnCode = 0

IF @OrderID IS NULL
	RAISERROR('SP_InsertIntoOrderItems: Order ID cannot be null', 16, 1)
	IF @OrderQuantity IS NULL
		RAISERROR('SP_InsertIntoOrderItems: Order Quantity cannot be null', 16, 1)
		IF @OrderItemPrice IS NULL
			RAISERROR('SP_InsertIntoOrderITems: Order Item Price cannot be null', 16, 1)
ELSE
	BEGIN
		BEGIN TRANSACTION
			INSERT INTO OrderItems
			(OrderID, SealedProductID, CardID, OrderQuantity, OrderItemPrice)
			VALUES
			(@OrderID, @SealedProductID, @CardID, @OrderQuantity, @OrderItemPrice)
			IF @@ERROR = 0
				BEGIN
					SET @ReturnCode = 0
					COMMIT TRANSACTION
				END
			ELSE
				BEGIN
					SET @ReturnCode = 1
					RAISERROR('SP_InsertIntoOrderItems: Failed to insert the items for the order', 16, 1)
					ROLLBACK TRANSACTION
				END
	END
RETURN @ReturnCode
	
GO
CREATE PROCEDURE SP_UpdateOrderItems
	@OrderItemID INT = NULL,
	@OrderID INT = NULL,
	@SealedProductID INT = NULL,
	@CardID INT = NULL,
	@OrderQuantity INT = NULL,
	@OrderItemPrice INT = NULL
AS

DECLARE @ReturnCode INT
SET @ReturnCode = 0

IF @OrderItemID IS NULL
	RAISERROR('SP_UpdateOrderItems: Order Items ID cannot be null', 16, 1)
	IF @OrderID IS NULL
		RAISERROR('SP_UpdateOrderItems: Order ID cannot be null', 16, 1)
		IF @OrderQuantity IS NULL
			RAISERROR('SP_UpdateOrderItems: Order Quantity cannot be null', 16, 1)
			IF @OrderItemPrice IS NULL
				RAISERROR('SP_UpdateOrderItems: Order Item Price cannot be null', 16, 1)
ELSE
	BEGIN
		BEGIN TRANSACTION
		UPDATE OrderItems
		SET
			CardID = @CardID,
			SealedProductID = @SealedProductID,
			OrderQuantity = @OrderQuantity,
			OrderItemPrice = @OrderItemPrice
		WHERE
			OrderItemID = @OrderItemID AND
			OrderID = @OrderItemID
	END

RETURN @ReturnCode

GO
CREATE PROCEDURE SP_DeleteOrderItems
	@OrderItemID INT = NULL,
	@OrderID INT = NULL
AS

DECLARE @ReturnCode INT
SET @ReturnCode = 0

IF @OrderItemID IS NULL
	RAISERROR('SP_DeleteOrderItems: Order Item ID cannot be null', 16, 1)
	IF @OrderID IS NULL
		RAISERROR('SP_DeleteOrderItems: Order ID cannot be null', 16, 1)
ELSE
	BEGIN
		BEGIN TRANSACTION
		DELETE FROM OrderItems
		WHERE OrderItemID = @OrderItemID
		AND OrderID = @OrderID
		IF @@ERROR = 0
			BEGIN
				SET @ReturnCode = 0
				COMMIT TRANSACTION
			END
		ELSE
			BEGIN
				SET @ReturnCode = 1
				RAISERROR('SP_DeleteOrderItems: Failed to delete the provided Order Item', 16, 1)
				ROLLBACK TRANSACTION
			END
	END
RETURN @ReturnCode

GO
CREATE PROCEDURE SP_GetOrderItemsByOrder
	@OrderID INT = NULL
AS

DECLARE @ReturnCode INT
SET @ReturnCode = 0

IF @OrderID IS NULL
	RAISERROR('SP_GetOrderItemsByOrder: Order ID cannot be null', 16, 1)
ELSE
	BEGIN
		SELECT OrderItemID, OrderID, SealedProductID, CardID, OrderQuantity, OrderItemPrice
		FROM OrderItems
		WHERE OrderID = @OrderID
		IF @@ERROR = 0
			SET @ReturnCode = 0
		ELSE
			BEGIN
				SET @ReturnCode = 1
				RAISERROR('SP_GetOrderItemsByOrder: Failed to retrieve any results for the provided order', 16 ,1)
			END
	END
RETURN @ReturnCode

GO
CREATE PROCEDURE SP_GetOrderItemDetails
	@OrderID INT = NULL,
	@OrderItemID INT = NULL
AS

DECLARE @ReturnCode INT
SET @ReturnCode = 0

IF @OrderID IS NULL
	RAISERROR('SP_GetOrderItemDetails: Order ID cannot be null', 16, 1)
	IF @OrderItemID IS NULL
		RAISERROR('SP_GetOrderItemDetails: Order Item ID cannot be null', 16, 1)
ELSE
	BEGIN
		SELECT OrderItemID, OrderID, SealedProductID, CardID, OrderQuantity, OrderItemPrice
		FROM OrderItems
		WHERE OrderID = @OrderID
		AND OrderItemID = @OrderItemID
		IF @@ERROR = 0
			SET @ReturnCode = 0
		ELSE
			BEGIN
				SET @ReturnCode = 1
				RAISERROR('SP_GetOrderItemDetails: Failed to retrieve any results for the provided order item', 16, 1)
			END
	END
RETURN @ReturnCode
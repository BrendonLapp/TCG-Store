CREATE PROCEDURE GetAllInventoryItems
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1
	BEGIN
		SELECT
			InventoryID,
			CardID,
			SealedProductID,
			QualityID,
			Quantity,
			FirstEdition
		FROM Inventory

		IF @@ERROR = 0
			SET @ReturnCode = 0
		ELSE
			RAISERROR('GetAllInventoryItems: Select failed on Inventory', 16, 1)
	END
RETURN @ReturnCode

CREATE PROCEDURE GetInventoryItemByID
(
	@InventoryID INT NULL
)
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1

	IF @InventoryID IS NULL
		RAISERROR('GetInventoryItemByID: Inventory ID cannot be null', 16, 1)
	ELSE
		BEGIN
			SELECT
				CardID,
				SealedProductID,
				QualityID,
				Quantity,
				FirstEdition
			FROM Inventory
			WHERE InventoryID = @InventoryID

			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('GetInventoryItemByID: Select failed on Inventory', 16. 1)
		END
RETURN @ReturnCode

CREATE PROCEDURE UpdateInventory
(
	@InventoryID INT NULL,
	@Quantity INT NULL
)
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1

	IF @InventoryID IS NULL
		RAISERROR('UpdateInventory: InventoryID cannot be null', 16, 1)
		IF @Quantity IS NULL
			RAISERROR('UpdateInventory: Quantity cannot be null', 16, 1)
		ELSE
			BEGIN
				BEGIN TRANSACTION
					UPDATE Inventory
					SET 
						Quantity = @Quantity
					WHERE InventoryID = @InventoryID
					
					IF @@ERROR = 0
						BEGIN
							SET @ReturnCode = 0
							COMMIT TRANSACTION
						END
					ELSE
						BEGIN
							RAISERROR('UpdateInventory: Update failed', 16, 1)
							ROLLBACK TRANSACTION
						END
			END
RETURN @ReturnCode

CREATE PROCEDURE DeleteInventoryItem
(
	@InventoryID INT NULL
)
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1

	IF @InventoryID IS NULL
		RAISERROR('DeleteInventoryItem: InventoryID cannot be null', 16, 1)
	ELSE
		BEGIN
			BEGIN TRANSACTIOn
				DELETE 
				FROM Inventory
				WHERE InventoryID = @InventoryID
				
				IF @@ERROR = 0
					BEGIN
						SET @ReturnCode = 0
						COMMIT TRANSACTION
					END
				ELSE
					BEGIN
						RAISERROR('DeleteInventoryItem: Failed to delete', 16, 1)
						ROLLBACK TRANSACTION
					END
		END
RETURN @ReturnCode

CREATE PROCEDURE InsertIntoInventory
(
	@CardID INT NULL,
	@SealedProdcutID INT NULL,
	@Quantity INT NULL,
	@QualityID INT NULL,
	@FirstEdition BIT NULL
)
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1

	IF @QualityID IS NULL
		RAISERROR('InsertIntoInventory: QualityID cannot be null', 16, 1)
		IF @Quantity IS NULL
			RAISERROR('InsertIntoInventory: Quantity cannot be null', 16, 1)
		BEGIN
			BEGIN TRANSACTIOn
				INSERT
				INTO Inventory
				(CardID, SealedProductID, Quantity, QualityID, FirstEdition)
				VALUES
				(@CardID, @SealedProdcutID, @Quantity, @QualityID, @FirstEdition)
				
				IF @@ERROR = 0
					BEGIN
						SET @ReturnCode = 0 
						COMMIT TRANSACTION
					END
				ELSE
					BEGIN
						RAISERROR('InsertIntoInventory: Insert failed', 16, 1)
						ROLLBACK TRANSACTION
					END
		END
RETURN @ReturnCode
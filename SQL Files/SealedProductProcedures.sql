CREATE PROCEDURE InsertSealedProduct
	@SetID INT = NULL,
	@SealedProductName VARCHAR(30) = NULL,
	@Quantity INT = NULL,
	@Price MONEY = NULL
AS

DECLARE @ReturnCode INT
SET @ReturnCode = 0

IF @SetID IS NULL
	RAISERROR('InsertSealedProduct: Set ID cannot be null', 16, 1)
	IF @SealedProductName IS NULL
		RAISERROR('InsertSealedProduct: Sealed Product Name cannot be null', 16, 1)
		IF @Quantity IS NULL
			RAISERROR('InsertSealedProduct: Quantity cannot be null', 16, 1)
			IF @Price IS NULL
				RAISERROR('InsertSealedProduct: Price cannot be null', 16, 1)
ELSE
	BEGIN
		BEGIN TRANSACTION
		INSERT INTO SealedProduct
		(SetID, SealedProductName, Quantity, Price)
		VALUES
		(@SetID, @SealedProductName, @Quantity, @Price)
		IF @@ERROR = 0
			BEGIN
				SET @ReturnCode = 0
				COMMIT TRANSACTION
			END
		ELSE
			BEGIN
				SET @ReturnCode = 1
				RAISERROR('InsertSealedProduct: Failed to insert into SealedProduct', 16 ,1)
				ROLLBACK TRANSACTION
			END
	END
RETURN @ReturnCode

CREATE PROCEDURE UpdateSealedProduct
	@SealedProductID INT = NULL,
	@SetID INT = NULL,
	@SealedProductName VARCHAR(30) = NULL,
	@Quantity INT = NULL,
	@Price MONEY = NULL
AS

DECLARE @ReturnCode INT
SET @ReturnCode = 0

IF @SealedProductID IS NULL
	RAISERROR('UpdateSealedProduct: Sealed Product ID cannot be null', 16, 1)
ELSE
	BEGIN
		BEGIN TRANSACTION
		UPDATE SealedProduct
		SET
			SealedProductID = @SealedProductID,
			SetID = @SetID,
			SealedProductName = @SealedProductName,
			Quantity = @Quantity,
			Price = @Price
		WHERE SealedProductID = @SealedProductID
		IF @@ERROR = 0
			BEGIN
				SET @ReturnCode = 0
				COMMIT TRANSACTION
			END
		ELSE
			BEGIN
				SET @ReturnCode = 1
				RAISERROR('UpdateSealedProduct: Failed to update the Sealed product', 16, 1)
				ROLLBACK TRANSACTION
			END
	END
RETURN @ReturnCode

CREATE PROCEDURE DeleteSealedProduct
	@SealedProductID INT = NULL
AS

DECLARE @ReturnCode INT
SET @ReturnCode = 0

IF @SealedProductID IS NULL
	RAISERROR('DeleteSealedProdcut: Sealed Product ID cannot be null', 16 , 1)
ELSE
	BEGIN
		BEGIN TRANSACTION
		DELETE SealedProduct
		WHERE SealedProductID = @SealedProductID
		IF @@ERROR = 0
			BEGIN
				SET @ReturnCode = 0
				COMMIT TRANSACTION
			END
		ELSE
			BEGIN
				SET @ReturnCode = 1
				RAISERROR('DeleteSealedProduct: Failed to delete the sealed product', 16, 1)
				ROLLBACK TRANSACTION
			END
	END
RETURN @ReturnCode

CREATE PROCEDURE GetSealedProduct
	@SealedProductID INT = NULL
AS

DECLARE @ReturnCode INT
SET @ReturnCode = 0

IF @SealedProductID IS NULL
	RAISERROR('GetSealedProduct: Sealed Product ID cannot be null', 16, 1)
ELSE
	BEGIN
		SELECT SealedProductID, SetID, SealedProductName, Quantity, Price
		FROM SealedProduct
		WHERE SealedProductID = @SealedProductID
		IF @@ERROR = 0
			SET @ReturnCode = 0
		ELSE
			BEGIN
				SET @ReturnCode = 1
				RAISERROR('GetSealedProduct: Failed to select on the Sealed Product table', 16, 1)
			END
	END
RETURN @ReturnCode

CREATE PROCEDURE GetSealedProductBySet
	@SetID INT = NULL
AS

DECLARE @ReturnCode INT
SET @ReturnCode = 0

IF @SetID IS NULL
	RAISERROR('GetSealedProductBySet: Set ID cannot be null', 16, 1)
ELSE
	BEGIN
		SELECT SealedProductID, SetID, SealedProductName, Quantity, Price
		FROM SealedProduct
		INNER JOIN [Set]
		ON SealedProduct.SetID = [Set].SetID
		WHERE [Set].SetID = @SetID
		IF @@ERROR = 0
			SET @ReturnCode = 0
		ELSE
			BEGIN
				SET @ReturnCode = 1
				RAISERROR('GetSealedProductBySet: Failed to select from the Sealed Product table', 16, 1)
			END
	END
RETURN @ReturnCode


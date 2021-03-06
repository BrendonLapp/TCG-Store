CREATE PROCEDURE InsertSealedProduct
	@SetID INT = NULL,
	@SealedProductName VARCHAR(30) = NULL
	@Price MONEY = NULL
AS

DECLARE @ReturnCode INT
SET @ReturnCode = 0

IF @SetID IS NULL
	RAISERROR('InsertSealedProduct: Set ID cannot be null', 16, 1)
	IF @SealedProductName IS NULL
		RAISERROR('InsertSealedProduct: Sealed Product Name cannot be null', 16, 1)
			IF @Price IS NULL
				RAISERROR('InsertSealedProduct: Price cannot be null', 16, 1)
ELSE
	BEGIN
		BEGIN TRANSACTION
		INSERT INTO SealedProduct
		(SetID, SealedProductName, Price)
		VALUES
		(@SetID, @SealedProductName, @Price)
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

CREATE PROCEDURE GetAllSealedProduct
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1

	BEGIN
		SELECT
			SealedProductID,
			SetID,
			SealedProductName
		FROM SealedProduct

		IF @@ERROR = 0
			SET @ReturnCode = 0
		ELSE
			BEGIN
				RAISERROR('GetAllSealedProduct: Failed to select from Sealed Product table', 16, 1)
			END
	END
RETURN @ReturnCode

CREATE PROCEDURE GetSealedProductByGame
(
	@GameID INT = NULL
)
AS
	IF @GameID IS NULL	
		RAISERROR('GetSealedProductByGame: GameID cannot be null', 16, 1)
	ELSE
		DECLARE @ReturnCode INT
		SET @ReturnCode = 1

		BEGIN
			SELECT
				SealedProductID,
				SealedProduct.SetID,
				SealedProductName
			FROM SealedProduct
			INNER JOIN [Set]
			ON SealedProduct.SetID = [Set].SetID
			INNER JOIN Game
			ON [Set].GameID = Game.GameID
			WHERE Game.GameID = @GameID
		END
RETURN @ReturnCode
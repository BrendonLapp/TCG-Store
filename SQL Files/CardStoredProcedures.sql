ALTER PROCEDURE SP_InsertIntoCard
	@SetID INT,
	@CardName VARCHAR(50),
	@CardSetNumber VARCHAR(10),
	@CardRarity VARCHAR(20),
	@Quantity INT,
	@CardPrice INT
AS

DECLARE @ReturnCode INT
SET @ReturnCode = 0

IF @SetID IS NULL
	RAISERROR('SP_InsertIntoCard: Set ID cannot be null', 16, 1)
	ELSE IF @CardName IS NULL
		RAISERROR('SP_InsertIntoCard: Card name cannot be null', 16, 1)
		ELSE IF @CardSetNumber IS NULL
			RAISERROR('SP_InsertIntoCard: Card set number cannot be null', 16, 1)
			ELSE IF @Quantity IS NULL
				RAISERROR('SP_InsertIntoCard: Quantity cannot be null', 16, 1)
				ELSE IF @CardPrice IS NULL
					RAISERROR('SP_InsertIntoCard: Card price cannot be null', 16, 1)
ELSE
	BEGIN
		BEGIN TRANSACTION
			INSERT Card
			(SetID, CardName, CardSetNumber, Quantity, CardPrice)
			VALUES  (@SetID, @CardName, @CardSetNumber, @Quantity, @CardPrice)
			IF @@ERROR = 0
				BEGIN
					SET @ReturnCode = 0
					COMMIT TRANSACTION
				END
			ELSE
				BEGIN
					RAISERROR('SP_InsertIntoCard: Failed to insert into card', 16, 1)
					ROLLBACK TRANSACTION
					SET @ReturnCode = 1
				END
	END	
RETURN @ReturnCode

GO
CREATE PROCEDURE SP_UpdateCard
	@CardID INT,
	@SetID INT,
	@CardName VARCHAR(50),
	@CardSetNumber VARCHAR(10),
	@CardRarity VARCHAR(20),
	@Quantity INT,
	@CardPrice INT
AS

DECLARE @ReturnCode INT
SET @ReturnCode = 0
	
IF @CardID IS NULL
	RAISERROR('SP_UpdateCard: Card ID cannot be null', 16, 1)
		ELSE IF @SetID IS NULL
			RAISERROR('SP_UpdateCard: Set ID cannot be null', 16, 1)
			ELSE IF @CardName IS NULL
				RAISERROR('SP_UpdateCard: Card name cannot be null', 16, 1)
				ELSE IF @CardSetNumber IS NULL
					RAISERROR('SP_UpdateCard: Card set number cannot be null', 16, 1)
					ELSE IF @Quantity IS NULL
						RAISERROR('SP_UpdateCard: Quantity cannot be null', 16, 1)
						ELSE IF @CardPrice IS NULL
							RAISERROR('SP_UpdateCard: Card price cannot be null', 16, 1)
ELSE
	BEGIN TRANSACTION
		UPDATE [Card]
		SET
		SetID = @SetID,
		CardName = @CardName,
		CardSetNumber = @CardSetNumber,
		Quantity = @Quantity,
		CardPrice = @CardPrice
		WHERE CardID = @CardID
		IF @@ERROR = 0
			BEGIN
				SET @ReturnCode = 0
				COMMIT TRANSACTION
			END
		ELSE
			BEGIN
				RAISERROR('SP_UpdateCard: Failed to update card', 16, 1)
				SET @ReturnCode = 1
				ROLLBACK TRANSACTION
			END
RETURN @ReturnCode

GO
CREATE PROCEDURE SP_DeleteCard 
	@CardID INT
AS

DECLARE @ReturnCode INT
SET @ReturnCode = 0

IF @CardID IS NULL
	RAISERROR('SP_DeleteCard: Card ID cannot be null', 16, 1)
ELSE
	BEGIN TRANSACTION
		DELETE FROM [Card]
		WHERE @CardID = CardID
		IF @@ERROR = 0
			BEGIN
				SET @ReturnCode = 0
				COMMIT TRANSACTION
			END
		ELSE
			BEGIN
				RAISERROR('SP_DeleteCard: Failed to delete card', 16, 1)
				ROLLBACK TRANSACTION
			END
RETURN

GO
ALTER PROCEDURE SP_GetAllCards 
AS

DECLARE @ReturnCode INT
SET @ReturnCode = 0

	SELECT 
	CardID, SetID, CardName, CardSetNumber, CardRarity, Quantity, CardPrice
	FROM CARD
	IF @@ERROR = 0
		SET @ReturnCode = 0
	ELSE
		BEGIN
			RAISERROR('SP_GetAllCards: Failed to get all cards', 16, 1)
			SET @ReturnCode = 1
		END
RETURN @ReturnCode

GO 
ALTER PROCEDURE SP_GetAllCardsBySet
	@SetID INT
AS

DECLARE @ReturnCode INT
SET @ReturnCode = 0

IF @SetID IS NULL
	RAISERROR('SP_GetAllCardsBySet: Set ID cannot be null', 16, 1)
ELSE
	SELECT 
	CardID, SetID, CardName, CardSetNumber, CardRarity, Quantity, CardPrice
	FROM [CARD]
	WHERE @SetID = SetID
	IF @@ERROR = 0
		SET @ReturnCode = 0
	ELSE
		BEGIN
			SET @ReturnCode = 1
			RAISERROR('SP_GetAllCardsBySet: Failed to get all cards by set',16, 1)
		END
RETURN @ReturnCode

GO
CREATE PROCEDURE SP_GetCardByCardID
	@CardID INT
AS

DECLARE @ReturnCode INT
SET @ReturnCode = 0

IF @CardID IS NULL
	RAISERROR('SP_GetCardByCardID: Card ID cannot be null', 16, 1)
ELSE
	SELECT 
	CardID, SetID, CardName, CardSetNumber, CardRarity, Quantity, CardPrice
	FROM Card
	WHERE @CardID = CardID
	IF @@ERROR = 0
		SET @ReturnCode = 0
	ELSE
		RAISERROR('SP_GetCardByCardID: Failed to find a card the by provided card ID', 16, 1)
		SET @ReturnCode = 1
RETURN @ReturnCode

GO 
ALTER PROCEDURE SP_GetCardByCardName
	@CardName VARCHAR(50)
AS

DECLARE @ReturnCode INT
SET @ReturnCode = 0

IF @CardName IS NULL
	RAISERROR('SP_GetCardByCardName: Card Name cannot be null', 16, 1)
ELSE
	SELECT
	CardID, SetID, CardName, CardSetNumber, CardRarity, Quantity, CardPrice
	FROM [CARD]
	WHERE @CardName = CardName
	IF @@ERROR = 0
		SET @ReturnCode = 0
	ELSE
		BEGIN
			RAISERROR('SP_GetCardByCardName: Failed to find a card by the provided card name', 16, 1)
			SET @ReturnCode = 1
		END
RETURN @ReturnCode

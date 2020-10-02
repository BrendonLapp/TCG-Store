CREATE PROCEDURE SP_InsertIntoCard
	@SetID INT,
	@CardName VARCHAR(50),
	@CardSetNumber VARCHAR(10),
	@CardRarity VARCHAR(20),
	@Quantity INT,
	@CardPrice INT
AS
	IF @SetID IS NULL
		RAISERROR('Set ID cannot be null', 16, 1)
		ELSE IF @CardName IS NULL
			RAISERROR('Card name cannot be null', 16, 1)
			ELSE IF @CardSetNumber IS NULL
				RAISERROR('Card set number cannot be null', 16, 1)
				ELSE IF @Quantity IS NULL
					RAISERROR('Quantity cannot be null', 16, 1)
					ELSE IF @CardPrice IS NULL
						RAISERROR('Card price cannot be null', 16, 1)
	ELSE
		BEGIN
			BEGIN TRANSACTION
				INSERT [Card]
				(SetID, CardName, CardSetNumber, Quantity, CardPrice)
				VALUES  (@SetID, @CardName, @CardSetNumber, @Quantity, @CardPrice)
				IF @@ERROR <> 0
					BEGIN
						RAISERROR('Failed to insert into card', 16, 1)
						ROLLBACK TRANSACTION
					END
				ELSE
					COMMIT TRANSACTION
		END	
RETURN

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
	IF @CardID IS NULL
		RAISERROR('Card ID cannot be null', 16, 1)
			ELSE IF @SetID IS NULL
				RAISERROR('Set ID cannot be null', 16, 1)
				ELSE IF @CardName IS NULL
					RAISERROR('Card name cannot be null', 16, 1)
					ELSE IF @CardSetNumber IS NULL
						RAISERROR('Card set number cannot be null', 16, 1)
						ELSE IF @Quantity IS NULL
							RAISERROR('Quantity cannot be null', 16, 1)
							ELSE IF @CardPrice IS NULL
								RAISERROR('Card price cannot be null', 16, 1)
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
			IF @@ERROR <> 0
				BEGIN
					RAISERROR('Failed to update card', 16, 1)
					ROLLBACK TRANSACTION
				END
			ELSE
				COMMIT TRANSACTION
RETURN

GO
CREATE PROCEDURE SP_DeleteCard 
	@CardID INT
AS
	IF @CardID IS NULL
		RAISERROR('Card ID cannot be null', 16, 1)
	ELSE
		BEGIN TRANSACTION
			DELETE FROM [Card]
			WHERE @CardID = CardID
			IF @@ERROR <> 0
				BEGIN
					RAISERROR('Failed to delete card', 16, 1)
					ROLLBACK TRANSACTION
				END
			ELSE
				COMMIT TRANSACTION
RETURN

GO
CREATE PROCEDURE SP_GetAllCards 
AS
	SELECT CardID, SetID, CardName, CardSetNumber, CardRarity, Quantity, CardPrice
	FROM [CARD]
RETURN

GO 
CREATE PROCEDURE SP_GetAllCardsBySet
	@SetID INT
AS
	IF @SetID IS NULL
		RAISERROR('Set ID cannot be null', 16, 1)
	ELSE
		SELECT 
		CardID, SetID, CardName, CardSetNumber, CardRarity, Quantity, CardPrice
		FROM [CARD]
		WHERE @SetID = SetID
RETURN

GO
CREATE PROCEDURE SP_GetCardByCardID
	@CardID INT
AS
	IF @CardID IS NULL
		RAISERROR('Card ID cannot be null', 16, 1)
	ELSE
		SELECT 
		CardID, SetID, CardName, CardSetNumber, CardRarity, Quantity, CardPrice
		FROM [CARD]
		WHERE @CardID = CardID
RETURN

GO 
CREATE PROCEDURE SP_GetCardByCardName
	@CardName VARCHAR(50)
AS
	IF @CardName IS NULL
		RAISERROR('Card Name cannot be null', 16, 1)
	ELSE
		SELECT
		CardID, SetID, CardName, CardSetNumber, CardRarity, Quantity, CardPrice
		FROM [CARD]
		WHERE @CardName = CardName
RETURN

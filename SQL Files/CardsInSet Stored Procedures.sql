CREATE PROCEDURE InsertIntoCardsInSet
(
	@SetID INT NULL,
	@CardCodeInSet VARCHAR(10) NULL,
	@CardName VARCHAR(75) NULL,
	@Rarity VARCHAR(25) NULL,
	@Price MONEY NULL,
	@ElementalType VARCHAR(25) NULL,
	@SubType VARCHAR(25) NULL,
	@SuperType VARCHAR(25) NULL,
	@PictureLink VARCHAR(250) NULL,
	@PictureLinkSmall VARCHAR(250) NULL,
	@APIImageID VARCHAR(15) NULL
)
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 0

	IF @SetID IS NULL
		RAISERROR('InsertIntoCardInSet - @SetID cannot be null', 16, 1)
		IF @CardCodeInSet IS NULL
			RAISERROR('InsertIntoCardInSet - @CardCodeInSet cannot be null', 16, 1)
			IF @CardName IS NULL
				RAISERROR('InsertIntoCardInSet - @CardName cannot be null', 16, 1)
				IF @Rarity IS NULL
					RAISERROR('InsertIntoCardInSet - @Rarity cannot be null', 16, 1)
					IF @Price IS NULL
						RAISERROR('InsertIntoCardInSet - @Price cannot be null', 16, 1)
	ELSE
		BEGIN
			BEGIN TRANSACTION
				INSERT CardsInSet
				(
					SetID, 
					CardCodeInSet, 
					CardName, 
					Rarity, 
					Price,
					ElementalType,
					SubType,
					SuperType,
					PictureLink,
					PictureSmallLink,
					APIImageID
				)
				VALUES
				(
					@SetID,
					@CardCodeInSet,
					@CardName,
					@Rarity,
					@Price,
					@ElementalType,
					@SubType,
					@SuperType,
					@PictureLink,
					@PictureLinkSmall,
					@APIImageID
				)
				IF @@ERROR = 0
					COMMIT TRANSACTION
				ELSE
					BEGIN
						SET @ReturnCode = 1
						RAISERROR('InsertIntoCardInSet - Insert error', 16, 1)
						ROLLBACK TRANSACTION
					END
		END
RETURN @ReturnCode
	
ALTER PROCEDURE SeachForCardsByPartialName
(
	@SearchQuery VARCHAR(100)
)
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1

	BEGIN
		SELECT 
			CardID,
			CardCodeInSet,
			SetID,
			CardName,
			Price,
			Rarity,
			ElementalType,
			SubType,
			SuperType,
			PictureLink,
			PictureSmallLink
		FROM CardsInSet
		WHERE CardName LIKE '%' + @SearchQuery + '%'

		IF @@ERROR = 0
			SET @ReturnCode = 0
		ELSE
			RAISERROR('SearchForCardsByPartialName: Select failed', 16, 1)
	END
RETURN @ReturnCode

SELECT * FROM CardsInSet


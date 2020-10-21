CREATE PROCEDURE InsertSet
	@SetName VARCHAR(40) = NULL,
	@GameID INT = NUL
AS

DECLARE @ReturnCode INT
SET @ReturnCode = 0

IF @SetName IS NULL
	RAISERROR('InsertSet: Set name cannot be null', 16, 1)
	IF @GameID IS NULL
		RAISERROR('InsertSet: Game ID cannot be null', 16, 1)
ELSE
	BEGIN
		BEGIN TRANSACTION
			INSERT [Set]
			(GameID, SetName)
			VALUES
			(@GameID, @SetName)
			IF @@ERROR = 0
				BEGIN
					SET @ReturnCode = 0
					COMMIT TRANSACTION
				END
			ELSE
				BEGIN
					SET @ReturnCode = 1
					RAISERROR('InsertSet: Failed to insert into set', 16, 1)
					ROLLBACK TRANSACTION
				END
	END
RETURN @ReturnCode

GO
CREATE PROCEDURE UpdateSet
	@SetID INT = NULL,
	@GameID INT = NULL,
	@SetName VARCHAR(40) = NULL
AS

DECLARE @ReturnCode INT
SET @ReturnCode = 0

IF @SetID IS NULL
	RAISERROR('UpdateSet: Set ID cannot be null', 16, 1)
	IF @GameID IS NULL
		RAISERROR('UpdateSet: Game ID cannot be null', 16, 1)
		IF @SetName IS NULL
			RAISERROR('UpdateSet: Game Name cannot be null', 16, 1)
ELSE
	BEGIN
		BEGIN TRANSACTION
		UPDATE [Set]
		SET
			GameID = @GameID,
			SetName = @SetName
		WHERE
			SetID = @SetID
		IF @@ERROR = 0
			BEGIN
				SET @ReturnCode = 0
				COMMIT TRANSACTION
			END
		ELSE
			BEGIN
				SET @ReturnCode = 1
				RAISERROR('UpdateSet: Failed to update set with the provided ID', 16, 1)
				ROLLBACK TRANSACTION
			END
	END
RETURN @ReturnCode

GO
CREATE PROCEDURE DeleteSet
	@SetID INT = NULL
AS

DECLARE @ReturnCode INT
SET @ReturnCode = 0

IF @SetID IS NULL
	RAISERROR('DeleteSet: Set ID cannot be null', 16, 1)
ELSE
	BEGIN
		BEGIN TRANSACTION
		DELETE FROM [Set]
		WHERE @SetID = SetID
		IF @@ERROR = 0
			BEGIN
				SET @ReturnCode = 0
				COMMIT TRANSACTION
			END
		ELSE
			BEGIN 
				SET @ReturnCode = 1
				RAISERROR('DeleteSet: Failed to delete the set of the provided ID', 16, 1)
				ROLLBACK TRANSACTION
			END
	END
RETURN @ReturnCode

GO
ALTER PROCEDURE GetAllSets
AS
DECLARE @ReturnCode INT
SET @ReturnCode = 0
	SELECT SetID, GameID, SetName
	FROM [Set]
	IF @@ERROR = 0
		SET @ReturnCode = 0
	ELSE
		BEGIN
			SET @ReturnCode = 1
			RAISERROR('GetAllSets: Failed to find any sets', 16, 1)
		END
RETURN @ReturnCode

DROP PROCEDURE SearchForGameByID

GO
CREATE PROCEDURE GetSetByID
	@SetID INT = NULL
AS
DECLARE @ReturnCode INT
SET @ReturnCode = 0

IF @SetID IS NULL
	RAISERROR('GetSetByID: Set ID cannot be null', 16, 1)

	SELECT Setid, GameID, SetName 
	FROM [Set]
	WHERE SetID = @SetID
	IF @@ERROR = 0
		SET @ReturnCode = 0
	ELSE
		BEGIN
			SET @ReturnCode = 0
			RAISERROR('GetSetByID: Failed to get a set by the provided ID', 16, 1)
		END
RETURN @ReturnCode

GO
CREATE PROCEDURE GetSetsByGameID
	@GameID INT = NULL
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 0

	IF @GameID IS NULL
		RAISERROR('GetSetsByGameID: Game ID cannot be null', 16, 1)

		SELECT SetID, GameID, SetName
		FROM [Set]
		WHERE GameID = @GameID
		IF @@ERROR = 0
			SET @ReturnCode = 0
		ELSE
			BEGIN
				SET @ReturnCode = 0
				RAISERROR('GetSetsByGameID: Failed to find any sets for the given game ID', 16, 1)
			END

RETURN @ReturnCode
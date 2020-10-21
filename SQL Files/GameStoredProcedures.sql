CREATE PROCEDURE GetGameByID 
	@GameID INT = NULL
AS
IF @GameID IS NULL
	RAISERROR('GetGameByID: Game ID cannot be null.', 16, 1)
ELSE

DECLARE @ReturnCode INT
SET @ReturnCode = 0

SELECT GameID, GameName
FROM Game
WHERE @GameID = GameID
IF @@ERROR = 0
	SET @ReturnCode = 0
ELSE
	BEGIN
		SET @ReturnCode = 1
		RAISERROR('GetGameByID: Failed to find a game by the provided ID', 16, 1)
	END
RETURN @ReturnCode

GO
CREATE PROCEDURE GetAllGames 
AS

DECLARE @ReturnCode INT
SET @ReturnCode = 0

SELECT GameID, GameName
FROM Game
IF @@ERROR = 0
	SET @ReturnCode = 0
ELSE
	BEGIN
		SET @ReturnCode = 1
		RAISERROR('GetAllGames: Failed to find any games', 16, 1)
	END
RETURN @ReturnCode


GO
CREATE PROCEDURE InsertIntoGame 
	@GameName VARCHAR(30) = null
AS

DECLARE @ReturnCode INT
SET @ReturnCode = 0

IF @GameName IS NULL
	RAISERROR('InsertIntoGame: Game name cannot be null.', 16, 1)
ELSE
	BEGIN
		BEGIN TRANSACTION
		INSERT Game (GameName)
		VALUES (@GameName)
		IF @@ERROR = 0
			BEGIN
				SET @ReturnCode = 0
				COMMIT TRANSACTION
			END
		ELSE
			BEGIN
				SET @ReturnCode = 1
				RAISERROR('InsertIntoGame: Insert into Game failed', 16, 1)
				ROLLBACK TRANSACTION
			END
	END
RETURN @ReturnCode

GO
CREATE PROCEDURE UpdateGame 
	@GameID INT = NULL,
	@GameName VARCHAR(30) = NULL 
AS

DECLARE @ReturnCode INT
SET @ReturnCode = 0

IF @GameName IS NULL
	RAISERROR('UpdateGame: Game name cannot be null.', 16, 1)
	ELSE IF @GameID IS NULL
		RAISERROR('UpdateGame: Game ID cannot be null.', 16, 1)
ELSE
	BEGIN 
		BEGIN TRANSACTION
			UPDATE Game
			SET
				GameName = @GameName
			WHERE @GameID = GameID
			IF @@ERROR = 0 
				BEGIN
					SET @ReturnCode = 0
					COMMIT TRANSACTION
				END
			ELSE
				BEGIN
					SET @ReturnCode = 1
					RAISERROR('UpdateGame: Cannot update game.', 16, 1)
					ROLLBACK TRANSACTION
				END
	END
RETURN @ReturnCode

GO
CREATE PROCEDURE DeleteGame 
	@GameID INT = NULL 
AS

DECLARE @ReturnCode INT
SET @ReturnCode = 0

IF @GameID IS NULL
	RAISERROR('DeleteGame: Cannot delete game. The ID does not exist.', 16, 1)
ELSE
	BEGIN TRANSACTION
		DELETE FROM Game
		WHERE @GameID = GameID
		IF @@ERROR = 0
			BEGIN
				SET @ReturnCode = 0
				COMMIT TRANSACTION
			END
		ELSE
			BEGIN
				SET @ReturnCode = 1
				RAISERROR('DeleteGame: Failed to delete the game.', 16, 1)
				ROLLBACK TRANSACTION
			END
RETURN @ReturnCode

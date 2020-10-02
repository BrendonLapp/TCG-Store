CREATE PROCEDURE SP_SearchForGameByID @GameID INT AS
IF @GameID IS NULL
	RAISERROR('Game ID cannot be null.', 16, 1)
ELSE
	SELECT GameName
	FROM Game
	WHERE @GameID = GameID
RETURN

GO
CREATE PROCEDURE SP_SearchForAllGames AS
	SELECT GameName
	FROM Game
RETURN

GO
CREATE PROCEDURE SP_InsertIntoGame (@GameName VARCHAR(30) = null) AS
IF @GameName IS NULL
	RAISERROR('Game name cannot be null.', 16, 1)
ELSE
	BEGIN
		BEGIN TRANSACTION
		INSERT Game (GameName)
		VALUES (@GameName)
		IF @@ERROR<> 0
			BEGIN
				RAISERROR('Insert into Game failed', 16, 1)
				ROLLBACK TRANSACTION
			END
		ELSE
			COMMIT TRANSACTION
	END
RETURN

GO
CREATE PROCEDURE SP_UpdateGame (@GameID INT NULL, @GameName VARCHAR(30) = NULL) AS
IF @GameName IS NULL
	RAISERROR('Game name cannot be null.', 16, 1)
	ELSE IF @GameID IS NULL
		RAISERROR('Game ID cannot be null.', 16, 1)
ELSE
	BEGIN 
		BEGIN TRANSACTION
			UPDATE Game
			SET
				GameName = @GameName
			WHERE @GameID = GameID
			IF @@ERROR<> 0 
				BEGIN
					RAISERROR('Cannot update game.', 16, 1)
					ROLLBACK TRANSACTION
				END
			ELSE
				COMMIT TRANSACTION
	END
RETURN

GO
CREATE PROCEDURE SP_DeleteGame (@GameID INT NULL) AS
IF @GameID IS NULL
	RAISERROR('Cannot delete game. The ID does not exist.', 16, 1)
ELSE
	BEGIN TRANSACTION
		DELETE FROM Game
		WHERE @GameID = GameID
		IF @@ERROR<> 0
			BEGIN
				RAISERROR('Failed to delete the game.', 16, 1)
				ROLLBACK TRANSACTION
			END
		ELSE
			COMMIT TRANSACTION
RETURN

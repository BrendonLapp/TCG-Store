CREATE PROCEDURE GetQualityByID
(
	@QualityID INT NULL
)
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1
	BEGIN
		IF @QualityID IS NULL
			RAISERROR('GetQualityByID: QualityID cannot be null', 16, 1)
		ELSE
			SELECT
				QualityID,
				QualityPercentage,
				QualityName,
				QualityShortName
			FROM Quality
			WHERE QualityID = @QualityID

			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('GetQualityByID: Select error on Quality table', 16, 1)
	END
RETURN @ReturnCode

CREATE PROCEDURE GetAllQualities
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1
	BEGIN
		SELECT
			QualityID,
			QualityPercentage,
			QualityName,
			QualityShortName
		FROM Quality

		IF @@ERROR = 0
			SET @ReturnCode = 0
		ELSE
			RAISERROR('GetQualityByID: Select error on Quality table', 16, 1)
	END
RETURN @ReturnCode

CREATE PROCEDURE InsertIntoQuality
(
	@QualityPercentage DECIMAL(5,2) NULL,
	@QualityName VARCHAR(25) NULL,
	@QualityShortName VARCHAR(2) NULL
)
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1

	BEGIN
		IF @QualityName IS NULL
			RAISERROR('InsertIntoQuality: Quality Name cannot be null', 16, 1)
		ELSE
			IF @QualityPercentage IS NULL
				RAISERROR('InsertIntoQuality: Quality Percentage cannot be null', 16, 1)
			ELSE
				IF @QualityShortName IS NULL
					RAISERROR('InsertIntoQuality: Quality Short Name cannot be null', 16, 1)
				ELSE
					BEGIN TRANSACTION

						INSERT INTO Quality 
						(QualityName, QualityShortName, QualityPercentage)
						VALUES 
						(@QualityName, @QualityShortName, @QualityPercentage)

						IF @@ERROR = 0
							BEGIN
								SET @ReturnCode = 0
								COMMIT TRANSACTION
							END
						ELSE
							BEGIN
								RAISERROR('InsertIntoQuality: Insert failed', 16, 1)
								ROLLBACK TRANSACTION
							END
	END
RETURN @ReturnCode

CREATE PROCEDURE UpdateQuality
(
	@QualityID INT NULL,
	@QualityPercentage DECIMAL(5,2) NULL,
	@QualityName VARCHAR(25) NULL,
	@QualityShortName VARCHAR(2) NULL
)
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1

	IF @QualityID IS NULL
		RAISERROR('UpdateQuality: Quality ID cannot be null', 16, 1)
		BEGIN
			BEGIN TRANSACTION
				UPDATE Quality
				SET
					QualityName = @QualityName,
					QualityShortName = @QualityShortName,
					QualityPercentage = @QualityPercentage
				WHERE
					QualityID = @QualityID
				IF @@ERROR = 0 
					BEGIN
						SET @ReturnCode = 0
						COMMIT TRANSACTION
					END
				ELSE
					BEGIN
						RAISERROR('UpdateQuality: Failed to update the quality', 16, 1)
						ROLLBACK TRANSACTION
					END
		END
RETURN @ReturnCode

CREATE PROCEDURE DeleteQuality
(
	@QualityID INT NULL
)
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1

	IF @QualityID IS NULL
		RAISERROR('DeleteQuality: Quality ID cannot be null', 16, 1)
	ELSE
		BEGIN
			BEGIN TRANSACTION
				DELETE 
				FROM Quality
				WHERE QualityID = @QualityID
				IF @@ERROR = 0
					BEGIN
						SET @ReturnCode = 0
						COMMIT TRANSACTION
					END
				ELSE
					BEGIN
						RAISERROR('DeleteQuality: Failed to delete the quality', 16, 1)
						ROLLBACK TRANSACTION
					END
		END
RETURN @ReturnCode

SP_HELP Quality
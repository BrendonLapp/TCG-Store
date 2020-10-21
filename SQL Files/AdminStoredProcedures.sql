ALTER PROCEDURE SP_InsertAdmin 
	@FirstName VARCHAR(15), 
	@LastName VARCHAR(15), 
	@Password VARCHAR(20),
	@Email VARCHAR(30) 
AS

DECLARE @ReturnCode INT
SET @ReturnCode = 0

IF @FirstName IS NULL
	RAISERROR('SP_InsertAdmin: First name cannot be null', 16, 1)
	ELSE IF @LastName IS NULL
		RAISERROR('SP_InsertAdmin: Last name cannot be null', 16, 1)
		ELSE IF @Password IS NULL
			RAISERROR('SP_InsertAdmin: Password cannot be null', 16, 1)
			ELSE IF @Email IS NULL
				RAISERROR('SP_InsertAdmin: Email cannot be null', 16, 1)
ELSE
	BEGIN TRANSACTION
		INSERT Administrator (FirstName, LastName, Password, Email)
		VALUES (@FirstName, @LastName, @Password, @Email)
		IF @@ERROR = 0
			BEGIN
				SET @ReturnCode = 0
				COMMIT TRANSACTION
			END
		ELSE
			BEGIN
				RAISERROR('SP_InsertAdmin: Failed to insert admin.', 16, 1)
				SET @ReturnCode = 1
				ROLLBACK TRANSACTION
			END
RETURN @ReturnCode

GO
ALTER PROCEDURE SP_UpdateAdmin 
	@AdminID INT, 
	@FirstName VARCHAR(15), 
	@LastName VARCHAR(15), 
	@Password VARCHAR(20), 
	@Email VARCHAR(30) 
AS

DECLARE @ReturnCode INT
SET @ReturnCode = 0

IF @AdminID IS NULL
	RAISERROR('SP_UpdateAdmin: Admin ID cannot be null.', 16, 1)
ELSE
	BEGIN TRANSACTION
		UPDATE Administrator
			SET 
			FirstName = @FirstName,
			LastName = @LastName,
			Password = @Password,
			Email = @Email
			WHERE AdminID = @AdminID
		IF @@ERROR = 0
			BEGIN
				SET @ReturnCode = 0
				COMMIT TRANSACTION
			END
		ELSE
			BEGIN
				RAISERROR('SP_UpdateAdmin: Failed to update admin,', 16, 1)
				SET @ReturnCode = 1
				ROLLBACK TRANSACTION
			END
RETURN @ReturnCode

GO
ALTER PROCEDURE SP_DeleteAdmin 
	@AdminID INT 
AS

DECLARE @ReturnCode INT
SET @ReturnCode = 0

IF @AdminID IS NULL
	RAISERROR('SP_DeleteAdmin: Admin ID cannot be null', 16, 1)
ELSE
	BEGIN TRANSACTION
		DELETE FROM Administrator
		WHERE AdminID = @AdminID
		IF @@ERROR = 0
			BEGIN
				SET @ReturnCode = 0
				COMMIT TRANSACTION
			END
		ELSE
			BEGIN
				RAISERROR('SP_DeleteAdmin: Failed to delete admin', 16, 1)
				SET @ReturnCode = 1
				ROLLBACK TRANSACTION
			END
RETURN

GO
ALTER PROCEDURE SP_GetAdminByID 
	@AdminID INT
AS

DECLARE @ReturnCode INT
SET @ReturnCode = 0

IF @AdminID IS NULL
	RAISERROR('SP_GetAdminByID: Admin ID CAnnot be null', 16, 1)
ELSE
	SELECT 
	FirstName, LastName, Email
	FROM Administrator
	WHERE AdminID = @AdminID
	IF @@ERROR = 0
		SET @ReturnCode = 0
	ELSE
		BEGIN
			SET @ReturnCode = 1
			RAISERROR('SP_GetAdminByID: Failed to get admin by the provided ID', 16, 1)
		END
RETURN

GO
ALTER PROCEDURE SP_GetAllAdmins 
AS

DECLARE @ReturnCode INT
SET @ReturnCode = 0

	SELECT
	FirstName, LastName, Email
	FROM Administrator
	ORDER BY LastName
	IF @@ERROR = 0
		SET @ReturnCode = 0
	ELSE
		BEGIN
			SET @ReturnCode = 1
			RAISERROR('SP_GetAllAdmins: Failed to get all admins', 16, 1)
		END
RETURN

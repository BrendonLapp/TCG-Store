CREATE PROCEDURE SP_InsertAdmin (@FirstName VARCHAR(15), @LastName VARCHAR(15), @Password VARCHAR(20), @Email VARCHAR(30)) AS
IF @FirstName IS NULL
	RAISERROR('First name cannot be null', 16, 1)
	ELSE IF @LastName IS NULL
		RAISERROR('Last name cannot be null', 16, 1)
		ELSE IF @Password IS NULL
			RAISERROR('Password cannot be null', 16, 1)
			ELSE IF @Email IS NULL
				RAISERROR('Email cannot be null', 16, 1)
ELSE
	BEGIN TRANSACTION
		INSERT Administrator (FirstName, LastName, [Password], Email)
		VALUES (@FirstName, @LastName, @Password, @Email)
		IF @@ERROR <> 0
			BEGIN
				RAISERROR('Failed to insert admin.', 16, 1)
				ROLLBACK TRANSACTION
			END
		ELSE
			COMMIT TRANSACTION
RETURN

GO
CREATE PROCEDURE SP_UpdateAdmin (@AdminID INT, @FirstName VARCHAR(15), @LastName VARCHAR(15), @Password VARCHAR(20), @Email VARCHAR(30)) AS
IF @AdminID IS NULL
	RAISERROR('Admin ID cannot be null.', 16, 1)
ELSE
	BEGIN TRANSACTION
		UPDATE Administrator
			SET 
			FirstName = @FirstName,
			LastName = @LastName,
			[Password] = @Password,
			Email = @Email
			WHERE AdminID = @AdminID
		IF @@ERROR <> 0
			BEGIN
				RAISERROR('Failed to update admin,', 16, 1)
				ROLLBACK TRANSACTION
			END
		ELSE
			COMMIT TRANSACTION
RETURN

GO
CREATE PROCEDURE SP_DeleteAdmin (@AdminID INT) AS
IF @AdminID IS NULL
	RAISERROR('Admin ID cannot be null', 16, 1)
ELSE
	BEGIN TRANSACTION
		DELETE FROM Administrator
		WHERE AdminID = @AdminID
		IF @@ERROR <> 0
			BEGIN
				RAISERROR('Failed to delete admin', 16, 1)
				ROLLBACK TRANSACTION
			END
		ELSE
			COMMIT TRANSACTION
RETURN

GO
CREATE PROCEDURE SP_GetAdminByID (@AdminID INT) AS
IF @AdminID IS NULL
	RAISERROR('Admin ID CAnnot be null', 16, 1)
ELSE
	SELECT 
	FirstName, LastName, Email
	FROM Administrator
	WHERE AdminID = @AdminID
RETURN

GO
CREATE PROCEDURE SP_GetAllAdmins AS
	SELECT
	FirstName, LastName, Email
	FROM Administrator
	ORDER BY LastName
RETURN

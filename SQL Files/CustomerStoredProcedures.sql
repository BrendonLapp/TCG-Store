CREATE PROCEDURE SP_InsertIntoCustomer 
	@FirstName VARCHAR(15), 
	@LastName VARCHAR(15), 
	@Password VARCHAR(20), 
	@Email VARCHAR(30), 
	@Address VARCHAR(50), 
	@PostalCode VARCHAR(7),
	@City VARCHAR(40),
	@Province VARCHAR(25),
	@PhoneNumber INT
AS
DECLARE @ReturnCode INT
SET @ReturnCode = 0

IF @FirstName IS NULL
	RAISERROR('SP_InsertIntoCustomer: First name cannot be null.', 16, 1)
	ELSE IF @LastName IS NULL
		RAISERROR('SP_InsertIntoCustomer: Last name cannot be null.', 16, 1)
		ELSE IF @Password IS NULL
			RAISERROR('SP_InsertIntoCustomer: Password cannot be null.', 16, 1)
			ELSE IF @Email IS NULL
				RAISERROR('SP_InsertIntoCustomer: Email cannot be null.', 16, 1)
ELSE
	BEGIN TRANSACTION
		INSERT Customer (FirstName, LastName, [Password], Email, [Address], PostalCode, City, Province, PhoneNumber)
		VALUES (@FirstName, @LastName, @Password, @Email, @Address, @PostalCode, @City, @Province, @PhoneNumber)
			IF @@ERROR = 0
				BEGIN
					COMMIT TRANSACTION
					SET @ReturnCode = 0
				END
			ELSE
				BEGIN
					RAISERROR('SP_InsertIntoCustomer: Failed to save customer.', 16, 1)
					SET @ReturnCode = 1
					ROLLBACK TRANSACTION
				END
RETURN @ReturnCode

GO
CREATE PROCEDURE SP_UpdateCustomer 
	@CustomerID INT, 
	@FirstName VARCHAR(15), 
	@LastName VARCHAR(15), 
	@Password VARCHAR(20), 
	@Email VARCHAR(30), 
	@Address VARCHAR(50), 
	@PostalCode VARCHAR(7),
	@City VARCHAR(40),
	@Province VARCHAR(25),
	@PhoneNumber INT
AS

DECLARE @ReturnCode INT
SET @ReturnCode = 0

IF @CustomerID IS NULL
	RAISERROR('SP_UpdateCustomer: Customer ID cannot be null', 16, 1)
ELSE
	BEGIN TRANSACTION
		UPDATE Customer
		SET
			FirstName = @FirstName,
			LastName = @LastName,
			[Password] = @Password,
			Email = @Email,
			[Address] = @Address,
			PostalCode = @PostalCode,
			City = @City,
			Province = @Province,
			PhoneNumber = @PhoneNumber
		WHERE @CustomerID = CustomerID
		IF @@ERROR = 0
			BEGIN
				COMMIT TRANSACTION
				SET @ReturnCode = 0
			END
		ELSE
			BEGIN
				RAISERROR('SP_UpdateCustomer: Failed to update customer information.', 16, 1)
				ROLLBACK TRANSACTION
				SET @ReturnCode = 1
			END
RETURN @ReturnCode

GO
CREATE PROCEDURE SP_DeleteCustomer 
	@CustomerID INT 
AS

DECLARE @ReturnCode INT
SET @ReturnCode = 0

IF @CustomerID IS NULL
	RAISERROR('SP_DeleteCustomer: Customer ID cannot be null', 16, 1)
ELSE
	BEGIN TRANSACTION
		DELETE FROM Customer
		WHERE CustomerID = @CustomerID
		IF @@ERROR = 0
			BEGIN
				SET @ReturnCode = 0
				COMMIT TRANSACTION
			END
		ELSE
			BEGIN
				RAISERROR('SP_DeleteCustomer: Failed to delete customer', 16, 1)
				ROLLBACK TRANSACTION
				SET @ReturnCode = 1
			END
RETURN @ReturnCode

GO
CREATE PROCEDURE SP_GetCustomerByID 
	@CustomerID INT 
AS

DECLARE @ReturnCode INT
SET @ReturnCode = 0

IF @CustomerID IS NULL
	RAISERROR('SP_GetCustomerByID: Customer ID cannot be null', 16, 1)
ELSE
	SELECT 
	FirstName, LastName, [Address], City, Province, PostalCode, PhoneNumber, email
	FROM Customer
	WHERE @CustomerID = CustomerID
	IF @@ERROR = 0
		SET @ReturnCode = 0
	ELSE
		BEGIN
			RAISERROR('SP_GetCustomerByID: Failed to seach for a customer by ID', 16, 1)
			SET @ReturnCode = 1
		END
RETURN

GO
CREATE PROCEDURE SP_GetAllCustomers 
AS

DECLARE @ReturnCode INT
SET @ReturnCode = 0

	SELECT 
	FirstName, LastName, [Address], City, Province, PostalCode, PhoneNumber, email
	FROM Customer
	ORDER BY LastName ASC
	IF @@ERROR = 0
		SET @ReturnCode = 0
	ELSE
		BEGIN
			RAISERROR('SP_GetAllCustomers: Failed to get any customers', 16, 1)
			SET @ReturnCode = 1
		END
RETURN @ReturnCode
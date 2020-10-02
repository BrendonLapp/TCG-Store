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
IF @FirstName IS NULL
	RAISERROR('First name cannot be null.', 16, 1)
	ELSE IF @LastName IS NULL
		RAISERROR('Last name cannot be null.', 16, 1)
		ELSE IF @Password IS NULL
			RAISERROR('Password cannot be null.', 16, 1)
			ELSE IF @Email IS NULL
				RAISERROR('Email cannot be null.', 16, 1)
ELSE
	BEGIN TRANSACTION
		INSERT Customer (FirstName, LastName, [Password], Email, [Address], PostalCode, City, Province, PhoneNumber)
		VALUES (@FirstName, @LastName, @Password, @Email, @Address, @PostalCode, @City, @Province, @PhoneNumber)
			IF @@ERROR <> 0
				BEGIN
					RAISERROR('Failed to save customer.', 16, 1)
					ROLLBACK TRANSACTION
				END
			ELSE
				COMMIT TRANSACTION
RETURN

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
IF @CustomerID IS NULL
	RAISERROR('Customer ID cannot be null', 16, 1)
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
		IF @@ERROR <> 0
			BEGIN
				RAISERROR('Failed to update customer information.', 16, 1)
				ROLLBACK TRANSACTION
			END
		ELSE
			COMMIT TRANSACTION
RETURN

GO
CREATE PROCEDURE SP_DeleteCustomer @CustomerID INT AS
IF @CustomerID IS NULL
	RAISERROR('Customer ID cannot be null', 16, 1)
ELSE
	BEGIN TRANSACTION
		DELETE FROM Customer
		WHERE CustomerID = @CustomerID
		IF @@ERROR <> 0
			BEGIN
				RAISERROR('Failed to delete customer', 16, 1)
				ROLLBACK TRANSACTION
			END
		ELSE
			COMMIT TRANSACTION
RETURN

GO
CREATE PROCEDURE SP_GetCustomerByID (@CustomerID INT) AS
IF @CustomerID IS NULL
	RAISERROR('Customer ID cannot be null', 16, 1)
ELSE
	SELECT 
	FirstName, LastName, [Address], City, Province, PostalCode, PhoneNumber, email
	FROM Customer
	WHERE @CustomerID = CustomerID
RETURN

GO
CREATE PROCEDURE SP_GetAllCustomers AS
	SELECT 
	FirstName, LastName, [Address], City, Province, PostalCode, PhoneNumber, email
	FROM Customer
	ORDER BY LastName ASC
RETURN
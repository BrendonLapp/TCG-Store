EXECUTE InsertIntoGame @GameName = 'Yu-Gi-OH';
EXECUTE SP_InsertIntoGame @GameName = 'Pokemon';
EXECUTE SP_InsertIntoGame @GameName = 'Magic The Gathering';

EXECUTE SP_InsertAdmin @FirstName = 'Brendon', @LastName = 'Lapp', @Password = 'Password01', @Email = 'Brendon.Lapp@gmail.com'

SELECT * FROM [Set]
SELECT * FROM Game

DELETE FROM [Set]
WHERE GameID = 1

SELECT * FROM [Set]
WHERE GameID = 1 AND SetName LIKE '%Duelist Pack%'
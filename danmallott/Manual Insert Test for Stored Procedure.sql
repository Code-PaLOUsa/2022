USE StackOverflow;
GO

-- Test the dbo.InsertUser stored procedure, making sure to clean up after ourselves.
BEGIN TRANSACTION;

DECLARE @newUserId INT;

EXEC @newUserId = dbo.InsertUser 'Test', 'TestUser', 'Test', 'Test';

IF EXISTS (SELECT * FROM dbo.Users WHERE Id = @newUserId)
    PRINT 'Insert User test passed';
ELSE
    PRINT 'Insert User test failed';

ROLLBACK;

IF EXISTS (SELECT * FROM dbo.Users WHERE Id = @newUserId)
    PRINT 'ALERT! Clean up from Insert User stored procedure failed! Make sure to delete user with Id: ' + CAST(@newUserId AS VARCHAR(7));
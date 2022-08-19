USE StackOverflow;
GO

CREATE OR ALTER PROCEDURE SampleTests.[Test That Data Inserts Correctly to Users Table]
AS
BEGIN
    -- Arrange
    EXEC tSQLt.FakeTable @TableName = 'dbo.Users', @Identity = 1;
    DECLARE @newUserId INT;

    -- Act
    EXEC @newUserId = dbo.InsertUser 'Test', 'Test', 'Test', 'Test';

    -- Assert
    DECLARE @countInTable INT = (SELECT COUNT(1) FROM dbo.Users);
    EXEC tSQLt.AssertEquals 1, @countInTable, 'Did not insert row';
    EXEC tSQLt.AssertEquals 1, @newUserId, 'Did not insert new Id';
END;
GO


EXEC tSQLt.RunAll;
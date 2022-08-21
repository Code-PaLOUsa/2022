-- https://www.tsqlt.org

-- EXEC tSQLt.NewTestClass 'SampleTests';
-- GO

CREATE OR ALTER PROCEDURE SampleTests.[Test Negative User Id Throws Error]
AS
BEGIN
    EXEC tSQLt.ExpectException 
        @ExpectedMessage = 'User Id must be a positive number', 
        @ExpectedSeverity = NULL, 
        @ExpectedState = 1;
    EXEC dbo.IncrementReputation -1, 10;
END;
GO

EXEC tSQLt.RunAll;
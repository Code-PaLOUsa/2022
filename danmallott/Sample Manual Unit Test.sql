USE StackOverflow;
GO

-- Test That Our Stored Procedure Returns the Proper Error Message
-- With Negative User Id
BEGIN TRY
    EXEC dbo.IncrementReputation -1, 10;
END TRY
BEGIN CATCH
    DECLARE @errorMessage VARCHAR(100);
    SET @errorMessage = ERROR_MESSAGE();
    IF @errorMessage <> 'User Id must be a positive number'
        THROW 55000, 'Error Message not produced for negative User Id', 1;
    PRINT 'Negative User Id Test Passed';
END CATCH;
GO
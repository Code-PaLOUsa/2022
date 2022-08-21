USE StackOverflow;
GO

-- Test that our Stored Procedure returns the proper error message
-- when NULL @displayName parameter is passed
BEGIN TRY
    EXEC dbo.InsertUser NULL, NULL, NULL, NULL;
END TRY
BEGIN CATCH
    DECLARE @errorMessage VARCHAR(100);
    SET @errorMessage = ERROR_MESSAGE();
    IF @errorMessage <> '@displayName must not be null'
        THROW 55000, 'Error Message not produced for NULL @displayName', 1;
    PRINT 'NULL @displayName Parameter Test Passed';
END CATCH;
GO
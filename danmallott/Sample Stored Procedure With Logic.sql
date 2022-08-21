USE StackOverflow;
GO

CREATE OR ALTER PROCEDURE dbo.IncrementReputation
    @userId INT, -- User Id (i.e., the Id column on the dbo.Users table)
    @reputationAdjustmentAmount INT -- Amount to increment or decrement reputation by
AS
BEGIN
    SET NOCOUNT ON;

    -- Perform some basic validation
    -- Only non-null values for Id are allowed
    IF @userId IS NULL
        THROW 50001, 'User Id must not be null', 1;

    -- Only non-null values for Reputation Adjustment Amount are allowed
    IF @reputationAdjustmentAmount IS NULL
        THROW 50002, 'Reputation Adjustment Amount must not be null', 1;

    -- Only positive values for Id are allowed
    IF @userId < 1
        THROW 50003, 'User Id must be a positive number', 1;

    DECLARE @currentReputation INT = (
        SELECT Reputation
        FROM dbo.Users
        WHERE Id = @userId
    );

    -- If no reputation was returned, the user doesn't exist
    IF @currentReputation IS NULL
    BEGIN
        DECLARE @userNotExistsMessage VARCHAR(500) = 'User  with id: ' + (CAST(@userId AS VARCHAR(10))) + ' does not exist';
        THROW 50004, @userNotExistsMessage, 1;
    END;

    -- If the reputation adjustment is negative and would take the user negative, adjust that (and message the user)
    IF @reputationAdjustmentAmount < 0 AND (@currentReputation + @reputationAdjustmentAmount) < 0
    BEGIN
        PRINT 'Unable to reduce user''s reputation by ' + (CAST(@reputationAdjustmentAmount AS VARCHAR(10))) + '. Setting to 1 instead';
        SET @reputationAdjustmentAmount = -(@currentReputation - 1);
    END;

    -- Process the update itself
    UPDATE dbo.Users
    SET Reputation = Reputation + @reputationAdjustmentAmount
    WHERE Id = @userId;
END;
GO
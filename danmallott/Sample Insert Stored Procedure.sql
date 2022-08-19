USE StackOverflow;
GO

CREATE OR ALTER PROCEDURE dbo.InsertUser
    @aboutMe NVARCHAR(MAX),
    @displayName NVARCHAR(40),
    @location NVARCHAR(100),
    @websiteUrl NVARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON;

    -- Perform basic validation on required field
    IF (@displayName IS NULL)
        THROW 50001, '@displayName must not be null', 1;
    
    -- Insert new row, capturing the new identity
    INSERT INTO dbo.Users
    (AboutMe, Age, CreationDate, DisplayName, DownVotes, EmailHash, LastAccessDate, [Location], Reputation, UpVotes, Views, WebsiteUrl, AccountId)
    VALUES
    (@aboutMe, NULL, GETDATE(), @displayName, 0, NULL, GETDATE(), @location, 1, 0, 0, @websiteUrl, NULL);

    RETURN SCOPE_IDENTITY();
END;
GO

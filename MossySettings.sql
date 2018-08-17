USE [Mossy]
GO
DECLARE @Environment varchar(5)='Dev'
DECLARE @ApplicationName varchar(200) = 'Html Dashboard'
DECLARE @CanvasHeight int = 400
DECLARE @CanvasWidth int = 600
DECLARE @HashName varchar(200)='Ofa.Moss.DashboardWebParts.Html5'
DECLARE @WebApiWrapperUrl varchar(200)

DECLARE @DebtTicketViewerUrl varchar(200)
DECLARE @DTLibDataProviderUrl varchar(200)

/*Borrowing Profile Settings*/
DECLARE @BorrowingProfileApplicationName varchar(200)='Borrowing Profile'
DECLARE @BorrowingProfileApplicationRoot varchar(200)='/CEODashboards/BorrowingProfile'
DECLARE @BorrowingProfileDataProviderUrl varchar(200)

/* Environment specific settings */
IF(@Environment='Dev')
BEGIN
	SET @DebtTicketViewerUrl ='https://localhost/WebTicket/DebtTicket.aspx'
	SET @DTLibDataProviderUrl ='https://localhost/ITSService/DTLibProvider.svc'
	SET @WebApiWrapperUrl ='https://localhost/WebApiWrapper'
	SET @BorrowingProfileDataProviderUrl ='https://localhost/ITSService/BorrowingProfileProvider.svc'
END
/* SET TEST VALUES
IF(@Environment='Test')
	BEGIN

	END
*/ 

/* SET PROD VALUES
IF(@Environment='Prod')
	BEGIN

	END
*/
EXEC SetApplicationSetting @HashName, 'Application Name', @ApplicationName
EXEC SetApplicationSetting @HashName, 'CanvasHeight', @CanvasHeight
EXEC SetApplicationSetting @HashName, 'CanvasWidth', @CanvasWidth
EXEC SetApplicationSetting @HashName, 'WebApiWrapperUrl', @WebApiWrapperUrl
EXEC SetApplicationSetting @HashName, 'DebtTicketViewerUrl', @DebtTicketViewerUrl
EXEC SetApplicationSetting @HashName, 'DTLibDataProviderUrl', @DTLibDataProviderUrl
EXEC SetApplicationSetting @HashName, 'BorrowingProfile.ApplicationName', @BorrowingProfileApplicationName
EXEC SetApplicationSetting @HashName, 'BorrowingProfileApplicationRoot', @BorrowingProfileApplicationRoot
EXEC SetApplicationSetting @HashName, 'BorrowingProfileDataProviderUrl', @BorrowingProfileDataProviderUrl
PRINT 'All settings were setup successfully in Mossy db'
SELECT * FROM ApplicationSettings WHERE HashName = @HashName
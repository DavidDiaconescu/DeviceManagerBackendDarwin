USE DeviceManagementDB;

IF NOT EXISTS (SELECT 1 FROM Users WHERE Email = 'andrei.pop@devicemgmt.ro')
BEGIN
    INSERT INTO Users (Name, Email, PasswordHash, Role, Location)
    VALUES (N'Andrei Pop', 'andrei.pop@devicemgmt.ro', 'hashed_password_here', 'Admin', N'Cluj-Napoca');
END

IF NOT EXISTS (SELECT 1 FROM Users WHERE Email = 'ioana.muresan@devicemgmt.ro')
BEGIN
    INSERT INTO Users (Name, Email, PasswordHash, Role, Location)
    VALUES (N'Ioana Mureșan', 'ioana.muresan@devicemgmt.ro', 'hashed_password_here', 'Developer', N'Cluj-Napoca');
END

IF NOT EXISTS (SELECT 1 FROM Users WHERE Email = 'mihai.ionescu@devicemgmt.ro')
BEGIN
    INSERT INTO Users (Name, Email, PasswordHash, Role, Location)
    VALUES (N'Mihai Ionescu', 'mihai.ionescu@devicemgmt.ro', 'hashed_password_here', 'QA', 'Bucharest');
END

IF NOT EXISTS (SELECT 1 FROM Users WHERE Email = 'elena.Constantin@devicemgmt.ro')
BEGIN
    INSERT INTO Users (Name, Email, PasswordHash, Role, Location)
    VALUES (N'Elena Constantin', 'elena.Constantin@devicemgmt.ro', 'hashed_password_here', 'Manager', 'Bucharest');
END

IF NOT EXISTS (SELECT 1 FROM Users WHERE Email = 'radu.stan@devicemgmt.ro')
BEGIN
    INSERT INTO Users (Name, Email, PasswordHash, Role, Location)
    VALUES (N'Radu Stan', 'radu.stan@devicemgmt.ro', 'hashed_password_here', 'Developer', N'Cluj-Napoca');
END

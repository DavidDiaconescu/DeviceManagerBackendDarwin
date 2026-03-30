IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'DeviceManagementDB')
BEGIN
    CREATE DATABASE DeviceManagementDB;
END

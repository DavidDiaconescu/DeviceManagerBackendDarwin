USE DeviceManagementDB;

-- Apple iPhone 15 Pro → assigned to user 1
IF NOT EXISTS (SELECT 1 FROM Devices WHERE Name = 'iPhone 15 Pro' AND Manufacturer = 'Apple')
BEGIN
    INSERT INTO Devices (Name, Manufacturer, Type, OperatingSystem, OSVersion, Processor, RAM, Description, AssignedUserId)
    VALUES ('iPhone 15 Pro', 'Apple', 'Phone', 'iOS', '17.4', 'Apple A17 Pro', 8, 'Flagship smartphone with titanium frame', 1);
END

-- Samsung Galaxy S24 Ultra → assigned to user 2
IF NOT EXISTS (SELECT 1 FROM Devices WHERE Name = 'Galaxy S24 Ultra' AND Manufacturer = 'Samsung')
BEGIN
    INSERT INTO Devices (Name, Manufacturer, Type, OperatingSystem, OSVersion, Processor, RAM, Description, AssignedUserId)
    VALUES ('Galaxy S24 Ultra', 'Samsung', 'Phone', 'Android', '14.0', 'Snapdragon 8 Gen 3', 12, 'Premium Android flagship with S Pen', 2);
END

-- Google Pixel 8 Pro → assigned to user 3
IF NOT EXISTS (SELECT 1 FROM Devices WHERE Name = 'Pixel 8 Pro' AND Manufacturer = 'Google')
BEGIN
    INSERT INTO Devices (Name, Manufacturer, Type, OperatingSystem, OSVersion, Processor, RAM, Description, AssignedUserId)
    VALUES ('Pixel 8 Pro', 'Google', 'Phone', 'Android', '14.0', 'Google Tensor G3', 12, 'Pure Android experience with AI features', 3);
END

-- Huawei Mate 60 Pro → unassigned
IF NOT EXISTS (SELECT 1 FROM Devices WHERE Name = 'Mate 60 Pro' AND Manufacturer = 'Huawei')
BEGIN
    INSERT INTO Devices (Name, Manufacturer, Type, OperatingSystem, OSVersion, Processor, RAM, Description, AssignedUserId)
    VALUES ('Mate 60 Pro', 'Huawei', 'Phone', 'HarmonyOS', '4.0', 'Kirin 9000S', 12, 'Huawei flagship with satellite connectivity', NULL);
END

-- Apple iPad Pro 13 → unassigned
IF NOT EXISTS (SELECT 1 FROM Devices WHERE Name = 'iPad Pro 13' AND Manufacturer = 'Apple')
BEGIN
    INSERT INTO Devices (Name, Manufacturer, Type, OperatingSystem, OSVersion, Processor, RAM, Description, AssignedUserId)
    VALUES ('iPad Pro 13', 'Apple', 'Tablet', 'iPadOS', '17.4', 'Apple M2', 16, 'Professional tablet with Liquid Retina XDR display', NULL);
END

-- Samsung Galaxy Tab S9+ → assigned to user 1
IF NOT EXISTS (SELECT 1 FROM Devices WHERE Name = 'Galaxy Tab S9+' AND Manufacturer = 'Samsung')
BEGIN
    INSERT INTO Devices (Name, Manufacturer, Type, OperatingSystem, OSVersion, Processor, RAM, Description, AssignedUserId)
    VALUES ('Galaxy Tab S9+', 'Samsung', 'Tablet', 'Android', '13.0', 'Snapdragon 8 Gen 2', 12, 'Android flagship tablet with AMOLED display', 1);
END

-- Google Pixel Tablet → unassigned
IF NOT EXISTS (SELECT 1 FROM Devices WHERE Name = 'Pixel Tablet' AND Manufacturer = 'Google')
BEGIN
    INSERT INTO Devices (Name, Manufacturer, Type, OperatingSystem, OSVersion, Processor, RAM, Description, AssignedUserId)
    VALUES ('Pixel Tablet', 'Google', 'Tablet', 'Android', '14.0', 'Google Tensor G2', 8, 'Android tablet with charging speaker dock', NULL);
END

-- Huawei MatePad Pro 13.2 → unassigned
IF NOT EXISTS (SELECT 1 FROM Devices WHERE Name = 'MatePad Pro 13.2' AND Manufacturer = 'Huawei')
BEGIN
    INSERT INTO Devices (Name, Manufacturer, Type, OperatingSystem, OSVersion, Processor, RAM, Description, AssignedUserId)
    VALUES ('MatePad Pro 13.2', 'Huawei', 'Tablet', 'HarmonyOS', '4.0', 'Kirin 9000S', 12, 'Premium tablet with OLED display and M-Pencil support', NULL);
END

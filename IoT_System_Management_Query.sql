/* 
	Project Name: IoT Device Management System
	Project Author: Mohamed Tawfiq Cassim
	Project Objective: Create and Manage the projects database
*/

--Creating the database
CREATE DATABASE IoTAssetManagementSystemDB;
PRINT 'Database created successfully';

--Using the database
USE IoTAssetManagementSystemDB;

--Creating the tables
--DeviceType 
CREATE TABLE DeviceTypes
(
	DeviceTypeID INT IDENTITY (1,1),
	Name NVARCHAR (100) NOT NULL,
	Description NVARCHAR (250) NULL,
	SupportedFeatures NVARCHAR (250) NULL,

	CONSTRAINT PK_DeviceTypes
		PRIMARY KEY (DeviceTypeID)
);

--Firmware
CREATE TABLE Firmware
(
	FirmwareID INT IDENTITY (1,1),
	Version NVARCHAR(20) NOT NULL,
	ReleaseDate DATE NOT NULL,
	Description NVARCHAR(250) NULL,

	CONSTRAINT PK_Firmware
        PRIMARY KEY (FirmwareID),
	--No Versions should be the duplicated
    CONSTRAINT UN_Firmware_Version
        UNIQUE (Version)
);

--Groups
CREATE TABLE Groups
(
    GroupID INT IDENTITY(1,1),
    GroupName NVARCHAR(100) NOT NULL,
    ParentGroupID INT NULL,
    Description NVARCHAR(250) NULL,

    CONSTRAINT PK_Groups
        PRIMARY KEY (GroupID),

    CONSTRAINT FK_Groups_ParentGroup
        FOREIGN KEY (ParentGroupID)
        REFERENCES Groups(GroupID)
);

--Devices
CREATE TABLE Devices
(
    DeviceID INT IDENTITY(1,1),
    DeviceName NVARCHAR(100) NOT NULL,
    SerialNumber NVARCHAR(100) NOT NULL,
    DeviceTypeID INT NOT NULL,
    FirmwareID INT NOT NULL,
    GroupID INT NOT NULL,

    CONSTRAINT PK_Devices
        PRIMARY KEY (DeviceID),
	--No serial number should be the same with different devices
    CONSTRAINT UN_Devices_SerialNumber
        UNIQUE (SerialNumber),

    CONSTRAINT FK_Devices_DeviceTypes
        FOREIGN KEY (DeviceTypeID)
        REFERENCES DeviceTypes(DeviceTypeID),

    CONSTRAINT FK_Devices_Firmware
        FOREIGN KEY (FirmwareID)
        REFERENCES Firmware(FirmwareID),

    CONSTRAINT FK_Devices_Groups
        FOREIGN KEY (GroupID)
        REFERENCES Groups(GroupID)
);
--No additional indexing because the project is relatively small, but still keeps efficiency and effectiveness. 


--Seeding Data in Tables
--DeviceTypes
INSERT INTO DeviceTypes
(
    Name,
    Description,
    SupportedFeatures
)
VALUES
	('GPS Asset Tracker',
	'Portable GPS tracking device.',
	'Real-time GPS, Geofencing, Accelerometer'),

	('Cold Chain Sensor',
	'Temperature monitoring device.',
	'Temperature Monitoring, Alerts'),

	('Fleet Tracker',
	'Vehicle tracking device.',
	'GPS, Ignition Monitoring, Driver Behaviour'),

	('Environmental Sensor',
	'Environmental monitoring device.',
	'Temperature, Humidity, Pressure');

--Firmware
INSERT INTO Firmware
(
    Version,
    ReleaseDate,
    Description
)
VALUES
	('1.0.0','2025-01-15','Initial firmware release.'),

	('1.1.0','2025-03-01','Performance improvements.'),

	('2.0.0','2025-06-20','Major feature update.'),

	('2.1.0','2025-10-05','Bug fixes and stability improvements.');

--Groups
INSERT INTO Groups 
(
	GroupName, 
	ParentGroupID, 
	Description
)
VALUES
	('Global Operations', NULL, 'Top level organisation'),

	('South Africa', 1, 'South African operations'),

	('Johannesburg', 2, 'Johannesburg branch'),

	('Warehouse A', 3, 'Johannesburg warehouse'),

	('Fleet Division', 3, 'Fleet operations'),

	('Cape Town', 2, 'Cape Town branch'),

	('Warehouse B', 6, 'Cape Town warehouse');

--Devices
INSERT INTO Devices
(
    DeviceName,
    SerialNumber,
    DeviceTypeID,
    FirmwareID,
    GroupID
)
VALUES
	('GPS Tracker JT001', 'DM-GPS-0001', 1, 3, 5),

	('Freezer Sensor A', 'DM-TEMP-0001', 2, 2, 4),

	('Truck Tracker 17', 'DM-FLEET-0017', 3, 3, 5),

	('Climate Sensor WHB', 'DM-ENV-0004', 4, 4, 7),

	('GPS Tracker JT002', 'DM-GPS-0002', 1, 3, 5);

--Testing Database Entries
SELECT * FROM DeviceTypes;
SELECT * FROM Firmware;
SELECT * FROM Groups;
SELECT * FROM Devices;

SELECT
    d.DeviceName,
    d.SerialNumber,
    dt.Name AS DeviceType,
    f.Version AS FirmwareVersion,
    g.GroupName
FROM Devices d
INNER JOIN DeviceTypes dt
    ON d.DeviceTypeID = dt.DeviceTypeID
INNER JOIN Firmware f
    ON d.FirmwareID = f.FirmwareID
INNER JOIN Groups g
    ON d.GroupID = g.GroupID;
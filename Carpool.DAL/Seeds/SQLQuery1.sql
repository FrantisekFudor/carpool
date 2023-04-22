USE Carpool
drop table if exists dbo.Passenger;
drop table if exists dbo.Cars;
drop table if exists dbo.Rides;
drop table if exists dbo.Users;

CREATE TABLE [dbo].[Users] (
    [Id]       UNIQUEIDENTIFIER NOT NULL,
    [Name]     NVARCHAR (MAX)   NOT NULL,
    [Surname]  NVARCHAR (MAX)   NOT NULL,
    [PhotoUrl] NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
);
CREATE TABLE [dbo].[Cars] (
    [Id]                      UNIQUEIDENTIFIER NOT NULL,
    [Manufacturer]            NVARCHAR (MAX)   NOT NULL,
    [Type]                    NVARCHAR (MAX)   NOT NULL,
    [DateOfFirstRegistration] DATETIME2 (7)    NOT NULL,
    [PhotoUrl]                NVARCHAR (MAX)   NULL,
    [Capacity]                INT              NOT NULL,
    [OwnerId]                 UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Cars] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Cars_Users_OwnerId] FOREIGN KEY ([OwnerId]) REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE
);
CREATE TABLE [dbo].[Rides] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [Start]         NVARCHAR (MAX)   NOT NULL,
    [Destination]   NVARCHAR (MAX)   NOT NULL,
    [DepartureTime] DATETIME2 (7)    NOT NULL,
    [Duration]      TIME (7)         NULL,
    [DriverId]      UNIQUEIDENTIFIER NOT NULL,
    [CarId]         UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Rides] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Rides_Cars_CarId] FOREIGN KEY ([CarId]) REFERENCES [dbo].[Cars] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Rides_Users_DriverId] FOREIGN KEY ([DriverId]) REFERENCES [dbo].[Users] ([Id])
);
CREATE TABLE [dbo].[Passengers] (
    [Id]     UNIQUEIDENTIFIER NOT NULL,
    [UserId] UNIQUEIDENTIFIER NOT NULL,
    [RideId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Passengers] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Passengers_Rides_RideId] FOREIGN KEY ([RideId]) REFERENCES [dbo].[Rides] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Passengers_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]) 
);

INSERT INTO dbo.Users (Id, Name, Surname, PhotoUrl)
VALUES ('9A0DCDBC-664A-4C54-86E6-BACF71896233' , 'Jan' , 'Novak' , 'https://www.clipartmax.com/png/full/258-2582267_circled-user-male-skin-type-1-2-icon-male-user-icon.png' ),
       ('544DF3F2-5105-4CF8-8826-F5ED03CB3E80' , 'Karel' , 'Polacek' , 'https://www.clipartmax.com/png/full/258-2582267_circled-user-male-skin-type-1-2-icon-male-user-icon.png' ),
       ('ABCDF3F2-5105-4CF8-8826-F5ED03CB3E80' , 'Ota' , 'Toman' , 'https://www.clipartmax.com/png/full/258-2582267_circled-user-male-skin-type-1-2-icon-male-user-icon.png' ),
       ('123DF3F2-5105-4CF8-8826-F5ED03CB3E80' , 'Pavel' , 'Zavodnik' , 'https://www.clipartmax.com/png/full/258-2582267_circled-user-male-skin-type-1-2-icon-male-user-icon.png' );

INSERT INTO dbo.Cars (Id, Manufacturer, Type, DateOfFirstRegistration, PhotoUrl, Capacity, OwnerId)
VALUES ('35D5F09D-3C86-45E5-AB96-3BEE88C5B0D9', 'Skoda', 'Superb', '2015-04-14', 'https://cdn-icons-png.flaticon.com/512/741/741407.png', '5', '9A0DCDBC-664A-4C54-86E6-BACF71896233'), 
       ('844F2F29-4A22-48C8-9659-24A236EED9D2', 'Tesla', 'S', '2015-04-14', 'https://cdn-icons-png.flaticon.com/512/741/741407.png', '5', '9A0DCDBC-664A-4C54-86E6-BACF71896233'), 
       ('3B70CEF4-9B4C-4F54-A30D-E6862409A568', 'BMW', 'M3', '2015-04-14', 'https://cdn-icons-png.flaticon.com/512/741/741407.png', '4', '544DF3F2-5105-4CF8-8826-F5ED03CB3E80');

INSERT INTO dbo.Rides (Id, Start, Destination, DepartureTime, Duration, DriverId, CarId)
VALUES ('F7B82F46-32C9-4DD6-BC20-E3C3B4461F64', 'Brno', 'Praha', '2022-05-03 08:45:00', '03:15:00', '9A0DCDBC-664A-4C54-86E6-BACF71896233', '35D5F09D-3C86-45E5-AB96-3BEE88C5B0D9'),
       ('C23119C2-77F5-4409-9388-4AC4476103A0', 'Ostrava', 'Ceske Budejovice', '2022-05-03 08:45:00', '05:30:00', '9A0DCDBC-664A-4C54-86E6-BACF71896233', '844F2F29-4A22-48C8-9659-24A236EED9D2'),
       ('C1D69238-CE88-4901-99E7-144D585E6CFD', 'Olomouc', 'Trinec', '2022-05-03 08:45:00', '01:30:00', '544DF3F2-5105-4CF8-8826-F5ED03CB3E80', '3B70CEF4-9B4C-4F54-A30D-E6862409A568');

INSERT INTO dbo.Passengers (Id, UserId, RideId)
VALUES ('B3D16028-5B07-46C5-BFC7-31CBC02F9580','544DF3F2-5105-4CF8-8826-F5ED03CB3E80','F7B82F46-32C9-4DD6-BC20-E3C3B4461F64'),
       ('F412DF9F-67A1-4C3D-91CA-F69994FA6774','ABCDF3F2-5105-4CF8-8826-F5ED03CB3E80','F7B82F46-32C9-4DD6-BC20-E3C3B4461F64'), 
       ('6B578F17-196C-4EC9-83EA-2858DF654120','ABCDF3F2-5105-4CF8-8826-F5ED03CB3E80','C23119C2-77F5-4409-9388-4AC4476103A0'), 
       ('E9883F70-2EAE-4822-91FF-E313947634A9','ABCDF3F2-5105-4CF8-8826-F5ED03CB3E80','C1D69238-CE88-4901-99E7-144D585E6CFD');
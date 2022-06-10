CREATE DATABASE [CarRental]
GO

USE [CarRental]
GO
/****** Object:  Table [dbo].[Accessories]    Script Date: 6/10/2022 1:15:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accessories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Accessories_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 6/10/2022 1:15:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Telephone] [nchar](20) NULL,
	[Email] [nvarchar](50) NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EngineTypes]    Script Date: 6/10/2022 1:15:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EngineTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_EngineTypes_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Maintenances]    Script Date: 6/10/2022 1:15:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Maintenances](
	[Vehicle] [int] NOT NULL,
	[MaintenanceEnd] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Maintenance] PRIMARY KEY CLUSTERED 
(
	[Vehicle] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Manufacturers]    Script Date: 6/10/2022 1:15:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Manufacturers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Manufacturers_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RentVehicles]    Script Date: 6/10/2022 1:15:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RentVehicles](
	[Vehicle] [int] NOT NULL,
	[RentFrom] [datetime2](7) NOT NULL,
	[RentTo] [datetime2](7) NOT NULL,
	[Customer] [int] NOT NULL,
 CONSTRAINT [PK_RentVehicles_1] PRIMARY KEY CLUSTERED 
(
	[Vehicle] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TransmissionTypes]    Script Date: 6/10/2022 1:15:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransmissionTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_TransmissionTypes_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VehicleAccessories]    Script Date: 6/10/2022 1:15:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VehicleAccessories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VehicleId] [int] NOT NULL,
	[AccessoryId] [int] NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_VehicleAccessories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VehicleCategories]    Script Date: 6/10/2022 1:15:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VehicleCategories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](10) NOT NULL,
	[DumpLoad] [bit] NOT NULL,
	[TailLift] [bit] NOT NULL,
	[ExtraDoors] [int] NOT NULL,
 CONSTRAINT [PK_VehicleCategory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vehicles]    Script Date: 6/10/2022 1:15:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vehicles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Manufacturer] [int] NOT NULL,
	[Engine] [int] NOT NULL,
	[Transmission] [int] NOT NULL,
	[Category] [int] NOT NULL,
 CONSTRAINT [PK_Vehicles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Accessories] ON 
GO
INSERT [dbo].[Accessories] ([Id], [Name]) VALUES (180, N'GPS')
GO
INSERT [dbo].[Accessories] ([Id], [Name]) VALUES (181, N'AirConditioned')
GO
INSERT [dbo].[Accessories] ([Id], [Name]) VALUES (182, N'Radio')
GO
SET IDENTITY_INSERT [dbo].[Accessories] OFF
GO
SET IDENTITY_INSERT [dbo].[Customers] ON 
GO
INSERT [dbo].[Customers] ([Id], [Name], [Telephone], [Email]) VALUES (1, N'Francesco Betti', N'0393476114507       ', NULL)
GO
INSERT [dbo].[Customers] ([Id], [Name], [Telephone], [Email]) VALUES (2, N'John Doe', NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Customers] OFF
GO
SET IDENTITY_INSERT [dbo].[EngineTypes] ON 
GO
INSERT [dbo].[EngineTypes] ([Id], [Name]) VALUES (185, N'Petrol')
GO
INSERT [dbo].[EngineTypes] ([Id], [Name]) VALUES (186, N'Diesel')
GO
INSERT [dbo].[EngineTypes] ([Id], [Name]) VALUES (187, N'Electric')
GO
SET IDENTITY_INSERT [dbo].[EngineTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[Manufacturers] ON 
GO
INSERT [dbo].[Manufacturers] ([Id], [Name]) VALUES (341, N'Toyota')
GO
INSERT [dbo].[Manufacturers] ([Id], [Name]) VALUES (342, N'Volkswagen')
GO
INSERT [dbo].[Manufacturers] ([Id], [Name]) VALUES (343, N'BMW')
GO
INSERT [dbo].[Manufacturers] ([Id], [Name]) VALUES (344, N'Mercedes')
GO
INSERT [dbo].[Manufacturers] ([Id], [Name]) VALUES (345, N'Ford')
GO
INSERT [dbo].[Manufacturers] ([Id], [Name]) VALUES (346, N'Fiat')
GO
SET IDENTITY_INSERT [dbo].[Manufacturers] OFF
GO
SET IDENTITY_INSERT [dbo].[TransmissionTypes] ON 
GO
INSERT [dbo].[TransmissionTypes] ([Id], [Name]) VALUES (126, N'Auto')
GO
INSERT [dbo].[TransmissionTypes] ([Id], [Name]) VALUES (127, N'Manual')
GO
SET IDENTITY_INSERT [dbo].[TransmissionTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[VehicleAccessories] ON 
GO
INSERT [dbo].[VehicleAccessories] ([Id], [VehicleId], [AccessoryId], [Status]) VALUES (521, 242, 180, 4)
GO
INSERT [dbo].[VehicleAccessories] ([Id], [VehicleId], [AccessoryId], [Status]) VALUES (522, 242, 182, 5)
GO
INSERT [dbo].[VehicleAccessories] ([Id], [VehicleId], [AccessoryId], [Status]) VALUES (523, 243, 180, 4)
GO
INSERT [dbo].[VehicleAccessories] ([Id], [VehicleId], [AccessoryId], [Status]) VALUES (524, 243, 181, 3)
GO
INSERT [dbo].[VehicleAccessories] ([Id], [VehicleId], [AccessoryId], [Status]) VALUES (525, 244, 182, 5)
GO
INSERT [dbo].[VehicleAccessories] ([Id], [VehicleId], [AccessoryId], [Status]) VALUES (526, 244, 181, 4)
GO
INSERT [dbo].[VehicleAccessories] ([Id], [VehicleId], [AccessoryId], [Status]) VALUES (527, 245, 180, 5)
GO
INSERT [dbo].[VehicleAccessories] ([Id], [VehicleId], [AccessoryId], [Status]) VALUES (528, 245, 181, 2)
GO
INSERT [dbo].[VehicleAccessories] ([Id], [VehicleId], [AccessoryId], [Status]) VALUES (529, 246, 182, 4)
GO
INSERT [dbo].[VehicleAccessories] ([Id], [VehicleId], [AccessoryId], [Status]) VALUES (530, 246, 181, 3)
GO
INSERT [dbo].[VehicleAccessories] ([Id], [VehicleId], [AccessoryId], [Status]) VALUES (531, 247, 180, 5)
GO
INSERT [dbo].[VehicleAccessories] ([Id], [VehicleId], [AccessoryId], [Status]) VALUES (532, 247, 181, 2)
GO
INSERT [dbo].[VehicleAccessories] ([Id], [VehicleId], [AccessoryId], [Status]) VALUES (533, 248, 180, 3)
GO
INSERT [dbo].[VehicleAccessories] ([Id], [VehicleId], [AccessoryId], [Status]) VALUES (534, 248, 182, 4)
GO
INSERT [dbo].[VehicleAccessories] ([Id], [VehicleId], [AccessoryId], [Status]) VALUES (535, 248, 181, 3)
GO
INSERT [dbo].[VehicleAccessories] ([Id], [VehicleId], [AccessoryId], [Status]) VALUES (536, 249, 180, 1)
GO
INSERT [dbo].[VehicleAccessories] ([Id], [VehicleId], [AccessoryId], [Status]) VALUES (537, 249, 181, 4)
GO
INSERT [dbo].[VehicleAccessories] ([Id], [VehicleId], [AccessoryId], [Status]) VALUES (538, 250, 180, 5)
GO
INSERT [dbo].[VehicleAccessories] ([Id], [VehicleId], [AccessoryId], [Status]) VALUES (539, 250, 181, 5)
GO
INSERT [dbo].[VehicleAccessories] ([Id], [VehicleId], [AccessoryId], [Status]) VALUES (540, 250, 182, 4)
GO
INSERT [dbo].[VehicleAccessories] ([Id], [VehicleId], [AccessoryId], [Status]) VALUES (541, 251, 180, 5)
GO
INSERT [dbo].[VehicleAccessories] ([Id], [VehicleId], [AccessoryId], [Status]) VALUES (542, 251, 181, 5)
GO
INSERT [dbo].[VehicleAccessories] ([Id], [VehicleId], [AccessoryId], [Status]) VALUES (543, 251, 182, 5)
GO
SET IDENTITY_INSERT [dbo].[VehicleAccessories] OFF
GO
SET IDENTITY_INSERT [dbo].[VehicleCategories] ON 
GO
INSERT [dbo].[VehicleCategories] ([Id], [Name], [DumpLoad], [TailLift], [ExtraDoors]) VALUES (137, N'Car       ', 0, 0, 0)
GO
INSERT [dbo].[VehicleCategories] ([Id], [Name], [DumpLoad], [TailLift], [ExtraDoors]) VALUES (138, N'Van       ', 0, 0, 0)
GO
INSERT [dbo].[VehicleCategories] ([Id], [Name], [DumpLoad], [TailLift], [ExtraDoors]) VALUES (139, N'Bus       ', 0, 0, 3)
GO
INSERT [dbo].[VehicleCategories] ([Id], [Name], [DumpLoad], [TailLift], [ExtraDoors]) VALUES (140, N'Truck     ', 1, 1, 0)
GO
SET IDENTITY_INSERT [dbo].[VehicleCategories] OFF
GO
SET IDENTITY_INSERT [dbo].[Vehicles] ON 
GO
INSERT [dbo].[Vehicles] ([Id], [Name], [Manufacturer], [Engine], [Transmission], [Category]) VALUES (242, N'BMW Van', 343, 186, 126, 138)
GO
INSERT [dbo].[Vehicles] ([Id], [Name], [Manufacturer], [Engine], [Transmission], [Category]) VALUES (243, N'Volkswagen Van', 342, 185, 127, 138)
GO
INSERT [dbo].[Vehicles] ([Id], [Name], [Manufacturer], [Engine], [Transmission], [Category]) VALUES (244, N'Ford Van', 345, 185, 127, 138)
GO
INSERT [dbo].[Vehicles] ([Id], [Name], [Manufacturer], [Engine], [Transmission], [Category]) VALUES (245, N'Mercedes Bus', 344, 185, 127, 139)
GO
INSERT [dbo].[Vehicles] ([Id], [Name], [Manufacturer], [Engine], [Transmission], [Category]) VALUES (246, N'Fiat Bus', 344, 186, 126, 139)
GO
INSERT [dbo].[Vehicles] ([Id], [Name], [Manufacturer], [Engine], [Transmission], [Category]) VALUES (247, N'Mercedes Truck', 344, 185, 127, 140)
GO
INSERT [dbo].[Vehicles] ([Id], [Name], [Manufacturer], [Engine], [Transmission], [Category]) VALUES (248, N'BMW Truck', 343, 186, 126, 140)
GO
INSERT [dbo].[Vehicles] ([Id], [Name], [Manufacturer], [Engine], [Transmission], [Category]) VALUES (249, N'Toyota Car', 341, 185, 126, 137)
GO
INSERT [dbo].[Vehicles] ([Id], [Name], [Manufacturer], [Engine], [Transmission], [Category]) VALUES (250, N'Ford Car', 345, 186, 127, 137)
GO
INSERT [dbo].[Vehicles] ([Id], [Name], [Manufacturer], [Engine], [Transmission], [Category]) VALUES (251, N'Mercedes Car', 344, 186, 126, 137)
GO
SET IDENTITY_INSERT [dbo].[Vehicles] OFF
GO
/****** Object:  Index [IX_VehicleAccessories_1]    Script Date: 6/10/2022 1:15:40 PM ******/
ALTER TABLE [dbo].[VehicleAccessories] ADD  CONSTRAINT [IX_VehicleAccessories_1] UNIQUE NONCLUSTERED 
(
	[AccessoryId] ASC,
	[VehicleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[VehicleAccessories] ADD  CONSTRAINT [DF_VehicleAccessories_Status]  DEFAULT ((5)) FOR [Status]
GO
ALTER TABLE [dbo].[VehicleCategories] ADD  CONSTRAINT [DF_VehicleCategory_DumpLoad]  DEFAULT ((0)) FOR [DumpLoad]
GO
ALTER TABLE [dbo].[VehicleCategories] ADD  CONSTRAINT [DF_VehicleCategory_TailLift]  DEFAULT ((0)) FOR [TailLift]
GO
ALTER TABLE [dbo].[VehicleCategories] ADD  CONSTRAINT [DF_VehicleCategory_ExtraDoors]  DEFAULT ((0)) FOR [ExtraDoors]
GO
ALTER TABLE [dbo].[Maintenances]  WITH CHECK ADD  CONSTRAINT [FK_Maintenances_Vehicles] FOREIGN KEY([Vehicle])
REFERENCES [dbo].[Vehicles] ([Id])
GO
ALTER TABLE [dbo].[Maintenances] CHECK CONSTRAINT [FK_Maintenances_Vehicles]
GO
ALTER TABLE [dbo].[RentVehicles]  WITH CHECK ADD  CONSTRAINT [FK_RentVehicles_Customers] FOREIGN KEY([Customer])
REFERENCES [dbo].[Customers] ([Id])
GO
ALTER TABLE [dbo].[RentVehicles] CHECK CONSTRAINT [FK_RentVehicles_Customers]
GO
ALTER TABLE [dbo].[RentVehicles]  WITH CHECK ADD  CONSTRAINT [FK_RentVehicles_Vehicles] FOREIGN KEY([Vehicle])
REFERENCES [dbo].[Vehicles] ([Id])
GO
ALTER TABLE [dbo].[RentVehicles] CHECK CONSTRAINT [FK_RentVehicles_Vehicles]
GO
ALTER TABLE [dbo].[VehicleAccessories]  WITH CHECK ADD  CONSTRAINT [FK_VehicleAccessories_Accessories] FOREIGN KEY([AccessoryId])
REFERENCES [dbo].[Accessories] ([Id])
GO
ALTER TABLE [dbo].[VehicleAccessories] CHECK CONSTRAINT [FK_VehicleAccessories_Accessories]
GO
ALTER TABLE [dbo].[VehicleAccessories]  WITH CHECK ADD  CONSTRAINT [FK_VehicleAccessories_Vehicles] FOREIGN KEY([VehicleId])
REFERENCES [dbo].[Vehicles] ([Id])
GO
ALTER TABLE [dbo].[VehicleAccessories] CHECK CONSTRAINT [FK_VehicleAccessories_Vehicles]
GO
ALTER TABLE [dbo].[Vehicles]  WITH CHECK ADD  CONSTRAINT [FK_Vehicles_EngineTypes] FOREIGN KEY([Engine])
REFERENCES [dbo].[EngineTypes] ([Id])
GO
ALTER TABLE [dbo].[Vehicles] CHECK CONSTRAINT [FK_Vehicles_EngineTypes]
GO
ALTER TABLE [dbo].[Vehicles]  WITH CHECK ADD  CONSTRAINT [FK_Vehicles_Manufacturers] FOREIGN KEY([Manufacturer])
REFERENCES [dbo].[Manufacturers] ([Id])
GO
ALTER TABLE [dbo].[Vehicles] CHECK CONSTRAINT [FK_Vehicles_Manufacturers]
GO
ALTER TABLE [dbo].[Vehicles]  WITH CHECK ADD  CONSTRAINT [FK_Vehicles_TransmissionTypes] FOREIGN KEY([Transmission])
REFERENCES [dbo].[TransmissionTypes] ([Id])
GO
ALTER TABLE [dbo].[Vehicles] CHECK CONSTRAINT [FK_Vehicles_TransmissionTypes]
GO
ALTER TABLE [dbo].[Vehicles]  WITH CHECK ADD  CONSTRAINT [FK_Vehicles_VehicleCategories] FOREIGN KEY([Category])
REFERENCES [dbo].[VehicleCategories] ([Id])
GO
ALTER TABLE [dbo].[Vehicles] CHECK CONSTRAINT [FK_Vehicles_VehicleCategories]
GO
ALTER TABLE [dbo].[VehicleAccessories]  WITH CHECK ADD  CONSTRAINT [CK_VehicleAccessories] CHECK  (([Status]>=(1) AND [Status]<=(5)))
GO
ALTER TABLE [dbo].[VehicleAccessories] CHECK CONSTRAINT [CK_VehicleAccessories]
GO
/****** Object:  StoredProcedure [dbo].[sp_removeFromMaintenance]    Script Date: 6/10/2022 1:15:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Francesco Betti
-- Create date: 6/10/2022
-- Description:	Removes given vehicle from maintenance table, checking wether the vehicle exists and if it is under maintenance
-- =============================================
CREATE PROCEDURE [dbo].[sp_removeFromMaintenance]
	@vehicleName nvarchar(max)
AS
BEGIN
	declare @vehicleId int

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select @vehicleId = id from Vehicles v where v.Name = @vehicleName
	if @vehicleId is NULL
		THROW 50001, 'Vehicle not found', 1;
	
	IF (NOT EXISTS (select * from Maintenances where Vehicle = @vehicleId)) -- already under maintenance
		THROW 50002, 'Vehicle is not under maintenances', 1;

	delete from dbo.Maintenances where Vehicle = @vehicleId
	select * from Vehicles where id = @vehicleId
END
GO
/****** Object:  StoredProcedure [dbo].[sp_RentVehicle]    Script Date: 6/10/2022 1:15:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Francesco Betti
-- Create date: 6/10/2022
-- Description:	Rents the given vehicle for the given time range, checking if the customer is existing, if the vehicle is existing, 
--				if it is already rent or if it is under maintenance
-- =============================================
CREATE PROCEDURE [dbo].[sp_RentVehicle]
	@customer nvarchar(max), @vehicleName nvarchar(max), @dtFrom datetime2, @dtTo datetime2
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	declare @vehicleId int, @customerId int
	select @vehicleId = id from Vehicles v where v.Name = @vehicleName
	if @vehicleId is NULL
		THROW 50001, 'Vehicle not found', 1;

	select @customerId = id from Customers c where c.Name = @customer
	if @customerId  is NULL
		THROW 50010, 'Customer not found', 1;
    
	IF (EXISTS (SELECT * FROM RentVehicles rv WHERE rv.Vehicle = @vehicleId))
		THROW 50011, 'Vehicle already rent', 1;

	IF (EXISTS (SELECT * FROM Maintenances m WHERE m.Vehicle = @vehicleId AND (m.MaintenanceEnd BETWEEN @dtFrom AND @dtTo)))
		THROW 50012, 'Vehicle is under maintenance', 1;

	INSERT INTO RentVehicles (Customer, Vehicle, RentFrom, RentTo)  VALUES (@customerId, @vehicleId, @dtFrom, @dtTo)

	select * from Vehicles v where v.Name = @vehicleName
END
GO
/****** Object:  StoredProcedure [dbo].[sp_sendToMaintenance]    Script Date: 6/10/2022 1:15:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Francesco Betti
-- Create date: 6/10/2022
-- Description:	Set the given vehicle to maintenance until the given date, checking if the vehicle exists 
--				and if it is not already under maintenance
-- =============================================
CREATE PROCEDURE [dbo].[sp_sendToMaintenance]
	@vehicleName nvarchar(max), @releaseDate datetime2
AS
BEGIN
	declare @vehicleId int

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select @vehicleId = id from Vehicles v where v.Name = @vehicleName
	if @vehicleId is NULL
		THROW 50001, 'Vehicle not found', 1;
	
	IF (EXISTS (select * from Maintenances where Vehicle = @vehicleId)) -- already under maintenance
		THROW 50002, 'Vehicle is already under maintenances', 1;

	insert into dbo.Maintenances values (@vehicleId, @releaseDate)
	select * from Vehicles v where v.Name = @vehicleName
END
GO
/****** Object:  StoredProcedure [dbo].[sp_UnrentVehicle]    Script Date: 6/10/2022 1:15:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Francesco Betti
-- Create date: 6/10/2022
-- Description:	Removes the given vehicle from the rent vehicles, checking if the vehicle is existing
-- =============================================
CREATE PROCEDURE [dbo].[sp_UnrentVehicle]
	@vehicleName nvarchar(max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	declare @vehicleId int
    select @vehicleId = id from Vehicles v where v.Name = @vehicleName
	if @vehicleId is NULL
		THROW 50001, 'Vehicle not found', 1;

	DELETE FROM RentVehicles WHERE Vehicle = @vehicleId

	select * from Vehicles v where v.Name = @vehicleName
END
GO
/****** Object:  StoredProcedure [dbo].[sp_VehicleRequest]    Script Date: 6/10/2022 1:15:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Francesco Betti
-- Create date: 6/10/2022
-- Description:	Gets all the vehicles avalable for rent, filtering by all the given options
--				
-- =============================================
CREATE PROCEDURE [dbo].[sp_VehicleRequest]
	@dtfrom DATETIME2, 
	@dtto DATETIME2, 
	@minStatus int = 2,
	@accessories NVARCHAR(MAX) = '',
	@manufacturers NVARCHAR(MAX) = '',
	@engines NVARCHAR(MAX) = '',
	@categories NVARCHAR(MAX) = '',
	@transmissions NVARCHAR(MAX) = ''
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- cleanup maintenances table from vehicles with expired date
	DELETE from Maintenances WHERE MaintenanceEnd >= GETDATE()

	-- if null string set all options
	IF @accessories = ''
		SELECT @accessories = STRING_AGG (Name, ',') FROM Accessories
	IF @manufacturers = ''
		SELECT @manufacturers = STRING_AGG (Name, ',') FROM Manufacturers
	IF @engines = ''
		SELECT @engines = STRING_AGG (Name, ',') FROM EngineTypes
	IF @categories = ''
		SELECT @categories = STRING_AGG (Name, ',') FROM VehicleCategories
	IF @transmissions = ''
		SELECT @transmissions = STRING_AGG (Name, ',') FROM TransmissionTypes

	SELECT v.* FROM Vehicles v
	JOIN Manufacturers m on m.Id = v.Manufacturer
	JOIN EngineTypes e on e.Id = v.Engine
	JOIN VehicleCategories vc on vc.Id = v.Category
	JOIN TransmissionTypes tt on tt.Id = v.Transmission
	WHERE 
		-- still under maintenance
		v.Id not in (SELECT m.Vehicle FROM Maintenances m WHERE m.MaintenanceEnd <= @dtfrom) 
		-- vehicle already rent
		and v.Id not in (SELECT rv.Vehicle FROM RentVehicles rv WHERE (rv.RentFrom between @dtfrom and @dtto) OR (rv.RentTo between @dtfrom and @dtto))
		-- accessories with minimum status
		and v.id in (
			SELECT va.VehicleId FROM VehicleAccessories va 
			JOIN Accessories a on a.Id = va.AccessoryId
			WHERE 
				va.Status > @minStatus
				AND a.Name in (select * from STRING_SPLIT(@accessories, ','))
		)
		and m.Name in (select * from STRING_SPLIT(@manufacturers, ','))
		and e.Name in (select * from STRING_SPLIT(@engines, ','))
		and vc.Name in (select * from STRING_SPLIT(@categories, ','))
		and tt.Name in (select * from STRING_SPLIT(@transmissions, ','))
END
GO

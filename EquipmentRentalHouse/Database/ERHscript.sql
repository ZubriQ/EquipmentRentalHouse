USE [astapchukdb2]
GO
/****** Object:  Table [dbo].[Clients]    Script Date: 05.05.2022 14:51:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clients](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[Surname] [nvarchar](50) NOT NULL,
	[Patronymic] [nvarchar](50) NULL,
	[Building] [nvarchar](50) NOT NULL,
	[StreetId] [int] NOT NULL,
	[Apartment] [nvarchar](50) NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[DateOfOrder] [datetime] NOT NULL,
	[Phone] [nvarchar](50) NOT NULL,
	[PassportIssuedBy] [nvarchar](100) NOT NULL,
	[PassportDateOfIssue] [date] NOT NULL,
	[PassportDateOfExpiration] [date] NOT NULL,
	[PassportCode] [nvarchar](50) NOT NULL,
	[PassportNumber] [nvarchar](50) NOT NULL,
	[PassportPlaceOfBirth] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Clients] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Devices]    Script Date: 05.05.2022 14:51:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Devices](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Devices] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmploymentHistory]    Script Date: 05.05.2022 14:51:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmploymentHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StaffId] [int] NOT NULL,
	[PositionId] [int] NOT NULL,
	[ReasonForTransfer] [nvarchar](200) NOT NULL,
	[DateOfOrder] [datetime] NOT NULL,
 CONSTRAINT [PK_EmploymentHistories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Manufacturers]    Script Date: 05.05.2022 14:51:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Manufacturers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Manufacturers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Objects]    Script Date: 05.05.2022 14:51:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Objects](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Objects] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ObjectsUsers]    Script Date: 05.05.2022 14:51:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ObjectsUsers](
	[UserId] [int] NOT NULL,
	[ObjectId] [int] NOT NULL,
	[C] [bit] NOT NULL,
	[R] [bit] NOT NULL,
	[U] [bit] NOT NULL,
	[D] [bit] NOT NULL,
 CONSTRAINT [PK_ObjectsUsers] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[ObjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 05.05.2022 14:51:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[ClientId] [int] NOT NULL,
	[StockKeepingUnitId] [int] NOT NULL,
	[DateOfOrder] [datetime] NOT NULL,
	[DateOfExpiration] [datetime] NOT NULL,
	[IsReturned] [bit] NOT NULL,
	[DateOfReturn] [datetime] NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[ClientId] ASC,
	[StockKeepingUnitId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Positions]    Script Date: 05.05.2022 14:51:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Positions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Positions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Staff]    Script Date: 05.05.2022 14:51:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Staff](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[Surname] [nvarchar](50) NOT NULL,
	[Patronymic] [nvarchar](50) NULL,
	[Building] [nvarchar](50) NOT NULL,
	[StreetId] [int] NOT NULL,
	[Apartment] [nvarchar](50) NOT NULL,
	[PositionId] [int] NOT NULL,
	[Gender] [nvarchar](50) NULL,
	[SalaryIncrease] [decimal](18, 2) NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[IsDismissed] [bit] NOT NULL,
	[DateOfDismissal] [datetime] NULL,
 CONSTRAINT [PK_Staff] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StockKeepingUnits]    Script Date: 05.05.2022 14:51:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StockKeepingUnits](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Number] [nvarchar](100) NOT NULL,
	[ManufacturerId] [int] NOT NULL,
	[DeviceId] [int] NOT NULL,
	[RentalPrice] [decimal](18, 2) NOT NULL,
	[DevicePrice] [decimal](18, 2) NOT NULL,
	[IsInStock] [bit] NOT NULL,
 CONSTRAINT [PK_StockKeepingUnits] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Streets]    Script Date: 05.05.2022 14:51:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Streets](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Streets] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 05.05.2022 14:51:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Login] [nvarchar](16) NOT NULL,
	[Password] [nvarchar](70) NOT NULL,
 CONSTRAINT [PK_Users_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Clients]  WITH CHECK ADD  CONSTRAINT [FK_Clients_Streets] FOREIGN KEY([StreetId])
REFERENCES [dbo].[Streets] ([Id])
GO
ALTER TABLE [dbo].[Clients] CHECK CONSTRAINT [FK_Clients_Streets]
GO
ALTER TABLE [dbo].[EmploymentHistory]  WITH CHECK ADD  CONSTRAINT [FK_EmploymentHistory_Positions] FOREIGN KEY([PositionId])
REFERENCES [dbo].[Positions] ([Id])
GO
ALTER TABLE [dbo].[EmploymentHistory] CHECK CONSTRAINT [FK_EmploymentHistory_Positions]
GO
ALTER TABLE [dbo].[EmploymentHistory]  WITH CHECK ADD  CONSTRAINT [FK_EmploymentHistory_Staff] FOREIGN KEY([StaffId])
REFERENCES [dbo].[Staff] ([Id])
GO
ALTER TABLE [dbo].[EmploymentHistory] CHECK CONSTRAINT [FK_EmploymentHistory_Staff]
GO
ALTER TABLE [dbo].[ObjectsUsers]  WITH CHECK ADD  CONSTRAINT [FK_ObjectsUsers_Objects] FOREIGN KEY([ObjectId])
REFERENCES [dbo].[Objects] ([Id])
GO
ALTER TABLE [dbo].[ObjectsUsers] CHECK CONSTRAINT [FK_ObjectsUsers_Objects]
GO
ALTER TABLE [dbo].[ObjectsUsers]  WITH CHECK ADD  CONSTRAINT [FK_ObjectsUsers_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ObjectsUsers] CHECK CONSTRAINT [FK_ObjectsUsers_Users]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Clients1] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Clients] ([Id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Clients1]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_StockKeepingUnits1] FOREIGN KEY([StockKeepingUnitId])
REFERENCES [dbo].[StockKeepingUnits] ([Id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_StockKeepingUnits1]
GO
ALTER TABLE [dbo].[Staff]  WITH CHECK ADD  CONSTRAINT [FK_Staff_Positions] FOREIGN KEY([PositionId])
REFERENCES [dbo].[Positions] ([Id])
GO
ALTER TABLE [dbo].[Staff] CHECK CONSTRAINT [FK_Staff_Positions]
GO
ALTER TABLE [dbo].[Staff]  WITH CHECK ADD  CONSTRAINT [FK_Staff_Streets] FOREIGN KEY([StreetId])
REFERENCES [dbo].[Streets] ([Id])
GO
ALTER TABLE [dbo].[Staff] CHECK CONSTRAINT [FK_Staff_Streets]
GO
ALTER TABLE [dbo].[StockKeepingUnits]  WITH CHECK ADD  CONSTRAINT [FK_StockKeepingUnits_Devices] FOREIGN KEY([DeviceId])
REFERENCES [dbo].[Devices] ([Id])
GO
ALTER TABLE [dbo].[StockKeepingUnits] CHECK CONSTRAINT [FK_StockKeepingUnits_Devices]
GO
ALTER TABLE [dbo].[StockKeepingUnits]  WITH CHECK ADD  CONSTRAINT [FK_StockKeepingUnits_Manufacturers] FOREIGN KEY([ManufacturerId])
REFERENCES [dbo].[Manufacturers] ([Id])
GO
ALTER TABLE [dbo].[StockKeepingUnits] CHECK CONSTRAINT [FK_StockKeepingUnits_Manufacturers]
GO
USE [master]
GO
ALTER DATABASE [astapchukdb2] SET  READ_WRITE 
GO

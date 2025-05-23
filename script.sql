USE [TravelFusion]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 07-05-2025 14:48:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Airport]    Script Date: 07-05-2025 14:48:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Airport](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Country] [nvarchar](128) NOT NULL,
	[City] [nvarchar](128) NOT NULL,
	[Address] [nvarchar](128) NULL,
	[Latitude] [decimal](9, 6) NOT NULL,
	[Longitude] [decimal](9, 6) NOT NULL,
	[Name] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Booking]    Script Date: 07-05-2025 14:48:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Booking](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BookingMadeAt] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[TravelPackageId] [int] NOT NULL,
	[UserId] [int] NULL,
	[TravelManagerContactId] [int] NOT NULL,
	[PriceAmount] [decimal](18, 0) NULL,
	[PriceCurrency] [nchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contact]    Script Date: 07-05-2025 14:48:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contact](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[PhoneNumber] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](128) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Country]    Script Date: 07-05-2025 14:48:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Iso2Code] [char](2) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Nationality] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Iso2Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Currency]    Script Date: 07-05-2025 14:48:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Currency](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Flight]    Script Date: 07-05-2025 14:48:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Flight](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Price] [decimal](30, 2) NOT NULL,
	[Airline] [nvarchar](128) NOT NULL,
	[ClassType] [nvarchar](50) NULL,
	[SeatsAvailable] [int] NOT NULL,
	[DepartureTime] [datetime] NOT NULL,
	[ArrivalTime] [datetime] NOT NULL,
	[DepartureFromAirportId] [int] NOT NULL,
	[ArrivalAtAirportId] [int] NOT NULL,
	[CurrencyId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Hotel]    Script Date: 07-05-2025 14:48:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hotel](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Description] [nvarchar](600) NULL,
	[Address] [nvarchar](128) NULL,
	[Latitude] [decimal](9, 6) NOT NULL,
	[Longitude] [decimal](9, 6) NOT NULL,
	[City] [nchar](50) NULL,
	[Country] [nchar](128) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HotelStay]    Script Date: 07-05-2025 14:48:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HotelStay](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Price] [decimal](30, 2) NOT NULL,
	[CheckInDate] [datetime] NOT NULL,
	[CheckOutDate] [datetime] NOT NULL,
	[DaysOfStay] [int] NOT NULL,
	[NoOfTravellers] [int] NOT NULL,
	[HotelId] [int] NOT NULL,
	[CurrencyId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 07-05-2025 14:48:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PriceAmount] [decimal](18, 2) NOT NULL,
	[PriceCurrency] [nvarchar](10) NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[StripePaymentIntentId] [nvarchar](255) NULL,
	[BookingId] [int] NOT NULL,
	[StripeSessionId] [nvarchar](255) NULL,
 CONSTRAINT [PK__Payment__9B556A38C3C29DD6] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Traveller]    Script Date: 07-05-2025 14:48:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Traveller](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[Gender] [nvarchar](20) NOT NULL,
	[NationalityId] [int] NOT NULL,
	[PassportNumber] [nvarchar](50) NOT NULL,
	[PassportExpiry] [date] NOT NULL,
	[PassportIssuingCountryId] [int] NOT NULL,
	[BookingId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TravelPackage]    Script Date: 07-05-2025 14:48:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TravelPackage](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PriceCurrency] [nchar](10) NULL,
	[PriceAmount] [decimal](30, 2) NULL,
	[NoOfTravellers] [int] NOT NULL,
	[Description] [nvarchar](600) NULL,
	[OutboundFlightId] [int] NOT NULL,
	[InboundFlightId] [int] NOT NULL,
	[HotelStayId] [int] NOT NULL,
	[ToHotelTransferId] [int] NULL,
	[FromHotelTransferId] [int] NULL,
	[Headline] [nvarchar](100) NULL,
	[status] [int] NOT NULL,
	[ImagePath] [nvarchar](300) NULL,
 CONSTRAINT [PK__TravelPa__3214EC0740F6A59B] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 07-05-2025 14:48:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[PasswordHash] [nvarchar](128) NOT NULL,
	[PasswordSalt] [nvarchar](128) NOT NULL,
	[EmailForPasswordReset] [nvarchar](128) NOT NULL,
	[UserRoleId] [int] NULL,
	[ContactId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 07-05-2025 14:48:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRole](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TravelPackage] ADD  CONSTRAINT [DF_TravelPackage_statusid]  DEFAULT ((0)) FOR [status]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD FOREIGN KEY([TravelManagerContactId])
REFERENCES [dbo].[Contact] ([Id])
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK__Booking__TravelP__656C112C] FOREIGN KEY([TravelPackageId])
REFERENCES [dbo].[TravelPackage] ([Id])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK__Booking__TravelP__656C112C]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Flight]  WITH CHECK ADD  CONSTRAINT [FK_Flight_ArrivalAirport] FOREIGN KEY([ArrivalAtAirportId])
REFERENCES [dbo].[Airport] ([Id])
GO
ALTER TABLE [dbo].[Flight] CHECK CONSTRAINT [FK_Flight_ArrivalAirport]
GO
ALTER TABLE [dbo].[Flight]  WITH CHECK ADD  CONSTRAINT [FK_Flight_Currency] FOREIGN KEY([CurrencyId])
REFERENCES [dbo].[Currency] ([Id])
GO
ALTER TABLE [dbo].[Flight] CHECK CONSTRAINT [FK_Flight_Currency]
GO
ALTER TABLE [dbo].[Flight]  WITH CHECK ADD  CONSTRAINT [FK_Flight_DepartureAirport] FOREIGN KEY([DepartureFromAirportId])
REFERENCES [dbo].[Airport] ([Id])
GO
ALTER TABLE [dbo].[Flight] CHECK CONSTRAINT [FK_Flight_DepartureAirport]
GO
ALTER TABLE [dbo].[HotelStay]  WITH CHECK ADD  CONSTRAINT [FK_HotelStay_Currency] FOREIGN KEY([CurrencyId])
REFERENCES [dbo].[Currency] ([Id])
GO
ALTER TABLE [dbo].[HotelStay] CHECK CONSTRAINT [FK_HotelStay_Currency]
GO
ALTER TABLE [dbo].[HotelStay]  WITH CHECK ADD  CONSTRAINT [FK_HotelStay_Hotel] FOREIGN KEY([HotelId])
REFERENCES [dbo].[Hotel] ([Id])
GO
ALTER TABLE [dbo].[HotelStay] CHECK CONSTRAINT [FK_HotelStay_Hotel]
GO
ALTER TABLE [dbo].[Traveller]  WITH CHECK ADD  CONSTRAINT [FK_Traveller_Booking] FOREIGN KEY([BookingId])
REFERENCES [dbo].[Booking] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Traveller] CHECK CONSTRAINT [FK_Traveller_Booking]
GO
ALTER TABLE [dbo].[Traveller]  WITH CHECK ADD  CONSTRAINT [FK_Traveller_Nationality] FOREIGN KEY([NationalityId])
REFERENCES [dbo].[Country] ([Id])
GO
ALTER TABLE [dbo].[Traveller] CHECK CONSTRAINT [FK_Traveller_Nationality]
GO
ALTER TABLE [dbo].[Traveller]  WITH CHECK ADD  CONSTRAINT [FK_Traveller_PassportIssuingCountry] FOREIGN KEY([PassportIssuingCountryId])
REFERENCES [dbo].[Country] ([Id])
GO
ALTER TABLE [dbo].[Traveller] CHECK CONSTRAINT [FK_Traveller_PassportIssuingCountry]
GO
ALTER TABLE [dbo].[TravelPackage]  WITH CHECK ADD  CONSTRAINT [FK_TravelPackage_FromHotelTransfer] FOREIGN KEY([FromHotelTransferId])
REFERENCES [dbo].[Flight] ([Id])
GO
ALTER TABLE [dbo].[TravelPackage] CHECK CONSTRAINT [FK_TravelPackage_FromHotelTransfer]
GO
ALTER TABLE [dbo].[TravelPackage]  WITH CHECK ADD  CONSTRAINT [FK_TravelPackage_HotelStay] FOREIGN KEY([HotelStayId])
REFERENCES [dbo].[HotelStay] ([Id])
GO
ALTER TABLE [dbo].[TravelPackage] CHECK CONSTRAINT [FK_TravelPackage_HotelStay]
GO
ALTER TABLE [dbo].[TravelPackage]  WITH CHECK ADD  CONSTRAINT [FK_TravelPackage_InboundFlight] FOREIGN KEY([InboundFlightId])
REFERENCES [dbo].[Flight] ([Id])
GO
ALTER TABLE [dbo].[TravelPackage] CHECK CONSTRAINT [FK_TravelPackage_InboundFlight]
GO
ALTER TABLE [dbo].[TravelPackage]  WITH CHECK ADD  CONSTRAINT [FK_TravelPackage_OutboundFlight] FOREIGN KEY([OutboundFlightId])
REFERENCES [dbo].[Flight] ([Id])
GO
ALTER TABLE [dbo].[TravelPackage] CHECK CONSTRAINT [FK_TravelPackage_OutboundFlight]
GO
ALTER TABLE [dbo].[TravelPackage]  WITH CHECK ADD  CONSTRAINT [FK_TravelPackage_ToHotelTransfer] FOREIGN KEY([ToHotelTransferId])
REFERENCES [dbo].[Flight] ([Id])
GO
ALTER TABLE [dbo].[TravelPackage] CHECK CONSTRAINT [FK_TravelPackage_ToHotelTransfer]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_ContactId] FOREIGN KEY([ContactId])
REFERENCES [dbo].[Contact] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_ContactId]
GO
ALTER TABLE [dbo].[User]  WITH NOCHECK ADD  CONSTRAINT [FK_User_UserRoleId] FOREIGN KEY([UserRoleId])
REFERENCES [dbo].[UserRole] ([Id])
GO
ALTER TABLE [dbo].[User] NOCHECK CONSTRAINT [FK_User_UserRoleId]
GO

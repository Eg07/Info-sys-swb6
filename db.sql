-- ****************** SqlDBM: Microsoft SQL Server ******************
-- ******************************************************************

-- ************************************** [dbo].[Tenant]

CREATE TABLE [dbo].[Tenant]
(
 [id]         int NOT NULL ,
 [first_name] varchar(50) NOT NULL ,
 [last_name]  varchar(50) NOT NULL ,


 CONSTRAINT [PK_Tenant] PRIMARY KEY CLUSTERED ([id] ASC)
);
GO








-- ************************************** [dbo].[address]

CREATE TABLE [dbo].[address]
(
 [id]       int NOT NULL ,
 [street]   varchar(50) NOT NULL ,
 [city]     varchar(50) NOT NULL ,
 [house_nr] int NOT NULL ,
 [zip]      int NOT NULL ,
 [state]    varchar(50) NOT NULL ,


 CONSTRAINT [PK_address] PRIMARY KEY CLUSTERED ([id] ASC)
);
GO








-- ************************************** [dbo].[Owner]

CREATE TABLE [dbo].[Owner]
(
 [id]         int NOT NULL ,
 [first_name] varchar(50) NOT NULL ,
 [last_name]  varchar(50) NOT NULL ,
 [adressid]   int NOT NULL ,


 CONSTRAINT [PK_DreamHouse] PRIMARY KEY CLUSTERED ([id] ASC),
 CONSTRAINT [FK_57] FOREIGN KEY ([adressid])  REFERENCES [dbo].[address]([id])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_57] ON [dbo].[Owner] 
 (
  [adressid] ASC
 )

GO







-- ************************************** [dbo].[Bank_Account]

CREATE TABLE [dbo].[Bank_Account]
(
 [IBAN]     varchar(22) NOT NULL ,
 [tenantId] int NOT NULL ,


 CONSTRAINT [PK_Bank_Account] PRIMARY KEY CLUSTERED ([IBAN] ASC),
 CONSTRAINT [FK_114] FOREIGN KEY ([tenantId])  REFERENCES [dbo].[Tenant]([id])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_114] ON [dbo].[Bank_Account] 
 (
  [tenantId] ASC
 )

GO







-- ************************************** [dbo].[Property]

CREATE TABLE [dbo].[Property]
(
 [id]       int NOT NULL ,
 [ownerId]  int NOT NULL ,
 [adressId] int NOT NULL ,


 CONSTRAINT [PK_Property] PRIMARY KEY CLUSTERED ([id] ASC),
 CONSTRAINT [FK_60] FOREIGN KEY ([ownerId])  REFERENCES [dbo].[Owner]([id]),
 CONSTRAINT [FK_63] FOREIGN KEY ([adressId])  REFERENCES [dbo].[address]([id])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_60] ON [dbo].[Property] 
 (
  [ownerId] ASC
 )

GO

CREATE NONCLUSTERED INDEX [fkIdx_63] ON [dbo].[Property] 
 (
  [adressId] ASC
 )

GO







-- ************************************** [dbo].[Monthly_payment]

CREATE TABLE [dbo].[Monthly_payment]
(
 [id]     int NOT NULL ,
 [amount] float NOT NULL ,
 [date]   datetime NOT NULL ,
 [IBAN]   varchar(22) NOT NULL ,


 CONSTRAINT [PK_Monthly_payment] PRIMARY KEY CLUSTERED ([id] ASC),
 CONSTRAINT [FK_117] FOREIGN KEY ([IBAN])  REFERENCES [dbo].[Bank_Account]([IBAN])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_117] ON [dbo].[Monthly_payment] 
 (
  [IBAN] ASC
 )

GO







-- ************************************** [dbo].[General_Service]

CREATE TABLE [dbo].[General_Service]
(
 [id]         int NOT NULL ,
 [name]       int NOT NULL ,
 [cost]       int NOT NULL ,
 [propertyId] int NOT NULL ,


 CONSTRAINT [PK_Genera_ Service] PRIMARY KEY CLUSTERED ([id] ASC),
 CONSTRAINT [FK_69] FOREIGN KEY ([propertyId])  REFERENCES [dbo].[Property]([id])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_69] ON [dbo].[General_Service] 
 (
  [propertyId] ASC
 )

GO







-- ************************************** [dbo].[Service]

CREATE TABLE [dbo].[Service]
(
 [id]                int NOT NULL ,
 [name]              varchar(50) NOT NULL ,
 [cost]              float NOT NULL ,
 [unitId]            int NOT NULL ,
 [general_serviceId] int NOT NULL ,


 CONSTRAINT [PK_Service] PRIMARY KEY CLUSTERED ([id] ASC),
 CONSTRAINT [FK_72] FOREIGN KEY ([general_serviceId])  REFERENCES [dbo].[General_Service]([id])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_72] ON [dbo].[Service] 
 (
  [general_serviceId] ASC
 )

GO







-- ************************************** [dbo].[Unit]

CREATE TABLE [dbo].[Unit]
(
 [id]         int NOT NULL ,
 [rooms_nr]   float NOT NULL ,
 [area]       float NOT NULL ,
 [floor]      int NOT NULL ,
 [propertyId] int NOT NULL ,
 [serviceId]  int NOT NULL ,


 CONSTRAINT [PK_Unit] PRIMARY KEY CLUSTERED ([id] ASC),
 CONSTRAINT [FK_123] FOREIGN KEY ([serviceId])  REFERENCES [dbo].[Service]([id]),
 CONSTRAINT [FK_66] FOREIGN KEY ([propertyId])  REFERENCES [dbo].[Property]([id])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_123] ON [dbo].[Unit] 
 (
  [serviceId] ASC
 )

GO

CREATE NONCLUSTERED INDEX [fkIdx_66] ON [dbo].[Unit] 
 (
  [propertyId] ASC
 )

GO







-- ************************************** [dbo].[Lease]

CREATE TABLE [dbo].[Lease]
(
 [id]                int NOT NULL ,
 [cost]              float NOT NULL ,
 [startDate]         datetime NOT NULL ,
 [endDate]           date NOT NULL ,
 [unitId]            int NOT NULL ,
 [tenantId]          int NOT NULL ,
 [monthly_paymentId] int NOT NULL ,
 [resident_nr]       int NOT NULL ,


 CONSTRAINT [PK_Lease] PRIMARY KEY CLUSTERED ([id] ASC),
 CONSTRAINT [FK_108] FOREIGN KEY ([monthly_paymentId])  REFERENCES [dbo].[Monthly_payment]([id]),
 CONSTRAINT [FK_75] FOREIGN KEY ([unitId])  REFERENCES [dbo].[Unit]([id]),
 CONSTRAINT [FK_92] FOREIGN KEY ([tenantId])  REFERENCES [dbo].[Tenant]([id])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_108] ON [dbo].[Lease] 
 (
  [monthly_paymentId] ASC
 )

GO

CREATE NONCLUSTERED INDEX [fkIdx_75] ON [dbo].[Lease] 
 (
  [unitId] ASC
 )

GO

CREATE NONCLUSTERED INDEX [fkIdx_92] ON [dbo].[Lease] 
 (
  [tenantId] ASC
 )

GO








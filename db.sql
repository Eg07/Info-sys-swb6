-- ****************** SqlDBM: Microsoft SQL Server ******************
-- ******************************************************************

-- ************************************** [dbo].[G3_Tenant]

CREATE TABLE [dbo].[G3_Tenant]
(
 [id]        int NOT NULL ,
 [firstName] varchar(50) NOT NULL ,
 [lastName]  varchar(50) NOT NULL ,


 CONSTRAINT [PK_Tenant] PRIMARY KEY CLUSTERED ([id] ASC)
);
GO








-- ************************************** [dbo].[G3_address]

CREATE TABLE [dbo].[G3_address]
(
 [id]      int NOT NULL ,
 [street]  varchar(50) NOT NULL ,
 [city]    varchar(50) NOT NULL ,
 [houseNr] int NOT NULL ,
 [zip]     int NOT NULL ,
 [state]   varchar(50) NOT NULL ,


 CONSTRAINT [PK_address] PRIMARY KEY CLUSTERED ([id] ASC)
);
GO








-- ************************************** [dbo].[G3_Owner]

CREATE TABLE [dbo].[G3_Owner]
(
 [id]        int NOT NULL ,
 [firstName] varchar(50) NOT NULL ,
 [lastName]  varchar(50) NOT NULL ,
 [adressid]  int NOT NULL ,


 CONSTRAINT [PK_DreamHouse] PRIMARY KEY CLUSTERED ([id] ASC),
 CONSTRAINT [FK_57] FOREIGN KEY ([adressid])  REFERENCES [dbo].[G3_address]([id])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_57] ON [dbo].[G3_Owner] 
 (
  [adressid] ASC
 )

GO







-- ************************************** [dbo].[G3_Bank_Account]

CREATE TABLE [dbo].[G3_Bank_Account]
(
 [IBAN]     varchar(22) NOT NULL ,
 [tenantId] int NOT NULL ,


 CONSTRAINT [PK_Bank_Account] PRIMARY KEY CLUSTERED ([IBAN] ASC),
 CONSTRAINT [FK_114] FOREIGN KEY ([tenantId])  REFERENCES [dbo].[G3_Tenant]([id])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_114] ON [dbo].[G3_Bank_Account] 
 (
  [tenantId] ASC
 )

GO







-- ************************************** [dbo].[G3_Property]

CREATE TABLE [dbo].[G3_Property]
(
 [id]       int NOT NULL ,
 [ownerId]  int NOT NULL ,
 [adressId] int NOT NULL ,


 CONSTRAINT [PK_Property] PRIMARY KEY CLUSTERED ([id] ASC),
 CONSTRAINT [FK_60] FOREIGN KEY ([ownerId])  REFERENCES [dbo].[G3_Owner]([id]),
 CONSTRAINT [FK_63] FOREIGN KEY ([adressId])  REFERENCES [dbo].[G3_address]([id])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_60] ON [dbo].[G3_Property] 
 (
  [ownerId] ASC
 )

GO

CREATE NONCLUSTERED INDEX [fkIdx_63] ON [dbo].[G3_Property] 
 (
  [adressId] ASC
 )

GO







-- ************************************** [dbo].[G3_Unit]

CREATE TABLE [dbo].[G3_Unit]
(
 [id]         int NOT NULL ,
 [roomsNr]    float NOT NULL ,
 [area]       float NOT NULL ,
 [floor]      int NOT NULL ,
 [propertyId] int NOT NULL ,
 [residentNr] int NOT NULL ,


 CONSTRAINT [PK_Unit] PRIMARY KEY CLUSTERED ([id] ASC),
 CONSTRAINT [FK_66] FOREIGN KEY ([propertyId])  REFERENCES [dbo].[G3_Property]([id])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_66] ON [dbo].[G3_Unit] 
 (
  [propertyId] ASC
 )

GO







-- ************************************** [dbo].[G3_Service]

CREATE TABLE [dbo].[G3_Service]
(
 [id]              int NOT NULL ,
 [name]            varchar(50) NOT NULL ,
 [cost]            float NOT NULL ,
 [dueDate]         datetime NOT NULL ,
 [unitId]          int NOT NULL ,
 [distributionKey] int NOT NULL ,
 [propertyId]      int NOT NULL ,


 CONSTRAINT [PK_Service] PRIMARY KEY CLUSTERED ([id] ASC),
 CONSTRAINT [FK_134] FOREIGN KEY ([unitId])  REFERENCES [dbo].[G3_Unit]([id]),
 CONSTRAINT [FK_142] FOREIGN KEY ([propertyId])  REFERENCES [dbo].[G3_Property]([id])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_134] ON [dbo].[G3_Service] 
 (
  [unitId] ASC
 )

GO

CREATE NONCLUSTERED INDEX [fkIdx_142] ON [dbo].[G3_Service] 
 (
  [propertyId] ASC
 )

GO







-- ************************************** [dbo].[G3_Lease]

CREATE TABLE [dbo].[G3_Lease]
(
 [id]        int NOT NULL ,
 [cost]      float NOT NULL ,
 [startDate] datetime NOT NULL ,
 [endDate]   date NOT NULL ,
 [unitId]    int NOT NULL ,
 [tenantId]  int NOT NULL ,


 CONSTRAINT [PK_Lease] PRIMARY KEY CLUSTERED ([id] ASC),
 CONSTRAINT [FK_75] FOREIGN KEY ([unitId])  REFERENCES [dbo].[G3_Unit]([id]),
 CONSTRAINT [FK_92] FOREIGN KEY ([tenantId])  REFERENCES [dbo].[G3_Tenant]([id])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_75] ON [dbo].[G3_Lease] 
 (
  [unitId] ASC
 )

GO

CREATE NONCLUSTERED INDEX [fkIdx_92] ON [dbo].[G3_Lease] 
 (
  [tenantId] ASC
 )

GO







-- ************************************** [dbo].[G3_Monthly_payment]

CREATE TABLE [dbo].[G3_Monthly_payment]
(
 [id]           int NOT NULL ,
 [date]         datetime NOT NULL ,
 [leaseId]      int NOT NULL ,
 [targetAmount] float NOT NULL ,


 CONSTRAINT [PK_Monthly_payment] PRIMARY KEY CLUSTERED ([id] ASC),
 CONSTRAINT [FK_137] FOREIGN KEY ([leaseId])  REFERENCES [dbo].[G3_Lease]([id])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_137] ON [dbo].[G3_Monthly_payment] 
 (
  [leaseId] ASC
 )

GO







-- ************************************** [dbo].[G3_Monthly_paid]

CREATE TABLE [dbo].[G3_Monthly_paid]
(
 [id]               int NOT NULL ,
 [amount]           float NOT NULL ,
 [paidDate]         date NOT NULL ,
 [IBAN]             varchar(22) NOT NULL ,
 [monthlyPaymnetId] int NOT NULL ,


 CONSTRAINT [PK_G3_Monthly_paid] PRIMARY KEY CLUSTERED ([id] ASC),
 CONSTRAINT [FK_165] FOREIGN KEY ([IBAN])  REFERENCES [dbo].[G3_Bank_Account]([IBAN]),
 CONSTRAINT [FK_168] FOREIGN KEY ([monthlyPaymnetId])  REFERENCES [dbo].[G3_Monthly_payment]([id])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_165] ON [dbo].[G3_Monthly_paid] 
 (
  [IBAN] ASC
 )

GO

CREATE NONCLUSTERED INDEX [fkIdx_168] ON [dbo].[G3_Monthly_paid] 
 (
  [monthlyPaymnetId] ASC
 )

GO








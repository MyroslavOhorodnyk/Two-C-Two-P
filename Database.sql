IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = '2c2p')
  BEGIN
    CREATE DATABASE [2c2p]
  END
    
GO
    USE [2c2p]
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Transactions' and xtype='U')
BEGIN
    CREATE TABLE Transactions (
        [Id] [uniqueidentifier] NOT NULL,
        [Ammount] [decimal](19, 4) NOT NULL,
		[Currency] [nvarchar](50) NOT NULL,
		[TransactionDate] [datetimeoffset](7) NOT NULL,
		[TransactionStatus] [nvarchar](50) NOT NULL,
		[Status] [tinyint] NOT NULL,
		[CreatedBy] [nvarchar](50) NOT NULL,
		[ModifiedBy] [nvarchar](50) NOT NULL,
		[CreatedDate] [datetimeoffset](7) NOT NULL,
		[ModifiedDate] [datetimeoffset](7) NOT NULL,
    )
END

DROP TABLE Transactions
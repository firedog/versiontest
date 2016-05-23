I have used a SQL Server 2012 database named "test" with a table "VersionTests" defined as follows:

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VersionTests]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[VersionTests](
	[Id] [uniqueidentifier] NOT NULL,
	[Version] [bigint] NOT NULL,
 CONSTRAINT [PK_VersionTests] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO





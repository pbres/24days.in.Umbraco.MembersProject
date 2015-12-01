/****** Object:  Table [dbo].[CustomMembers]    Script Date: 2015-12-01 18:16:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CustomMembers](
	[Id] [uniqueidentifier] NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](255) NOT NULL,
	[LastLoginDate] [datetime2](7) NULL,
	[IsApproved] [bit] NOT NULL,
	[IsLockedOut] [bit] NULL,
	[Comment] [nvarchar](500) NULL,
	[LastLockedoutDate] [datetime2](7) NULL,
	[IsFrontendDev] [bit] NULL,
	[IsBackendDev] [bit] NULL,
 CONSTRAINT [PK_CustomMembers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

INSERT [dbo].[CustomMembers] ([Id], [Username], [Email], [Password], [LastLoginDate], [IsApproved], [IsLockedOut], [Comment], [LastLockedoutDate], [IsFrontendDev], [IsBackendDev]) VALUES (N'c06ee089-3a10-4e5c-95b7-355ce056cd13', N'maciek', N'maciek@sicc.pl', N'maciek123', NULL, 0, 0, N'', NULL, 1, 0)
INSERT [dbo].[CustomMembers] ([Id], [Username], [Email], [Password], [LastLoginDate], [IsApproved], [IsLockedOut], [Comment], [LastLockedoutDate], [IsFrontendDev], [IsBackendDev]) VALUES (N'43f40fae-9ad5-40a6-8456-7d8358adbf03', N'pawel', N'pawel@sicc.pl', N'pawel123', NULL, 1, 0, NULL, NULL, 0, 1)
INSERT [dbo].[CustomMembers] ([Id], [Username], [Email], [Password], [LastLoginDate], [IsApproved], [IsLockedOut], [Comment], [LastLockedoutDate], [IsFrontendDev], [IsBackendDev]) VALUES (N'8e1c4fed-a0c9-4014-9d26-f4ba6c8cf4e4', N'marcin', N'marcin@sicc.pl', N'marcin123', NULL, 1, 0, NULL, NULL, 1, 1)


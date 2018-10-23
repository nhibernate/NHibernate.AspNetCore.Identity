ALTER TABLE [dbo].[ApplicationRoles] DROP CONSTRAINT [FK_ApplicationRoles_AspNetRoles]
GO
/****** Object:  Table [dbo].[ApplicationRoles]    Script Date: 2018/10/23 13:57:12 ******/
DROP TABLE [dbo].[ApplicationRoles]
GO
/****** Object:  Table [dbo].[ApplicationRoles]    Script Date: 2018/10/23 13:57:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplicationRoles](
	[Id] [nvarchar](32) NOT NULL,
	[CustomProperty] [nvarchar](256) NULL,
 CONSTRAINT [PK_ApplicationRoles] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ApplicationRoles]  WITH CHECK ADD  CONSTRAINT [FK_ApplicationRoles_AspNetRoles] FOREIGN KEY([Id])
REFERENCES [dbo].[AspNetRoles] ([Id])
GO
ALTER TABLE [dbo].[ApplicationRoles] CHECK CONSTRAINT [FK_ApplicationRoles_AspNetRoles]
GO

ALTER TABLE [dbo].[AppRoles] DROP CONSTRAINT [FK_AppRoles_AspNetRoles]
GO
/****** Object:  Table [dbo].[AppRoles]    Script Date: 2018/10/23 13:57:12 ******/
DROP TABLE [dbo].[AppRoles]
GO
/****** Object:  Table [dbo].[AppRoles]    Script Date: 2018/10/23 13:57:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppRoles](
  [Id] [nvarchar](32) NOT NULL,
  [Description][nvarchar](256) NOT NULL
 CONSTRAINT [PK_AppRoles] PRIMARY KEY CLUSTERED
(
  [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AppRoles]  WITH CHECK ADD  CONSTRAINT [FK_AppRoles_AspNetRoles] FOREIGN KEY([Id])
REFERENCES [dbo].[AspNetRoles] ([Id])
GO
ALTER TABLE [dbo].[AppRoles] CHECK CONSTRAINT [FK_AppRoles_AspNetRoles]
GO

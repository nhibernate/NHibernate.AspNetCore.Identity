SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplicationUsers](
	[Id] [nvarchar](32) NOT NULL,
	[create_time] [datetime] NOT NULL,
	[last_login] [datetime] NULL,
	[login_count] [integer](256) NULL,
 CONSTRAINT [PK_ApplicationUsers] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ApplicationUsers]  WITH CHECK ADD  CONSTRAINT [FK_ApplicationUsers_AspNetUsers] FOREIGN KEY([Id])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[ApplicationUsers] CHECK CONSTRAINT [FK_ApplicationUsers_AspNetUsers]
GO

USE [CropPlanner]
GO
SET IDENTITY_INSERT [dbo].[Role] ON 
GO
INSERT [dbo].[Role] ([Id], [Name]) VALUES (1, N'Standard')
GO
INSERT [dbo].[Role] ([Id], [Name]) VALUES (2, N'Administrator')
GO

SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 
GO
INSERT [dbo].[User] ([Id], [FirstName], [LastName], [UserName], [Password], [Salt], [RoleId]) VALUES (1, N'Chris', N'Rupp', N'crupp', N'1111', N'1111', 1)
GO
INSERT [dbo].[User] ([Id], [FirstName], [LastName], [UserName], [Password], [Salt], [RoleId]) VALUES (2, N'Chris', N'Rupp', N'admin', N'1111', N'1111', 2)
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO

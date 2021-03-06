USE [GardenPlanner]
GO
SET IDENTITY_INSERT [dbo].[Plant] ON 

INSERT [dbo].[Plant] ([Id], [Name], [SizeSq], [TempLow], [SunReqHrs], [TempHigh], [Cost]) VALUES (2, N'Tomato', 24, 55, 8, 85, 3)
INSERT [dbo].[Plant] ([Id], [Name], [SizeSq], [TempLow], [SunReqHrs], [TempHigh], [Cost]) VALUES (3, N'Potato', 12, 40, 6, 80, 5)
INSERT [dbo].[Plant] ([Id], [Name], [SizeSq], [TempLow], [SunReqHrs], [TempHigh], [Cost]) VALUES (4, N'Corn', 6, 77, 6, 91, 4)
INSERT [dbo].[Plant] ([Id], [Name], [SizeSq], [TempLow], [SunReqHrs], [TempHigh], [Cost]) VALUES (5, N'Squash', 4, 70, 8, 95, 6)
INSERT [dbo].[Plant] ([Id], [Name], [SizeSq], [TempLow], [SunReqHrs], [TempHigh], [Cost]) VALUES (6, N'Cabbage', 12, 60, 6, 80, 4)
INSERT [dbo].[Plant] ([Id], [Name], [SizeSq], [TempLow], [SunReqHrs], [TempHigh], [Cost]) VALUES (7, N'Pineapple', 36, 68, 6, 86, 8)
SET IDENTITY_INSERT [dbo].[Plant] OFF

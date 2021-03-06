USE [GardenPlanner]
GO
SET IDENTITY_INSERT [dbo].[Materials] ON 

INSERT [dbo].[Materials] ([MateralID], [MatName], [CostPerSqFt], [MatDescrip], [MatQty]) VALUES (1, N'Top Soil', CAST(0.01000 AS Decimal(30, 5)), N'Makes land easier to till and enriches ground with nutrients for growing plants.', NULL)
INSERT [dbo].[Materials] ([MateralID], [MatName], [CostPerSqFt], [MatDescrip], [MatQty]) VALUES (2, N'Weed Killer', CAST(0.00670 AS Decimal(30, 5)), N'Kills over 200 types of broadleaf weeds.', NULL)
INSERT [dbo].[Materials] ([MateralID], [MatName], [CostPerSqFt], [MatDescrip], [MatQty]) VALUES (3, N'Fertilizer', CAST(0.00300 AS Decimal(30, 5)), N'Provides essential nutrients to help supplement plant growth.', NULL)
INSERT [dbo].[Materials] ([MateralID], [MatName], [CostPerSqFt], [MatDescrip], [MatQty]) VALUES (4, N'Mulch Paper', CAST(0.20000 AS Decimal(30, 5)), N'A perfect, natural way to blocks weeds, reduce erosion, and help keep the soil warm.
Lasts for a full growing season and gradually decomposes.', NULL)
INSERT [dbo].[Materials] ([MateralID], [MatName], [CostPerSqFt], [MatDescrip], [MatQty]) VALUES (5, N'Tomato Cage', CAST(1.00000 AS Decimal(30, 5)), N'Great container for gardening!
Keeps plants from falling over', NULL)
SET IDENTITY_INSERT [dbo].[Materials] OFF

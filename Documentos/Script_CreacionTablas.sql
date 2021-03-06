USE [DbPagos]

/****** Object:  Table [dbo].[Facturas]    Script Date: 18/08/2021 9:27:25 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[Facturas](
	[Id] [uniqueidentifier] NOT NULL,
	[FechaFactura] [datetime] NOT NULL,
	[ClienteId] [int] NOT NULL,
	[Total] [float] NOT NULL,
 CONSTRAINT [PK_Factura] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [dbo].[ItemFacturas]    Script Date: 18/08/2021 9:27:25 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[ItemFacturas](
	[FacturaId] [uniqueidentifier] NOT NULL,
	[id] [int] NOT NULL,
	[ProductoId] [int] NULL,
	[Cantidad] [int] NULL,
	[Precio] [float] NULL
) ON [PRIMARY]


/****** Object:  Table [dbo].[Logistica]    Script Date: 18/08/2021 9:27:25 p. m. ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[Logistica](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdFactura] [uniqueidentifier] NOT NULL,
	[Direccion] [nvarchar](80) NULL,
	[Ciudad] [nvarchar](30) NULL,
 CONSTRAINT [PK_Logistica] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


ALTER TABLE [dbo].[ItemFacturas]  WITH CHECK ADD  CONSTRAINT [FK_ItemFacturas_Facturas] FOREIGN KEY([FacturaId])
REFERENCES [dbo].[Facturas] ([Id])

ALTER TABLE [dbo].[ItemFacturas] CHECK CONSTRAINT [FK_ItemFacturas_Facturas]

ALTER TABLE [dbo].[Logistica]  WITH CHECK ADD  CONSTRAINT [FK_Logistica_Facturas] FOREIGN KEY([IdFactura])
REFERENCES [dbo].[Facturas] ([Id])

ALTER TABLE [dbo].[Logistica] CHECK CONSTRAINT [FK_Logistica_Facturas]


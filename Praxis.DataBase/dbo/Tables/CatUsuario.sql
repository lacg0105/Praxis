CREATE TABLE [dbo].[CatUsuario] (
    [IdUsuario]        UNIQUEIDENTIFIER NOT NULL,
    [IdInstitucion]    INT              NOT NULL,
    [IdArea]           INT              NOT NULL,
    [IdRol]            INT              NOT NULL,
    [IdEstatusUsuario] INT              NOT NULL,
    [Nombre]           NVARCHAR (300)   NOT NULL,
    [ApellidoPaterno]  NVARCHAR (300)   NOT NULL,
    [ApellidoMaterno]  NVARCHAR (300)   NOT NULL,
    [Pwd]              VARBINARY (MAX)  NULL,
    [Correo]           NVARCHAR (100)   NOT NULL,
    [Curp]             NVARCHAR (100)   NULL,
    [FechaCreacion]    DATETIME         NOT NULL,
    [FechaPwd]         DATETIME         NOT NULL,
    [FechaAcceso]      DATETIME         NOT NULL,
    [NombreImagen]     NVARCHAR (300)   NULL,
    CONSTRAINT [PK__CatUsuario__3214EC07CACE267A] PRIMARY KEY CLUSTERED ([IdUsuario] ASC),
    CONSTRAINT [FK__CatUsuario__IdArea__25869641] FOREIGN KEY ([IdArea]) REFERENCES [dbo].[CatArea] ([IdArea]),
    CONSTRAINT [FK__CatUsuario__IdEstatus__286302EC] FOREIGN KEY ([IdEstatusUsuario]) REFERENCES [dbo].[CatEstatus_Usuario] ([IdEstatusUsuario]),
    CONSTRAINT [FK__CatUsuario__IdRol__276EDEB3] FOREIGN KEY ([IdRol]) REFERENCES [dbo].[CatRol] ([IdRol]),
    CONSTRAINT [FK_CatUsuario_CatInstitucion] FOREIGN KEY ([IdInstitucion]) REFERENCES [dbo].[CatInstitucion] ([IdInstitucion])
);




CREATE TABLE [dbo].[CatPantalla] (
    [IdPantalla]  INT            NOT NULL,
    [IdIcono]     INT            NOT NULL,
    [IdSubModulo] INT            NOT NULL,
    [Nombre]      NVARCHAR (100) NOT NULL,
    [Orden]       INT            NOT NULL,
    [Controlador] NVARCHAR (100) NULL,
    [Accion]      NVARCHAR (100) NULL,
    [Estatus]     BIT            NOT NULL,
    CONSTRAINT [PK__CatPantalla__3214EC073951332B] PRIMARY KEY CLUSTERED ([IdPantalla] ASC),
    CONSTRAINT [FK__CatPantalla__Orden__4F7CD00D] FOREIGN KEY ([IdSubModulo]) REFERENCES [dbo].[CatSubModulo] ([IdSubModulo]),
    CONSTRAINT [FK_CatPantalla_CatIcono] FOREIGN KEY ([IdIcono]) REFERENCES [dbo].[CatIcono] ([IdIcono])
);


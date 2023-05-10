CREATE TABLE [dbo].[CatSubModulo] (
    [IdSubModulo] INT            NOT NULL,
    [IdIcono]     INT            NULL,
    [IdModulo]    INT            NOT NULL,
    [Nombre]      NVARCHAR (100) NOT NULL,
    [Orden]       INT            NOT NULL,
    [Estatus]     BIT            NOT NULL,
    CONSTRAINT [PK__SubModul__3214EC078CF2F16E] PRIMARY KEY CLUSTERED ([IdSubModulo] ASC),
    CONSTRAINT [FK__CatSubModulo__Orden__4CA06362] FOREIGN KEY ([IdModulo]) REFERENCES [dbo].[CatModulo] ([IdModulo])
);


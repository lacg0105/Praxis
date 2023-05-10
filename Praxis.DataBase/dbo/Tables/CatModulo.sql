CREATE TABLE [dbo].[CatModulo] (
    [IdModulo] INT            NOT NULL,
    [IdIcono]  INT            NULL,
    [Nombre]   NVARCHAR (100) NOT NULL,
    [Orden]    INT            NOT NULL,
    [Estatus]  BIT            NOT NULL,
    CONSTRAINT [PK__Modulo__3214EC0762624C88] PRIMARY KEY CLUSTERED ([IdModulo] ASC)
);


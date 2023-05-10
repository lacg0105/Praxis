CREATE TABLE [Seguridad].[CatTipoEvento_Bitacora] (
    [IdTipoEvento] INT            NOT NULL,
    [Nombre]       NVARCHAR (255) NOT NULL,
    CONSTRAINT [PK_CatTipoEvento_Bitacora] PRIMARY KEY CLUSTERED ([IdTipoEvento] ASC)
);


CREATE TABLE [Seguridad].[CatEvento_Bitacora] (
    [IdEvento]     INT            NOT NULL,
    [IdTipoEvento] INT            NOT NULL,
    [Nombre]       NVARCHAR (255) NOT NULL,
    CONSTRAINT [PK_CatEvento_Bitacora] PRIMARY KEY CLUSTERED ([IdEvento] ASC),
    CONSTRAINT [FK_CatEvento_Bitacora_CatTipoEvento_Bitacora] FOREIGN KEY ([IdTipoEvento]) REFERENCES [Seguridad].[CatTipoEvento_Bitacora] ([IdTipoEvento])
);


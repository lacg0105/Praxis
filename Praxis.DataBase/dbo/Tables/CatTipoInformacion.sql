CREATE TABLE [dbo].[CatTipoInformacion] (
    [IdTipoInformacion] INT            NOT NULL,
    [Nombre]            NVARCHAR (200) NOT NULL,
    CONSTRAINT [PK_CatTipoInformacion] PRIMARY KEY CLUSTERED ([IdTipoInformacion] ASC)
);


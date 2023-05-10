CREATE TABLE [dbo].[CatTipoSangre] (
    [IdTipoSangre] INT           NOT NULL,
    [Nombre]       NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_CatTipoSangre] PRIMARY KEY CLUSTERED ([IdTipoSangre] ASC)
);


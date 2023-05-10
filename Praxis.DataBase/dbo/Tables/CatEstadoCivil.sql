CREATE TABLE [dbo].[CatEstadoCivil] (
    [IdEstadoCivil] INT           NOT NULL,
    [Nombre]        NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_CatEstadoCivil] PRIMARY KEY CLUSTERED ([IdEstadoCivil] ASC)
);


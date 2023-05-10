CREATE TABLE [dbo].[CatEntidad] (
    [IdEntidad]     INT           NOT NULL,
    [IdInstitucion] INT           NOT NULL,
    [Nombre]        VARCHAR (50)  NOT NULL,
    [NombreLargo]   VARCHAR (300) NOT NULL,
    [Estatus]       BIT           NOT NULL,
    CONSTRAINT [PK__CatFondo__3214EC07ACA7F8F2] PRIMARY KEY CLUSTERED ([IdEntidad] ASC),
    CONSTRAINT [FK__CatEntidad__IdIns__0C85DE4D] FOREIGN KEY ([IdInstitucion]) REFERENCES [dbo].[CatInstitucion] ([IdInstitucion])
);


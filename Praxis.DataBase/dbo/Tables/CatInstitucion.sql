CREATE TABLE [dbo].[CatInstitucion] (
    [IdInstitucion]     INT            NOT NULL,
    [IdTipoInstitucion] INT            NOT NULL,
    [Nombre]            NVARCHAR (100) NOT NULL,
    [NombreLargo]       NVARCHAR (300) NOT NULL,
    [Carpeta]           VARCHAR (50)   NULL,
    CONSTRAINT [PK__CatInsti__3214EC07932C945C] PRIMARY KEY CLUSTERED ([IdInstitucion] ASC),
    CONSTRAINT [FK__CatInstit__IdTip__1CF15040] FOREIGN KEY ([IdTipoInstitucion]) REFERENCES [dbo].[CatTipoInstitucion] ([IdTipoInstitucion])
);


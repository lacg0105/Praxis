CREATE TABLE [Admin].[Parametro] (
    [IdParametro] INT            NOT NULL,
    [Nombre]      NVARCHAR (50)  NULL,
    [Descripcion] NVARCHAR (300) NULL,
    [Valor]       NVARCHAR (300) NULL,
    [Seccion]     NVARCHAR (60)  NULL,
    CONSTRAINT [PK__Parametr__3214EC078A541D13] PRIMARY KEY CLUSTERED ([IdParametro] ASC)
);


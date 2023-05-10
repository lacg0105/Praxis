CREATE TABLE [Admin].[FolioClaveAcceso] (
    [IdFolio]         UNIQUEIDENTIFIER NOT NULL,
    [IdUsuario]       UNIQUEIDENTIFIER NULL,
    [Folio]           NVARCHAR (300)   NULL,
    [FechaExpiracion] DATETIME         NULL,
    [Activo]          BIT              NULL,
    PRIMARY KEY CLUSTERED ([IdFolio] ASC)
);


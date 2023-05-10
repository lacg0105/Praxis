CREATE TABLE [Admin].[EntidadxUsuario] (
    [IdUsuario] NVARCHAR (100) NOT NULL,
    [IdEntidad] INT            NOT NULL,
    [IdArea]    INT            NOT NULL,
    [Aprobar]   BIT            NOT NULL,
    [Escritura] BIT            NOT NULL,
    [Lectura]   BIT            NOT NULL,
    CONSTRAINT [PK__Entidadx__679C1C50D005CAD0] PRIMARY KEY CLUSTERED ([IdUsuario] ASC, [IdEntidad] ASC, [IdArea] ASC)
);


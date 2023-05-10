CREATE TABLE [Admin].[ModuloxUsuario] (
    [IdPantalla] INT            NOT NULL,
    [IdUsuario]  NVARCHAR (100) NOT NULL,
    [Aprobar]    BIT            NOT NULL,
    [Escritura]  BIT            NOT NULL,
    [Lectura]    BIT            NOT NULL,
    CONSTRAINT [PK__ModuloxU__AF4EB5B6534A175A] PRIMARY KEY CLUSTERED ([IdPantalla] ASC, [IdUsuario] ASC)
);


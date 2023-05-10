CREATE TABLE [Admin].[ModuloxRol] (
    [IdPantalla]    INT NOT NULL,
    [IdInstitucion] INT NOT NULL,
    [IdRol]         INT NOT NULL,
    [Aprobar]       BIT NOT NULL,
    [Escritura]     BIT NOT NULL,
    [Lectura]       BIT NOT NULL,
    CONSTRAINT [PK_ModuloxRol] PRIMARY KEY CLUSTERED ([IdPantalla] ASC, [IdInstitucion] ASC, [IdRol] ASC)
);


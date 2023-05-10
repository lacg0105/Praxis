CREATE TABLE [Seguridad].[BitacoraNavegacion] (
    [IdBitacora]    INT              IDENTITY (1, 1) NOT NULL,
    [IdUsuario]     NVARCHAR (100)   NOT NULL,
    [IdPantalla]    INT              NOT NULL,
    [IdEvento]      INT              NOT NULL,
    [FechaRegistro] DATETIME         NOT NULL,
    [IdRegistro]    NVARCHAR (100)   NULL,
    [Campo]         NVARCHAR (100)   NULL,
    [ValorActual]   NVARCHAR (MAX)   NULL,
    [ValorNuevo]    NVARCHAR (MAX)   NULL,
    [Parametro]     NVARCHAR (100)   NULL,
    [Acuse]         VARBINARY (MAX)  NULL,
    [Folio]         UNIQUEIDENTIFIER NULL,
    [Host]          NVARCHAR (100)   NULL
);


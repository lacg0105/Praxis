CREATE TABLE [Seguridad].[BitacoraClaveAcceso] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [IdUsuario]         NVARCHAR (100) NOT NULL,
    [IdUsuarioConsulta] NVARCHAR (100) NULL,
    [IdEvento]          INT            NULL,
    [FechaRegistro]     DATETIME       NULL,
    [Campo]             NVARCHAR (255) NULL,
    [ValorActual]       NVARCHAR (255) NULL,
    [ValorNuevo]        NVARCHAR (255) NULL,
    [Acuse]             NVARCHAR (MAX) NULL,
    [Host]              NVARCHAR (255) NULL,
    CONSTRAINT [PK_BitacoraClaveAcceso] PRIMARY KEY CLUSTERED ([Id] ASC)
);


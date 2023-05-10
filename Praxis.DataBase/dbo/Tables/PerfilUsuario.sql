CREATE TABLE [dbo].[PerfilUsuario] (
    [IdUsuario]            UNIQUEIDENTIFIER NOT NULL,
    [FechaNacimiento]      DATE             NOT NULL,
    [Rfc]                  NVARCHAR (30)    NULL,
    [Profesion]            NVARCHAR (300)   NULL,
    [Cedula]               NVARCHAR (30)    NULL,
    [Consultorio]          NVARCHAR (300)   NULL,
    [Direccion]            NVARCHAR (300)   NULL,
    [AlcaldiaMunicipio]    NVARCHAR (300)   NULL,
    [Estado]               NVARCHAR (300)   NULL,
    [CodigoPostal]         NVARCHAR (30)    NULL,
    [CostoConsulta]        FLOAT (53)       NULL,
    [ConsultaDomicilio]    BIT              NULL,
    [ConsultaVideollamada] BIT              NULL,
    CONSTRAINT [PK_PerfilUsuario] PRIMARY KEY CLUSTERED ([IdUsuario] ASC),
    CONSTRAINT [FK_PerfilUsuario_CatUsuario] FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[CatUsuario] ([IdUsuario])
);




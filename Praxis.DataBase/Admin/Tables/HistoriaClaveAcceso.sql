CREATE TABLE [Admin].[HistoriaClaveAcceso] (
    [IdHistoria]    INT              IDENTITY (1, 1) NOT NULL,
    [IdUsuario]     UNIQUEIDENTIFIER NULL,
    [Pwd]           VARBINARY (MAX)  NULL,
    [FechaCreacion] DATETIME         NULL,
    CONSTRAINT [PK__PWDHisto__3214EC07FA1BAD05] PRIMARY KEY CLUSTERED ([IdHistoria] ASC)
);


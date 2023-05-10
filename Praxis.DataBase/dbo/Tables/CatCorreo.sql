CREATE TABLE [dbo].[CatCorreo] (
    [IdCorreo]      INT            NOT NULL,
    [Nombre]        NVARCHAR (50)  NULL,
    [Descripcion]   NVARCHAR (300) NULL,
    [Asunto]        NVARCHAR (50)  NULL,
    [Para]          NVARCHAR (800) NULL,
    [BCC]           NVARCHAR (800) NULL,
    [BCCO]          NVARCHAR (800) NULL,
    [ContenidoHTML] NVARCHAR (MAX) NULL,
    [Seccion]       NVARCHAR (60)  NULL,
    CONSTRAINT [PK__CatCorre__3214EC074502752B] PRIMARY KEY CLUSTERED ([IdCorreo] ASC)
);


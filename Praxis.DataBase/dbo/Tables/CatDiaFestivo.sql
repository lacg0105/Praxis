CREATE TABLE [dbo].[CatDiaFestivo] (
    [IdDiaFestivo] INT            IDENTITY (1, 1) NOT NULL,
    [Fecha]        DATE           NOT NULL,
    [Nombre]       NVARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([IdDiaFestivo] ASC)
);


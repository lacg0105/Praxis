-- =============================================
-- Author:      Angel Cázares
-- Create Date: 21/01/2021
-- Description: Consulta las historias clinicas de los pacientes
-- Prueba:		[dbo].[sp_ModuloMedico_HistoriaClinica_ConsultaPacientes] 
-- =============================================
CREATE PROCEDURE [dbo].[sp_ModuloMedico_HistoriaClinica_ConsultaPacientes]

AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    -- Insert statements for procedure here
	SELECT 
		 usr.IdUsuario
		,usr.Correo
		,usr.Nombre
		,usr.ApellidoPaterno
		,usr.ApellidoMaterno
		,usr.Curp
		,usr.IdEstatusUsuario
		,ISNULL(his.FechaActualizacion,'')				FechaActualizacion
		,IIF(ISNULL(his.IdHistoriaClinica,0) = 0,0,1)	HistoriaClinicaActiva
	FROM CatUsuario usr
	LEFT JOIN HistoriaClinica his
	ON his.IdUsuario = usr.IdUsuario
	WHERE usr.IdRol = 3 -- PACIENTE
END
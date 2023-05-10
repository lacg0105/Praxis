-- =============================================
-- Author:      Angel Cázares
-- Create Date: 08/07/2020
-- Description: Bloqueo de los Usuarios por Inactividadn de Claves de Acceso
-- Prueba:		[dbo].[sp_ModuloAcceso_Claves_BloqueoxInactividad]
-- =============================================
CREATE PROCEDURE [dbo].[sp_ModuloAcceso_Claves_BloqueoxInactividad]
--(
--    -- Add the parameters for the stored procedure here
--	--@DiasBloqueo int
--)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    -- Insert statements for procedure here
	BEGIN TRY 
		DECLARE	@FechaActual Date = GETDATE()
		,@DiasBloqueo int

		 SET @DiasBloqueo =  (SELECT Valor FROM Admin.Parametro WHERE Nombre = 'Inactividad Clave de Acceso')

		;WITH tblRotacionClave
		AS
		(
			SELECT
			 usr.IdUsuario
			,usr.Nombre		NombreUsuario
			,usr.Correo
			,usr.Curp		Curp
			,rol.Nombre		Rol
			,DiasClave = DATEDIFF(DAY,usr.FechaAcceso,@FechaActual)
			FROM CatUsuario usr
			INNER JOIN CatRol rol
			ON usr.IdRol = rol.IdRol
			WHERE usr.IdEstatusUsuario = 1 -- USUARIOS ACTIVOS
		)

		UPDATE CatUsuario SET IdEstatusUsuario = 5 --- USUARIOS BLOQUEADOS POR INACTIVIDAD
		WHERE IdUsuario IN 
		(
			SELECT IdUsuario FROM tblRotacionClave WHERE DiasClave > @DiasBloqueo -- BLOQUEO A LOS n DÍAS
		)
	END TRY  

	BEGIN CATCH  
	-- Insert statements for cath
	END CATCH  

END
-- =============================================
-- Author:      Angel Cázares
-- Create Date: 02/06/2020
-- Description: Consulta del perfil del usuario para el Login_2 por usuario o curp
-- Prueba:		[dbo].[sp_ModuloAcceso_Login1_ConsultaUsuario] 'oscarbush164@gmail.com', ''
-- =============================================
CREATE PROCEDURE [dbo].[sp_ModuloAcceso_Login1_ConsultaUsuario]
(
    -- Add the parameters for the stored procedure here
     @Usuario	 nvarchar(100)
	,@Curp		 nvarchar(100)
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    -- Insert statements for procedure here
	BEGIN TRY 
		IF @Usuario <> ''
			SELECT 
				 usr.NombreImagen
				,usr.Nombre				NombreUsuario
				,usr.ApellidoPaterno	ApellidoPaterno
				,usr.ApellidoMaterno	ApellidoMaterno
				,usr.Curp
				,rol.Nombre				NombreRol
				,usr.IdEstatusUsuario
				,estatus.Nombre			NombreEstatus
				,usr.Correo
				,per.Cedula
				,usr.IdUsuario
			FROM CatUsuario usr
			LEFT JOIN PerfilUsuario per ON usr.IdUsuario = per.IdUsuario 
			INNER JOIN CatRol rol ON usr.IdRol = rol.IdRol
			INNER JOIN CatEstatus_Usuario estatus ON usr.IdEstatusUsuario = estatus.IdEstatusUsuario
			WHERE usr.Correo = @Usuario
		ELSE IF @Curp <> '' 
			SELECT 
				 usr.NombreImagen
				,usr.Nombre				NombreUsuario
				,usr.Curp
				,rol.Nombre				NombreRol
				,usr.IdEstatusUsuario
				,estatus.Nombre			NombreEstatus
				,usr.Correo
				,per.Cedula
			FROM CatUsuario usr
			LEFT JOIN PerfilUsuario per ON usr.IdUsuario = per.IdUsuario 
			INNER JOIN CatRol rol ON usr.IdRol = rol.IdRol
			INNER JOIN CatEstatus_Usuario estatus ON usr.IdEstatusUsuario = estatus.IdEstatusUsuario
			WHERE usr.Curp = @Curp
	END TRY  

	BEGIN CATCH  
	-- Insert statements for cath
	END CATCH  

END

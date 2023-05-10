-- =============================================
-- Author:      LUIS COVARRUBIAS
-- Create Date: 12/06/2020
-- Description: CONSULTA LA CONTRASEÑA DEL USUARIO
-- Prueba:		[dbo].[sp_ModuloAcceso_Login2_ConsultaContraseñaUsuario] 'lcovarrubias@outlook.com', ''
-- =============================================
CREATE PROCEDURE [dbo].[sp_ModuloAcceso_Login2_ConsultaContraseñaUsuario]
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
				 usr.IdUsuario
				,usr.Pwd
			FROM CatUsuario usr
			WHERE usr.Correo = @Usuario
		ELSE IF @Curp <> '' 
			SELECT 
				usr.IdUsuario
			   ,usr.Pwd
			FROM CatUsuario usr
			WHERE usr.Curp = @Curp
	END TRY  

	BEGIN CATCH  
	-- Insert statements for cath
	END CATCH  
END

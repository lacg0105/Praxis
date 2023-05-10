-- =============================================
-- Author:      Angel Cázares
-- Create Date: 03/06/2020
-- Description: Módulos de Acceso por Usuario
-- Prueba:		[dbo].[sp_ModuloAcceso_Modulos_ConsultaxUsuario] '804C79CC-E4D0-42E3-9321-95B59B1A09F9'
-- =============================================
CREATE PROCEDURE [dbo].[sp_ModuloAcceso_Modulos_ConsultaxUsuario]
(
    -- Add the parameters for the stored procedure here
	@IdUsuario nvarchar(100)
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    -- Insert statements for procedure here
	BEGIN TRY 
		
		;WITH cteModuloxUsuario(IdPantalla, IdUsuario, Aprobar, Escritura, Lectura)
		AS
		(
			SELECT 
				 IdPantalla
				,IdUsuario
				,Aprobar
				,Escritura
				,Lectura
			FROM Admin.ModuloxUsuario
			WHERE IdUsuario = @IdUsuario
		)

		SELECT
			-- MODULO --
			 IdModulo = ModuloVM.IdModulo
			,NombreModulo = ModuloVM.Nombre
			,IconoModuo = ISNULL((SELECT Nombre FROM CatIcono WHERE IdIcono = ModuloVM.IdIcono),1)
			-- SUBMODULO --
			,IdSubModulo = SubModuloVM.IdSubModulo
			,NombreSubModulo = SubModuloVM.Nombre
			,IconoSubModulo = ISNULL((SELECT Nombre FROM CatIcono WHERE IdIcono = SubModuloVM.IdIcono),1)
			-- PANTALLA --
			,IdPantalla = PantallaViewModel.IdPantalla
			,NombrePantalla = PantallaViewModel.Nombre
			,IconoPantalla = ISNULL((SELECT Nombre FROM CatIcono WHERE IdIcono = PantallaViewModel.IdIcono),1)
			,Controlador = PantallaViewModel.Controlador
			,Accion = PantallaViewModel.Accion
			,Aprobar = ISNULL((SELECT Aprobar FROM cteModuloxUsuario WHERE IdPantalla = PantallaViewModel.IdPantalla),0)
			,Escritura = ISNULL((SELECT Escritura FROM cteModuloxUsuario WHERE IdPantalla = PantallaViewModel.IdPantalla),0)
			,Lectura = ISNULL((SELECT Lectura FROM cteModuloxUsuario WHERE IdPantalla = PantallaViewModel.IdPantalla),0)
		FROM CatPantalla PantallaViewModel
		LEFT JOIN cteModuloxUsuario cte ON PantallaViewModel.IdPantalla = cte.IdPantalla
		INNER JOIN CatSubModulo SubModuloVM ON PantallaViewModel.IdSubModulo = SubModuloVM.IdSubModulo
		INNER JOIN CatModulo ModuloVM ON SubModuloVM.IdModulo = ModuloVM.IdModulo
		ORDER BY ModuloVM.Nombre, SubModuloVM.Nombre, PantallaViewModel.Nombre
		FOR XML AUTO, ELEMENTS, ROOT('MenuViewModel');

	END TRY  

	BEGIN CATCH  
	-- Insert statements for cath
	END CATCH  

END

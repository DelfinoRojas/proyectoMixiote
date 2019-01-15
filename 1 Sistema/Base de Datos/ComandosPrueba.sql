USE casaMixiote;

/*
	Creaión del SCHEMA para el sistema casaMixiote
*/

CREATE SCHEMA SPsistema;

DROP SCHEMA SP_sistema;

/*----------------------------------------------------------------------------------------*/

/*
	Creaión del procedimiento almacenado SPsistema_FormacionMesa 
	--Inserta el número de mesas indicado para la parte frontal 
	  y el jardin de la tabla FormacionMesa 
*/

CREATE PROCEDURE SPsistema_SetFormacionMesa
	@frontal as int,
	@jardin as int
AS
BEGIN
	INSERT INTO FormacionMesa (parteFrontal,jardin) VALUES(@frontal,@jardin);
END

-- DROP PROCEDURE SPsistema_SetFormacionMesa

	/*	Ejecución del procedimiento almacenado SPsistema_FormacionMesa		*/

EXECUTE SPsistema_FormacionMesa 24,10

SELECT * FROM FormacionMesa

--DELETE FROM FormacionMesa

/*----------------------------------------------------------------------------------------*/

/*
	Creaión del procedimiento almacenado SPsistema_GetFormacion 
	--Devuelve el némero de mesas almacenado en la parte frontal 
	  o el jardin de acuerdo a la petición solicitada por el usuario
*/

CREATE PROCEDURE SPsistema_GetFormacionMesa
	@elec int,
	@result int OUTPUT --Variable de salida
AS
BEGIN
	If (@elec=0)
		SELECT @result=parteFrontal FROM FormacionMesa;
	ELSE IF (@elec=1)
		SELECT @result=jardin FROM FormacionMesa;
END

-- DROP PROCEDURE SPsistema_GetFormacionMesa

/*	Ejecución del procedimiento almacenado SPsistema_GetFormacion		*/

DECLARE @salida int
EXECUTE SPsistema_GetFormacion 1, @salida OUTPUT
SELECT @salida AS nMesas;

--	----------------------------------------------------------------------

SELECT * FROM FormacionMesa;

/*----------------------------------------------------------------------------------------*/

/*
	Creaión del procedimiento almacenado SPsistema_CrearEstadoMesa 
	--Inserta el nombre de cada una de las mesas y al mismo tiempo
	  marca el estado de la mesa ('' para indicar que esta desocupada)
*/
CREATE PROCEDURE SPsistema_CrearEstadoMesa
AS
BEGIN
	Declare 
	@frontal int,
	@jardin int,
	@nMesas int,
	@conta int
	SET @conta=1
	SET @nMesas=0

	SELECT @frontal=parteFrontal FROM FormacionMesa;
	SELECT @jardin=jardin FROM FormacionMesa;
	SET @nMesas=@frontal+@jardin;

	BEGIN -- ----------------------------		Se crea la tabla Mesa
		CREATE TABLE Mesa(
			idMesa int identity (1,1),
			nombreMesa varchar (7) Not Null,
			estado varchar(10) Not Null
			CONSTRAINT pkMesa PRIMARY KEY (idMesa)
		)
	END
	WHILE @conta<=@nMesas -- ------------		Se inserta el nombre de cada una de las mesas
		BEGIN
			INSERT INTO Mesa VALUES('Mesa_'+CONVERT(VARCHAR,@conta),'')
			SET @conta=@conta+1
		END
END

-- DROP PROCEDURE SPsistema_CrearEstadoMesa

/*	Ejecución del procedimiento almacenado SPsistema_CrearEstadoMesa		*/

EXECUTE  SPsistema_CrearEstadoMesa

--	----------------------------------------------------------------------
SELECT * FROM Mesa

--DROP TABLE Mesa
/*----------------------------------------------------------------------------------------*/

/*
	Creaión del procedimiento almacenado SPsistema_AsignarEstadoMesa 
	--Inserta el folio de venta dentro del campo estado para las mesas
	  que haya indicado el usuario como ocupadas
*/

CREATE PROCEDURE SPsistema_AsignarEstadoMesa
	@mesa varchar(7),
	@folio varchar(10)
AS
BEGIN
	IF EXISTS (SELECT * FROM sysobjects WHERE type = 'U' AND name = 'Mesa') -- Verifica la existencia de la tabla Mesa
		UPDATE Mesa SET estado=@folio WHERE nombreMesa=@mesa;
	ELSE
		PRINT 'La tabla Mesa no existe'
END


-- DROP PROCEDURE SPsistema_AsignarEstadoMesa

/*	Ejecución del procedimiento almacenado SPsistema_ActualizarEstadoMesa		*/

EXECUTE  SPsistema_AsignarEstadoMesa 'Mesa_1','F0012'

--	----------------------------------------------------------------------
SELECT * FROM Mesa

--DROP TABLE Mesa

/*----------------------------------------------------------------------------------------*/

/*
	Creaión del procedimiento almacenado SPsistema_LimpiarEstadoMesa 
	--Establece el campo estado de la tabla Mesa como Nada. Ejemplo = ''
*/

CREATE PROCEDURE SPsistema_LimpiarEstadoMesa
	@mesa varchar(7)
AS
BEGIN
	IF EXISTS (SELECT * FROM sysobjects WHERE type = 'U' AND name = 'Mesa') -- Verifica la existencia de la tabla Mesa
		UPDATE Mesa SET estado='' WHERE nombreMesa=@mesa;
	ELSE
		PRINT 'La tabla Mesa no existe'
END


-- DROP PROCEDURE SPsistema_LimpiarEstadoMesa

/*	Ejecución del procedimiento almacenado SPsistema_ActualizarEstadoMesa		*/

EXECUTE  SPsistema_LimpiarEstadoMesa 'Mesa_1';

--	----------------------------------------------------------------------
SELECT * FROM Mesa

--DROP TABLE Mesa

/*----------------------------------------------------------------------------------------*/

/*
	Creaión del procedimiento almacenado SPsistema_GetEstadoMesa 
	--Devuelve el valor almacenado en el campo estado de cada una de las
	  mesas
*/

CREATE PROCEDURE SPsistema_GetEstadoMesa
AS
BEGIN
	IF EXISTS (SELECT * FROM sysobjects WHERE type = 'U' AND name = 'Mesa') -- Verifica la existencia de la tabla Mesa
		SELECT estado FROM Mesa;
	ELSE
		PRINT 'La tabla Mesa no existe'
END


-- DROP PROCEDURE SPsistema_GetEstadoMesa

/*	Ejecución del procedimiento almacenado SPsistema_ActualizarEstadoMesa		*/

EXECUTE  SPsistema_GetEstadoMesa;

--	----------------------------------------------------------------------
SELECT * FROM Mesa

--DROP TABLE Mesa

/*----------------------------------------------------------------------------------------*/

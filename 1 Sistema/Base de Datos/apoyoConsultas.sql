USE casaMixiote;

SELECT * FROM Cortesia;

SELECT * FROM Inventario;

SELECT * FROM Venta;
/*
	En lugar del campo idPlatillo se debe crear el campo platillo varchar (150) donde
	se especifique la descripción del platillo (En caso de que se haya modificado)
	
	El campo cantidad se debe dejar

	Se debe agregar el precio unitario del platillo. RAZÓN: (El precio no se puede obtener del
	inventario porque puede sufir variaciones en su constitución.
	El invetnatrio termina siendo un catálogo muy flexible)
*/

SELECT * FROM PagoCuenta; --Esta tabla existe para conservar una idea de los precios establecidos en tiempos anteriores


/*---------------------------------------------------------------*/
SELECT * FROM Folio;
--DELETE FROM Folio;

INSERT INTO Folio VALUES('F00-12','Mesa_14/','EM-013/','2',1,getDate(),'7:37','')
INSERT INTO Folio VALUES('F00-95','Mesa_1/Mesa_3/','EM-192/','10',2,getDate(),'7:50','')


/*---------------------------------------------------------------*/
SELECT * FROM Empleado;
--DELETE FROM Empleado;

INSERT INTO Empleado VALUES('EM-013','P_891','Fernando','Pérez',Null,'4447893421',Null,'Puebla','17/03/1995','13/09/2016',Null)
INSERT INTO Empleado VALUES('EM-192','P_891','Farid','Fernández','','4445674690','','Puebla','28/02/1993','30/06/2013','')

/*---------------------------------------------------------------*/
SELECT * FROM TipoPersonal
SELECT * FROM TipoPersonal WHERE hEntrada>'9:29';

INSERT INTO TipoPersonal VALUES('P_891','Mesero',100,'9:30')

/*---------------------------------------------------------------*/
/*---------------------------------------------------------------*/
--Tabla temproral Mesa
SELECT * FROM Mesa
SELECT * FROM Folio;

UPDATE Mesa SET estado='F00-12' WHERE nombreMesa='Mesa_14'
UPDATE Mesa SET estado='F00-95' WHERE nombreMesa='Mesa_1' OR nombreMesa='Mesa_3'

SELECT * FROM Folio WHERE folioVenta = 'F00-12'
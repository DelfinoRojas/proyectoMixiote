CREATE DATABASE casaMixiote;
USE casaMixiote;

CREATE TABLE Inventario(
	idPlatillo varchar (10),
	platillo varchar (30) Not Null,
	descripcion varchar (100) Not Null Default 'NA',
	precioCompra decimal (6,2) Not Null,
	precioVenta decimal (6,2) Not Null,
	fechaInicio date,
	CONSTRAINT pkInventario PRIMARY KEY (idPlatillo)
)


/*----------------------------------------------------------------------------------------------------------
DROP TABLE Inventario

/*		Insertar un valor Null de forma correcta
	posicion a insertar = Null (Se recomienda usar una variable de tipo Object desde el sistema)
	posicion a insertar = '07/01/2019' o '07-01-2019'
		Insertar un valor Null de forma incorrecta
	posicion a insertar = 'Null' (Marca conflicto al tratar de convertir una cadena de formato no valido a una fecha)

				Ejemplo:
				'Mixiote de carnero, salsa y tortillas'

INSERT INTO Inventario VALUES ('P001-18','Mixiote',default,30,75.00,default)

	SELECT * FROM Inventario
	DELETE FROM Inventario
*/
--------------------------------------------------------------------------------------------------------------------
*/

CREATE TABLE TipoPersonal(
	idPuesto varchar (10),
	puesto varchar (30) Not Null,
	sueldo decimal (6,2) Not Null,
	hEntrada time
	CONSTRAINT pkTipoPersonal PRIMARY KEY (idPuesto)
)

CREATE TABLE Empleado(
	idEmpleado varchar (10),
	idPuesto varchar (10),
	nombre varchar (30) Not Null,
	apePaterno varchar (30) Not Null,
	apeMaterno varchar (30),
	tel1 varchar (12) Not Null,
	tel2 varchar (12),
	ciudad varchar (30),
	fechaNac date,
	fechaIngresoR date,
	fechaSalidaR date,
	CONSTRAINT pkEmpleado PRIMARY KEY (idEmpleado),
	CONSTRAINT fkEPuesto FOREIGN KEY (idPuesto) REFERENCES TipoPersonal(idPuesto)
)

CREATE TABLE FormacionMesa(
	id int identity(1,1),
	parteFrontal int Not Null,
	jardin int Not Null,
	CONSTRAINT pkFormacionMesa PRIMARY KEY (id)
)

CREATE TABLE Folio(
	folioVenta varchar (10),
	mesa varchar (24) Not Null,
	idEmpleado varchar (10),
	nPersonas int Not Null, 
	nCuentas int Not Null, 
	fechaHoy date Not Null,
	horaEntrada time Not Null,
	horaSalida time Not Null,
	CONSTRAINT pkFolio PRIMARY KEY (folioVenta),
	CONSTRAINT fkFidEmpleado FOREIGN KEY (idEmpleado) REFERENCES Empleado(idEmpleado)
)


CREATE TABLE Venta(
	id int identity (1,1),
	folioVenta varchar (10),
	cuenta int Not Null,
	idPlatillo varchar (10),
	cantidad int Not Null,
	CONSTRAINT pkVenta PRIMARY KEY (id),
	CONSTRAINT fkVfolioVenta FOREIGN KEY (folioVenta) REFERENCES Folio(folioVenta),
	CONSTRAINT fkVidPlatillo FOREIGN KEY (idPlatillo) REFERENCES Inventario(idPlatillo)
)


CREATE TABLE PagoCuenta(
	id int identity(1,1),
	folioVenta varchar(10),
	cuenta int Not Null,
	pago decimal(6,2) Not Null,
	CONSTRAINT pkPagoCuenta PRIMARY KEY (id),
	CONSTRAINT fkPCfolioVenta FOREIGN KEY (folioVenta) REFERENCES Folio(folioVenta)
)

CREATE TABLE Cliente(
	idCliente varchar (10),
	nombre varchar (30) Not Null,
	apePaterno varchar (30) Not Null,
	apeMaterno varchar (30),
	rfc varchar (10) Not Null,
	tel1 varchar (12) Not Null,
	tel2 varchar (12),
	pais varchar (30),
	ciudad varchar (30),
	email varchar (60) Not Null,
	CONSTRAINT pkCliente PRIMARY KEY (idCliente)
)

CREATE TABLE Factura(
	idFactura varchar(10),
	folioVenta varchar(10),
	idCliente varchar(10),
	fechaFactu date Not Null,
	CONSTRAINT pkFactura PRIMARY KEY (idFactura),
	CONSTRAINT fkFfolioVenta FOREIGN KEY (folioVenta) REFERENCES Folio(folioVenta),
	CONSTRAINT fkFidCliente FOREIGN KEY (idCliente) REFERENCES Cliente(idCliente)
)


CREATE TABLE Propina(
	id	int identity(1,1),
	idEmpleado varchar(10),
	folioVenta varchar(10),
	cantidad decimal(6,2) Not Null,
	CONSTRAINT pkPropina PRIMARY KEY (id),
	CONSTRAINT fkPidEmpleado FOREIGN KEY (idEmpleado) REFERENCES Empleado(idEmpleado),
	CONSTRAINT fkPfolioVenta FOREIGN KEY (folioVenta) REFERENCES Folio(folioVenta)	
)


CREATE TABLE Proveedor(
	idProveedor varchar(10),
	nombreComercial varchar(50) Not Null,
	gerente varchar(70) Not Null Default 'NA',
	tel1 varchar(12) Not Null Default 'NA',
	tel2 varchar(12),
	ciudad varchar(30) Not Null Default 'NA',
	direccion varchar(70) Not Null Default 'NA',
	fechaVinculacion date,
	CONSTRAINT pkProveedor PRIMARY KEY (idProveedor)
)


CREATE TABLE Compra(
	id int identity(1,1),
	producto varchar(70) Not Null,
	cantidad decimal(6,2) Not Null,
	idProveedor varchar(10),
	fechaCompra dateTime Not Null,
	CONSTRAINT pkCompra PRIMARY KEY (id),
	CONSTRAINT fkCidProveedor FOREIGN KEY (idProveedor) REFERENCES Proveedor(idProveedor)	
)

CREATE TABLE Cortesia(
	id int identity(1,1),
	folioVenta varchar(10),
	cantidad decimal(6,2) Not Null,
	CONSTRAINT pkCortesia PRIMARY KEY (id),
	CONSTRAINT fkCfolioVenta FOREIGN KEY (folioVenta) REFERENCES Folio(folioVenta)
)

SP-'casaMixiote'

/*	Esta tabla se generará a partir de un procedimiento 
	almacenado
CREATE TABLE Mesa(
	idMesa varchar(10),
	nombreMesa varchar(20) Not Null,
	estado varchar(10) Not Null,
	CONSTRAINT pkMesa PRIMARY KEY (idMesa),
)

*/
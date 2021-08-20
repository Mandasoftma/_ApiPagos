## Api de pagos
Prueba Técnica Desarrollo -.NET Core 3.1

## Prerequisites
 * Docker instalado
 * .Net Core instalado
 * VS Code instalado (opcional)
 * Postman desktop
 * Motor Base de Datos Local SQL SERVER
 
## Conocimientos aplicados: 
 * Desarrollo C# en .NET Core
 * Desarrollo EF en .NET Core
 * Versionamiento de código (Git)
 * Conocimiento Patrones de Arquitectura de Software.
 * Manejo de Base de Datos 
 * Buenas Practicas de Desarrollo.
 
# Introducción:
  El Proyecto de evaluación consiste en simular una operación de pago típica en una solución de comercio electrónico.
  El servicio de pago recibirá pedidos en un Endpoint REST. Cuando se invoca el servicio de pago, se realizarán 2 operaciones:
    1. Facturar la suma de todos los productos al usuario en el servicio Facturar
    2. Llamar al servicio de Logística para crear un pedido enviado.

# Que hacen estas Apis:
  Para cumplir con el desafío se desarrollaron tres Apis:
  * Pagos
  * Factura
  * Logística
  
  El api de pago hace las veces de integrador de los otros dos servicios (Factura/logística)
  1.	Se consume el api de facturas y se generar el registro en base de datos con los productos ingresados.
  2.	Una vez creada la factura de manera satisfactoria se procede consume el servicio de logística el cual generar la guía de entrega de los productos adquiridos.
  
  
# Documentos:
  * Arquitectura.png
  * MER.png
  * PagosTuya.postman_collection.json
  * Script_CreacionTablas.sql
  * Oas
  	* Facturas => Api_de_facturas.json
  	* Logistica => Api_de_logistica.json
  	* Pagos => Api_de_pago.json
  	
# Cómo inicializar ,compilar y ejecutar el proyecto:
  1. Descargue el proyecto en un repositorio local.
  2. Una vez descargado en su repositorio loca, instale las dependencias utilizando
  	npm install
  3. Abra SQL server y cree la base de datos con el siguiente comando:
  	CREATE DATABASE DbPagos
  4. Ejecute el script .sql que se encuentra en los documentos del repositorio
  	Script_CreacionTablas.sql
  5. Configure la conexión de la base de datos para las Apis de facturas y logística
  	En el archivo <appsettings.Development.json>
  6. Abra la solución en visual estudio 2019 y verifique que la opción de proyecto de inicio múltiple este activa y que cada proyecto tenga la acción en iniciar.
  7. Ahora si play en vs2019.
 
# Consumo y uso de las apis:
 1. Importe la colección postman
  	PagosTuya.postman_collection.json
 2. Una importada la colección ubíquese en la carpeta de Pagos
 3. Y se visualizan los endpoint para generar la factura y consultar
 4. Para generar una factura el método post requiere un body con los siguientes parámetros:  

#Body
   
      
	int Id => de 1 a 1000
	int ClienteId => de 1 al 1000
	Lista productos que recibe la siguiente estructura
	
	int Id => de 1 a 1000
        int ProductoId => de 1 a 1000 esto pensando en generar api de productos.
        int Cantidad => de 1 a 1000
        double Precio
	
	Ejemplo:
		{  
		  "clienteId": 300,
		  "productos": [
			{
			  "id": 1,
			  "productoId": 2,
			  "cantidad": 3,
			  "precio": 500
			},
			 {
			  "id": 2,
			  "productoId": 3,
			  "cantidad": 4,
			  "precio": 600
			},
			 {
			  "id": 3,
			  "productoId": 1,
			  "cantidad": 10,
			  "precio": 1200
			}
		  ]
		}
		
		Respuesta:
		
		{
			"factura": {
				"id": "aab08cec-0c77-4abd-8237-dce082f31fb1",
				"fechaFactura": "2021-08-20T00:59:58.0761366-05:00",
				"clienteId": 300,
				"total": 12000
			},
			"productos": [
				{
					"id": 1,
					"productoId": 2,
					"cantidad": 3,
					"precio": 500
				},
				{
					"id": 2,
					"productoId": 3,
					"cantidad": 4,
					"precio": 600
				},
				{
					"id": 3,
					"productoId": 1,
					"cantidad": 10,
					"precio": 1200
				}
			],
			"logistica": {
				"id": 1002,
				"direccion": "Calle 893 Esquina",
				"ciudad": "Barranquilla",
				"descripcion": "Guía generada de forma exitosa",
				"estado": "True"
			}
		}


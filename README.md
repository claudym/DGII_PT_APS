DGII - Prueba Tecnica - Analista Programador Senior 2023

Diseñar e implementar una aplicación para ayudar a los directivos de la institución.

Los directivos necesitan un listado de los contribuyentes (persona o entidad que tiene la obligación de
pagar un impuesto), y el total de la suma del ITBIS (Impuesto sobre Transferencias de Bienes
Industrializados y Servicios) de sus comprobantes fiscales reportados.

Aqui detallo los pasos para correr el proyecto:

# Backend

1. Crear un archivo ".env" dentro de Backend/Api/ con las credenciales de la DB (serverValue, dbName, dbUser, y dbPassword son los valores reales de la base de datos a conectar):
   'Backend/Api/.env':
   DB_SERVER=serverValue
   DB_NAME=dbName
   DB_USER=dbUser
   DB_PASSWORD=dbPassword

2. Se puede correr en Visual Studio o usando .NET CLI (correr "dotnet run" dentro del directorio Backend/Api).

## Endpoints:

1. GET /api/Contribuyentes:
   Este endpoint devuelve un listado de todos los contribuyentes, incluyendo información como su RNC/Cédula, nombre, tipo y estatus. Esto permite a los directivos obtener el listado de los contribuyentes.

2. GET /api/ComprobantesFiscales:

Este endpoint devuelve un listado de todos los comprobantes fiscales reportados. Cada comprobante fiscal incluye información como el RNC/Cédula asociado, número de comprobante (NCF), monto y el ITBIS correspondiente. Esto permite a los directivos acceder a los comprobantes fiscales.

3. GET /api/ComprobantesFiscales/{rncCedula}/ITBIS/Total:

Este endpoint devuelve el total de la suma del ITBIS de los comprobantes fiscales de un contribuyente específico identificado por su RNC/Cédula. Esto permite a los directivos obtener el total de la suma del ITBIS de un contribuyente en particular.

4. GET /api/ComprobantesFiscales/ITBIS/Total:

Este endpoint devuelve una lista de la suma del ITBIS de todos los comprobantes fiscales agrupados por el campo "RNC/Cédula". Esto permite a los directivos obtener la suma del ITBIS de todos los comprobantes fiscales por cada contribuyente.

# Frontend

1. Crear un archivo ".env" en Frontend/frontend/ con el react app api url (el url puede ser distinto):
   'Frontend/frontend/.env':
   REACT_APP_API_URL=https://localhost:7220/api

2. Correr using 'npm start' en Frontend/frontend

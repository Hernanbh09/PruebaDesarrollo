# Proyecto ApiProductos & ClienteWeb
Este repositorio contiene dos proyectos:

1. ApiProductos: Microservicio en C# (.NET 6+) que expone una API REST para gestionar productos.  
2. ClienteWeb: Aplicación web (HTML + JavaScript) que consume la API y muestra un listado y formulario de productos.

---
## 1. Requisitos previos

- .NET 6 SDK o superior  
- SQL Server
- Navegador moderno  
- Git
---
## 2. Crear la base de datos
1. Abre **SQL Server Management Studio** (u otra herramienta de tu preferencia).  
2. Ejecuta el script `ApiProductos/Script.sql`:

## 3. Configuración de la API (ApiProductos)
1. Abre ApiProductos/ApiProductos/appsettings.json.
2. Localiza la sección "ConnectionStrings" y ajusta la conexión a tu servidor:
{
  "ConnectionStrings": {
    "Conexion": "Server=<TU_SERVIDOR>\\SQLEXPRESS;Database=ApiProductos;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}

## 4. Configuración del Cliente Web (ClienteWeb)
1. Abre ClienteWeb/ClienteWeb/appsettings.json.
2. Ajusta la clave "ApiBaseUrl" con la URL y puerto donde esté corriendo tu API:
{
  "ApiBaseUrl": "https://localhost:5001/api/Productos"
}

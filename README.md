
# PruebaTecnicaRedArbor2024

# InventorySolution

## Descripción del Proyecto 📦

**InventorySolution** es una solución completa que gestiona productos, movimientos de inventario y categorías. La aplicación está desarrollada en **ASP.NET Core** y se compone de múltiples proyectos que implementan un diseño basado en microservicios. Utiliza **SQL Server** como base de datos.


### Proyectos en la solución:

1. **InventoryAPIAuth**: Servicio de autenticación que gestiona la autenticación de usuarios y la emisión de tokens JWT.
2. **InventoryAPIRead**: Servicio que expone los endpoints de solo lectura para productos, movimientos de inventario y categorías.
3. **InventoryAPIWrite**: Servicio que maneja la escritura y actualización de productos, movimientos de inventario y categorías.
4. **InventoryAuthLibrary**: Biblioteca compartida para la gestión de autenticación entre servicios.
5. **InventorySharedLibrary**: Biblioteca compartida que contiene entidades y DTOs utilizados en toda la solución.
6. **InventoryTest**: Proyecto de pruebas unitarias y de integración.

---

## Requisitos del Sistema 🛠️

- **Docker** y **Docker Compose** instalados en tu sistema.
- **.NET SDK 8.0** o superior (solo si deseas ejecutar la solución sin Docker).
- **Git** instalado para la clonación del repositorio.

---

## Estructura del Proyecto 📂

```bash
InventorySolution/
│
├── InventoryAPIAuth/                                                   # Servicio de autenticación
├── InventoryAPIRead/                                                   # Servicio de lectura de datos
├── InventoryAPIWrite/                                                  # Servicio de escritura de datos
├── InventoryAuthLibrary/                                               # Biblioteca compartida para autenticación
├── InventorySharedLibrary/                                             # Biblioteca compartida con entidades y DTOs
├── InventoryTest/                                                      # Proyecto de pruebas
├── docker-compose.yml                                                  # Archivo de configuración para docker-compose
└── Inventory API Collection.postman_collection.json                    # Archivo de collection para postman
└── README.md                                                           # Readme de la aplicación
```


## Ejecución 🛠️

Para ejecutar el proyecto debes usar el comando:
```bash
  npm run deploy
```
en la ruta donde se encuentra el archivo 
```bash
  docker-compose.yml
```
- **Ejecutar** el archivo **docker-compose.yml**
- **Comando** 

## Aplicar migraciones

Es necesario aplicar las migraciones para el correcto funcionamiento de la Base de Datos, para esto puedes ejecutarlas ubicandote en el directorion del proyecto **InventoryAPIRead** y ejecutar el siguiente comando

```bash
  dotnet ef database update
```

## Ejecución desde Visual Studio con Múltiples APIs 🚀

Para ejecutar los proyectos InventoryAPIAuth, InventoryAPIRead, y InventoryAPIWrite simultáneamente desde Visual Studio, sigue estos pasos:

Configuración en Visual Studio 🖥️
1. Abrir la solución en Visual Studio
Abre Visual Studio y carga la solución InventorySolution.sln desde el explorador de archivos.

2. Configurar múltiples proyectos de inicio
Para ejecutar las tres APIs al mismo tiempo:

Haz clic derecho en la solución desde el Solution Explorer.
Selecciona Properties (Propiedades).
En el panel de la izquierda, selecciona Startup Project (Proyecto de inicio).
Marca la opción Multiple startup projects (Múltiples proyectos de inicio).
Configura los siguientes proyectos para que se inicien simultáneamente seleccionando Start en la columna Action:

**InventoryAPIAuth**

**InventoryAPIRead**

**InventoryAPIWrite**

3. Ejecuta la aplicación



## Postman ![Postman](https://img.shields.io/badge/Postman-FF6C37?logo=postman&logoColor=white)

Puedes importar la collection de postman la cual se encuentra en la raiz del repositorio con el nombre.

```bash
  Inventory API Collection.postman_collection.json
```


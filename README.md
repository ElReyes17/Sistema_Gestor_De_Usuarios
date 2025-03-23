## Contenido

Este proyecto es una minimal API basada en sistema de autenticación de usuarios, la cual fue desarrollada en .NET 8 con una proyección basada en Clean Architecture, específicamente con la arquitectura ONION. 

En este proyecto se implementaron diversos patrones de diseños funcionales dentro de la arquitectura y conveniente a casos en concreto como:
    - CQRS Y MediatR
    - Repository
    - Decorator
    - Dependency Injection
    - Dto
    - Unit Of Work
    - Option Pattern
    - Result Pattern.
    
Dentro del flujo podemos encontrar diversas librerías y prácticas que nos ayudan a la mejora del sistema, de estas se pueden destacar:
    - AutoMapper
    - Middlewares
    - Filters
    - Helpers.
    
Todo bajo la conexión principal basada en un ORM, que en este caso es Entity Framework Core, diseñando la base de datos desde fluent API con Code First, utilizando contexto, configurations y dbSet para probar las acciones de SQL.
  

## Pasos para configurar el entorno de la app.

### 1. Ir al AppSettingJson

Dentro del AppSettingJson en DefaultConnection, cambiaremos el servidor hacia donde apunta la base de datos a crear.

```
"DefaultConnection": "Server={VALOR_A_MODIFICAR};Database=Sistema_Gestor_Usuarios;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
```

### 2. Comando para ejecutar la configuración de la base de datos.

Una vez completado el paso anterior, se debe abrir el Package manager Console. Para que las configuraciones hechas en fluentApi se puedan aplicar a base de datos,
colocar el comando:
```
Update-Database
```

### 3. Endpoints

Luego de que hayamos cargado la base de datos, nos encontraremos con 5 endpoints, 4 que tienen que ver con User y 1 con Login.
Hay 2 endpoint que se pueden probar sin autenticarse:

```
Post - User
Post - Login
```

### 4. Autenticación y JWT

Después crear un usuario o loguearte, te devolverá una respuesta con el token, el cual utilizarás con bearer para poder autenticarte 
y que se permita utilizar los otros 3 endpoint que hay:
```
Get - User
Patch - User
Delete - User
```
En la parte superior de swagger te aparecerá el botón authorize, que al presionarlo te permitirá colocar el JWT.
Se debe de introducir de la siguiente manera
```
Bearer TOKEN
Ojo: Se debe poner la palabra Bearer primero, seguido del token sin las comillas.
```

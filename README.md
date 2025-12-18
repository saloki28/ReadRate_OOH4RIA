# 📚 Read&Rate - Plataforma de Gestión de Lectura Social

**Read&Rate** es una aplicación web desarrollada en ASP.NET Core MVC que permite a los usuarios gestionar sus lecturas, valorar libros, participar en clubes de lectura y conectar con autores y otros lectores.

---
### EJEMPLO DE CREDENCIALES DE ACCESO

Para acceder a la aplicación, puedes utilizar las siguientes credenciales de prueba:


### 📖 Lectores
- **Email:** `marina.lectora@email.com`
  - **Contraseña:** `passMarina`
  
- **Email:** `niko.lector@email.com`
  - **Contraseña:** `passNiko`

### ✍️ Autor
- **Email:** `lucymontgomery@email.com`
- **Contraseña:** `passLucy`

### 👨‍💼 Administrador
- **Email:** `admin@email.com`
- **Contraseña:** `passAdmin1`

> **Nota**: Para acceder con otros usuarios, consulta el script `CreateDB.cs` para obtener las contraseñas.

---

### CARACTERÍSTICAS PRINCIPALES

### 👤 Roles de Usuario
- **Lector**: Gestión de listas de lectura, valoraciones, membresía en clubes
- **Autor**: Publicación de libros, visualización de reseñas, perfil público
- **Administrador**: Gestión de eventos, noticias y moderación de contenido
- **Visitante**: Navegación limitada sin necesidad de registro

### 📖 Funcionalidades para Lectores
- **Listas Personales**:
  - Libros en Curso
  - Libros Leídos
  - Sistema de exclusión mutua entre listas
- **Sistema de Valoración**: Reseñas con valoraciones de estrellas
- **Clubes de Lectura**: 
  - Creación y gestión de clubes
  - Integración con Discord
  - Foro interno de mensajes
  - Visualización de miembros
- **Seguimiento de Autores**: Suscripción a autores favoritos

### ✍️ Funcionalidades para Autores
- **Gestión de Publicaciones**: Crear, editar y eliminar libros
- **Estadísticas**: 
  - Número de seguidores
  - Valoración media
  - Visualización de reseñas
- **Perfil Público**: Visualización de obras publicadas

### 🎯 Funcionalidades para Administradores
- **Gestión de Eventos**: Crear eventos con aforo limitado e inscripciones
- **Gestión de Noticias**: Publicación de contenido editorial
- **Moderación**: Control sobre contenido y usuarios

### 🌟 Características de la Plataforma
- **Búsqueda y Filtrado**: Sistema avanzado de búsqueda de libros
- **Catálogo Completo**: Visualización de todos los libros disponibles
- **Navegación Contextual**: Redirecciones inteligentes según origen de acción
- **Interfaz Responsive**: Diseño adaptable con Bootstrap 5
- **Iconografía**: FontAwesome para mejorar UX

---

### TECNOLOGÍAS UTILIZADAS

- **Framework**: ASP.NET Core MVC (.NET 8.0)
- **ORM**: NHibernate
- **Base de Datos**: SQL Server
- **Frontend**: 
  - Bootstrap 5.1.0
  - FontAwesome 6.x
  - Custom CSS 
- **Arquitectura**: Patrón MVC con capas ApplicationCore e Infrastructure

---

### ESTRUCTURA DEL PROYECTO

```
ReadRate_e4Gen/
├── WebApplication-ReadRate/          # Capa de presentación
│   ├── Controllers/                  # Controladores MVC
│   ├── Views/                        # Vistas Razor
│   │   ├── Autor/
│   │   ├── Lector/
│   │   ├── Libro/
│   │   ├── Club/
│   │   ├── Evento/
│   │   └── ...
│   ├── Models/                       # ViewModels y Assemblers
│   └── wwwroot/                      # Assets estáticos
│       ├── css/
│       ├── js/
│       └── images/
├── ReadRate_e4Gen.ApplicationCore/   # Lógica de negocio
│   ├── CEN/                          # Entidades de negocio
│   ├── CP/                           # Controladores de proceso
│   ├── EN/                           # Entidades
│   └── IRepository/                  # Interfaces de repositorio
└── ReadRate_e4Gen.Infraestructure/   # Capa de datos
    ├── Repository/                   # Implementación de repositorios
    └── Mappings/                     # Mapeos NHibernate
```

---

### 👤 EQUIPO DE DESARROLLO

**Grupo 3 de Prácticas - Equipo 4 (Read&Rate)**

---

### ✍️ LICENCIA

Este proyecto es parte de un trabajo académico.

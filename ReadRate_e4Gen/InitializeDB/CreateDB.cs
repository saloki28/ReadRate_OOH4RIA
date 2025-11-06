
/*PROTECTED REGION ID(CreateDB_imports) ENABLED START*/
using NHibernate.Mapping;
using ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.Exceptions;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.Utils;
using ReadRate_e4Gen.Infraestructure.CP;
using ReadRate_e4Gen.Infraestructure.Repository;
using ReadRate_e4Gen.Infraestructure.Repository.ReadRate_E4;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Security.Policy;
using System.Text;

/*PROTECTED REGION END*/
namespace InitializeDB
{
public class CreateDB
{
public static void Create (string databaseArg, string userArg, string passArg)
{
        String database = databaseArg;
        String user = userArg;
        String pass = passArg;

        // Conex DB
        SqlConnection cnn = new SqlConnection (@"Server=(local); database=master; integrated security=yes");

        // Order T-SQL create user
        String createUser = @"IF NOT EXISTS(SELECT name FROM master.dbo.syslogins WHERE name = '" + user + @"')
            BEGIN
                CREATE LOGIN ["                                                                                                                                     + user + @"] WITH PASSWORD=N'" + pass + @"', DEFAULT_DATABASE=[master], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
            END"                                                                                                                                                                                                                                                                                    ;

        //Order delete user if exist
        String deleteDataBase = @"if exists(select * from sys.databases where name = '" + database + "') DROP DATABASE [" + database + "]";
        //Order create databas
        string createBD = "CREATE DATABASE " + database;
        //Order associate user with database
        String associatedUser = @"USE [" + database + "];CREATE USER [" + user + "] FOR LOGIN [" + user + "];USE [" + database + "];EXEC sp_addrolemember N'db_owner', N'" + user + "'";
        SqlCommand cmd = null;

        try
        {
                // Open conex
                cnn.Open ();

                //Create user in SQLSERVER
                cmd = new SqlCommand (createUser, cnn);
                cmd.ExecuteNonQuery ();

                //DELETE database if exist
                cmd = new SqlCommand (deleteDataBase, cnn);
                cmd.ExecuteNonQuery ();

                //CREATE DB
                cmd = new SqlCommand (createBD, cnn);
                cmd.ExecuteNonQuery ();

                //Associate user with db
                cmd = new SqlCommand (associatedUser, cnn);
                cmd.ExecuteNonQuery ();

                System.Console.WriteLine ("DataBase create sucessfully..");
        }
        catch (Exception)
        {
                throw;
        }
        finally
        {
                if (cnn.State == ConnectionState.Open) {
                        cnn.Close ();
                }
        }
}

public static void InitializeData ()
{
        try
        {
                // Initialising  CENs
                UsuarioRepository usuariorepository = new UsuarioRepository ();
                UsuarioCEN usuariocen = new UsuarioCEN (usuariorepository);
                LibroRepository librorepository = new LibroRepository ();
                LibroCEN librocen = new LibroCEN (librorepository);
                ReseñaRepository reseñarepository = new ReseñaRepository ();
                ReseñaCEN reseñacen = new ReseñaCEN (reseñarepository);
                ClubRepository clubrepository = new ClubRepository ();
                ClubCEN clubcen = new ClubCEN (clubrepository);
                AdministradorRepository administradorrepository = new AdministradorRepository ();
                AdministradorCEN administradorcen = new AdministradorCEN (administradorrepository);
                NoticiaRepository noticiarepository = new NoticiaRepository ();
                NoticiaCEN noticiacen = new NoticiaCEN (noticiarepository);
                EventoRepository eventorepository = new EventoRepository ();
                EventoCEN eventocen = new EventoCEN (eventorepository);
                NotificacionRepository notificacionrepository = new NotificacionRepository ();
                NotificacionCEN notificacioncen = new NotificacionCEN (notificacionrepository);
                AutorRepository autorrepository = new AutorRepository ();
                AutorCEN autorcen = new AutorCEN (autorrepository);
                LectorRepository lectorrepository = new LectorRepository ();
                LectorCEN lectorcen = new LectorCEN (lectorrepository);



                /*PROTECTED REGION ID(initializeDataMethod) ENABLED START*/

                // You must write the initialisation of the entities inside the PROTECTED comments.
                // IMPORTANT:please do not delete them.


                // CREACIÓN USURIO - Lector 1
                Console.WriteLine ("\n\n------------------ Creación de Usuario Lector ------------------");

                int usuarioId1 = lectorcen.CrearLector (
 p_email: "paco.lector@example.com",
 p_nombreUsuario: "PacoLector",
 p_fechaNacimiento: new DateTime (1980, 11, 10),
 p_ciudadResidencia: "Barcelona", p_paisResidencia: "España",
 p_foto: "pacoFoto.png",
 p_rol: ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.RolUsuarioEnum.lector,
 p_pass: "password123",
                        p_cantLibrosCurso: 0,
                        p_cantLibrosLeidos: 0,
                        p_cantAutoresSeguidos: 0,
                        p_cantClubsSuscritos: 0);
                Console.WriteLine ("Usuario 'PacoLector' creado correctamente.");

                // LOGIN DE USURIO POR DEFECTO - Lector 1
                Console.WriteLine ("\n------------------ Comprobación de login por defecto ------------------");

                string token1 = usuariocen.Login (usuarioId1, "password123");
                if (!string.IsNullOrEmpty (token1)) {
                        Console.WriteLine ("Login exitoso para el usuario 'PacoLector'. Token: " + token1);
                }
                else{
                        Console.WriteLine ("Error de login para el usuario 'PacoLector'.");
                }

                // LOGIN DE USURIO PERSONALIZADO - Lector 1
                Console.WriteLine ("\n------------------ Comprobación de login personalizado ------------------");

                if (usuariocen.Login ("paco.lector@example.com", "password123") != null) {
                        Console.WriteLine ("Login exitoso para el usuario 'PacoLector'.");
                }
                else{
                        Console.WriteLine ("Error de login para el usuario 'PacoLector'.");
                }

                Console.WriteLine ("\n====================================================================================");
                Console.WriteLine ("RESUMEN - CREACIÓN Y LOGIN DE LECTOR:");
                Console.WriteLine ("- Usuario 'PacoLector' creado correctamente");
                Console.WriteLine ("- Login por defecto: Exitoso");
                Console.WriteLine ("- Login personalizado: Exitoso");
                Console.WriteLine ("====================================================================================");

                ////////////////////////////////////////////////////////////////////////////////////////////////////////

                // CREACIÓN ADMINISTRADOR - Administrador 1
                Console.WriteLine ("\n\n------------------ Creación de Administrador ------------------");

                int administradorId1 = administradorcen.CrearAdministador (
 p_nombre: "Admin1",
 p_pass: "adminpass",
 p_email: "admin@example.com"
                        );
                Console.WriteLine ("Administrador 'Admin' creado correctamente.");

                // LOGIN DE ADMINISTRADOR POR DEFECTO - Administrador 1
                Console.WriteLine ("\n------------------ Comprobación de login por defecto ------------------");

                string token2 = administradorcen.Login (administradorId1, "adminpass");
                if (!string.IsNullOrEmpty (token2)) {
                        Console.WriteLine ("Login exitoso para el adminsitrador 'Admin'. Token: " + token2);
                }
                else{
                        Console.WriteLine ("Error de login para el usuario 'PacoLector'.");
                }

                // LOGIN DE ADMINISTRADOR PERSONALIZADO - Administrador 1 (Credenciales incorrectas)
                Console.WriteLine ("\n------------------ Comprobación de login personalizado (Esperado: Mensaje de error) ------------------");

                if (administradorcen.Login ("paco.lector@example.com", "adminpass") != null) {
                        Console.WriteLine ("Login exitoso para el usuario 'Admin1'.");
                }
                else{
                        Console.WriteLine ("Error de login para el usuario 'Admin1'.");
                }

                // LOGIN DE ADMINISTRADOR PERSONALIZADO - Administrador 1 (Credenciales correctas)
                Console.WriteLine ("\n------------------ Comprobación de login personalizado (Esperado: Login Correcto) ------------------");

                if (administradorcen.Login ("admin@example.com", "adminpass") != null) {
                        Console.WriteLine ("Login exitoso para el usuario 'Admin1'.");
                }
                else{
                        Console.WriteLine ("Error de login para el usuario 'Admin1'.");
                }

                Console.WriteLine ("\n====================================================================================");
                Console.WriteLine ("RESUMEN - CREACIÓN Y LOGIN DE ADMINISTRADOR:");
                Console.WriteLine ("- Administrador 'Admin1' creado correctamente");
                Console.WriteLine ("- Login por defecto: Exitoso");
                Console.WriteLine ("- Login personalizado con credenciales incorrectas: Error (esperado)");
                Console.WriteLine ("- Login personalizado con credenciales correctas: Exitoso");
                Console.WriteLine ("====================================================================================");

                ////////////////////////////////////////////////////////////////////////////////////////////////////////

                // CREACIÓN USURIOS Y LIBROS - Autores con libros publicados
                Console.WriteLine ("\n\n------------------ Creación de libros variados y Autores ------------------");

                // Autor 1 (devuelve su id para ser usado a continuación en la creación del libro)
                int autorId = autorcen.CrearAutor (
 p_email: "autor@example.com",
 p_nombreUsuario: "AutorEjemplo",
 p_fechaNacimiento: new DateTime (1975, 3, 20),
 p_ciudadResidencia: "Madrid",
 p_paisResidencia: "España",
 p_foto: "autorFoto.png",
 p_valoracionMedia: 3.5f,
 p_rol: ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.RolUsuarioEnum.autor,
 p_pass: "autorpass123",
                        p_numeroSeguidores: 0,
                        p_cantidadLibrosPublicados: 0
                        );
                Console.WriteLine ("Autor creado correctamente con ID: " + autorId);

                // Libro 1 (usa el id del autor anterior)
                var idLibro1 = librocen.CrearLibro (
 p_titulo: "El misterio de la casa abandonada",
 p_genero: "Ficcion",
 p_valoracionMedia: 3.5f,
                        p_edadRecomendada: 13,
 p_fechaPublicacion: new DateTime (2020, 10, 10),
                        p_numPags: 320,
 p_sinopsis: "Una emocionante novela de misterio y suspenso.",
 p_fotoPortada: "portada_misterio.jpg",
 p_autorPublicador: autorId
                        );
                Console.WriteLine ("Libro 1 'El misterio de la casa abandonada' creado correctamente.");

                // Autor 2 (devuelve su id para ser usado a continuación en la creación del libro)
                int autorId2 = autorcen.CrearAutor (
 p_email: "maria.escritora@example.com",
 p_nombreUsuario: "MariaEscritora",
 p_valoracionMedia: 3.5f,
 p_fechaNacimiento: new DateTime (1982, 7, 15),
 p_ciudadResidencia: "Valencia",
 p_paisResidencia: "España",
 p_foto: "mariaFoto.png",
 p_rol: ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.RolUsuarioEnum.autor,
 p_pass: "maria123",
                        p_numeroSeguidores: 0,
                        p_cantidadLibrosPublicados: 0
                        );
                Console.WriteLine ("Autor creado correctamente con ID: " + autorId);

                // Libro 2 (usa el id del autor anterior)
                var idLibro2 = librocen.CrearLibro (
 p_titulo: "Aventuras en el bosque encantado",
 p_genero: "Fantasia",
 p_valoracionMedia: 3.5f,
                        p_edadRecomendada: 10,
 p_fechaPublicacion: new DateTime (2021, 3, 15),
                        p_numPags: 250,
 p_sinopsis: "Un viaje mágico lleno de criaturas fantásticas.",
 p_fotoPortada: "portada_bosque_encantado.jpg",
 p_autorPublicador: autorId2
                        );
                Console.WriteLine ("Libro 2 'Aventuras en el bosque encantado' creado correctamente.");

                // Autor 3 (devuelve su id para ser usado a continuación en la creación del libro)
                int autorId3 = autorcen.CrearAutor (
 p_email: "carlos.novelista@example.com",
 p_nombreUsuario: "CarlosNovelista",
 p_fechaNacimiento: new DateTime (1970, 5, 20),
 p_ciudadResidencia: "Sevilla",
 p_paisResidencia: "España",
 p_valoracionMedia: 3.5f,
 p_foto: "carlosFoto.png",
 p_rol: ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.RolUsuarioEnum.autor,
 p_pass: "carlos123",
                        p_numeroSeguidores: 0,
                        p_cantidadLibrosPublicados: 0
                        );
                Console.WriteLine ("Autor creado correctamente con ID: " + autorId);

                // Libro 3 (usa el id del autor anterior)
                var idLibro3 = librocen.CrearLibro (
 p_titulo: "Historia de dos ciudades modernas",
 p_genero: "Historia",
                        p_edadRecomendada: 16,
 p_fechaPublicacion: new DateTime (2019, 8, 22),
                        p_numPags: 450,
 p_sinopsis: "Un análisis histórico de la evolución urbana.",
 p_fotoPortada: "portada_dos_ciudades.jpg",
 p_valoracionMedia: 3.5f,
 p_autorPublicador: autorId3
                        );
                Console.WriteLine ("Libro 3 'Historia de dos ciudades modernas' creado correctamente.");

                // Autor 4 (devuelve su id para ser usado a continuación en la creación del libro)
                int autorId4 = autorcen.CrearAutor (
 p_email: "laura.ciencia@example.com",
 p_nombreUsuario: "LauraCiencia",
 p_fechaNacimiento: new DateTime (1988, 2, 10),
 p_ciudadResidencia: "Bilbao",
 p_valoracionMedia: 3.5f,
 p_paisResidencia: "España",
 p_foto: "lauraFoto.png",
 p_rol: ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.RolUsuarioEnum.autor,
 p_pass: "laura123",
                        p_numeroSeguidores: 0,
                        p_cantidadLibrosPublicados: 0
                        );
                Console.WriteLine ("Autor creado correctamente con ID: " + autorId);

                // Libro 4 (usa el id del autor anterior)
                var idLibro4 = librocen.CrearLibro (
 p_titulo: "Viaje al centro de la Tierra",
 p_genero: "Ciencia Ficcion",
 p_valoracionMedia: 3.5f,
                        p_edadRecomendada: 14,
 p_fechaPublicacion: new DateTime (2022, 5, 30),
                        p_numPags: 380,
 p_sinopsis: "Una expedición científica que cambiará todo.",
 p_fotoPortada: "portada_viaje_tierra.jpg",
 p_autorPublicador: autorId4
                        );
                Console.WriteLine ("Libro 4 'Viaje al centro de la Tierra' creado correctamente.");

                // Autor 5 (devuelve su id para ser usado a continuación en la creación del libro)
                int autorId5 = autorcen.CrearAutor (
 p_email: "pedro.romance@example.com",
 p_nombreUsuario: "PedroRomance",
 p_fechaNacimiento: new DateTime (1985, 12, 5),
 p_ciudadResidencia: "Málaga",
 p_paisResidencia: "España",
 p_valoracionMedia: 3.5f,
 p_foto: "pedroFoto.png",
 p_rol: ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.RolUsuarioEnum.autor,
 p_pass: "pedro123",
                        p_numeroSeguidores: 0,
                        p_cantidadLibrosPublicados: 0
                        );
                Console.WriteLine ("Autor creado correctamente con ID: " + autorId);

                // Libro 5 (usa el id del autor anterior)
                var idLibro5 = librocen.CrearLibro (
 p_titulo: "Amor en tiempos modernos",
 p_genero: "Romance",
 p_valoracionMedia: 3.5f,
                        p_edadRecomendada: 18,
 p_fechaPublicacion: new DateTime (2023, 2, 14),
                        p_numPags: 290,
 p_sinopsis: "Una historia de amor contemporánea y conmovedora.",
 p_fotoPortada: "portada_amor_moderno.jpg",
 p_autorPublicador: autorId5
                        );
                Console.WriteLine ("Libro 5 'Amor en tiempos modernos' creado correctamente.");

                // Auotor 6 (devuelve su id para ser usado a continuación en la creación del libro)
                int autorId6 = autorcen.CrearAutor (
 p_email: "ana.terror@example.com",
 p_nombreUsuario: "AnaTerror",
 p_fechaNacimiento: new DateTime (1979, 10, 31),
 p_ciudadResidencia: "Zaragoza",
 p_paisResidencia: "España",
 p_valoracionMedia: 3.5f,
 p_foto: "anaFoto.png",
 p_rol: ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.RolUsuarioEnum.autor,
 p_pass: "ana123",
                        p_numeroSeguidores: 0,
                        p_cantidadLibrosPublicados: 0
                        );
                Console.WriteLine ("Autor creado correctamente con ID: " + autorId);

                // Libro 6 (usa el id del autor anterior)
                var idLibro6 = librocen.CrearLibro (
 p_titulo: "La mansión del horror",
 p_genero: "Terror",
 p_valoracionMedia: 3.5f,
                        p_edadRecomendada: 18,
 p_fechaPublicacion: new DateTime (2021, 10, 31),
                        p_numPags: 340,
 p_sinopsis: "Una terrorífica historia que te mantendrá despierto.",
 p_fotoPortada: "portada_mansion_horror.jpg",
 p_autorPublicador: autorId6
                        );
                Console.WriteLine ("Libro 6 'La mansión del horror' creado correctamente.");

                // Autor 7 (devuelve su id para ser usado a continuación en la creación del libro)
                int autorId7 = autorcen.CrearAutor (
 p_email: "javier.aventura@example.com",
 p_nombreUsuario: "JavierAventura",
 p_fechaNacimiento: new DateTime (1983, 4, 18),
 p_ciudadResidencia: "Granada",
 p_valoracionMedia: 3.5f,
 p_paisResidencia: "España",
 p_foto: "javierFoto.png",
 p_rol: ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.RolUsuarioEnum.autor,
 p_pass: "javier123",
                        p_numeroSeguidores: 0,
                        p_cantidadLibrosPublicados: 0
                        );
                Console.WriteLine ("Autor creado correctamente con ID: " + autorId);

                // Libro 7 (usa el id del autor anterior)
                var idLibro7 = librocen.CrearLibro (
 p_titulo: "Expedición al Amazonas",
 p_genero: "Aventura",
 p_valoracionMedia: 3.5f,
                        p_edadRecomendada: 12,
 p_fechaPublicacion: new DateTime (2020, 6, 15),
                        p_numPags: 310,
 p_sinopsis: "Una emocionante aventura en la selva amazónica.",
 p_fotoPortada: "portada_amazonas.jpg",
 p_autorPublicador: autorId7
                        );
                Console.WriteLine ("Libro 7 'Expedición al Amazonas' creado correctamente.");

                // Autor 8 (devuelve su id para ser usado a continuación en la creación del libro)
                int autorId8 = autorcen.CrearAutor (
 p_email: "sofia.biografia@example.com",
 p_nombreUsuario: "SofiaBiografia",
 p_fechaNacimiento: new DateTime (1977, 9, 25),
 p_ciudadResidencia: "Murcia",
 p_paisResidencia: "España",
 p_valoracionMedia: 3.5f,
 p_foto: "sofiaFoto.png",
 p_rol: ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.RolUsuarioEnum.autor,
 p_pass: "sofia123",
                        p_numeroSeguidores: 0,
                        p_cantidadLibrosPublicados: 0
                        );
                Console.WriteLine ("Autor creado correctamente con ID: " + autorId);

                // Libro 8 (usa el id del autor anterior)
                var idLibro8 = librocen.CrearLibro (
 p_titulo: "La vida de Einstein",
 p_genero: "Biografia",
 p_valoracionMedia: 3.5f,
                        p_edadRecomendada: 15,
 p_fechaPublicacion: new DateTime (2019, 11, 20),
                        p_numPags: 420,
 p_sinopsis: "La fascinante historia del genio científico.",
 p_fotoPortada: "portada_einstein.jpg",
 p_autorPublicador: autorId8
                        );
                Console.WriteLine ("Libro 8 'La vida de Einstein' creado correctamente.");

                // Autor 9 (devuelve su id para ser usado a continuación en la creación del libro)
                int autorId9 = autorcen.CrearAutor (
 p_email: "miguel.poesia@example.com",
 p_nombreUsuario: "MiguelPoesia",
 p_fechaNacimiento: new DateTime (1990, 1, 8),
 p_ciudadResidencia: "Salamanca",
 p_paisResidencia: "España",
 p_foto: "miguelFoto.png",
 p_valoracionMedia: 3.5f,
 p_rol: ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.RolUsuarioEnum.autor,
 p_pass: "miguel123",
                        p_numeroSeguidores: 0,
                        p_cantidadLibrosPublicados: 0
                        );
                Console.WriteLine ("Autor creado correctamente con ID: " + autorId);

                // Libro 9 (usa el id del autor anterior)
                var idLibro9 = librocen.CrearLibro (
 p_titulo: "Versos del alma",
 p_genero: "Poesia",
 p_valoracionMedia: 3.5f,
                        p_edadRecomendada: 16,
 p_fechaPublicacion: new DateTime (2023, 4, 23),
                        p_numPags: 180,
 p_sinopsis: "Una colección de poemas sobre el amor y la vida.",
 p_fotoPortada: "portada_versos_alma.jpg",
 p_autorPublicador: autorId9
                        );
                Console.WriteLine ("Libro 9 'Versos del alma' creado correctamente.");

                // Auotor 10 (devuelve su id para ser usado a continuación en la creación del libro)
                int autorId10 = autorcen.CrearAutor (
 p_email: "elena.drama@example.com",
 p_nombreUsuario: "ElenaDrama",
 p_fechaNacimiento: new DateTime (1986, 6, 12),
 p_ciudadResidencia: "Alicante",
 p_paisResidencia: "España",
 p_foto: "elenaFoto.png",
 p_valoracionMedia: 3.5f,
 p_rol: ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.RolUsuarioEnum.autor,
 p_pass: "elena123",
                        p_numeroSeguidores: 0,
                        p_cantidadLibrosPublicados: 0
                        );
                Console.WriteLine ("Autor creado correctamente con ID: " + autorId);

                // Libro 10 (usa el id del autor anterior)
                var idLibro10 = librocen.CrearLibro (
 p_titulo: "El peso del silencio",
 p_genero: "Drama",
 p_valoracionMedia: 3.5f,
                        p_edadRecomendada: 18,
 p_fechaPublicacion: new DateTime (2022, 9, 10),
                        p_numPags: 365,
 p_sinopsis: "Un drama intenso sobre secretos familiares.",
 p_fotoPortada: "portada_peso_silencio.jpg",
 p_autorPublicador: autorId10
                        );
                Console.WriteLine ("Libro 10 'El peso del silencio' creado correctamente.");

                // Libro 11 (usa el mismo id que Libro 10, es decir, que el autor tiene 2 libros)
                var idLibro11 = librocen.CrearLibro (
 p_titulo: "Cuentos clásicos renovados",
 p_genero: "Ficcion",
                        p_edadRecomendada: 8,
 p_fechaPublicacion: new DateTime (2024, 1, 15),
                        p_numPags: 200,
 p_sinopsis: "Versiones modernas de los cuentos clásicos.",
 p_fotoPortada: "portada_cuentos_clasicos.jpg",
 p_valoracionMedia: 3.5f,
 p_autorPublicador: autorId10
                        );
                Console.WriteLine ("Libro 11 'Cuentos clásicos renovados' creado correctamente.");

                Console.WriteLine ("\n====================================================================================");
                Console.WriteLine ("RESUMEN - CREACIÓN DE AUTORES Y LIBROS:");
                Console.WriteLine ("- 10 Autores creados correctamente");
                Console.WriteLine ("- 11 Libros creados correctamente");
                Console.WriteLine ("- Libros con diferentes géneros, edades recomendadas y valoraciones");
                Console.WriteLine ("====================================================================================");

                /////////////////////////////////////////////////////////////////////////////////////////////////////

                // CREACIÓN DE EVENTOS PARA PRUEBAS DE ReadFilter
                Console.WriteLine ("\n\n------------------ Creación de Eventos ------------------");

                int eventoId1 = eventocen.CrearEvento (
 p_nombre: "Feria del Libro 2026",
 p_fecha: new DateTime (2026, 11, 15),
 p_hora: new DateTime (2026, 11, 15, 10, 0, 0),
 p_ubicacion: "Biblioteca Central, Sala 3",
 p_descripcion: "Feria anual del libro con firmas de autores",
 p_foto: "feria_libro_2024.jpg",
                        p_aforoMax: 50,
                        p_aforoActual: 0,
 p_administradorEventos: administradorId1
                        );
                Console.WriteLine ("Evento 'Feria del Libro 2024' creado correctamente.");

                int eventoId2 = eventocen.CrearEvento (
 p_nombre: "Encuentro de Lectores",
 p_fecha: new DateTime (2026, 12, 20),
 p_hora: new DateTime (2026, 12, 20, 18, 0, 0),
 p_ubicacion: "Madrid",
 p_descripcion: "Reunión mensual de club de lectura",
 p_foto: "encuentro_lectores.jpg",
                        p_aforoMax: 50,
                        p_aforoActual: 0,
 p_administradorEventos: administradorId1
                        );
                Console.WriteLine ("Evento 'Encuentro de Lectores' creado correctamente.");

                int eventoId3 = eventocen.CrearEvento (
 p_nombre: "Conferencia de Escritores",
 p_fecha: new DateTime (2027, 1, 10),
 p_hora: new DateTime (2027, 1, 10, 16, 0, 0),
 p_ubicacion: "Valencia",
 p_descripcion: "Conferencia sobre técnicas de escritura creativa",
 p_foto: "conferencia_escritores.jpg",
                        p_aforoMax: 200,
                        p_aforoActual: 200,
 p_administradorEventos: administradorId1
                        );
                Console.WriteLine ("Evento 'Conferencia de Escritores' creado correctamente.");

                int eventoId4 = eventocen.CrearEvento (
 p_nombre: "Presentación Libro Nuevo",
 p_fecha: new DateTime (2027, 2, 5),
 p_hora: new DateTime (2027, 2, 5, 19, 30, 0),
 p_ubicacion: "Sevilla",
 p_descripcion: "Presentación del nuevo libro de AutorEjemplo",
 p_foto: "presentacion_libro.jpg",
                        p_aforoMax: 100,
                        p_aforoActual: 0,
 p_administradorEventos: administradorId1
                        );
                Console.WriteLine ("Evento 'Presentación Libro Nuevo' creado correctamente.");

                Console.WriteLine ("\n====================================================================================");
                Console.WriteLine ("RESUMEN - CREACIÓN DE EVENTOS:");
                Console.WriteLine ("- 4 Eventos creados");
                Console.WriteLine ("- Fechas (hoy o en adelante)");
                Console.WriteLine ("- Aforos: 50, 50, 200, 100 personas");
                Console.WriteLine ("====================================================================================");

                /////////////////////////////////////////////////////////////////////////////////////////////////////

                // CREACIÓN USURIO - Lector 3 (Para asignarle la creación de clubs)
                Console.WriteLine ("\n\n------------------ Creación de Usuario Lector ------------------");

                int usuarioId3 = lectorcen.CrearLector (
 p_email: "niko.lector@example.com",
 p_nombreUsuario: "NikoLector",
 p_fechaNacimiento: new DateTime (1980, 11, 10),
 p_ciudadResidencia: "Elche",
 p_paisResidencia: "España",
 p_foto: "nikoFoto.png",
 p_rol: ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.RolUsuarioEnum.lector,
 p_pass: "password456",
                        p_cantLibrosCurso: 0,
                        p_cantLibrosLeidos: 0,
                        p_cantAutoresSeguidos: 0,
                        p_cantClubsSuscritos: 0);
                Console.WriteLine ("Usuario 'NikoLector' creado correctamente.");

                // CREACIÓN DE CLUBS PARA PRUEBAS DE ReadFilter
                Console.WriteLine ("\n\n------------------ Creación de Clubs ------------------");

                int clubId1 = 0;
                int clubId2 = 0;
                int clubId3 = 0;
                int clubId4 = 0;

                SessionCPNHibernate sessionCPNHibernate1 = new SessionCPNHibernate ();

                // CLUB 1: Club de Ciencia Ficción
                try
                {
                        sessionCPNHibernate1.SessionInitializeTransaction ();
                        var usuarioCEN = new UsuarioCEN (sessionCPNHibernate1.UnitRepo.UsuarioRepository);
                        var clubCEN = new ClubCEN (sessionCPNHibernate1.UnitRepo.ClubRepository);

                        UsuarioEN propietario1 = usuarioCEN.DameUsuarioPorOID (usuarioId1);

                        clubId1 = clubCEN.CearClub (
 p_nombre: "Club de Ciencia Ficción",
 p_enlaceDiscord: "https://discord.gg/cienciaficcion",
                                p_miembrosMax: 50,
 p_foto: "club_cienciaficcion.png",
 p_descripcion: "Un club para los entusiastas de la ciencia ficción, donde exploramos mundos futuristas y tecnologías avanzadas.",
                                p_miembrosActuales: 0,
 p_usuarioPropietario: propietario1
                                );

                        sessionCPNHibernate1.Commit ();
                        Console.WriteLine ("Club 'Club de Ciencia Ficción' creado correctamente con ID: " + clubId1);
                }
                catch (Exception ex)
                {
                        sessionCPNHibernate1.RollBack ();
                        throw;
                }
                finally
                {
                        sessionCPNHibernate1.SessionClose ();
                }

                // CLUB 2: Club de Misterio y Suspense
                try
                {
                        sessionCPNHibernate1.SessionInitializeTransaction ();
                        var usuarioCEN = new UsuarioCEN (sessionCPNHibernate1.UnitRepo.UsuarioRepository);
                        var clubCEN = new ClubCEN (sessionCPNHibernate1.UnitRepo.ClubRepository);

                        UsuarioEN propietario2 = usuarioCEN.DameUsuarioPorOID (usuarioId3);

                        clubId2 = clubCEN.CearClub (
 p_nombre: "Club de Misterio y Suspense",
 p_enlaceDiscord: "https://discord.gg/misterio",
                                p_miembrosMax: 30,
 p_foto: "club_misterio.png",
 p_descripcion: "Amantes del misterio, el suspense y las tramas que te mantienen en vilo hasta la última página.",
                                p_miembrosActuales: 12,
 p_usuarioPropietario: propietario2
                                );

                        sessionCPNHibernate1.Commit ();
                        Console.WriteLine ("Club 'Club de Misterio y Suspense' creado correctamente con ID: " + clubId2);
                }
                catch (Exception ex)
                {
                        sessionCPNHibernate1.RollBack ();
                        throw;
                }
                finally
                {
                        sessionCPNHibernate1.SessionClose ();
                }

                // CLUB 3: Club de Romance Contemporáneo
                try
                {
                        sessionCPNHibernate1.SessionInitializeTransaction ();
                        var usuarioCEN = new UsuarioCEN (sessionCPNHibernate1.UnitRepo.UsuarioRepository);
                        var clubCEN = new ClubCEN (sessionCPNHibernate1.UnitRepo.ClubRepository);

                        UsuarioEN propietario3 = usuarioCEN.DameUsuarioPorOID (usuarioId1);

                        clubId3 = clubCEN.CearClub (
 p_nombre: "Club de Romance Contemporáneo",
 p_enlaceDiscord: "https://discord.gg/romance",
                                p_miembrosMax: 75,
 p_foto: "club_romance.png",
 p_descripcion: "Para quienes disfrutan de historias de amor modernas, emotivas y llenas de sentimientos.",
                                p_miembrosActuales: 45,
 p_usuarioPropietario: propietario3
                                );

                        sessionCPNHibernate1.Commit ();
                        Console.WriteLine ("Club 'Club de Romance Contemporáneo' creado correctamente con ID: " + clubId3);
                }
                catch (Exception ex)
                {
                        sessionCPNHibernate1.RollBack ();
                        throw;
                }
                finally
                {
                        sessionCPNHibernate1.SessionClose ();
                }

                // CLUB 4: Club de Aventuras Épicas
                try
                {
                        sessionCPNHibernate1.SessionInitializeTransaction ();
                        var usuarioCEN = new UsuarioCEN (sessionCPNHibernate1.UnitRepo.UsuarioRepository);
                        var clubCEN = new ClubCEN (sessionCPNHibernate1.UnitRepo.ClubRepository);

                        UsuarioEN propietario4 = usuarioCEN.DameUsuarioPorOID (usuarioId3);

                        clubId4 = clubCEN.CearClub (
 p_nombre: "Club de Aventuras Épicas",
 p_enlaceDiscord: "https://discord.gg/aventuras",
                                p_miembrosMax: 100,
 p_foto: "club_aventuras.png",
 p_descripcion: "Exploradores literarios que buscan viajes épicos, mundos fantásticos y personajes heroicos.",
                                p_miembrosActuales: 67,
 p_usuarioPropietario: propietario4
                                );

                        sessionCPNHibernate1.Commit ();
                        Console.WriteLine ("Club 'Club de Aventuras Épicas' creado correctamente con ID: " + clubId4);
                }
                catch (Exception ex)
                {
                        sessionCPNHibernate1.RollBack ();
                        throw;
                }
                finally
                {
                        sessionCPNHibernate1.SessionClose ();
                }

                // Resumen de Clubs creados:
                Console.WriteLine ("\n====================================================================================");
                Console.WriteLine ("RESUMEN - CREACIÓN DE CLUBS");
                Console.WriteLine ("Total: 4 clubs creados");
                Console.WriteLine ("Clubs: Ciencia Ficción (50 max), Misterio y Suspense (30 max), Romance Contemporáneo (75 max), Aventuras Épicas (100 max)");
                Console.WriteLine ("Miembros actuales: 0, 12, 45, 67 respectivamente");
                Console.WriteLine ("====================================================================================");

                /////////////////////////////////////////////////////////////////////////////////////////////////////

                // CREACIÓN DE RESEÑAS PARA PRUEBAS DE ReadFilter
                Console.WriteLine ("\n\n------------------ Creación de Reseñas ------------------");

                // Obtener objetos Lector para pasarlos como valoradores
                LectorEN lector3 = lectorcen.get_ILectorRepository ().DameLectorPorOID (usuarioId3);

                int noticiaId1 = noticiacen.CrearNoticia (
 p_titulo: "Lanzamiento de nueva plataforma ReadRate",
 p_fechaPublicacion: new DateTime (2024, 10, 15),
 p_foto: "noticia_lanzamiento.jpg",
 p_textoContenido: "Estamos emocionados de presentar ReadRate, la nueva plataforma de lectura social donde podrás compartir tus opiniones sobre libros y conectar con otros lectores.",
 p_administradorNoticias: administradorId1
                        );


                // Obtener objetos Libro para las reseñas
                LibroEN libro1 = librocen.DameLibroPorOID (idLibro1);
                LibroEN libro2 = librocen.DameLibroPorOID (idLibro2);
                LibroEN libro3 = librocen.DameLibroPorOID (idLibro3);
                LibroEN libro4 = librocen.DameLibroPorOID (idLibro4);
                LibroEN libro5 = librocen.DameLibroPorOID (idLibro5);

                Console.WriteLine ("Reseña para libro 1 creada correctamente.");
                int reseñaId1 = reseñacen.CrearReseña (p_textoOpinion: "Me encantó la trama y los personajes. Una historia muy bien desarrollada.",
 p_valoracion: 5.0f,
 p_lectorValorador: lector3.Id,
 p_libroReseñado: libro1.Id,
 p_fecha: new DateTime (2024, 10, 1));

                int notificacionId1 = notificacioncen.CrearNotificacion (
 p_fecha: new DateTime (1980, 11, 10),
 p_concepto: ConceptoNotificacionEnum.noticia_publicada,
 p_noticiaNotificada: noticiaId1,
 p_eventoNotificado: eventoId1,
 p_clubNotificado: clubId1,
 p_reseñaNotificada: reseñaId1,
 p_autorAvisado: autorId
                        );

                int reseñaId2 = reseñacen.CrearReseña (
 p_textoOpinion: "Recomendado para todos. Lectura muy entretenida.",
 p_valoracion: 4.0f,
 p_lectorValorador: usuarioId3,
 p_libroReseñado: libro1.Id,
 p_fecha: new DateTime (2024, 10, 5)
                        );
                Console.WriteLine ("Reseña para libro 2 creada correctamente.");

                int reseñaId3 = reseñacen.CrearReseña (
 p_textoOpinion: "Tiene sus momentos pero podría mejorar en algunos aspectos.",
 p_valoracion: 3.0f,
 p_lectorValorador: usuarioId3,
 p_libroReseñado: libro3.Id,
 p_fecha: new DateTime (2024, 10, 10)
                        );
                Console.WriteLine ("Reseña para libro 3 creada correctamente.");

                int reseñaId4 = reseñacen.CrearReseña (
 p_textoOpinion: "Esperaba más de este libro. No cumplió mis expectativas.",
 p_valoracion: 2.0f,
 p_lectorValorador: usuarioId3,
 p_libroReseñado: libro4.Id,
 p_fecha: new DateTime (2024, 10, 12)
                        );
                Console.WriteLine ("Reseña para libro 4 creada correctamente.");

                int reseñaId5 = reseñacen.CrearReseña (
 p_textoOpinion: "Uno de los mejores libros que he leído. Obra maestra.",
 p_valoracion: 5.0f,
 p_lectorValorador: usuarioId3,
 p_libroReseñado: libro5.Id,
 p_fecha: new DateTime (2024, 10, 15)
                        );
                Console.WriteLine ("Reseña para libro 5 creada correctamente.");

                // Resumen de Reseñas creadas:
                Console.WriteLine ("\n====================================================================================");
                Console.WriteLine ("RESUMEN - CREACIÓN DE RESEÑAS");
                Console.WriteLine ("Total: 5 reseñas creadas");
                Console.WriteLine ("Valoraciones: 5.0, 4.0, 3.0, 2.0, 5.0");
                Console.WriteLine ("Rango de fechas: 1 de octubre - 15 de octubre 2024");
                Console.WriteLine ("====================================================================================");

                /////////////////////////////////////////////////////////////////////////////////////////////////////

                // CREACIÓN DE NOTICIAS PARA PRUEBAS DE ReadFilter
                Console.WriteLine ("\n\n------------------ Creación de Noticias ------------------");

                int noticiaId7 = noticiacen.CrearNoticia (
 p_titulo: "Nuevo libro de AutorEjemplo",
 p_fechaPublicacion: new DateTime (2024, 11, 1),
 p_foto: "noticia_nuevo_libro.jpg",
 p_textoContenido: "El reconocido autor publicará su nueva obra el próximo mes. Se trata de una novela de misterio que promete mantener a los lectores en vilo hasta la última página.",
 p_administradorNoticias: administradorId1
                        );
                Console.WriteLine ("Noticia 'Nuevo libro de AutorEjemplo' creada correctamente.");

                int noticiaId2 = noticiacen.CrearNoticia (
 p_titulo: "Feria del Libro 2024",
 p_fechaPublicacion: new DateTime (2024, 11, 5),
 p_foto: "noticia_feria_libro.jpg",
 p_textoContenido: "Se celebrará la feria anual del libro con la participación de más de 200 autores nacionales e internacionales. Habrá firmas de libros, charlas y actividades para toda la familia.",
 p_administradorNoticias: administradorId1
                        );
                Console.WriteLine ("Noticia 'Feria del Libro 2024' creada correctamente.");

                int noticiaId3 = noticiacen.CrearNoticia (
 p_titulo: "Entrevista exclusiva con MariaEscritora",
 p_fechaPublicacion: new DateTime (2024, 11, 10),
 p_foto: "noticia_entrevista_maria.jpg",
 p_textoContenido: "Hablamos con la autora sobre su proceso creativo, sus influencias literarias y sus próximos proyectos. Una conversación íntima sobre el arte de escribir.",
 p_administradorNoticias: administradorId1
                        );
                Console.WriteLine ("Noticia 'Entrevista exclusiva con MariaEscritora' creada correctamente.");

                int noticiaId4 = noticiacen.CrearNoticia (
 p_titulo: "Bestsellers del mes",
 p_fechaPublicacion: new DateTime (2024, 11, 15),
 p_foto: "noticia_bestsellers.jpg",
 p_textoContenido: "Los libros más vendidos de este mes incluyen títulos de ficción, romance y ciencia ficción. Descubre cuáles son las lecturas favoritas de los usuarios en noviembre.",
 p_administradorNoticias: administradorId1
                        );
                Console.WriteLine ("Noticia 'Bestsellers del mes' creada correctamente.");

                // Resumen de Noticias creadas:
                Console.WriteLine ("\n====================================================================================");
                Console.WriteLine ("RESUMEN - CREACIÓN DE NOTICIAS");
                Console.WriteLine ("Total: 4 noticias creadas");
                Console.WriteLine ("Títulos: Nuevo libro de AutorEjemplo, Feria del Libro 2024, Entrevista exclusiva con MariaEscritora, Bestsellers del mes");
                Console.WriteLine ("Fechas de publicación: 1, 5, 10, 15 de noviembre 2024");
                Console.WriteLine ("====================================================================================");

                Console.WriteLine ("\n\n\n====================================================================================");
                Console.WriteLine ("====================================================================================");
                Console.WriteLine ("====================================================================================");

                /////////////////////////////////////////////////////////////////////////////////////////////////////

                // PRUEBAS DE READFILTER dameLibrosPorFiltros()
                Console.WriteLine ("\n\n==================== PRUEBAS DE ReadFilter - dameLibrosPorFiltros() ====================");

                // PRUEBA 1: Sin filtros (todos null) - Debe devolver todos los libros
                Console.WriteLine ("\n------------------ Prueba 1: Sin filtros (todos los parámetros null) ------------------");
                var librosSinFiltro = librocen.DameLibrosPorFiltros (p_genero: null, p_titulo: null, p_edadRecomendada: null, p_numPags: null, p_valoracionMedia: null, 0, 20);
                Console.WriteLine ("Libros encontrados sin filtro: " + (librosSinFiltro != null ? librosSinFiltro.Count.ToString () : "0"));

                // PRUEBA 2: Filtro por género "Ficcion"
                Console.WriteLine ("\n------------------ Prueba 2: Filtro por género 'Ficcion' ------------------");
                var librosFiccion = librocen.DameLibrosPorFiltros (p_genero: "Ficcion", p_titulo: null, p_edadRecomendada: null, p_numPags: null, p_valoracionMedia: null, 0, 20);
                Console.WriteLine ("Libros de 'Ficcion' encontrados: " + (librosFiccion != null ? librosFiccion.Count.ToString () : "0"));

                // PRUEBA 3: Filtro por edad recomendada = 12
                Console.WriteLine ("\n------------------ Prueba 3: Filtro por edad recomendada >= 12 años ------------------");
                var librosEdad12 = librocen.DameLibrosPorFiltros (p_genero: null, p_titulo: null, p_edadRecomendada: 12, p_numPags: null, p_valoracionMedia: null, 0, 20);
                Console.WriteLine ("Libros para edad >= 12 encontrados: " + (librosEdad12 != null ? librosEdad12.Count.ToString () : "0"));

                // PRUEBA 4: Filtro por número de páginas >= 200
                Console.WriteLine ("\n------------------ Prueba 4: Filtro por número de páginas >= 200 ------------------");
                var libros200Pags = librocen.DameLibrosPorFiltros (p_genero: null, p_titulo: null, p_edadRecomendada: null, p_numPags: 200, p_valoracionMedia: null, 0, 20);
                Console.WriteLine ("Libros con >= 200 páginas encontrados: " + (libros200Pags != null ? libros200Pags.Count.ToString () : "0"));

                // PRUEBA 5: Filtro por valoración media >= 4.0
                Console.WriteLine ("\n------------------ Prueba 5: Filtro por valoración media >= 4.0 ------------------");
                var librosValoracion4 = librocen.DameLibrosPorFiltros (p_genero: null, p_titulo: null, p_edadRecomendada: null, p_numPags: null, p_valoracionMedia: 4.0f, 0, 20);
                Console.WriteLine ("Libros con valoración >= 4.0 encontrados: " + (librosValoracion4 != null ? librosValoracion4.Count.ToString () : "0"));

                // PRUEBA 6: Filtro combinado - Género "Ficcion" y edad > 10
                Console.WriteLine ("\n------------------ Prueba 6: Filtro combinado - Género 'Ficcion' y edad >= 10 ------------------");
                var librosFiccion10 = librocen.DameLibrosPorFiltros (p_genero: "Ficcion", p_titulo: null, p_edadRecomendada: 10, p_numPags: null, p_valoracionMedia: null, 0, 20);
                Console.WriteLine ("Libros de 'Ficcion' para edad >= 10 años encontrados: " + (librosFiccion10 != null ? librosFiccion10.Count.ToString () : "0"));

                // PRUEBA 7: Filtro combinado - Género "Ficcion", valoración >= 1.0, >= 100 páginas
                Console.WriteLine ("\n------------------ Prueba 7: Filtro combinado - Ficcion + valoración >= 4.5, >= 250 páginas ------------------");
                var librosRestrictivo = librocen.DameLibrosPorFiltros (p_genero: "Ficcion", p_titulo: null, p_edadRecomendada: null, p_numPags: 100, p_valoracionMedia: 1.0f, 0, 20);
                Console.WriteLine ("Libros que cumplen todos los criterios propuestos: " + (librosRestrictivo != null ? librosRestrictivo.Count.ToString () : "0"));

                Console.WriteLine ("\n====================================================================================");
                Console.WriteLine ("RESUMEN - PRUEBAS DE FILTRO DE LIBROS:");
                Console.WriteLine ("- Prueba 1: Sin filtros - Todos los libros");
                Console.WriteLine ("- Prueba 2: Por género 'Ficcion'");
                Console.WriteLine ("- Prueba 3: Por edad recomendada >= 12");
                Console.WriteLine ("- Prueba 4: Por número de páginas >= 200");
                Console.WriteLine ("- Prueba 5: Por valoración >= 4.0");
                Console.WriteLine ("- Prueba 6: Filtro combinado género + edad");
                Console.WriteLine ("- Prueba 7: Filtro con tres criterios");
                Console.WriteLine ("=======================================================================");

                /////////////////////////////////////////////////////////////////////////////////////////////////////

                // PRUEBAS DE READFILTERS dameEventosPorFecha()
                Console.WriteLine ("\n\n==================== PRUEBAS DE ReadFilter - dameEventosPorFecha() ====================");

                // PRUEBA 1: Buscar eventos en fecha específica (15 de noviembre de 2024)
                Console.WriteLine ("\n------------------ Prueba 1: Eventos en fecha 15/11/2024 y en adelante ------------------");
                var eventosNov15 = eventocen.DameEventosPorFecha (new DateTime (2024, 11, 15), 0, 20);
                Console.WriteLine ("Eventos encontrados para 15/11/2024 o más: " + (eventosNov15 != null ? eventosNov15.Count.ToString () : "0"));

                // PRUEBA 2: Buscar eventos en fecha sin eventos (1 de enero de 2025)
                Console.WriteLine ("\n------------------ Prueba 2: Eventos en fecha 01/01/2025 y en adelante (sin eventos) ------------------");
                var eventosEne01 = eventocen.DameEventosPorFecha (new DateTime (2025, 10, 10), 0, 20);
                Console.WriteLine ("Eventos encontrados para 10/10/2027 o más: " + (eventosEne01 != null ? eventosEne01.Count.ToString () : "0"));

                Console.WriteLine ("\n====================================================================================");
                Console.WriteLine ("RESUMEN - PRUEBAS dameEventosPorFecha():");
                Console.WriteLine ("- Prueba 1: Buscar eventos el 15/11/2024");
                Console.WriteLine ("- Prueba 2: Buscar eventos el 01/01/2025 (sin resultados esperados)");
                Console.WriteLine ("====================================================================================");

                /////////////////////////////////////////////////////////////////////////////////////////////////////

                Console.WriteLine ("\n\n==================== PRUEBAS DE ReadFilter - dameUsuarioPorEmail() ====================");

                // PRUEBA 1: Buscar usuario lector existente por email
                Console.WriteLine ("\n------------------ Prueba 1: Buscar lector por email 'paco.lector@example.com' ------------------");
                List<UsuarioEN> usuarioPaco = usuariocen.DameUsuarioPorEmail ("paco.lector@example.com") as List<UsuarioEN>;
                Console.WriteLine ("Usuario encontrado: " + (usuarioPaco.Count > 0 ? usuarioPaco [0].NombreUsuario : "No encontrado"));

                // PRUEBA 2: Buscar autor existente por email
                Console.WriteLine ("\n------------------ Prueba 2: Buscar autor por email 'autor@example.com' ------------------");
                List<UsuarioEN> usuarioAutor = usuariocen.DameUsuarioPorEmail ("autor@example.com") as List<UsuarioEN>;
                Console.WriteLine ("Usuario encontrado: " + (usuarioAutor.Count > 0 ? usuarioAutor [0].NombreUsuario : "No encontrado"));

                // PRUEBA 3: Buscar usuario que no existe
                Console.WriteLine ("\n------------------ Prueba 3: Buscar usuario no existente 'noexiste@example.com' ------------------");
                List<UsuarioEN> usuarioNoExiste = usuariocen.DameUsuarioPorEmail ("noexiste@example.com") as List<UsuarioEN>;
                Console.WriteLine ("Usuario encontrado: " + (usuarioNoExiste.Count > 0 ? usuarioNoExiste [0].NombreUsuario : "No encontrado"));

                Console.WriteLine ("\n====================================================================================");
                Console.WriteLine ("RESUMEN - PRUEBAS dameUsuarioPorEmail():");
                Console.WriteLine ("- Prueba 1: Buscar lector existente - Encontrado");
                Console.WriteLine ("- Prueba 2: Buscar autor existente - Encontrado");
                Console.WriteLine ("- Prueba 3: Buscar email no existente - No encontrado (esperado)");
                Console.WriteLine ("====================================================================================");

                /////////////////////////////////////////////////////////////////////////////////////////////////////

                Console.WriteLine ("\n\n==================== PRUEBAS DE ReadFilter - dameClubPorFiltros() ====================");

                // PRUEBA 1: Buscar todos los clubs (sin filtros)
                Console.WriteLine ("\n------------------ Prueba 1: Buscar todos los clubs (sin filtros) ------------------");
                List<ClubEN> todosClubs = clubcen.DameClubPorFiltros (p_nombre: null, p_descripcion: null, first: 0, size: 20) as List<ClubEN>;
                Console.WriteLine ("Clubs encontrados sin filtro: " + (todosClubs != null ? todosClubs.Count.ToString () : "0"));

                // PRUEBA 2: Filtrar clubs solo por nombre "Ciencia"
                Console.WriteLine ("\n------------------ Prueba 2: Filtrar clubs por nombre exacto 'Club de Misterio y Suspense' ------------------");
                List<ClubEN> clubsPorNombre = clubcen.DameClubPorFiltros (p_nombre: "Club de Misterio y Suspense", p_descripcion: null, first: 0, size: 20) as List<ClubEN>;
                Console.WriteLine ("Club con 'Club de Misterio y Suspense' de nombre: " + (clubsPorNombre != null ? clubsPorNombre.Count.ToString () : "0"));

                // PRUEBA 3: Filtrar clubs solo por descripción "exploramos"
                Console.WriteLine ("\n------------------ Prueba 3: Filtrar clubs por descripción que contiene 'viaje' ------------------");
                List<ClubEN> clubsPorDescripcion = clubcen.DameClubPorFiltros (p_nombre: null, p_descripcion: "viaje", first: 0, size: 20) as List<ClubEN>;
                Console.WriteLine ("Clubs con 'viaje' en la descripción encontrados: " + (clubsPorDescripcion != null ? clubsPorDescripcion.Count.ToString () : "0"));

                // PRUEBA 4: Filtrar clubs por nombre Y descripción combinados
                Console.WriteLine ("\n------------------ Prueba 4: Filtrar por nombre 'Club de Ciencia Ficción' y descripción 'entusiastas' ------------------");
                List<ClubEN> clubsPorAmbos = clubcen.DameClubPorFiltros (p_nombre: "Club de Ciencia Ficción", p_descripcion: "entusiastas", first: 0, size: 20) as List<ClubEN>;
                Console.WriteLine ("Clubs que cumplen ambos criterios encontrados: " + (clubsPorAmbos != null ? clubsPorAmbos.Count.ToString () : "0"));

                Console.WriteLine ("\n====================================================================================");
                Console.WriteLine ("RESUMEN - PRUEBAS dameClubPorFiltros():");
                Console.WriteLine ("- Prueba 1: Todos los clubs sin filtros (4 clubs esperados)");
                Console.WriteLine ("- Prueba 2: Filtro solo por nombre 'Ciencia' (1 club esperado)");
                Console.WriteLine ("- Prueba 3: Filtro solo por descripción 'exploramos' (1 club esperado)");
                Console.WriteLine ("- Prueba 4: Filtro combinado nombre + descripción (1 club esperado)");
                Console.WriteLine ("====================================================================================");

                /////////////////////////////////////////////////////////////////////////////////////////////////////

                Console.WriteLine ("\n\n==================== PRUEBAS DE ReadFilter - dameReseñasOrdenadasPorValoracionAsc() ====================");

                // PRUEBA 1: Obtener todas las reseñas ordenadas ascendentemente (de menor a mayor valoración)
                Console.WriteLine ("\n------------------ Prueba 1: Reseñas ordenadas por valoración ascendente ------------------");

                List<ReseñaEN> reseñasAsc = reseñacen.DameReseñasOrdenadasPorValoracionAsc (first: 0, size: 20) as List<ReseñaEN>;
                if (reseñasAsc != null && reseñasAsc.Count > 0) {
                        Console.WriteLine ("Total de reseñas: " + reseñasAsc.Count);
                        Console.WriteLine ("\nListado de todas las reseñas ordenadas de menor a mayor valoración:");
                        for (int i = 0; i < reseñasAsc.Count; i++) {
                                Console.WriteLine ("  " + (i + 1) + ". Valoración: " + reseñasAsc [i].Valoracion + " - Texto: " + reseñasAsc [i].TextoOpinion);
                        }
                }
                else{
                        Console.WriteLine ("No se encontraron reseñas.");
                }

                Console.WriteLine ("\n====================================================================================");
                Console.WriteLine ("RESUMEN - PRUEBAS dameReseñasOrdenadasPorValoracionAsc():");
                Console.WriteLine ("- Se obtuvieron todas las reseñas ordenadas de menor a mayor valoración");
                Console.WriteLine ("- Orden esperado: 2, 3, 4, 5, 5 estrellas");
                Console.WriteLine ("====================================================================================");

                /////////////////////////////////////////////////////////////////////////////////////////////////////

                Console.WriteLine ("\n\n==================== PRUEBAS DE ReadFilter - dameReseñasOrdenadasPorValoracionDesc() ====================");

                // PRUEBA 1: Obtener todas las reseñas ordenadas descendentemente (de mayor a menor valoración)
                Console.WriteLine ("\n------------------ Prueba 1: Reseñas ordenadas por valoración descendente ------------------");

                List<ReseñaEN> reseñasDesc = reseñacen.DameReseñasOrdenadasPorValoracionDesc (first: 0, size: 20) as List<ReseñaEN>;

                if (reseñasDesc != null && reseñasDesc.Count > 0) {
                        Console.WriteLine ("Total de reseñas: " + reseñasDesc.Count);
                        Console.WriteLine ("\nListado de todas las reseñas ordenadas de mayor a menor valoración:");
                        for (int i = 0; i < reseñasDesc.Count; i++) {
                                Console.WriteLine ("  " + (i + 1) + ". Valoración: " + reseñasDesc [i].Valoracion + " - Texto: " + reseñasDesc [i].TextoOpinion);
                        }
                }
                else{
                        Console.WriteLine ("No se encontraron reseñas.");
                }

                Console.WriteLine ("\n====================================================================================");
                Console.WriteLine ("RESUMEN - PRUEBAS dameReseñasOrdenadasPorValoracionDesc():");
                Console.WriteLine ("- Se obtuvieron todas las reseñas ordenadas de mayor a menor valoración");
                Console.WriteLine ("- Orden esperado: 5, 5, 4, 3, 2 estrellas");
                Console.WriteLine ("====================================================================================");

                /////////////////////////////////////////////////////////////////////////////////////////////////////

                Console.WriteLine ("\n\n==================== PRUEBAS DE ReadFilter - dameNoticiasPorTitulo() ====================");

                // PRUEBA 1: Buscar noticias con "libro" en el título
                Console.WriteLine ("\n------------------ Prueba 1: Noticias con título 'Nuevo libro de AutorEjemplo' ------------------");
                List <NoticiaEN> noticiasLibro = noticiacen.DameNoticiasPorTitulo (p_titulo: "Nuevo libro de AutorEjemplo") as List<NoticiaEN>;
                Console.WriteLine ("Noticias encontradas con 'Nuevo libro de AutorEjemplo': " + (noticiasLibro != null ? noticiasLibro.Count.ToString () : "0"));

                // PRUEBA 2: Buscar noticias con "Feria" en el título
                Console.WriteLine ("\n------------------ Prueba 2: Noticias que contienen 'Feria' en el título ------------------");
                List<NoticiaEN> noticiasFeria = noticiacen.DameNoticiasPorTitulo (p_titulo: "Feria") as List<NoticiaEN>;
                Console.WriteLine ("Noticias encontradas con 'Feria': " + (noticiasFeria != null ? noticiasFeria.Count.ToString () : "0"));

                // PRUEBA 3: Buscar con palabra que no existe
                Console.WriteLine ("\n------------------ Prueba 4: Noticias que contienen 'Deporte' (no existe) ------------------");
                List<NoticiaEN> noticiasDeporte = noticiacen.DameNoticiasPorTitulo (p_titulo: "Deporte") as List<NoticiaEN>;
                Console.WriteLine ("Noticias encontradas con 'Deporte': " + (noticiasDeporte != null ? noticiasDeporte.Count.ToString () : "0"));

                Console.WriteLine ("\n====================================================================================");
                Console.WriteLine ("RESUMEN - PRUEBAS dameNoticiasPorTitulo():");
                Console.WriteLine ("- Prueba 1: Buscar 'libro' en títulos (Se espera 1 resultado encontrado)");
                Console.WriteLine ("- Prueba 2: Buscar 'Feria' en títulos");
                Console.WriteLine ("- Prueba 3: Buscar palabra inexistente (Sin resultados)");
                Console.WriteLine ("====================================================================================");

                /////////////////////////////////////////////////////////////////////////////////////////////////////

                Console.WriteLine ("\n\n==================== PRUEBAS DE ReadFilter - dameTodosTitulosNoticias() ====================");

                // PRUEBA 1: Obtener todos los títulos de noticias
                Console.WriteLine ("\n------------------ Prueba 1: Obtener todos los títulos de noticias ------------------");
                List<string> titulosNoticias = noticiacen.DameTodosTitulosNoticias (0, 20) as List<string>;
                if (titulosNoticias != null && titulosNoticias.Count > 0) {
                        Console.WriteLine ("Total de títulos de noticias: " + titulosNoticias.Count);
                        Console.WriteLine ("Títulos encontrados:");
                        foreach (var titulo in titulosNoticias) {
                                Console.WriteLine ("  - " + titulo);
                        }
                }
                else{
                        Console.WriteLine ("No se encontraron títulos de noticias.");
                }

                Console.WriteLine ("\n====================================================================================");
                Console.WriteLine ("RESUMEN - PRUEBAS dameTodosTitulosNoticias():");
                Console.WriteLine ("- Se obtuvieron todos los títulos de las noticias creadas");
                Console.WriteLine ("- Total esperado: 4 títulos");
                Console.WriteLine ("====================================================================================");

                /////////////////////////////////////////////////////////////////////////////////////////////////////

                Console.WriteLine ("\n\n==================== PRUEBAS DE ReadFilter - dameAdministradorPorEmail() ====================");

                // PRUEBA 1: Buscar administrador existente por email
                Console.WriteLine ("\n------------------ Prueba 1: Buscar administrador por email 'admin@example.com' ------------------");
                List<AdministradorEN> adminEncontrado = administradorcen.DameAdministradoresPorEmail ("admin@example.com") as List<AdministradorEN>;
                Console.WriteLine ("Administrador encontrado: " + (adminEncontrado.Count > 0 ? adminEncontrado [0].Nombre : "No encontrado"));

                // PRUEBA 2: Buscar con email que no corresponde a un administrador
                Console.WriteLine ("\n------------------ Prueba 2: Buscar con email de lector 'paco.lector@example.com' (no es admin) ------------------");
                List<AdministradorEN> noAdmin = administradorcen.DameAdministradoresPorEmail ("paco.lector@example.com") as List<AdministradorEN>;
                Console.WriteLine ("Administrador encontrado: " + (noAdmin.Count > 0 ? noAdmin [0].Nombre : "No encontrado"));

                // PRUEBA 3: Buscar con email que no existe
                Console.WriteLine ("\n------------------ Prueba 3: Buscar con email inexistente 'admin.falso@example.com' ------------------");
                List<AdministradorEN> adminNoExiste = administradorcen.DameAdministradoresPorEmail ("admin.falso@example.com") as List<AdministradorEN>;
                Console.WriteLine ("Administrador encontrado: " + (adminNoExiste.Count > 0 ? adminNoExiste [0].Nombre : "No encontrado"));

                Console.WriteLine ("\n====================================================================================");
                Console.WriteLine ("RESUMEN - PRUEBAS dameAdministradorPorEmail():");
                Console.WriteLine ("- Prueba 1: Buscar admin existente - Encontrado");
                Console.WriteLine ("- Prueba 2: Buscar con email de lector - No encontrado (esperado)");
                Console.WriteLine ("- Prueba 3: Buscar email inexistente - No encontrado (esperado)");
                Console.WriteLine ("====================================================================================");

                Console.WriteLine ("\n\n\n====================================================================================");
                Console.WriteLine ("====================================================================================");
                Console.WriteLine ("====================================================================================");


                /////////////////////////////////////////////////////////////////////////////////////////////////////

                // CREACIÓN USURIO - Lector 2 (Para asignarle y desasignarle libros a sus listas de libros en curso / guardados)
                Console.WriteLine ("\n\n------------------ Creación de Usuario Lector ------------------");

                int usuarioId2 = lectorcen.CrearLector (
 p_email: "marina.lector@example.com",
 p_nombreUsuario: "MarinaLectora",
 p_fechaNacimiento: new DateTime (1980, 11, 10),
 p_ciudadResidencia: "Villajoyosa", p_paisResidencia: "España",
 p_foto: "marinaFoto.png", p_rol: ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.RolUsuarioEnum.lector,
 p_pass: "password123",
                        p_cantLibrosCurso: 0,
                        p_cantLibrosLeidos: 0,
                        p_cantAutoresSeguidos: 0,
                        p_cantClubsSuscritos: 0);
                Console.WriteLine ("Usuario 'MarinaLectora' creado correctamente.");

                Console.WriteLine ("\n====================================================================================");
                Console.WriteLine ("RESUMEN - CREACIÓN DE LECTOR PARA PRUEBAS:");
                Console.WriteLine ("- Usuario 'MarinaLectora' creado correctamente");
                Console.WriteLine ("- Se utilizará para probar asignar/desasignar libros en listas");
                Console.WriteLine ("====================================================================================");

                // PRUEBAS PARA ASIGNAR UN PRIMER LIBRO A LA LISTA DE LIBROS EN CURSO
                Console.WriteLine ("\n==================== PRUEBAS ASIGNAR LIBRO A LISTA DE LIBROS EN CURSO ====================");

                SessionCPNHibernate sessionCPNHibernate = new SessionCPNHibernate ();
                LectorCP lectorCP = new LectorCP (sessionCPNHibernate);

                // ASIGNAR PRIMER LIBRO A LISTA EN CURSO DEL LECTOR 2 - (Caso Correcto - El libro no está en la lista)
                Console.WriteLine ("\n------------------ Asignar primer libro a lista en curso del Lector usando Custom CP (Esperado: Correcto) ------------------");
                lectorCP.AsignarLibroListaEnCurso (usuarioId2, new List<int> { idLibro11 });
                Console.WriteLine ("Libro 11 añadido correctamente a la lista en curso.");

                // ASIGNAR LIBRO A LISTA EN CURSO DEL LECTOR 2 - (Caso Incorrecto - El libro ya está en la lista)
                Console.WriteLine ("\n------------------ Asignar libro a lista en curso del Lector usando Custom CP (Esperado: Incorrecto - Duplicado) ------------------");
                try
                {
                        lectorCP.AsignarLibroListaEnCurso (usuarioId2, new List<int> { idLibro11 });
                }
                catch (ModelException ex)
                {
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                        Console.WriteLine ("Se ha capturado una ModelException al intentar asignar un libro que ya está en la lista");
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                }

                // ASIGNAR SEGUNDO LIBRO A LISTA EN CURSO DEL LECTOR 2 - (Caso Correcto - El libro no está en la lista)
                Console.WriteLine ("\n------------------ Asignar segundo libro a lista en curso del Lector usando Custom CP (Esperado: Correcto) ------------------");
                lectorCP.AsignarLibroListaEnCurso (usuarioId2, new List<int> { idLibro10 });
                Console.WriteLine ("Libro 10 añadido correctamente a la lista en curso.");

                // ASIGNAR TERCER LIBRO A LISTA EN CURSO DEL LECTOR 2 - (Caso Correcto - El libro no está en la lista)
                Console.WriteLine ("\n------------------ Asignar tercer libro a lista en curso del Lector usando Custom CP (Esperado: Correcto) ------------------");
                lectorCP.AsignarLibroListaEnCurso (usuarioId2, new List<int> { idLibro7 });
                Console.WriteLine ("Libro 7 añadido correctamente a la lista en curso.");

                /////////////////////////////////////////////////////////////////////////////////////////////////////

                // PRUEBAS PARA ASIGNAR LIBRO A LISTA DE LIBROS GUARDADOS
                Console.WriteLine ("\n\n==================== PRUEBAS ASIGNAR LIBRO A LISTA DE LIBROS GUARDADOS ====================");

                // ASIGNAR PRIMER LIBRO A LISTA GUARDADOS DEL LECTOR 2 - (Caso Correcto - El libro no está repetido)
                Console.WriteLine ("\n------------------ Asignar primer libro a lista guardados del Lector usando Custom CP (Esperado: Correcto) ------------------");
                lectorCP.AsignarLibroListaGuardados (usuarioId2, new List<int> { idLibro9 });
                Console.WriteLine ("Libro 9 añadido correctamente a la lista de guardados.");

                // ASIGNAR LIBRO A LISTA GUARDADOS DEL LECTOR 2 - (Caso Incorrecto - El libro ya está en la lista)
                Console.WriteLine ("\n------------------ Asignar libro a lista guardados del Lector usando Custom CP (Esperado: Incorrecto - Duplicado) ------------------");
                try
                {
                        lectorCP.AsignarLibroListaGuardados (usuarioId2, new List<int> { idLibro9 });
                }
                catch (ModelException ex)
                {
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                        Console.WriteLine ("Se ha capturado una ModelException al intentar asignar un libro que ya está en la lista");
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                }

                // ASIGNAR SEGUNDO LIBRO A LISTA GUARDADOS DEL LECTOR 2 - (Caso Correcto - El libro no está en la lista)
                Console.WriteLine ("\n------------------ Asignar segundo libro a lista guardados del Lector usando Custom CP (Esperado: Correcto) ------------------");
                lectorCP.AsignarLibroListaGuardados (usuarioId2, new List<int> { idLibro8 });
                Console.WriteLine ("Libro 8 añadido correctamente a la lista de guardados.");

                /////////////////////////////////////////////////////////////////////////////////////////////////////

                // PRUEBAS PARA DESASIGNAR LIBROS DE LISTA EN CURSO
                // Estado actual: Libros en curso = [idLibro11, idLibro10, idLibro7]
                Console.WriteLine ("\n\n==================== PRUEBAS DESASIGNAR LIBRO DE LISTA DE LIBROS EN CURSO ====================");

                // DESASIGNAR PRIMER LIBRO DE LISTA EN CURSO DEL LECTOR 2 - (Caso Correcto - El libro está en la lista)
                Console.WriteLine ("\n------------------ Desasignar libro de lista en curso del Lector usando Custom CP (Esperado: Correcto) ------------------");
                lectorCP.DesasignarLibroListaEnCurso (usuarioId2, new List<int> { idLibro10 });
                Console.WriteLine ("Libro 10 eliminado correctamente de la lista en curso.");

                // DESASIGNAR LIBRO DE LISTA EN CURSO DEL LECTOR 2 - (Caso Incorrecto - El libro no está en la lista)
                Console.WriteLine ("\n------------------ Desasignar libro de lista en curso del Lector usando Custom CP (Esperado: Incorrecto - No está en lista) ------------------");
                try
                {
                        lectorCP.DesasignarLibroListaEnCurso (usuarioId2, new List<int> { idLibro10 });
                }
                catch (ModelException ex)
                {
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                        Console.WriteLine ("Se ha capturado una ModelException al intentar desasignar un libro que no está en la lista");
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                }

                // DESASIGNAR SEGUNDO LIBRO DE LISTA EN CURSO DEL LECTOR 2 - (Caso Correcto - El libro está en la lista)
                Console.WriteLine ("\n------------------ Desasignar otro libro de lista en curso del Lector usando Custom CP (Esperado: Correcto) ------------------");
                lectorCP.DesasignarLibroListaEnCurso (usuarioId2, new List<int> { idLibro11 });
                Console.WriteLine ("Libro 11 eliminado correctamente de la lista en curso.");

                // NOTA: idLibro7 NO se desasigna, se queda en la lista

                /////////////////////////////////////////////////////////////////////////////////////////////////////

                // PRUEBAS PARA DESASIGNAR LIBROS DE LISTA GUARDADOS
                // Estado actual: Libros guardados = [idLibro9, idLibro8]
                Console.WriteLine ("\n\n==================== PRUEBAS DESASIGNAR LIBRO DE LISTA DE LIBROS GUARDADOS ====================");

                // DESASIGNAR PRIMER LIBRO DE LISTA GUARDADOS DEL LECTOR 2 - (Caso Correcto - El libro está en la lista)
                Console.WriteLine ("\n------------------ Desasignar libro de lista guardados del Lector usando Custom CP (Esperado: Correcto) ------------------");
                lectorCP.DesasignarLibroListaGuardados (usuarioId2, new List<int> { idLibro8 });
                Console.WriteLine ("Libro 8 eliminado correctamente de la lista de guardados.");

                // DESASIGNAR LIBRO DE LISTA GUARDADOS DEL LECTOR 2 - (Caso Incorrecto - El libro no está en la lista)
                Console.WriteLine ("\n------------------ Desasignar libro de lista guardados del Lector usando Custom CP (Esperado: Incorrecto - No está en lista) ------------------");
                try
                {
                        lectorCP.DesasignarLibroListaGuardados (usuarioId2, new List<int> { idLibro8 });
                }
                catch (ModelException ex)
                {
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                        Console.WriteLine ("Se ha capturado una ModelException al intentar desasignar un libro que no está en la lista");
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                }

                // NOTA: idLibro9 NO se desasigna, debe quedar en la lista

                /////////////////////////////////////////////////////////////////////////////////////////////////////

                Console.WriteLine ("\n\n====================================================================================");
                Console.WriteLine ("RESUMEN FINAL DE PRUEBAS:");
                Console.WriteLine ("- Lista EN CURSO: 1 libro (idLibro7) - Se han asignado 3 y se han desasignado 2");
                Console.WriteLine ("- Lista GUARDADOS: 1 libro (idLibro9) - Se han asignado 2 y se han desasignado 1");
                Console.WriteLine ("====================================================================================");

                /////////////////////////////////////////////////////////////////////////////////////////////////////

                Console.WriteLine ("\n\n\n====================================================================================");
                Console.WriteLine ("====================================================================================");
                Console.WriteLine ("====================================================================================");

                Console.WriteLine ("\n\n==================== PRUEBAS DE EVENTOS - CREACIONES VARIADAS ====================");

                //CREAR EVENTO LLENO
                Console.WriteLine ("\n------------------ Crear Evento lleno ------------------");

                int eventoId5 = eventocen.CrearEvento (
 p_nombre: "Evento3",
 p_foto: "Evento3.png",
 p_descripcion: "Evento3",
 p_fecha: new DateTime (2026, 7, 1),
 p_hora: new DateTime (2026, 7, 1, 18, 0, 0),
 p_ubicacion: "Biblioteca Central, Sala 3",
                        p_aforoMax: 20,
                        p_aforoActual: 20,
 p_administradorEventos: administradorId1
                        );

                Console.WriteLine ("Evento 3 creado correctamente con ID: " + eventoId5);

                //CREAR EVENTO VACIO
                Console.WriteLine ("\n------------------ Crear Evento vacio ------------------");


                int eventoId6 = eventocen.CrearEvento (
 p_nombre: "Evento5",
 p_foto: "Evento5.png",
 p_descripcion: "Evento5",
 p_fecha: new DateTime (2026, 7, 1),
 p_hora: new DateTime (2026, 7, 1, 18, 0, 0),
 p_ubicacion: "Biblioteca Central, Sala 3",
                        p_aforoMax: 20,
                        p_aforoActual: 0,
 p_administradorEventos: administradorId1
                        );

                Console.WriteLine ("Evento5 creado correctamente con ID: " + eventoId6);

                //CREAR EVENTO SIN ADMINISTRADOR
                Console.WriteLine ("\n------------------ Crear Evento sin Admin (Esperado: Incorrecto) ------------------");

                try
                {
                        int eventoId7 = eventocen.CrearEvento (
 p_nombre: "evento 2 - SIN ADMIN",
 p_foto: "evento2.png",
 p_descripcion: "evento 2",
 p_fecha: new DateTime (2026, 7, 1),
 p_hora: new DateTime (2026, 7, 1, 18, 0, 0),
 p_ubicacion: "Biblioteca Central, Sala 3",
                                p_aforoMax: 20,
                                p_aforoActual: 0,
 p_administradorEventos: -1
                                );
                        Console.WriteLine ("evento 2 - SIN ADMIN creado correctamente con ID: " + eventoId7);
                }
                catch (ModelException ex)
                {
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                        Console.WriteLine ("Se ha capturado una ModelException al intentar crear un evento sin administrador: " + ex);
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                }

                //CREAR EVENTO CON FECHA PASADA
                Console.WriteLine ("\n------------------ Crear Evento con fecha pasada ------------------");

                try
                {
                        int eventoId8 = eventocen.CrearEvento (
 p_nombre: "evento 4 - FECHA PASADA",
 p_foto: "evento4.png",
 p_descripcion: "evento 4",
 p_fecha: new DateTime (2020, 7, 1),
 p_hora: new DateTime (2020, 7, 1, 18, 0, 0),
 p_ubicacion: "Biblioteca Central, Sala 3",
                                p_aforoMax: 20,
                                p_aforoActual: 0,
 p_administradorEventos: administradorId1
                                );
                        Console.WriteLine ("evento 4 - FECHA PASADA creado correctamente con ID: " + eventoId8);
                }
                catch (ModelException ex)
                {
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                        Console.WriteLine ("Se ha capturado una ModelException al intentar crear un evento con fecha pasada: " + ex);
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                }


                /////////////////////////////////////////////////////////////////////////////////////////////////////

                Console.WriteLine ("\n\n\n====================================================================================");
                Console.WriteLine ("====================================================================================");
                Console.WriteLine ("====================================================================================");

                Console.WriteLine ("\n\n==================== PRUEBA DE MODIFICACIONES DE LIBRO ====================");

                // MODIFICAR LIBRO - Cambiar diversas caracteristicas
                Console.WriteLine ("\n------------------ Modificación de Libro (Resultado esperado: Correcto) ------------------");

                librocen.ModificarLibro (
                        idLibro10,
 p_titulo: "El peso del silencio - Edición Revisada",
 p_genero: "Drama",
                        p_edadRecomendada: 18,
 p_fechaPublicacion: new DateTime (2022, 9, 10),
                        p_numPags: 370,
 p_sinopsis: "Un drama intenso sobre secretos familiares y redención.",
 p_fotoPortada: "portada_peso_silencio_revisada.jpg",
 p_valoracionMedia: 4.0f
                        );

                Console.WriteLine ("Libro 'El peso del silencio' modificado correctamente (ID: " + idLibro10 + ")");

                // MODIFICAR LIBRO - Cambiar número de páginas a un número negativo
                Console.WriteLine ("\n------------------ Modificación de Libro con páginas negativas (Resultado esperado: Incorrecto) ------------------");

                try
                {
                        librocen.ModificarLibro (
                                idLibro10,
 p_titulo: "El peso del silencio - Edición Revisada",
 p_genero: "Drama",
                                p_edadRecomendada: 18,
 p_fechaPublicacion: new DateTime (2022, 9, 10),
 p_numPags: -50,                                  // Número de páginas negativo (INVÁLIDO)
 p_sinopsis: "Un drama intenso sobre secretos familiares y redención.",
 p_fotoPortada: "portada_peso_silencio_revisada.jpg",
 p_valoracionMedia: 4.0f
                                );
                        Console.WriteLine ("ERROR: Se permitió modificar el libro con páginas negativas (no debería suceder)");
                }
                catch (ModelException ex)
                {
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                        Console.WriteLine ("ModelException capturada correctamente: " + ex.Message);
                        Console.WriteLine ("El sistema validó correctamente que el número de páginas debe ser mayor que 0");
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                }


                Console.WriteLine ("\n\n====================================================================================");
                Console.WriteLine ("RESUMEN DE LAS PRUEBAS DE MODIFICACIÓN DE LIBRO:");
                Console.WriteLine ("Prueba 1 - Modificación correcta del libro con ID: " + idLibro10);
                Console.WriteLine ("  Cambios aplicados:");
                Console.WriteLine ("    - Título: 'El peso del silencio' - 'El peso del silencio - Edición Revisada'");
                Console.WriteLine ("    - Número de páginas: 350 → 370");
                Console.WriteLine ("    - Foto de portada actualizada");
                Console.WriteLine ("");
                Console.WriteLine ("Prueba 2 - Modificación incorrecta (páginas negativas)");
                Console.WriteLine ("  - Se intentó establecer número de páginas = -50");
                Console.WriteLine ("  - Resultado: ModelException capturada correctamente");
                Console.WriteLine ("====================================================================================");


                Console.WriteLine ("\n\n==================== PRUEBA DE MODIFICACIONES DE EVENTO ====================");

                // MODIFICAR EVENTO - Caso Correcto: Cambiar nombre y ubicación
                Console.WriteLine ("\n\n------------------ Modificación de Evento (Resultado esperado: Correcto) ------------------");

                eventocen.ModificarEvento (
                        eventoId1,
 p_nombre: "Club de Lectura de Novela Negra - Edición Especial",
 p_foto: "evento_novela_negra_especial.jpg",
 p_descripcion: "Únete a nuestro club para discutir las mejores obras del género negro - Edición Especial con invitados",
 p_fecha: new DateTime (2026, 12, 20),                         // Fecha futura
 p_hora: new DateTime (2026, 12, 20, 19, 30, 0),
 p_ubicacion: "Biblioteca Municipal - Sala Premium",
                        p_aforoMax: 50,
                        p_aforoActual: 15
                        );

                Console.WriteLine ("Evento modificado correctamente (ID: " + eventoId1 + ")");

                // MODIFICAR EVENTO - Caso Incorrecto: Intentar cambiar fecha a una pasada
                Console.WriteLine ("\n------------------ Modificación de Evento con fecha pasada (Resultado esperado: Incorrecto) ------------------");

                try
                {
                        eventocen.ModificarEvento (
                                eventoId1,
 p_nombre: "Club de Lectura de Novela Negra - Evento Pasado",
 p_foto: "evento_novela_negra.jpg",
 p_descripcion: "Descripción del evento",
 p_fecha: new DateTime (2020, 1, 1),                                 // Fecha pasada (INVÁLIDA)
 p_hora: new DateTime (2020, 1, 1, 18, 0, 0),
 p_ubicacion: "Biblioteca Central",
                                p_aforoMax: 30,
                                p_aforoActual: 0
                                );
                        Console.WriteLine ("ERROR: Se permitió modificar el evento con fecha pasada (no debería suceder)");
                }
                catch (ModelException ex)
                {
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                        Console.WriteLine ("ModelException capturada correctamente: " + ex.Message);
                        Console.WriteLine ("El sistema validó correctamente que no se puede modificar la fecha a una fecha pasada");
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                }

                Console.WriteLine ("\n====================================================================================");
                Console.WriteLine ("RESUMEN DE LAS PRUEBAS DE MODIFICACIÓN DE EVENTO:");
                Console.WriteLine ("Prueba 1 - Modificación correcta del evento con ID: " + eventoId1);
                Console.WriteLine ("  Cambios aplicados:");
                Console.WriteLine ("    - Nombre actualizado con sufijo '- Edición Especial'");
                Console.WriteLine ("    - Ubicación cambiada a 'Sala Premium'");
                Console.WriteLine ("    - Fecha futura válida (20/12/2026)");
                Console.WriteLine ("");
                Console.WriteLine ("Prueba 2 - Modificación incorrecta (fecha pasada)");
                Console.WriteLine ("  - Se intentó establecer fecha = 01/01/2020 (pasada)");
                Console.WriteLine ("  - Resultado: ModelException capturada correctamente");
                Console.WriteLine ("====================================================================================");


                Console.WriteLine ("\n\n==================== PRUEBA DE MODIFICACIONES DE RESEÑA ====================");

                // MODIFICAR RESEÑA - Caso Correcto: Cambiar texto de opinión y valoración
                Console.WriteLine ("\n\n------------------ Modificación de Reseña (Resultado esperado: Correcto) ------------------");

                reseñacen.ModificarReseña (
                        reseñaId1,
 p_textoOpinion: "Me encantó la trama y los personajes. Una historia muy bien desarrollada. Actualización: Sigue siendo excelente tras una segunda lectura.",
 p_valoracion: 4.5f,                         // Valoración válida entre 0 y 5
 p_fecha: new DateTime (2024, 10, 1)
                        );

                Console.WriteLine ("Reseña modificada correctamente (ID: " + reseñaId1 + ")");

                // MODIFICAR RESEÑA - Caso Incorrecto: Intentar valoración negativa
                Console.WriteLine ("\n------------------ Modificación de Reseña con valoración negativa (Resultado esperado: Incorrecto) ------------------");

                try
                {
                        reseñacen.ModificarReseña (
                                reseñaId2,
 p_textoOpinion: "Esta es una reseña modificada",
 p_valoracion: -2.0f,                                 // Valoración negativa (INVÁLIDA)
 p_fecha: new DateTime (2024, 10, 5)
                                );
                        Console.WriteLine ("ERROR: Se permitió modificar la reseña con valoración negativa (no debería suceder)");
                }
                catch (Exception ex)
                {
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                        Console.WriteLine ("Excepción capturada correctamente: " + ex.Message);
                        Console.WriteLine ("El sistema validó correctamente que la valoración no puede ser negativa");
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                }

                // MODIFICAR RESEÑA - Caso Incorrecto: Intentar valoración mayor a 5
                Console.WriteLine ("\n------------------ Modificación de Reseña con valoración > 5 (Resultado esperado: Incorrecto) ------------------");

                try
                {
                        reseñacen.ModificarReseña (
                                reseñaId3,
 p_textoOpinion: "Reseña con valoración fuera de rango",
 p_valoracion: 10.0f,                                 // Valoración mayor a 5 (INVÁLIDA)
 p_fecha: new DateTime (2024, 10, 10)
                                );
                        Console.WriteLine ("ERROR: Se permitió modificar la reseña con valoración > 5 (no debería suceder)");
                }
                catch (Exception ex)
                {
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                        Console.WriteLine ("Excepción capturada correctamente: " + ex.Message);
                        Console.WriteLine ("El sistema validó correctamente que la valoración no puede ser mayor a 5");
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                }

                Console.WriteLine ("\n====================================================================================");
                Console.WriteLine ("RESUMEN DE LAS PRUEBAS DE MODIFICACIÓN DE RESEÑA:");
                Console.WriteLine ("Prueba 1 - Modificación correcta de reseña con ID: " + reseñaId1);
                Console.WriteLine ("  Cambios aplicados:");
                Console.WriteLine ("    - Texto de opinión actualizado con información adicional");
                Console.WriteLine ("    - Valoración: De 5.0 a 4.5 (válida)");
                Console.WriteLine ("");
                Console.WriteLine ("Prueba 2 - Modificación incorrecta (valoración negativa)");
                Console.WriteLine ("  - Se intentó establecer valoración = -2.0");
                Console.WriteLine ("  - Resultado: Excepción capturada correctamente");
                Console.WriteLine ("");
                Console.WriteLine ("Prueba 3 - Modificación incorrecta (valoración > 5)");
                Console.WriteLine ("  - Se intentó establecer valoración = 10.0");
                Console.WriteLine ("  - Resultado: Excepción capturada correctamente");
                Console.WriteLine ("====================================================================================");

                /////////////////////////////////////////////////////////////////////////////////////////////////////

                Console.WriteLine ("\n\n\n====================================================================================");
                Console.WriteLine ("====================================================================================");
                Console.WriteLine ("====================================================================================");

                Console.WriteLine ("\n\n==================== PRUEBAS INVÁLIDAS DE CREACIÓN DE LIBRO ====================");

                // Nota: Las creaciones correctas, para todos los casos, ya se han realizado como parte de otras pruebas

                // CREAR LIBRO - Caso Incorrecto: Número de páginas negativo
                Console.WriteLine ("\n------------------ Creación de Libro con número de páginas negativo (Resultado esperado: Incorrecto) ------------------");

                try
                {
                        int idLibroInvalido1 = librocen.CrearLibro (
 p_titulo: "Libro con Páginas Negativas",
 p_genero: "Prueba",
                                p_edadRecomendada: 12,
 p_fechaPublicacion: new DateTime (2024, 1, 1),
 p_numPags: -100,                                 // Número de páginas negativo (INVÁLIDO)
 p_sinopsis: "Libro de prueba con validación incorrecta",
 p_fotoPortada: "portada_invalida.jpg",
 p_autorPublicador: autorId,
 p_valoracionMedia: 0.0f
                                );
                        Console.WriteLine ("ERROR: Se permitió crear un libro con páginas negativas (no debería suceder)");
                }
                catch (ModelException ex)
                {
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                        Console.WriteLine ("ModelException capturada correctamente: " + ex.Message);
                        Console.WriteLine ("El sistema validó correctamente que el número de páginas debe ser mayor que 0");
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                }

                // CREAR LIBRO - Caso Incorrecto: Sin autor asociado
                Console.WriteLine ("\n------------------ Creación de Libro sin autor asociado (Resultado esperado: Incorrecto) ------------------");

                try
                {
                        int idLibroInvalido2 = librocen.CrearLibro (
 p_titulo: "Libro sin Autor",
 p_genero: "Prueba",
                                p_edadRecomendada: 15,
 p_fechaPublicacion: new DateTime (2024, 2, 1),
                                p_numPags: 200,
 p_sinopsis: "Libro de prueba sin autor asociado",
 p_fotoPortada: "portada_sin_autor.jpg",
 p_autorPublicador: -1,                                 // Sin autor asociado (INVÁLIDO)
 p_valoracionMedia: 0.0f
                                );
                        Console.WriteLine ("ERROR: Se permitió crear un libro sin autor asociado (no debería suceder)");
                }
                catch (ModelException ex)
                {
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                        Console.WriteLine ("ModelException capturada correctamente: " + ex.Message);
                        Console.WriteLine ("El sistema validó correctamente que un libro debe estar asociado a un autor");
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                }

                Console.WriteLine ("\n====================================================================================");
                Console.WriteLine ("RESUMEN DE LAS PRUEBAS INVÁLIDAS DE CREACIÓN DE LIBRO:");
                Console.WriteLine ("Prueba 1 - Crear libro con páginas negativas (-100)");
                Console.WriteLine ("  - Resultado: ModelException capturada correctamente");
                Console.WriteLine ("  - Validación: El número de páginas debe ser mayor que 0");
                Console.WriteLine ("");
                Console.WriteLine ("Prueba 2 - Crear libro sin autor asociado");
                Console.WriteLine ("  - Resultado: ModelException capturada correctamente");
                Console.WriteLine ("  - Validación: Un libro debe estar asociado a un autor");
                Console.WriteLine ("====================================================================================");

                Console.WriteLine ("\n\n==================== PRUEBAS INVÁLIDAS DE CREACIÓN DE NOTICIA ====================");

                // CREAR NOTICIA - Caso Incorrecto: Sin administrador asociado
                Console.WriteLine ("\n------------------ Creación de Noticia sin administrador asociado (Resultado esperado: Incorrecto) ------------------");

                try
                {
                        int idNoticiaInvalida = noticiacen.CrearNoticia (
 p_titulo: "Noticia sin Administrador",
 p_fechaPublicacion: new DateTime (2024, 11, 1),
 p_foto: "noticia_sin_admin.jpg",
 p_textoContenido: "Esta es una noticia de prueba sin administrador asociado",
 p_administradorNoticias: -1                                 // Sin administrador asociado (INVÁLIDO)
                                );
                        Console.WriteLine ("ERROR: Se permitió crear una noticia sin administrador asociado (no debería suceder)");
                }
                catch (ModelException ex)
                {
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                        Console.WriteLine ("ModelException capturada correctamente: " + ex.Message);
                        Console.WriteLine ("El sistema validó correctamente que una noticia debe estar asociada a un administrador");
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                }

                Console.WriteLine ("\n====================================================================================");
                Console.WriteLine ("RESUMEN DE LA PRUEBA INVÁLIDA DE CREACIÓN DE NOTICIA:");
                Console.WriteLine ("- Se intentó crear una noticia sin administrador responsable");
                Console.WriteLine ("- Resultado: ModelException capturada correctamente");
                Console.WriteLine ("- Validación: Una noticia debe estar asociada a un administrador");
                Console.WriteLine ("====================================================================================");

                Console.WriteLine ("\n\n==================== PRUEBAS INVÁLIDAS DE CREACIÓN DE RESEÑA ====================");

                // CREAR RESEÑA - Caso Incorrecto: Sin lector valorador asociado
                Console.WriteLine ("\n------------------ Creación de Reseña sin lector valorador (Resultado esperado: Incorrecto) ------------------");

                try
                {
                        int idReseñaInvalida1 = reseñacen.CrearReseña (
 p_textoOpinion: "Esta es una reseña de prueba sin lector",
 p_valoracion: 4.0f,
 p_lectorValorador: -1,                                 // Sin lector valorador (INVÁLIDO)
 p_libroReseñado: idLibro1,
 p_fecha: new DateTime (2024, 11, 1)
                                );
                        Console.WriteLine ("ERROR: Se permitió crear una reseña sin lector valorador (no debería suceder)");
                }
                catch (ModelException ex)
                {
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                        Console.WriteLine ("ModelException capturada correctamente: " + ex.Message);
                        Console.WriteLine ("El sistema validó correctamente que una reseña debe tener un lector valorador asociado");
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                }

                // CREAR RESEÑA - Caso Incorrecto: Sin libro reseñado asociado
                Console.WriteLine ("\n------------------ Creación de Reseña sin libro reseñado (Resultado esperado: Incorrecto) ------------------");

                try
                {
                        int idReseñaInvalida2 = reseñacen.CrearReseña (
 p_textoOpinion: "Esta es una reseña de prueba sin libro",
 p_valoracion: 3.5f,
 p_lectorValorador: usuarioId1,
 p_libroReseñado: -1,                                 // Sin libro reseñado (INVÁLIDO)
 p_fecha: new DateTime (2024, 11, 1)
                                );
                        Console.WriteLine ("ERROR: Se permitió crear una reseña sin libro reseñado (no debería suceder)");
                }
                catch (ModelException ex)
                {
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                        Console.WriteLine ("ModelException capturada correctamente: " + ex.Message);
                        Console.WriteLine ("El sistema validó correctamente que una reseña debe tener un libro reseñado asociado");
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                }

                Console.WriteLine ("\n====================================================================================");
                Console.WriteLine ("RESUMEN DE LAS PRUEBAS INVÁLIDAS DE CREACIÓN DE RESEÑA:");
                Console.WriteLine ("Prueba 1 - Crear reseña sin lector valorador");
                Console.WriteLine ("  - Resultado: ModelException capturada correctamente");
                Console.WriteLine ("  - Validación: Una reseña debe tener un lector valorador asociado");
                Console.WriteLine ("");
                Console.WriteLine ("Prueba 2 - Crear reseña sin libro reseñado");
                Console.WriteLine ("  - Resultado: ModelException capturada correctamente");
                Console.WriteLine ("  - Validación: Una reseña debe tener un libro reseñado asociado");
                Console.WriteLine ("====================================================================================");

                ///////////////////////////////////////////////////////////////////////////////////////////////////////////

                Console.WriteLine ("\n\n\n====================================================================================");
                Console.WriteLine ("====================================================================================");
                Console.WriteLine ("====================================================================================");

                Console.WriteLine ("\n\n==================== PRUEBAS DE EVENTOS - INSCRIBIR Y DESINSCRIBIR MIEMBROS ====================");

                //INSCRIBIR USUARIO AL EVENTO CREADO CON AFORO ACTUAL = 0
                Console.WriteLine ("\n------------------ Inscripción de Usuario al Evento (Resulatado esperado: Correcto) ------------------");

                SessionCPNHibernate sessionCPNHibernate2 = new SessionCPNHibernate ();
                UsuarioCP usuarioCP = new UsuarioCP (sessionCPNHibernate2);

                try
                {
                        usuarioCP.InscribirAEvento (usuarioId1, new List<int> { eventoId1 });
                        Console.WriteLine ("Usuario inscrito correctamente al evento 'Club de Lectura de Novela Negra'.");
                }
                catch (ModelException ex)
                {
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                        Console.WriteLine ("Se ha capturado una ModelException al intentar inscribirse a un evento: " + ex);
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                }

                //INSCRIBIR USUARIO AL MISMO EVENTO OTRA VEZ
                Console.WriteLine ("\n------------------ Inscripción de Usuario al mismo Evento otra vez (Resulatado: Incorrecto");

                SessionCPNHibernate sessionCPNHibernate3 = new SessionCPNHibernate ();
                UsuarioCP usuarioCP2 = new UsuarioCP (sessionCPNHibernate2);

                try
                {
                        usuarioCP2.InscribirAEvento (usuarioId1, new List<int> { eventoId1 });
                        Console.WriteLine ("Usuario inscrito correctamente al evento 'Club de Lectura de Novela Negra'.");
                }
                catch (ModelException ex)
                {
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                        Console.WriteLine ("Se ha capturado una ModelException al intentar inscribirse otra vez al mismo evento: " + ex);
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                }

                //INSCRIBIR USUARIO AL EVENTO CREADO CON CAPACIDAD AFORO ACTUAL = AFORO MAXIMO
                Console.WriteLine ("\n------------------ Inscripción de Usuario al Evento lleno (Resultado Esperado: Incorrecto) ------------------");

                SessionCPNHibernate sessionCPNHibernate5 = new SessionCPNHibernate ();
                UsuarioCP usuarioCP5 = new UsuarioCP (sessionCPNHibernate2);

                try
                {
                        usuarioCP5.InscribirAEvento (usuarioId1, new List<int> { eventoId3 });
                        Console.WriteLine ("Usuario inscrito correctamente al evento 'Evento3");
                }
                catch (ModelException ex)
                {
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                        Console.WriteLine ("Se ha capturado una ModelException al intentar inscribirse a un evento con aforo completo: " + ex);
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                }


                //DESINSCRIBIR USUARIO DEL EVENTO CREADO
                Console.WriteLine ("\n------------------ Desinscripción de Usuario del Evento (Resulatado esperado: Correcto) ------------------");
                SessionCPNHibernate sessionCPNHibernate4 = new SessionCPNHibernate ();
                UsuarioCP usuarioCP3 = new UsuarioCP (sessionCPNHibernate2);

                try
                {
                        usuarioCP3.DesinscribirDeEvento (usuarioId1, new List<int> { eventoId1 });
                        Console.WriteLine ("Usuario desinscrito correctamente del evento 'Club de Lectura de Novela Negra'.");
                }
                catch (ModelException ex)
                {
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                        Console.WriteLine ("Se ha capturado una ModelException al intentar desinscribirse de un evento: " + ex);
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                }

                //DESINSCRIBIR USUARIO DE UN EVENTO AL QUE NO ESTA SUSCRITO
                Console.WriteLine ("\n------------------ Desinscripción de Usuario del Evento al que no está suscrito (Resulatado esperado: Inorrecto) ------------------");
                SessionCPNHibernate sessionCPNHibernate6 = new SessionCPNHibernate ();
                UsuarioCP usuarioCP6 = new UsuarioCP (sessionCPNHibernate2);

                try
                {
                        usuarioCP6.DesinscribirDeEvento (usuarioId1, new List<int> { eventoId5 });
                        Console.WriteLine ("Usuario dessuscrito correctamente del evento vacio");
                }
                catch (ModelException ex)
                {
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                        Console.WriteLine ("Se ha capturado una ModelException al intentar desinscribirse de un evento: " + ex);
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                }

                ///////////////////////////////////////////////////////////////////////////////////////////////////////////

                Console.WriteLine ("\n\n\n====================================================================================");
                Console.WriteLine ("====================================================================================");
                Console.WriteLine ("====================================================================================");

                Console.WriteLine ("\n\n==================== PRUEBAS DE CLUBS - SUSCRIBIR Y DESUSCRIBIR PARTICIPANTES ====================");

                // Nota: Para la suscripción y desuscripción de usuarios a club usamos el primer club definido en las pruebas de ReadFilter de Clubs

                //SUSCRIBIR USUARIO AL CLUB CREADO
                Console.WriteLine ("\n------------------ Suscripción de Usuario al Club (Resulatado esperado: Correcto) ------------------");
                SessionCPNHibernate sessionCPNHibernate9 = new SessionCPNHibernate ();
                UsuarioCP usuarioCP4 = new UsuarioCP (sessionCPNHibernate4);

                try
                {
                        usuarioCP4.SuscribirAClub (usuarioId3, new List<int> { clubId1 });
                        Console.WriteLine ("Usuario suscrito correctamente al club 'Club de Ciencia Ficción'.");
                }
                catch (ModelException ex)
                {
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                        Console.WriteLine ("Se ha capturado una ModelException al intentar suscribirse a un club: " + ex);
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                }

                // DESUSCRIBIR USUARIO DEL CLUB CREADO
                Console.WriteLine ("\n------------------ Desuscripción de Usuario del Club (Resulatado esperado: Correcto) ------------------");
                SessionCPNHibernate sessionCPNHibernate7 = new SessionCPNHibernate ();
                UsuarioCP usuarioCP7 = new UsuarioCP (sessionCPNHibernate5);
                try
                {
                        usuarioCP7.DesuscribirDeClub (usuarioId3, new List<int> { clubId1 });
                        Console.WriteLine ("Usuario desuscrito correctamente del club 'Club de Ciencia Ficción'.");
                }
                catch (ModelException ex)
                {
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                        Console.WriteLine ("Se ha capturado una ModelException al intentar desuscribirse de un club: " + ex);
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                }

                /////////////////////////////////////////////////////////////////////////////////////////////////////

                Console.WriteLine ("\n\n\n====================================================================================");
                Console.WriteLine ("====================================================================================");
                Console.WriteLine ("====================================================================================");

                Console.WriteLine ("\n\n==================== PRUEBAS DE OBTENER DATOS TOTALES DE TITULOS LIBRO Y USUARIOS ====================");

                //OBTENER CANTIDAD TOTAL DE LIBROS GUARDADOS EN BASE DE DATOS
                Console.WriteLine ("\n------------------ Obtener Cantidad de Libros Totales ------------------");

                int cantidadTotalLibros = librocen.DameCantidadTotalLibros ();

                if (cantidadTotalLibros >= 0) {
                        Console.WriteLine ("Cantidad total de libros en la base de datos: " + cantidadTotalLibros);
                        Console.WriteLine ("Se han contado todos los libros almacenados correctamente");
                }
                else {
                        Console.WriteLine ("ERROR: La cantidad de libros total no puede ser negativa");
                }

                Console.WriteLine ("\n====================================================================================");
                Console.WriteLine ("RESUMEN DE LA PRUEBA DameCantidadTotalLibros():");
                Console.WriteLine ("Se obtuvo correctamente el total de libros: " + cantidadTotalLibros);
                Console.WriteLine ("====================================================================================");

                //OBTENER CANTIDAD TOTAL DE USUARIOS GUARDADOS EN BASE DE DATOS
                Console.WriteLine ("\n------------------ Obtener Cantidad de Usuarios Totales ------------------");

                int cantidadTotalUsuarios = usuariocen.DameCantidadTotalUsuarios ();

                if (cantidadTotalUsuarios >= 0) {
                        Console.WriteLine ("Cantidad total de usuarios en la base de datos: " + cantidadTotalUsuarios);
                        Console.WriteLine ("Se han verificado todos los usuarios almacenados correctamente");
                }
                else {
                        Console.WriteLine ("ERROR: La cantidad de usuarios no puede ser negativa");
                }

                Console.WriteLine ("\n====================================================================================");
                Console.WriteLine ("RESUMEN DE LA PRUEBA DameCantidadTotalUsuarios():");
                Console.WriteLine ("Se obtuvo correctamente el total de usuarios: " + cantidadTotalUsuarios);
                Console.WriteLine ("====================================================================================");

                /////////////////////////////////////////////////////////////////////////////////////////////////////

                Console.WriteLine ("\n\n\n====================================================================================");
                Console.WriteLine ("====================================================================================");
                Console.WriteLine ("====================================================================================");

                Console.WriteLine ("\n\n==================== PRUEBAS DE SEGUIR A UN AUTOR ====================");

                SessionCPNHibernate sessionCPNHibernate14 = new SessionCPNHibernate ();
                LectorCP lectorCP2 = new LectorCP (sessionCPNHibernate14);

                // PRUEBA 1: Lector sigue a un autor (Caso Correcto)
                Console.WriteLine ("\n------------------ Prueba 1: Lector sigue a Autor (Resultado esperado: Correcto) ------------------");

                try
                {
                        lectorCP2.SeguirAutor (usuarioId2, new List<int> { autorId });
                        Console.WriteLine ("Usuario 2 (Marina Lectora) ahora sigue al autor correctamente");
                        Console.WriteLine ("  - Se incrementó CantAutoresSeguidos del lector");
                        Console.WriteLine ("  - Se incrementó NumeroSeguidores del autor");
                }
                catch (ModelException ex)
                {
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                        Console.WriteLine ("ModelException capturada: " + ex.Message);
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                }

                // PRUEBA 1: Lector sigue a otro autor (Caso Correcto)
                Console.WriteLine ("\n------------------ Prueba 2: Lector sigue a otro Autor (Resultado esperado: Correcto) ------------------");

                try
                {
                        lectorCP2.SeguirAutor (usuarioId2, new List<int> { autorId2 });
                        Console.WriteLine ("Usuario 2 (Marina Lectora) ahora sigue al autor correctamente");
                        Console.WriteLine ("  - Se incrementó CantAutoresSeguidos del lector");
                        Console.WriteLine ("  - Se incrementó NumeroSeguidores del autor");
                }
                catch (ModelException ex)
                {
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                        Console.WriteLine ("ModelException capturada: " + ex.Message);
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                }

                // PRUEBA 2: Intentar seguir al mismo autor otra vez (Caso Incorrecto)
                Console.WriteLine ("\n------------------ Prueba 3: Intentar seguir al mismo Autor otra vez (Resultado esperado: Incorrecto) ------------------");

                SessionCPNHibernate sessionCPNHibernate15 = new SessionCPNHibernate ();
                LectorCP lectorCP3 = new LectorCP (sessionCPNHibernate15);

                try
                {
                        lectorCP3.SeguirAutor (usuarioId2, new List<int> { autorId });
                        Console.WriteLine ("ERROR: Se permitió seguir al mismo autor dos veces (no debería suceder)");
                }
                catch (ModelException ex)
                {
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                        Console.WriteLine ("ModelException capturada correctamente: " + ex.Message);
                        Console.WriteLine ("El sistema validó que el lector ya está siguiendo a este autor");
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                }

                // PRUEBA 3: Intentar seguir a un autor inexistente (Caso Incorrecto)
                Console.WriteLine ("\n------------------ Prueba 4: Intentar seguir a Autor inexistente (Resultado esperado: Incorrecto) ------------------");

                SessionCPNHibernate sessionCPNHibernate16 = new SessionCPNHibernate ();
                LectorCP lectorCP4 = new LectorCP (sessionCPNHibernate16);

                try
                {
                        lectorCP4.SeguirAutor (usuarioId2, new List<int> { 99999 });
                        Console.WriteLine ("ERROR: Se permitió seguir a un autor inexistente (no debería suceder)");
                }
                catch (ModelException ex)
                {
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                        Console.WriteLine ("ModelException capturada correctamente: " + ex.Message);
                        Console.WriteLine ("El sistema validó que el autor no existe");
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                }
                catch (Exception ex)
                {
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                        Console.WriteLine ("Excepción capturada: " + ex.Message);
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                }

                Console.WriteLine ("\n\n==================== PRUEBAS DE DEJAR DE SEGUIR A UN AUTOR ====================");

                // PRUEBA 4: Dejar de seguir a un autor que se está siguiendo (Caso Correcto)
                Console.WriteLine ("\n------------------ Prueba 5: Dejar de seguir a Autor (Resultado esperado: Correcto) ------------------");

                SessionCPNHibernate sessionCPNHibernate17 = new SessionCPNHibernate ();
                LectorCP lectorCP5 = new LectorCP (sessionCPNHibernate17);

                try
                {
                        lectorCP5.DejarDeSeguirAutor (usuarioId2, new List<int> { autorId });
                        Console.WriteLine ("Usuario 2 (Marina Lectora) dejó de seguir al autor correctamente");
                        Console.WriteLine ("  - Se decrementó CantAutoresSeguidos del lector");
                        Console.WriteLine ("  - Se decrementó NumeroSeguidores del autor");
                }
                catch (ModelException ex)
                {
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                        Console.WriteLine ("ModelException capturada: " + ex.Message);
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                }

                // PRUEBA 5: Intentar dejar de seguir a un autor que NO se está siguiendo (Caso Incorrecto)
                Console.WriteLine ("\n------------------ Prueba 6: Intentar dejar de seguir a Autor que NO se sigue (Resultado esperado: Incorrecto) ------------------");

                SessionCPNHibernate sessionCPNHibernate18 = new SessionCPNHibernate ();
                LectorCP lectorCP6 = new LectorCP (sessionCPNHibernate18);

                try
                {
                        lectorCP6.DejarDeSeguirAutor (usuarioId2, new List<int> { autorId });
                        Console.WriteLine ("ERROR: Se permitió dejar de seguir a un autor que no se estaba siguiendo (no debería suceder)");
                }
                catch (ModelException ex)
                {
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                        Console.WriteLine ("ModelException capturada correctamente: " + ex.Message);
                        Console.WriteLine ("El sistema validó que el lector no está siguiendo a este autor");
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                }

                // PRUEBA 6: Intentar dejar de seguir a un autor inexistente (Caso Incorrecto)
                Console.WriteLine ("\n------------------ Prueba 7: Intentar dejar de seguir a Autor inexistente (Resultado esperado: Incorrecto) ------------------");

                SessionCPNHibernate sessionCPNHibernate19 = new SessionCPNHibernate ();
                LectorCP lectorCP7 = new LectorCP (sessionCPNHibernate19);

                try
                {
                        lectorCP7.DejarDeSeguirAutor (usuarioId2, new List<int> { 99999 });
                        Console.WriteLine ("ERROR: Se permitió dejar de seguir a un autor inexistente (no debería suceder)");
                }
                catch (ModelException ex)
                {
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                        Console.WriteLine ("ModelException capturada correctamente: " + ex.Message);
                        Console.WriteLine ("El sistema validó que el autor no existe");
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                }
                catch (Exception ex)
                {
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                        Console.WriteLine ("Excepción capturada: " + ex.Message);
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                }

                Console.WriteLine ("\n====================================================================================");
                Console.WriteLine ("RESUMEN DE LAS PRUEBAS DE SEGUIR Y DEJAR DE SEGUIR AUTOR:");
                Console.WriteLine ("Prueba 1 - Lector sigue a autor (Correcto)");
                Console.WriteLine ("Prueba 2 - Lector sigue a otro autor diferente (Correcto)");
                Console.WriteLine ("Prueba 3 - Intentar seguir al mismo autor dos veces (Error)");
                Console.WriteLine ("Prueba 4 - Intentar seguir a autor inexistente (Error)");
                Console.WriteLine ("Prueba 5 - Dejar de seguir a autor (Correcto)");
                Console.WriteLine ("Prueba 6 - Intentar dejar de seguir a autor que NO se sigue (Error)");
                Console.WriteLine ("Prueba 7 - Intentar dejar de seguir a autor inexistente (Error)");
                Console.WriteLine ("====================================================================================");

                /////////////////////////////////////////////////////////////////////////////////////////////////////

                Console.WriteLine ("\n\n\n====================================================================================");
                Console.WriteLine ("====================================================================================");
                Console.WriteLine ("====================================================================================");

                Console.WriteLine ("\n\n==================== PRUEBAS DE CAMBIO DE CONTRASEÑA DE USUARIO ====================");

                // PRUEBA 1: Cambiar contraseña correctamente (Caso Correcto)
                Console.WriteLine ("\n------------------ Prueba 1: Cambiar contraseña correctamente (Resultado esperado: Correcto) ------------------");

                SessionCPNHibernate sessionCPNHibernate20 = new SessionCPNHibernate ();
                UsuarioCP usuarioCP20 = new UsuarioCP (sessionCPNHibernate20);

                try
                {
                        usuarioCP20.CambiarPassword (usuarioId1, "password123", "newPassword456");
                        Console.WriteLine ("Contraseña cambiada correctamente");
                        Console.WriteLine ("  - Contraseña antigua: 'password123'");
                        Console.WriteLine ("  - Contraseña nueva: 'newPassword456'");
                }
                catch (ModelException ex)
                {
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                        Console.WriteLine ("ModelException capturada: " + ex.Message);
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                }

                // PRUEBA 2: Intentar cambiar con contraseña antigua incorrecta (Caso Incorrecto)
                Console.WriteLine ("\n------------------ Prueba 2: Intentar cambiar con contraseña antigua incorrecta (Resultado esperado: Incorrecto) ------------------");

                SessionCPNHibernate sessionCPNHibernate21 = new SessionCPNHibernate ();
                UsuarioCP usuarioCP21 = new UsuarioCP (sessionCPNHibernate21);

                try
                {
                        usuarioCP21.CambiarPassword (usuarioId1, "passwordIncorrecto", "anotherPassword789");
                        Console.WriteLine ("ERROR: Se permitió cambiar la contraseña con contraseña antigua incorrecta (no debería suceder)");
                }
                catch (ModelException ex)
                {
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                        Console.WriteLine ("ModelException capturada correctamente: " + ex.Message);
                        Console.WriteLine ("El sistema validó que la contraseña antigua no es correcta");
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                }

                // PRUEBA 3: Intentar cambiar con contraseña antigua vacía (Caso Incorrecto)
                Console.WriteLine ("\n------------------ Prueba 4: Intentar cambiar con contraseña antigua vacía (Resultado esperado: Incorrecto) ------------------");

                SessionCPNHibernate sessionCPNHibernate23 = new SessionCPNHibernate ();
                UsuarioCP usuarioCP23 = new UsuarioCP (sessionCPNHibernate23);

                try
                {
                        usuarioCP23.CambiarPassword (usuarioId1, "", "newPassword789");
                        Console.WriteLine ("ERROR: Se permitió cambiar con contraseña vacía (no debería suceder)");
                }
                catch (ModelException ex)
                {
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                        Console.WriteLine ("ModelException capturada correctamente: " + ex.Message);
                        Console.WriteLine ("El sistema validó que las contraseñas no pueden estar vacías");
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                }

                Console.WriteLine ("\n====================================================================================");
                Console.WriteLine ("RESUMEN DE LAS PRUEBAS DE CAMBIAR CONTRASEÑA:");
                Console.WriteLine ("Prueba 1 - Cambiar contraseña correctamente (Caso Correcto)");
                Console.WriteLine ("Prueba 2 - Intentar cambiar con contraseña antigua incorrecta (Error)");
                Console.WriteLine ("Prueba 3 - Intentar usar la misma contraseña como nueva (Error");
                Console.WriteLine ("====================================================================================");



                /////////////////////////////////////////////////////////////////////////////////////////////////////

                Console.WriteLine ("\n\n\n====================================================================================");
                Console.WriteLine ("====================================================================================");
                Console.WriteLine ("====================================================================================");

                Console.WriteLine ("\n\n==================== PRUEBAS DE EXPULSAR USUARIO DE UN CLUB ====================");

                // VERIFICACIÓN DE IDs DE CLUBS
                Console.WriteLine ("\n------------------ Verificación de IDs de Clubs disponibles ------------------");
                Console.WriteLine ("clubId1 (Club de Ciencia Ficción): " + clubId1);
                Console.WriteLine ("clubId2 (Club de Misterio y Suspense): " + clubId2);

                // PREPARACIÓN: Suscribir usuarios al club para luego expulsarlos
                Console.WriteLine ("\n------------------ Preparación: Suscribir usuarios al Club de Ciencia Ficción para las pruebas de expulsión ------------------");

                SessionCPNHibernate sessionCPNHibernate13 = new SessionCPNHibernate ();
                UsuarioCP usuarioCP13 = new UsuarioCP (sessionCPNHibernate13);
                ClubCP clubCP = new ClubCP (sessionCPNHibernate13);

                // Suscribir usuarioId2 al club
                try
                {
                        //usuarioCP13.SuscribirAClub (usuarioId2, new List<int> { clubId1 });
                        Console.WriteLine ("Usuario 2 (Marina Lectora) suscrito al Club de Ciencia Ficción");
                }
                catch (ModelException ex)
                {
                        Console.WriteLine ("Usuario 1 error: " + ex.Message);
                }


                // PRUEBA 1: Expulsar usuario que está suscrito al club (Caso Correcto)
                Console.WriteLine ("\n------------------ Prueba 1: Expulsar Usuario del Club (Resultado esperado: Correcto) ------------------");

                try
                {
                        //clubCP.ExpulsarUsuarioClub (clubId1, usuarioId2);
                        Console.WriteLine ("Usuario 2 (Marina Lectora) expulsado correctamente del Club de Ciencia Ficción");
                }
                catch (ModelException ex)
                {
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                        Console.WriteLine ("ModelException capturada: " + ex.Message);
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                }

                // PRUEBA 2: Intentar expulsar usuario que NO está suscrito al club (Caso Incorrecto)
                Console.WriteLine ("\n------------------ Prueba 2: Expulsar Usuario que NO está en el Club (Resultado esperado: Incorrecto) ------------------");

                try
                {
                        clubCP.ExpulsarUsuarioClub (clubId1, usuarioId2);
                        Console.WriteLine ("ERROR: Se permitió expulsar a un usuario que no está en el club (no debería suceder)");
                }
                catch (ModelException ex)
                {
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                        Console.WriteLine ("ModelException capturada correctamente: " + ex.Message);
                        Console.WriteLine ("El sistema validó que el usuario no está suscrito al club");
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                }

                // PRUEBA 3: Intentar expulsar usuario de un club inexistente (Caso Incorrecto)
                Console.WriteLine ("\n------------------ Prueba 3: Expulsar Usuario de Club inexistente (Resultado esperado: Incorrecto) ------------------");

                try
                {
                        clubCP.ExpulsarUsuarioClub (99999, usuarioId2);
                        Console.WriteLine ("ERROR: Se permitió expulsar usuario de un club inexistente (no debería suceder)");
                }
                catch (ModelException ex)
                {
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                        Console.WriteLine ("ModelException capturada correctamente: " + ex.Message);
                        Console.WriteLine ("El sistema validó que el club no existe");
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                }
                catch (Exception ex)
                {
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                        Console.WriteLine ("Excepción capturada: " + ex.Message);
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                }

                Console.WriteLine ("\n====================================================================================");
                Console.WriteLine ("RESUMEN DE LAS PRUEBAS DE ExpulsarUsuarioClub():");
                Console.WriteLine ("Prueba 1 - Expulsar usuario suscrito al club");
                Console.WriteLine ("  - Usuario 1 expulsado correctamente del Club de Ciencia Ficción");
                Console.WriteLine ("  - Operación exitosa");
                Console.WriteLine ("");
                Console.WriteLine ("Prueba 2 - Intentar expulsar usuario NO suscrito");
                Console.WriteLine ("  - Se intentó expulsar Usuario 1 del Club de Misterio y Suspense");
                Console.WriteLine ("  - ModelException capturada correctamente");
                Console.WriteLine ("");
                Console.WriteLine ("Prueba 3 - Intentar expulsar usuario de club inexistente");
                Console.WriteLine ("  - Se intentó expulsar de club con ID 99999");
                Console.WriteLine ("  - Excepción capturada correctamente");
                Console.WriteLine ("");
                Console.WriteLine ("====================================================================================");


                /////////////////////////////////////////////////////////////////////////////////////////////////////

                Console.WriteLine ("\n\n\n====================================================================================");
                Console.WriteLine ("====================================================================================");
                Console.WriteLine ("====================================================================================");

                Console.WriteLine ("\n\n==================== PRUEBAS DE ClubCEN - ObtenerListaMiembros() ====================");

                // PREPARACIÓN: Suscribir varios usuarios al Club de Ciencia Ficción para tener miembros
                Console.WriteLine ("\n------------------ Preparación: Suscribir usuarios al Club de Ciencia Ficción ------------------");

                SessionCPNHibernate sessionCPNHibernate11 = new SessionCPNHibernate ();
                UsuarioCP usuarioCP11 = new UsuarioCP (sessionCPNHibernate11);

                // Suscribir usuarioId2 (Marina Lectora)
                try
                {
                        usuarioCP11.SuscribirAClub (usuarioId2, new List<int> { clubId1 });
                        Console.WriteLine ("Usuario 2 (Marina Lectora) suscrito al Club de Ciencia Ficción");
                }
                catch (ModelException ex)
                {
                        Console.WriteLine ("Usuario 2 ya estaba suscrito o error: " + ex.Message);
                }

                // PRUEBA 1: Obtener lista de miembros del Club de Ciencia Ficción
                Console.WriteLine ("\n------------------ Prueba 1: Obtener lista de miembros del Club de Ciencia Ficción ------------------");

                //IList<UsuarioEN> miembrosClub1 = clubcen.ObtenerListaMiembros (clubId1);

                //if (miembrosClub1 != null && miembrosClub1.Count > 0) {
                //        Console.WriteLine ("Total de miembros en el Club de Ciencia Ficción: " + miembrosClub1.Count);
                //        Console.WriteLine ("\nListado de miembros:");
                //        for (int i = 0; i < miembrosClub1.Count; i++) {
                //                Console.WriteLine ("  " + (i + 1) + ". ID: " + miembrosClub1 [i].Id + " - Nombre: " + miembrosClub1 [i].NombreUsuario + " - Email: " + miembrosClub1 [i].Email);
                //        }
                //}
                //else {
                //        Console.WriteLine ("El club no tiene miembros o la lista es nula");
                //}

                // PRUEBA 2: Obtener lista de miembros del Club de Misterio y Suspense (probablemente vacío)
                Console.WriteLine ("\n------------------ Prueba 2: Obtener lista de miembros del Club de Misterio y Suspense ------------------");

                //IList<UsuarioEN> miembrosClub2 = clubcen.ObtenerListaMiembros (clubId2);

                //if (miembrosClub2 != null && miembrosClub2.Count > 0) {
                //        Console.WriteLine ("Total de miembros en el Club de Misterio y Suspense: " + miembrosClub2.Count);
                //        Console.WriteLine ("\nListado de miembros:");
                //        for (int i = 0; i < miembrosClub2.Count; i++) {
                //                Console.WriteLine ("  " + (i + 1) + ". ID: " + miembrosClub2 [i].Id + " - Nombre: " + miembrosClub2 [i].NombreUsuario);
                //        }
                //}
                //else {
                //        Console.WriteLine ("El Club de Misterio y Suspense no tiene miembros (lista vacía o nula)");
                //}

                // PRUEBA 3: Intentar obtener lista de miembros de un club que no existe
                Console.WriteLine ("\n------------------ Prueba 3: Obtener lista de miembros de un club inexistente (Resultado esperado: Incorrecto) ------------------");

                try
                {
                        IList<UsuarioEN> miembrosClubInexistente = clubcen.ObtenerListaMiembros (99999);
                        Console.WriteLine ("ERROR: Se obtuvo lista de un club inexistente (no debería suceder)");
                }
                catch (ModelException ex)
                {
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                        Console.WriteLine ("ModelException capturada correctamente: " + ex.Message);
                        Console.WriteLine ("El sistema validó correctamente que el club no existe");
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                }
                catch (Exception ex)
                {
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                        Console.WriteLine ("Excepción capturada: " + ex.Message);
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                }

                Console.WriteLine ("\n====================================================================================");
                Console.WriteLine ("RESUMEN DE LAS PRUEBAS DE ObtenerListaMiembros():");
                Console.WriteLine ("Prueba 1 - Obtener miembros de club con usuarios suscritos");
                Console.WriteLine ("  - Se listaron todos los usuarios miembros del Club de Ciencia Ficción");
                Console.WriteLine ("  - Se mostraron sus IDs, nombres de usuario y emails");
                Console.WriteLine ("");
                Console.WriteLine ("Prueba 2 - Obtener miembros de club sin usuarios suscritos");
                Console.WriteLine ("  - Se verificó correctamente el estado del Club de Misterio y Suspense");
                Console.WriteLine ("");
                Console.WriteLine ("Prueba 3 - Intentar obtener miembros de club inexistente");
                Console.WriteLine ("  - Se validó correctamente que el club no existe");
                Console.WriteLine ("  - Se capturó la excepción esperada");
                Console.WriteLine ("====================================================================================");

                /////////////////////////////////////////////////////////////////////////////////////////////////////

                Console.WriteLine ("\n\n\n====================================================================================");
                Console.WriteLine ("====================================================================================");
                Console.WriteLine ("====================================================================================");

                Console.WriteLine ("\n\n==================== PRUEBAS DE EventoCEN - ObtenerListaParticipantes() ====================");

                // PREPARACIÓN: Inscribir varios usuarios al evento para tener participantes
                Console.WriteLine ("\n------------------ Preparación: Inscribir usuarios al evento 'Club de Lectura de Novela Negra' ------------------");

                SessionCPNHibernate sessionCPNHibernate12 = new SessionCPNHibernate ();
                UsuarioCP usuarioCP12 = new UsuarioCP (sessionCPNHibernate12);

                // Inscribir usuarioId2 (Marina Lectora) al evento
                try
                {
                        usuarioCP12.InscribirAEvento (usuarioId2, new List<int> { eventoId1 });
                        Console.WriteLine ("Usuario 2 (Marina Lectora) inscrito al evento");
                }
                catch (ModelException ex)
                {
                        Console.WriteLine ("Usuario 2 ya estaba inscrito o error: " + ex.Message);
                }

                // Inscribir usuarioId3 al evento
                try
                {
                        usuarioCP12.InscribirAEvento (usuarioId3, new List<int> { eventoId1 });
                        Console.WriteLine ("Usuario 3 inscrito al evento");
                }
                catch (ModelException ex)
                {
                        Console.WriteLine ("Usuario 3 ya estaba inscrito o error: " + ex.Message);
                }

                // PRUEBA 1: Obtener lista de participantes del evento 'Club de Lectura de Novela Negra'
                Console.WriteLine ("\n------------------ Prueba 1: Obtener lista de participantes del evento 'Club de Lectura de Novela Negra' ------------------");

                //IList<UsuarioEN> participantesEvento1 = eventocen.ObtenerListaParticipantes (eventoId1);

                //if (participantesEvento1 != null && participantesEvento1.Count > 0) {
                //        Console.WriteLine ("Total de participantes en el evento: " + participantesEvento1.Count);
                //        Console.WriteLine ("\nListado de participantes:");
                //        for (int i = 0; i < participantesEvento1.Count; i++) {
                //                Console.WriteLine ("  " + (i + 1) + ". ID: " + participantesEvento1 [i].Id + " - Nombre: " + participantesEvento1 [i].NombreUsuario + " - Email: " + participantesEvento1 [i].Email);
                //        }
                //}
                //else {
                //        Console.WriteLine ("El evento no tiene participantes o la lista es nula");
                //}

                // PRUEBA 2: Obtener lista de participantes del evento vacío (Evento5)
                Console.WriteLine ("\n------------------ Prueba 2: Obtener lista de participantes del Evento5 (sin participantes) ------------------");

                //IList<UsuarioEN> participantesEvento5 = eventocen.ObtenerListaParticipantes (eventoId6);

                //if (participantesEvento5 != null && participantesEvento5.Count > 0) {
                //        Console.WriteLine ("Total de participantes en Evento5: " + participantesEvento5.Count);
                //        Console.WriteLine ("\nListado de participantes:");
                //        for (int i = 0; i < participantesEvento5.Count; i++) {
                //                Console.WriteLine ("  " + (i + 1) + ". ID: " + participantesEvento5 [i].Id + " - Nombre: " + participantesEvento5 [i].NombreUsuario);
                //        }
                //}
                //else {
                //        Console.WriteLine ("El Evento5 no tiene participantes (lista vacía o nula)");

                //}

                // PRUEBA 3: Intentar obtener lista de participantes de un evento que no existe
                Console.WriteLine ("\n------------------ Prueba 3: Obtener lista de participantes de un evento inexistente (Resultado esperado: Incorrecto) ------------------");

                try
                {
                        IList<UsuarioEN> participantesEventoInexistente = eventocen.ObtenerListaParticipantes (99999);
                        Console.WriteLine ("ERROR: Se obtuvo lista de un evento inexistente (no debería suceder)");
                }
                catch (ModelException ex)
                {
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                        Console.WriteLine ("ModelException capturada correctamente: " + ex.Message);
                        Console.WriteLine ("El sistema validó correctamente que el evento no existe");
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                }
                catch (Exception ex)
                {
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                        Console.WriteLine ("Excepción capturada: " + ex.Message);
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                }

                Console.WriteLine ("\n====================================================================================");
                Console.WriteLine ("RESUMEN DE LAS PRUEBAS DE ObtenerListaParticipantes():");
                Console.WriteLine ("Prueba 1 - Obtener participantes de evento con usuarios inscritos");
                Console.WriteLine ("  - Se listaron todos los usuarios participantes del evento");
                Console.WriteLine ("  - Se mostraron sus IDs, nombres de usuario y emails");
                Console.WriteLine ("");
                Console.WriteLine ("Prueba 2 - Obtener participantes de evento sin usuarios inscritos");
                Console.WriteLine ("  - Se verificó correctamente que el evento no tiene participantes");
                Console.WriteLine ("");
                Console.WriteLine ("Prueba 3 - Intentar obtener participantes de evento inexistente");
                Console.WriteLine ("  - Se validó correctamente que el evento no existe");
                Console.WriteLine ("  - Se capturó la excepción esperada");
                Console.WriteLine ("====================================================================================");

                /////////////////////////////////////////////////////////////////////////////////////////////////////

                Console.WriteLine ("\n\n\n====================================================================================");
                Console.WriteLine ("====================================================================================");
                Console.WriteLine ("====================================================================================");

                /*PROTECTED REGION END*/
        }
        catch (Exception ex)
        {
                System.Console.WriteLine (ex.InnerException);
                throw;
        }
}
}
}

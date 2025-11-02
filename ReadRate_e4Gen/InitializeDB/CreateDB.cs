
/*PROTECTED REGION ID(CreateDB_imports) ENABLED START*/
using ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.Exceptions;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.Utils;
using ReadRate_e4Gen.Infraestructure.CP;
using ReadRate_e4Gen.Infraestructure.Repository;
using ReadRate_e4Gen.Infraestructure.Repository.ReadRate_E4;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
                Console.WriteLine ("------------------ Creación de Usuario Lector ------------------");

                int usuarioId1 = lectorcen.CrearLector (
 p_email: "paco.lector@example.com",
 p_nombreUsuario: "PacoLector",
 p_fechaNacimiento: new DateTime (1980, 11, 10),
 p_ciudadResidencia: "Barcelona", p_paisResidencia: "España",
 p_foto: "pacoFoto.png", p_rol: ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.RolUsuarioEnum.lector,
 p_pass: "password123",
                        p_cantLibrosCurso: 0,
                        p_cantLibrosLeidos: 0,
                        p_cantAutoresSeguidos: 0,
                        p_cantClubsSuscritos: 0);
                Console.WriteLine ("Usuario 'PacoLector' creado correctamente.");

                // LOGIN DE USURIO POR DEFECTO - Lector 1
                Console.WriteLine ("------------------ Comprobación de login por defecto ------------------");

                string token1 = usuariocen.Login (usuarioId1, "password123");
                if (!string.IsNullOrEmpty (token1)) {
                        Console.WriteLine ("Login exitoso para el usuario 'PacoLector'. Token: " + token1);
                }
                else{
                        Console.WriteLine ("Error de login para el usuario 'PacoLector'.");
                }

                // LOGIN DE USURIO PERSONALIZADO - Lector 1
                Console.WriteLine ("------------------ Comprobación de login personalizado ------------------");

                if (usuariocen.Login ("paco.lector@example.com", "password123") != null) {
                        Console.WriteLine ("Login exitoso para el usuario 'PacoLector'.");
                }
                else{
                        Console.WriteLine ("Error de login para el usuario 'PacoLector'.");
                }

                ////////////////////////////////////////////////////////////////////////////////////////////////////////

                // CREACIÓN ADMINISTRADOR - Administrador 1
                Console.WriteLine ("------------------ Creación de Administrador ------------------");

                int administradorId1 = administradorcen.CrearAdministador (
 p_nombre: "Admin1",
 p_pass: "adminpass"
                        );
                Console.WriteLine ("Administrador 'Admin' creado correctamente.");

                // LOGIN DE ADMINISTRADOR POR DEFECTO - Administrador 1
                Console.WriteLine ("------------------ Comprobación de login por defecto ------------------");

                string token2 = administradorcen.Login (administradorId1, "adminpass");
                if (!string.IsNullOrEmpty (token2)) {
                        Console.WriteLine ("Login exitoso para el adminsitrador 'Admin'. Token: " + token2);
                }
                else{
                        Console.WriteLine ("Error de login para el usuario 'PacoLector'.");
                }

                // LOGIN DE ADMINISTRADOR PERSONALIZADO - Administrador 1
                Console.WriteLine ("------------------ Comprobación de login personalizado ------------------");

                if (usuariocen.Login ("paco.lector@example.com", "password123") != null) {
                        Console.WriteLine ("Login exitoso para el usuario 'PacoLector'.");
                }
                else{
                        Console.WriteLine ("Error de login para el usuario 'PacoLector'.");
                }

                ////////////////////////////////////////////////////////////////////////////////////////////////////////

                // CREACIÓN USURIOS Y LIBROS - Autores con libros publicados
                Console.WriteLine ("------------------ Creación de libros variados y Autores ------------------");

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
                librocen.CrearLibro (
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
                librocen.CrearLibro (
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
                librocen.CrearLibro (
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
                librocen.CrearLibro (
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
                librocen.CrearLibro (
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
                librocen.CrearLibro (
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
                librocen.CrearLibro (
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
                librocen.CrearLibro (
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
                librocen.CrearLibro (
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
                var idLibro10 =  librocen.CrearLibro (
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

                /////////////////////////////////////////////////////////////////////////////////////////////////////

                // COMPROBACIÓN FILTRO ReadFilter - DameLibrosPorFiltros()
                Console.WriteLine ("------------------ ReadFilter de librosPorFiltro ------------------");

                var librosPorFiltro = librocen.DameLibrosPorFiltros (p_genero: null, p_titulo: null, p_edadRecomendada: null, p_numPags: null, p_valoracionMedia: null, 0, 20);
                Console.WriteLine (librosPorFiltro);

                /////////////////////////////////////////////////////////////////////////////////////////////////////

                // CREACIÓN USURIO - Lector 2 (Para asignarle y desasignarle libros a sus listas de libros en curso / guardados)
                Console.WriteLine ("------------------ Creación de Usuario Lector ------------------");

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

                List<LibroEN> listaEnCursoMarina = new List<LibroEN>();
                LibroEN libroEnCurso = librocen.DameLibroPorOID (idLibro11);

                // COMPROBACIÓN DE SI UN LIBRO ESTÁ EN LA LISTA DE LIBROS PASADOS POR PARÁMETRO
                Console.WriteLine ("------------------ Comprobación de Custom CEN - comprobarSiEstaEnLista() (Esperado: FALSE) ------------------");

                bool resultadoComprobacionEnCurso1 = lectorcen.ComprobarSiEstaEnLista (idLibro11, listaEnCursoMarina); 
                Console.WriteLine ("El resultado es: " + resultadoComprobacionEnCurso1);

                // ASIGNAR LIBRO A LISTA EN CURSO DEL LECTOR 2 - (Caso Correcto - El libro no esta repetido ni en la lista contraria)
                Console.WriteLine ("------------------ Asignar libro a lista en curso del Lector (Esperado: Correcto) ------------------");
                listaEnCursoMarina.Add (libroEnCurso);

                // COMPROBACIÓN DE SI UN LIBRO PASADO POR PARÁMETRO ESTÁ O NO EN LA LISTA PASADA POR PARÁMETRO
                Console.WriteLine ("------------------ Comprobación de Si Libro Está en Lista (Esperado: TRUE) ------------------");

                bool resultadoComprobacionEnCurso2 = lectorcen.ComprobarSiEstaEnLista (idLibro11, listaEnCursoMarina);
                Console.WriteLine ("El resultado es: " + resultadoComprobacionEnCurso2);

                // ASIGNAR LIBRO A LISTA EN CURSO DEL LECTOR 2 - (Caso Inorrecto - El libro ya está en la lista) 
                Console.WriteLine ("------------------ Asignar libro a lista en curso del Lector usando Custom CP (Esperado: Incorrecto) ------------------");
                
                //GenericSessionCP genericSessionCP = new GenericSessionCPImpl();
                SessionCPNHibernate sessionCPNHibernate = new SessionCPNHibernate();
                LectorCP lectorCP = new LectorCP(sessionCPNHibernate);
                
                try
                {
                    lectorCP.AsignarLibroListaEnCurso(usuarioId2, new List<int> { idLibro11 });
                }
                catch (ModelException ex)
                {
                    Console.WriteLine("---------------------------------------------------------------------------------------");
                    Console.WriteLine("Se ha capturado una ModelException al intentar asignar un libro que ya está en la lista");
                    Console.WriteLine("---------------------------------------------------------------------------------------");
                }

                // ASIGNAR OTRO LIBRO DIFERENTE A LISTA EN CURSO DEL LECTOR 2 - (Caso Correcto - El libro no está en la lista)
                lectorCP.AsignarLibroListaEnCurso(usuarioId2, new List<int> { idLibro10 });

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


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

                // Inicializar LibroCP para crear libros
                SessionCPNHibernate sessionCPLibro = new SessionCPNHibernate ();
                LibroCP librocp = new LibroCP (sessionCPLibro);

                // CREACIÓN USURIO - Lector 1
                Console.WriteLine ("\n\n------------------ Creación de Usuario Lector ------------------");

                int usuarioId1 = lectorcen.CrearLector (
 p_email: "paco.lector@email.com",
 p_nombreUsuario: "Paco Lector",
 p_fechaNacimiento: new DateTime (1980, 11, 10),
 p_ciudadResidencia: "Barcelona", p_paisResidencia: "España",
 p_foto: "/images/fotosUsuarios/usuarioDefault.webp",
 p_rol: ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.RolUsuarioEnum.lector,
 p_pass: "passPaco",
                        p_cantLibrosCurso: 0,
                        p_cantLibrosLeidos: 0,
                        p_cantAutoresSeguidos: 0,
                        p_numModificaciones: 0,
                        p_cantClubsSuscritos: 0);
                Console.WriteLine ("Usuario lector 'Paco Lector' creado correctamente.");

                // LOGIN DE USURIO POR DEFECTO - Lector 1
                Console.WriteLine ("\n------------------ Comprobación de login por defecto ------------------");

                string token1 = usuariocen.Login (usuarioId1, "passPaco");
                if (!string.IsNullOrEmpty (token1)) {
                        Console.WriteLine ("Login exitoso para el usuario 'PacoLector'. Token: " + token1);
                }
                else{
                        Console.WriteLine ("Error de login para el usuario 'Paco Lector'.");
                }

                // LOGIN DE USURIO PERSONALIZADO - Lector 1
                Console.WriteLine ("\n------------------ Comprobación de login personalizado ------------------");

                if (usuariocen.Login ("paco.lector@email.com", "passPaco") != null) {
                        Console.WriteLine ("Login exitoso para el usuario 'Paco Lector'.");
                }
                else{
                        Console.WriteLine ("Error de login para el usuario 'Paco Lector'.");
                }

                Console.WriteLine ("\n====================================================================================");
                Console.WriteLine ("RESUMEN - CREACIÓN Y LOGIN DE LECTOR:");
                Console.WriteLine ("- Usuario 'Paco Lector' creado correctamente");
                Console.WriteLine ("- Login por defecto: Exitoso");
                Console.WriteLine ("- Login personalizado: Exitoso");
                Console.WriteLine ("====================================================================================");

                ////////////////////////////////////////////////////////////////////////////////////////////////////////

                // CREACIÓN ADMINISTRADOR - Administrador 1
                Console.WriteLine ("\n\n------------------ Creación de Administrador ------------------");

                int administradorId1 = administradorcen.CrearAdministador (
 p_nombre: "Admin1",
 p_pass: "passAdmin1",
 p_email: "admin@email.com",
 p_foto: "/images/fotosUsuarios/usuarioDefault.webp"
                        );
                Console.WriteLine ("Administrador 'Admin' creado correctamente.");

                // LOGIN DE ADMINISTRADOR POR DEFECTO - Administrador 1
                Console.WriteLine ("\n------------------ Comprobación de login por defecto ------------------");

                string token2 = administradorcen.Login (administradorId1, "passAdmin1");
                if (!string.IsNullOrEmpty (token2)) {
                        Console.WriteLine ("Login exitoso para el adminsitrador 'Admin'. Token: " + token2);
                }
                else{
                        Console.WriteLine ("Error de login para el adminsitrador 'Admin'.");
                }

                // LOGIN DE ADMINISTRADOR PERSONALIZADO - Administrador 1 (Credenciales incorrectas)
                Console.WriteLine ("\n------------------ Comprobación de login personalizado (Esperado: Mensaje de error) ------------------");

                if (administradorcen.Login ("paco.lector@email.com", "passAdmin1") != null) {
                        Console.WriteLine ("Login exitoso para el usuario 'Admin1'.");
                }
                else{
                        Console.WriteLine ("Error de login para el usuario 'Admin1'.");
                }

                // LOGIN DE ADMINISTRADOR PERSONALIZADO - Administrador 1 (Credenciales correctas)
                Console.WriteLine ("\n------------------ Comprobación de login personalizado (Esperado: Login Correcto) ------------------");

                if (administradorcen.Login ("admin@email.com", "passAdmin1") != null) {
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
 p_email: "leighbardugo@email.com",
 p_nombreUsuario: "Leigh Bardugo",
 p_fechaNacimiento: new DateTime (1975, 4, 6),
 p_ciudadResidencia: "Jerusalén",
 p_paisResidencia: "Israel/EE.UU.",
 p_foto: "/images/fotosUsuarios/leighBardugo.webp",
 p_valoracionMedia: 5.0f,
 p_rol: ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.RolUsuarioEnum.autor,
 p_pass: "passLeigh",
                        p_numModificaciones: 0,
                        p_numeroSeguidores: 0,
                        p_cantidadLibrosPublicados: 0
                        );
                Console.WriteLine ("Autora creada correctamente con ID: " + autorId);

                // Libro 1 (usa el id del autor anterior)
                var libro1 = librocp.CrearLibro (
 p_titulo: "Seis de Cuervos",
 p_genero: "Fantasía",
 p_valoracionMedia: 5.0f,
                        p_edadRecomendada: 14,
 p_fechaPublicacion: new DateTime (2015, 9, 29),
                        p_numPags: 480,
 p_sinopsis: "Seis peligrosos marginados deben unir fuerzas para realizar un robo imposible en el mundo del Grishaverse.",
 p_fotoPortada: "/images/portadasLibros/seisDeCuervos.webp",
 p_autorPublicador: autorId
                        );
                var idLibro1 = libro1.Id;
                Console.WriteLine ("Libro 'Seis de Cuervos' creado correctamente.");

                // Autor 2 (devuelve su id para ser usado a continuación en la creación del libro)
                int autorId2 = autorcen.CrearAutor (
 p_email: "robertjordan@email.com",
                        p_numModificaciones: 0,
 p_nombreUsuario: "Robert Jordan",
 p_valoracionMedia: 4.5f,
 p_fechaNacimiento: new DateTime (1948, 10, 17),
 p_ciudadResidencia: "Charleston",
 p_paisResidencia: "Estados Unidos",
 p_foto: "/images/fotosUsuarios/robertJordan.webp",
 p_rol: ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.RolUsuarioEnum.autor,
 p_pass: "passRobert",
                        p_numeroSeguidores: 0,
                        p_cantidadLibrosPublicados: 0
                        );
                Console.WriteLine ("Autor creado correctamente con ID: " + autorId2);

                // Libro 2 (usa el id del autor anterior)
                var libro2 = librocp.CrearLibro (
 p_titulo: "La Rueda Del Tiempo 1: El Ojo del Mundo",
 p_genero: "Fantasía",
 p_valoracionMedia: 3.5f,
                        p_edadRecomendada: 15,
 p_fechaPublicacion: new DateTime (1990, 1, 15),
                        p_numPags: 814,
 p_sinopsis: "Un grupo de jóvenes se ve arrastrado a un viaje épico donde el destino del mundo depende de ellos, dando inicio a la monumental saga de La Rueda del Tiempo.",
 p_fotoPortada: "/images/portadasLibros/ruedaDelTiempo1.webp",
 p_autorPublicador: autorId2
                        );
                var idLibro2 = libro2.Id;
                Console.WriteLine ("Libro 'El Ojo del Mundo' creado correctamente.");

                // Libro 2_2 (usa el id del autor anterior)
                var libro2_2 = librocp.CrearLibro (
 p_titulo: "La Rueda Del Tiempo 2: La Gran Cacería",
 p_genero: "Fantasía",
 p_valoracionMedia: 3.5f,
                        p_edadRecomendada: 15,
 p_fechaPublicacion: new DateTime (1990, 11, 15),
                        p_numPags: 705,
 p_sinopsis: "Los jóvenes héroes comienzan una búsqueda épica del mítico cuerno de Valere. Mientras tanto, fuerzas oscuras se movilizan, y la guerra por el destino del mundo comienza a despertar.",
 p_fotoPortada: "/images/portadasLibros/ruedaDelTiempo2.webp",
 p_autorPublicador: autorId2
                        );
                var idLibro2_2 = libro2_2.Id;
                Console.WriteLine ("Libro 'La Gran Cacería' creado correctamente.");

                // Libro 2_3 (usa el id del autor anterior)
                var libro2_3 = librocp.CrearLibro (
 p_titulo: "La Rueda Del Tiempo 3: El Dragón Renacido",
 p_genero: "Fantasía",
 p_valoracionMedia: 3.5f,
                        p_edadRecomendada: 15,
 p_fechaPublicacion: new DateTime (1991, 10, 5),
                        p_numPags: 624,
 p_sinopsis: "Rand al’Thor asume su papel como el “Dragón Renacido”, lo cual lo convierte en pieza clave del destino del mundo; debe aceptar su poder, pese al miedo y al peligro que ello trae.",
 p_fotoPortada: "/images/portadasLibros/ruedaDelTiempo3.webp",
 p_autorPublicador: autorId2
                        );
                var idLibro2_3 = libro2_3.Id;
                Console.WriteLine ("Libro 'El Dragón Renacido' creado correctamente.");

                // Libro 2_4 (usa el id del autor anterior)
                var libro2_4 = librocp.CrearLibro (
 p_titulo: "La Rueda Del Tiempo 4: El Ascenso de la Sombra",
 p_genero: "Fantasía",
 p_valoracionMedia: 3.5f,
                        p_edadRecomendada: 15,
 p_fechaPublicacion: new DateTime (1992, 9, 27),
                        p_numPags: 1007,
 p_sinopsis: "Las sombras y los antiguos enemigos resurgen con fuerza. Los protagonistas enfrentan nuevas amenazas internas y externas, y el equilibrio del mundo empieza a pender de un hilo.",
 p_fotoPortada: "/images/portadasLibros/ruedaDelTiempo4.webp",
 p_autorPublicador: autorId2
                        );
                var idLibro2_4 = libro2_4.Id;
                Console.WriteLine ("Libro 'El Ascenso de la Sombra' creado correctamente.");

                // Libro 2_5 (usa el id del autor anterior)
                var libro2_5 = librocp.CrearLibro (
 p_titulo: "La Rueda Del Tiempo 5: Cielo en Llamas",
 p_genero: "Fantasía",
 p_valoracionMedia: 3.5f,
                        p_edadRecomendada: 15,
 p_fechaPublicacion: new DateTime (1993, 11, 15),
                        p_numPags: 989,
 p_sinopsis: "Las tensiones aumentan: conflictos políticos, viejos enemigos y magias oscuras complican aún más el destino del mundo. Las fuerzas del bien y del mal se preparan para una confrontación épica.",
 p_fotoPortada: "/images/portadasLibros/ruedaDelTiempo5.webp",
 p_autorPublicador: autorId2
                        );
                var idLibro2_5 = libro2_5.Id;
                Console.WriteLine ("Libro 'Cielo en Llamas' creado correctamente.");

                // Libro 2_6 (usa el id del autor anterior)
                var libro2_6 = librocp.CrearLibro (
 p_titulo: "La Rueda Del Tiempo 6: El Señor del Caos",
 p_genero: "Fantasía",
 p_valoracionMedia: 3.5f,
                        p_edadRecomendada: 15,
 p_fechaPublicacion: new DateTime (1994, 6, 13),
                        p_numPags: 1011,
 p_sinopsis: "Con el caos extendiéndose, los héroes deben lidiar con traiciones, guerras internas y la inseguridad de si podrán resistir ante un mal creciente. La lucha por el poder y el orden se intensifica.",
 p_fotoPortada: "/images/portadasLibros/ruedaDelTiempo6.webp",
 p_autorPublicador: autorId2
                        );
                var idLibro2_6 = libro2_6.Id;
                Console.WriteLine ("Libro 'El Señor del Caos' creado correctamente.");

                // Libro 2_7 (usa el id del autor anterior)
                var libro2_7 = librocp.CrearLibro (
 p_titulo: "La Rueda Del Tiempo 7: Corona de Espadas",
 p_genero: "Fantasía",
 p_valoracionMedia: 3.5f,
                        p_edadRecomendada: 15,
 p_fechaPublicacion: new DateTime (1996, 5, 3),
                        p_numPags: 880,
 p_sinopsis: "Rand, como Dragón Renacido, debe enfrentarse a enemigos tanto abiertos como ocultos. Mientras tanto, intrigas políticas y poderes mágicos se entrecruzan.",
 p_fotoPortada: "/images/portadasLibros/ruedaDelTiempo7.webp",
 p_autorPublicador: autorId2
                        );
                var idLibro2_7 = libro2_7.Id;
                Console.WriteLine ("Libro 'Corona de Espadas' creado correctamente.");

                // Libro 2_8 (usa el id del autor anterior)
                var libro2_8 = librocp.CrearLibro (
 p_titulo: "La Rueda Del Tiempo 8: El Camino de Dagas",
 p_genero: "Fantasía",
 p_valoracionMedia: 3.5f,
                        p_edadRecomendada: 15,
 p_fechaPublicacion: new DateTime (1998, 7, 15),
                        p_numPags: 672,
 p_sinopsis: "El conflicto se intensifica: invasiones, engaños y manipulaciones mágicas amenazan la estabilidad política y social del mundo. Los protagonistas deben tomar decisiones difíciles.",
 p_fotoPortada: "/images/portadasLibros/ruedaDelTiempo8.webp",
 p_autorPublicador: autorId2
                        );
                var idLibro2_8 = libro2_8.Id;
                Console.WriteLine ("Libro 'El Camino de Dagas' creado correctamente.");

                // Libro 2_9 (usa el id del autor anterior)
                var libro2_9 = librocp.CrearLibro (
 p_titulo: "La Rueda Del Tiempo 9: El Corazón del Invierno",
 p_genero: "Fantasía",
 p_valoracionMedia: 3.5f,
                        p_edadRecomendada: 15,
 p_fechaPublicacion: new DateTime (2000, 8, 11),
                        p_numPags: 668,
 p_sinopsis: "A medida que las fuerzas del mal buscan destruir todo, los héroes deben afrontar sus miedos internos y externos. El clima político, mágico y social se vuelve más inestable.",
 p_fotoPortada: "/images/portadasLibros/ruedaDelTiempo9.webp",
 p_autorPublicador: autorId2
                        );
                var idLibro2_9 = libro2_9.Id;
                Console.WriteLine ("Libro 'El Corazón del Invierno' creado correctamente.");

                // Libro 2_10 (usa el id del autor anterior)
                var libro2_10 = librocp.CrearLibro (
 p_titulo: "La Rueda Del Tiempo 10: Encrucijada en el Crepúsculo",
 p_genero: "Fantasía",
 p_valoracionMedia: 3.5f,
                        p_edadRecomendada: 15,
 p_fechaPublicacion: new DateTime (2003, 1, 15),
                        p_numPags: 832,
 p_sinopsis: "Las consecuencias de decisiones anteriores empiezan a notarse: traiciones, luchas de poder, tensiones crecientes. El mundo se prepara para lo inevitable: una confrontación final.",
 p_fotoPortada: "/images/portadasLibros/ruedaDelTiempo10.webp",
 p_autorPublicador: autorId2
                        );
                var idLibro2_10 = libro2_10.Id;
                Console.WriteLine ("Libro 'Encrucijada en el Crepúsculo' creado correctamente.");

                // Libro 2_11 (usa el id del autor anterior)
                var libro2_11 = librocp.CrearLibro (
 p_titulo: "La Rueda Del Tiempo 11: Cuchillo de Sueños",
 p_genero: "Fantasía",
 p_valoracionMedia: 3.5f,
                        p_edadRecomendada: 15,
 p_fechaPublicacion: new DateTime (2005, 6, 7),
                        p_numPags: 837,
 p_sinopsis: "La saga se acerca a su clímax: amenazas mayores emergen, y los protagonistas deben unir fuerzas. Se revelan secretos cruciales y alianzas impensadas.",
 p_fotoPortada: "/images/portadasLibros/ruedaDelTiempo11.webp",
 p_autorPublicador: autorId2
                        );
                var idLibro2_11 = libro2_11.Id;
                Console.WriteLine ("Libro 'Cuchillo de Sueños' creado correctamente.");

                // Libro 2_12 (usa el id del autor anterior)
                var libro2_12 = librocp.CrearLibro (
 p_titulo: "La Rueda Del Tiempo 12: La Tormenta",
 p_genero: "Fantasía",
 p_valoracionMedia: 3.5f,
                        p_edadRecomendada: 15,
 p_fechaPublicacion: new DateTime (2009, 1, 10),
                        p_numPags: 783,
 p_sinopsis: "El mundo se rompe: el protagonista lucha con su destino, la corrupción del poder y la inminente guerra final se aproxima.",
 p_fotoPortada: "/images/portadasLibros/ruedaDelTiempo12.webp",
 p_autorPublicador: autorId2
                        );
                var idLibro2_12 = libro2_12.Id;
                Console.WriteLine ("Libro 'La Tormenta' creado correctamente.");

                // Libro 2_13 (usa el id del autor anterior)
                var libro2_13 = librocp.CrearLibro (
 p_titulo: "La Rueda Del Tiempo 13: Torres de Medianoche",
 p_genero: "Fantasía",
 p_valoracionMedia: 3.5f,
                        p_edadRecomendada: 15,
 p_fechaPublicacion: new DateTime (2010, 2, 12),
                        p_numPags: 861,
 p_sinopsis: "Todos los hilos de la trama convergen: conspiraciones, poderes antiguos, alianzas decisivas. El mundo entero se prepara para la batalla final contra la Sombra.",
 p_fotoPortada: "/images/portadasLibros/ruedaDelTiempo13.webp",
 p_autorPublicador: autorId2
                        );
                var idLibro2_13 = libro2_13.Id;
                Console.WriteLine ("Libro 'Torres de Medianoche' creado correctamente.");

                // Libro 2_14 (usa el id del autor anterior)
                var libro2_14 = librocp.CrearLibro (
 p_titulo: "La Rueda Del Tiempo 14: Un Recuerdo de Luz",
 p_genero: "Fantasía",
 p_valoracionMedia: 3.5f,
                        p_edadRecomendada: 15,
 p_fechaPublicacion: new DateTime (2013, 11, 19),
                        p_numPags: 912,
 p_sinopsis: "Las fuerzas de la Luz y la Sombra chocan en la legendaria batalla final. Todos los destinos se definen: salvación o destrucción para la humanidad.",
 p_fotoPortada: "/images/portadasLibros/ruedaDelTiempo14.webp",
 p_autorPublicador: autorId2
                        );
                var idLibro2_14 = libro2_14.Id;
                Console.WriteLine ("Libro 'Un Recuerdo de Luz' creado correctamente.");

                // Autor 3 (devuelve su id para ser usado a continuación en la creación del libro)
                int autorId3 = autorcen.CrearAutor (
 p_email: "brandonsanderson@email.com",
 p_nombreUsuario: "Brandon Sanderson",
 p_fechaNacimiento: new DateTime (1975, 12, 19),
 p_ciudadResidencia: "Lincoln",
 p_paisResidencia: "Estados Unidos",
 p_valoracionMedia: 4.5f,
 p_foto: "/images/fotosUsuarios/brandonSanderson.webp",
 p_rol: ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.RolUsuarioEnum.autor,
 p_pass: "passBrandon",
                        p_numeroSeguidores: 0,
                        p_numModificaciones: 0,
                        p_cantidadLibrosPublicados: 0
                        );
                Console.WriteLine ("Autor creado correctamente con ID: " + autorId3);

                // Libro 3 (usa el id del autor anterior)
                var libro3 = librocp.CrearLibro (
 p_titulo: "Mistborn 1: El Imperio Final",
 p_genero: "Fantasía",
                        p_edadRecomendada: 15,
 p_fechaPublicacion: new DateTime (2006, 7, 17),
                        p_numPags: 672,
 p_sinopsis: "En un mundo dominado por cenizas y brumas, una joven ladrona descubre que posee un poder único que podría cambiar el destino del Imperio Final gobernado por el tiránico Lord Legislador.",
 p_fotoPortada: "/images/portadasLibros/imperioFinal.webp",
 p_valoracionMedia: 5.0f,
 p_autorPublicador: autorId3
                        );
                var idLibro3 = libro3.Id;
                Console.WriteLine ("Libro 'El Imperio Final' creado correctamente.");

                // Libro 3.2 (usa el id del autor anterior)
                var libro3_2 = librocp.CrearLibro (
 p_titulo: "Mistborn 2: El Pozo de la Ascensión",
 p_genero: "Fantasía",
                        p_edadRecomendada: 15,
 p_fechaPublicacion: new DateTime (2007, 8, 21),
                        p_numPags: 784,
 p_sinopsis: "Vin y sus aliados deben enfrentar el caos de un Imperio en ruinas. Mientras las fuerzas externas amenazan la ciudad y entidades misteriosas manipulan los acontecimientos, Vin siente la llamada de un antiguo poder.",
 p_fotoPortada: "/images/portadasLibros/pozoAscension.webp",
 p_valoracionMedia: 5.0f,
 p_autorPublicador: autorId3
                        );
                var idLibro3_2 = libro3_2.Id;
                Console.WriteLine ("Libro 'El Pozo de la Ascensión' creado correctamente.");

                // Libro 3.3 (usa el id del autor anterior)
                var libro3_3 = librocp.CrearLibro (
 p_titulo: "Mistborn 3: El Héroe de las Eras",
 p_genero: "Fantasía",
                        p_edadRecomendada: 15,
 p_fechaPublicacion: new DateTime (2008, 9, 23),
                        p_numPags: 848,
 p_sinopsis: "Con el mundo al borde de la destrucción, Vin y Elend luchan por descubrir la verdad detrás del legendario Héroe de las Eras. Solo entendiendo los secretos perdidos de la alomancia podrán enfrentar a una amenaza que lleva mil años preparándose.",
 p_fotoPortada: "/images/portadasLibros/heroeEras.webp",
 p_valoracionMedia: 5.0f,
 p_autorPublicador: autorId3
                        );
                var idLibro3_3 = libro3_3.Id;
                Console.WriteLine ("Libro 'El Héroe de las Eras' creado correctamente.");

                // Autor 4 (devuelve su id para ser usado a continuación en la creación del libro)
                int autorId4 = autorcen.CrearAutor (
 p_email: "cervantes@email.com",
 p_nombreUsuario: "Miguel De Cervantes",
                        p_numModificaciones: 0,
 p_fechaNacimiento: new DateTime (1753, 9, 29),
 p_ciudadResidencia: "Alcalá de Henares",
 p_valoracionMedia: 3.0f,
 p_paisResidencia: "España",
 p_foto: "/images/fotosUsuarios/cervantes.webp",
 p_rol: ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.RolUsuarioEnum.autor,
 p_pass: "passCervantes",
                        p_numeroSeguidores: 0,
                        p_cantidadLibrosPublicados: 0
                        );
                Console.WriteLine ("Autor creado correctamente con ID: " + autorId4);

                // Libro 4 (usa el id del autor anterior)
                var libro4 = librocp.CrearLibro (
 p_titulo: "Don Quijote de la Mancha",
 p_genero: "Literatura Clásica",
 p_valoracionMedia: 4.0f,
                        p_edadRecomendada: 14,
 p_fechaPublicacion: new DateTime (1753, 1, 16),
                        p_numPags: 863,
 p_sinopsis: "La historia de un caballero idealista que, acompañado de su fiel escudero Sancho Panza, lucha contra la realidad misma impulsado por sus sueños de justicia y valentía.",
 p_fotoPortada: "/images/portadasLibros/donQuijote.webp",
 p_autorPublicador: autorId4
                        );
                var idLibro4 = libro4.Id;
                Console.WriteLine ("Libro 'Don Quijote de la Mancha' creado correctamente.");

                // Autor 5 (devuelve su id para ser usado a continuación en la creación del libro)
                int autorId5 = autorcen.CrearAutor (
 p_email: "lucymontgomery@email.com",
 p_nombreUsuario: "Lucy Maud Montgomery",
 p_fechaNacimiento: new DateTime (1874, 11, 30),
 p_ciudadResidencia: "Clifton (actual New London), Isla del Príncipe Eduardo",
                        p_numModificaciones: 0,
 p_paisResidencia: "Canadá",
 p_valoracionMedia: 4.0f,
 p_foto: "/images/fotosUsuarios/lucyMontgomery.webp",
 p_rol: ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.RolUsuarioEnum.autor,
 p_pass: "passLucy",
                        p_numeroSeguidores: 0,
                        p_cantidadLibrosPublicados: 0
                        );
                Console.WriteLine ("Autora creada correctamente con ID: " + autorId5);

                // Libro 5.1 (usa el id del autor anterior)
                var libro5 = librocp.CrearLibro (
 p_titulo: "Ana de las Tejas Verdes",
 p_genero: "Literatura Infantil",
 p_valoracionMedia: 3.0f,
                        p_edadRecomendada: 10,
 p_fechaPublicacion: new DateTime (1908, 6, 13),
                        p_numPags: 320,
 p_sinopsis: "La historia de Anne Shirley, una huérfana soñadora que llega por accidente a la casa de los hermanos Cuthbert en Tejas Verdes, donde transformará la vida de todos con su imaginación y corazón.",
 p_fotoPortada: "/images/portadasLibros/anaTejasVerdes1.webp",
 p_autorPublicador: autorId5
                        );
                var idLibro5 = libro5.Id;
                Console.WriteLine ("Libro 'Ana de las Tejas Verdes' creado correctamente.");

                // Libro 5.2 (usa el id del autor anterior)
                var libro5_2 = librocp.CrearLibro (
 p_titulo: "Ana de Avonlea",
 p_genero: "Literatura Infantil",
 p_valoracionMedia: 3.0f,
                        p_edadRecomendada: 10,
 p_fechaPublicacion: new DateTime (1909, 8, 22),
                        p_numPags: 350,
 p_sinopsis: "Anne comienza su vida adulta como maestra en Avonlea, donde deberá lidiar con alumnos difíciles, nuevos vecinos y responsabilidades que pondrán a prueba su madurez.",
 p_fotoPortada: "/images/portadasLibros/anaTejasVerdes2.webp",
 p_autorPublicador: autorId5
                        );
                var idLibro5_2 = libro5_2.Id;
                Console.WriteLine ("Libro 'Ana de Avonlea' creado correctamente.");

                // Libro 5.3 (usa el id del autor anterior)
                var libro5_3 = librocp.CrearLibro (
 p_titulo: "Ana de la Isla",
 p_genero: "Literatura Infantil",
 p_valoracionMedia: 3.0f,
                        p_edadRecomendada: 10,
 p_fechaPublicacion: new DateTime (1915, 7, 9),
                        p_numPags: 310,
 p_sinopsis: "Anne viaja a Redmond para estudiar en la universidad. Nuevas amistades, desafíos y sentimientos la acompañan en su camino hacia la adultez.",
 p_fotoPortada: "/images/portadasLibros/anaTejasVerdes3.webp",
 p_autorPublicador: autorId5
                        );
                var idLibro5_3 = libro5_3.Id;
                Console.WriteLine ("Libro 'Ana de la Isla' creado correctamente.");

                // Libro 5.4 (usa el id del autor anterior)
                var libro5_4 = librocp.CrearLibro (
 p_titulo: "Ana de Álamos Ventosos",
 p_genero: "Literatura Infantil",
 p_valoracionMedia: 3.0f,
                        p_edadRecomendada: 10,
 p_fechaPublicacion: new DateTime (1916, 8, 6),
                        p_numPags: 300,
 p_sinopsis: "Ahora directora de una escuela en Summerside, Anne escribe cartas a Gilbert en las que narra sus experiencias, retos y personajes peculiares del lugar.",
 p_fotoPortada: "/images/portadasLibros/anaTejasVerdes4.webp",
 p_autorPublicador: autorId5
                        );
                var idLibro5_4 = libro5_4.Id;
                Console.WriteLine ("Libro 'Ana de Álamos Ventosos' creado correctamente.");

                // Libro 5.5 (usa el id del autor anterior)
                var libro5_5 = librocp.CrearLibro (
 p_titulo: "Ana y la Casa de sus Sueños",
 p_genero: "Literatura Infantil",
 p_valoracionMedia: 3.0f,
                        p_edadRecomendada: 10,
 p_fechaPublicacion: new DateTime (1917, 9, 13),
                        p_numPags: 290,
 p_sinopsis: "Anne y Gilbert se casan e inician su vida juntos en la Casa de los Sueños, donde forjan amistades entrañables y enfrentan alegrías y penas.",
 p_fotoPortada: "/images/portadasLibros/anaTejasVerdes5.webp",
 p_autorPublicador: autorId5
                        );
                var idLibro5_5 = libro5_5.Id;
                Console.WriteLine ("Libro 'Ana y la Casa de sus Sueños' creado correctamente.");

                // Libro 5.6 (usa el id del autor anterior)
                var libro5_6 = librocp.CrearLibro (
 p_titulo: "Ana de Ingleside",
 p_genero: "Literatura Infantil",
 p_valoracionMedia: 3.0f,
                        p_edadRecomendada: 10,
 p_fechaPublicacion: new DateTime (1919, 7, 21),
                        p_numPags: 430,
 p_sinopsis: "Anne, ya madre de seis hijos, vive nuevas aventuras familiares mientras descubre que la vida adulta puede ser tan maravillosa como desafiante.",
 p_fotoPortada: "/images/portadasLibros/anaTejasVerdes6.webp",
 p_autorPublicador: autorId5
                        );
                var idLibro5_6 = libro5_6.Id;
                Console.WriteLine ("Libro 'Ana de Ingleside' creado correctamente.");

                // Auotor 6 (devuelve su id para ser usado a continuación en la creación del libro)
                int autorId6 = autorcen.CrearAutor (
 p_email: "ggmarquez@email.com",
 p_nombreUsuario: "Gabriel Garcia Marquez",
 p_fechaNacimiento: new DateTime (1927, 3, 6),
 p_ciudadResidencia: "Aracataca",
                        p_numModificaciones: 0,
 p_paisResidencia: "Colombia",
 p_valoracionMedia: 4.0f,
 p_foto: "/images/fotosUsuarios/garciaMarquez.webp",
 p_rol: ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.RolUsuarioEnum.autor,
 p_pass: "passGabriel",
                        p_numeroSeguidores: 0,
                        p_cantidadLibrosPublicados: 0
                        );
                Console.WriteLine ("Autor creado correctamente con ID: " + autorId6);

                // Libro 6 (usa el id del autor anterior)
                var libro6 = librocp.CrearLibro (
 p_titulo: "Cien años de soledad",
 p_genero: "Literatura Clásica",
 p_valoracionMedia: 4.0f,
                        p_edadRecomendada: 16,
 p_fechaPublicacion: new DateTime (1967, 5, 30),
                        p_numPags: 471,
 p_sinopsis: "La historia mítica de la familia Buendía en el pueblo de Macondo, donde lo extraordinario convive con lo cotidiano en una obra maestra del realismo mágico.",
 p_fotoPortada: "/images/portadasLibros/cienAnosSoledad.webp",
 p_autorPublicador: autorId6
                        );
                var idLibro6 = libro6.Id;
                Console.WriteLine ("Libro 'Cien años de soledad' creado correctamente.");

                // Autor 7 (devuelve su id para ser usado a continuación en la creación del libro)
                int autorId7 = autorcen.CrearAutor (
 p_email: "danbrown@email.com",
 p_nombreUsuario: "Dan Brown",
 p_fechaNacimiento: new DateTime (1964, 6, 22),
 p_ciudadResidencia: "New Hampshire",
 p_valoracionMedia: 4.5f,
 p_paisResidencia: "Estados Unidos",
                        p_numModificaciones: 0,
 p_foto: "/images/fotosUsuarios/danBrown.webp",
 p_rol: ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.RolUsuarioEnum.autor,
 p_pass: "passDan",
                        p_numeroSeguidores: 0,
                        p_cantidadLibrosPublicados: 0
                        );
                Console.WriteLine ("Autor creado correctamente con ID: " + autorId7);

                // Libro 7 (usa el id del autor anterior)
                var libro7 = librocp.CrearLibro (
 p_titulo: "El Código Da Vinci",
 p_genero: "Misterio",
 p_valoracionMedia: 2.0f,
                        p_edadRecomendada: 16,
 p_fechaPublicacion: new DateTime (2003, 3, 18),
                        p_numPags: 592,
 p_sinopsis: "Tras un misterioso asesinato en el Museo del Louvre, el profesor Robert Langdon descubre una serie de pistas ocultas en obras de arte que lo conducen a un secreto guardado durante siglos.",
 p_fotoPortada: "/images/portadasLibros/codigoDaVinci.webp",
 p_autorPublicador: autorId7
                        );
                var idLibro7 = libro7.Id;
                Console.WriteLine ("Libro 'El Código Da Vinci' creado correctamente.");

                // Autor 8 (devuelve su id para ser usado a continuación en la creación del libro)
                int autorId8 = autorcen.CrearAutor (
 p_email: "julioverne@email.com",
 p_nombreUsuario: "Julio Verne",
 p_fechaNacimiento: new DateTime (1828, 2, 8),
 p_ciudadResidencia: "Nantes",
 p_paisResidencia: "Francia",
                        p_numModificaciones: 0,
 p_valoracionMedia: 3.0f,
 p_foto: "/images/fotosUsuarios/julioVerne.webp",
 p_rol: ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.RolUsuarioEnum.autor,
 p_pass: "passJulio",
                        p_numeroSeguidores: 0,
                        p_cantidadLibrosPublicados: 0
                        );
                Console.WriteLine ("Autor creado correctamente con ID: " + autorId8);

                // Libro 8 (usa el id del autor anterior)
                var libro8 = librocp.CrearLibro (
 p_titulo: "Viaje al centro de la Tierra",
 p_genero: "Aventura",
 p_valoracionMedia: 2.5f,
                        p_edadRecomendada: 12,
 p_fechaPublicacion: new DateTime (1864, 11, 25),
                        p_numPags: 300,
 p_sinopsis: "El profesor Otto Lidenbrock descubre un manuscrito que revela un camino hacia las profundidades de la Tierra, iniciando una extraordinaria expedición al mundo subterráneo lleno de criaturas y paisajes sorprendentes.",
 p_fotoPortada: "/images/portadasLibros/viajeCentroTierra.webp",
 p_autorPublicador: autorId8
                        );
                var idLibro8 = libro8.Id;
                Console.WriteLine ("Libro 'Viaje al centro de la Tierra' creado correctamente.");

                // Autor 9 (devuelve su id para ser usado a continuación en la creación del libro)
                int autorId9 = autorcen.CrearAutor (
 p_email: "saintexupery@email.com",
 p_nombreUsuario: "Antoine Saint Exupery",
 p_fechaNacimiento: new DateTime (1900, 6, 29),
 p_ciudadResidencia: "Lyon",
 p_paisResidencia: "Francia",
                        p_numModificaciones: 0,
 p_valoracionMedia: 4.0f,
 p_foto: "/images/fotosUsuarios/saintExupery.webp",
 p_rol: ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.RolUsuarioEnum.autor,
 p_pass: "passAntoine",
                        p_numeroSeguidores: 0,
                        p_cantidadLibrosPublicados: 0
                        );
                Console.WriteLine ("Autor creado correctamente con ID: " + autorId9);

                // Libro 9 (usa el id del autor anterior)
                var libro9 = librocp.CrearLibro (
 p_titulo: "El Principito",
 p_genero: "Literatura Infantil",
 p_valoracionMedia: 5.0f,
                        p_edadRecomendada: 5,
 p_fechaPublicacion: new DateTime (1943, 4, 6),
                        p_numPags: 96,
 p_sinopsis: "Un joven príncipe de otro planeta viaja por el universo, aprendiendo sobre la amistad, el amor y la naturaleza humana, en una historia llena de ternura y reflexión.",
 p_fotoPortada: "/images/portadasLibros/elPrincipito.webp",
 p_autorPublicador: autorId9
                        );
                var idLibro9 = libro9.Id;
                Console.WriteLine ("Libro 'El Principito' creado correctamente.");

                // Auotor 10 (devuelve su id para ser usado a continuación en la creación del libro)
                int autorId10 = autorcen.CrearAutor (
 p_email: "carlosruizzafon@email.com",
 p_nombreUsuario: "Carlos Ruiz Zafón",
 p_fechaNacimiento: new DateTime (1964, 9, 25),
 p_ciudadResidencia: "Barcelona",
 p_paisResidencia: "España",
                        p_numModificaciones: 0,
 p_valoracionMedia: 4.5f,
 p_foto: "/images/fotosUsuarios/carlosRuizZafon.webp",
 p_rol: ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.RolUsuarioEnum.autor,
 p_pass: "passCarlos",
                        p_numeroSeguidores: 0,
                        p_cantidadLibrosPublicados: 0
                        );
                Console.WriteLine ("Autor creado correctamente con ID: " + autorId10);

                // Libro 10 (usa el id del autor anterior)
                var libro10 = librocp.CrearLibro (
 p_titulo: "La Sombra del Viento (Antes de Modificar)",
 p_genero: "Aventura",
 p_valoracionMedia: 4.0f,
                        p_edadRecomendada: 20,
 p_fechaPublicacion: new DateTime (2001, 4, 12),
                        p_numPags: 565,
 p_sinopsis: "En la Barcelona de la posguerra, Daniel Sempere descubre un libro olvidado que lo introduce en un misterio literario que cambiará su vida y desvelará secretos del pasado.",
 p_fotoPortada: "/images/portadasLibros/sombraDelViento.webp",
 p_autorPublicador: autorId10
                        );
                var idLibro10 = libro10.Id;
                Console.WriteLine ("Libro 'La Sombra del Viento' creado correctamente.");

                // Libro 11 (usa el mismo id que Libro 10, es decir, que el autor tiene 2 libros)
                var libro11 = librocp.CrearLibro (
 p_titulo: "El Juego del Ángel",
 p_genero: "Misterio",
                        p_edadRecomendada: 15,
 p_fechaPublicacion: new DateTime (2008, 9, 17),
                        p_numPags: 672,
 p_sinopsis: "David Martín, un joven escritor en la Barcelona de los años 20, recibe la oferta de un misterioso editor que lo llevará a descubrir secretos oscuros y peligrosos mientras escribe un libro que cambiará su destino.",
 p_fotoPortada: "/images/portadasLibros/juegoDelAngel.webp",
 p_valoracionMedia: 3.5f,
 p_autorPublicador: autorId10
                        );
                var idLibro11 = libro11.Id;
                Console.WriteLine ("Libro 'El Juego del Ángel' creado correctamente.");

                // Libro 11_2 (usa el mismo id que Libro 10, es decir, que el autor tiene 2 libros)
                var libro11_2 = librocp.CrearLibro (
 p_titulo: "El Prisionero del Cielo",
 p_genero: "Misterio",
                        p_edadRecomendada: 15,
 p_fechaPublicacion: new DateTime (2009, 3, 12),
                        p_numPags: 384,
 p_sinopsis: "Cuando un misterioso visitante llega a la librería Sempere & Hijos, un oscuro secreto del pasado de Fermín Romero de Torres sale a la luz, conectando los hechos de los libros anteriores.",
 p_fotoPortada: "/images/portadasLibros/prisioneroDelCielo.webp",
 p_valoracionMedia: 3.5f,
 p_autorPublicador: autorId10
                        );
                var idLibro11_2 = libro11_2.Id;
                Console.WriteLine ("Libro 'El Prisionero del Cielo' creado correctamente.");

                // Libro 11_3 (usa el mismo id que Libro 10, es decir, que el autor tiene 2 libros)
                var libro11_3 = librocp.CrearLibro (
 p_titulo: "El Laberinto de los Espíritus",
 p_genero: "Misterio",
                        p_edadRecomendada: 15,
 p_fechaPublicacion: new DateTime (2016, 6, 3),
                        p_numPags: 925,
 p_sinopsis: "Alicia Gris, una investigadora con un pasado atormentado, se ve envuelta en una conspiración que conecta todos los hilos del Cementerio de los Libros Olvidados, revelando la verdad final sobre la historia de los Sempere y Carax.",
 p_fotoPortada: "/images/portadasLibros/laberintoEspiritus.webp",
 p_valoracionMedia: 3.5f,
 p_autorPublicador: autorId10
                        );
                var idLibro11_3 = libro11_3.Id;
                Console.WriteLine ("Libro 'El Laberinto de los Espíritus' creado correctamente.");

                // Auotor 12 (devuelve su id para ser usado a continuación en la creación del libro)
                int autorId12 = autorcen.CrearAutor (
 p_email: "jrrtolkien@email.com",
 p_nombreUsuario: "JRR Tolkien",
 p_fechaNacimiento: new DateTime (1892, 1, 3),
 p_ciudadResidencia: "Bloemfontein",
 p_paisResidencia: "Reino Unido",
                        p_numModificaciones: 0,
 p_valoracionMedia: 4.5f,
 p_foto: "/images/fotosUsuarios/tolkien.webp",
 p_rol: ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.RolUsuarioEnum.autor,
 p_pass: "passTolkien",
                        p_numeroSeguidores: 0,
                        p_cantidadLibrosPublicados: 0
                        );
                Console.WriteLine ("Autor creado correctamente con ID: " + autorId12);

                // Libro 12 (usa el id del autor anterior)
                var libro12 = librocp.CrearLibro (
 p_titulo: "El Señor de los Anillos",
 p_genero: "Aventura",
 p_valoracionMedia: 4.5f,
                        p_edadRecomendada: 14,
 p_fechaPublicacion: new DateTime (1954, 7, 29),
                        p_numPags: 1178,
 p_sinopsis: "La Tierra Media está al borde de la guerra mientras un hobbit llamado Frodo Bolsón debe destruir un anillo único que posee un poder maligno inmenso, acompañado de su leal Compañía del Anillo.",
 p_fotoPortada: "/images/portadasLibros/senorAnillos1.webp",
 p_autorPublicador: autorId12
                        );
                var idLibro12 = libro12.Id;
                Console.WriteLine ("Libro 'El Señor de los Anillos' creado correctamente.");

                // Libro 13 (usa el id del autor anterior)
                var libro13 = librocp.CrearLibro (
 p_titulo: "La Comunidad del Anillo",
 p_genero: "Aventura",
 p_valoracionMedia: 4.5f,
                        p_edadRecomendada: 14,
 p_fechaPublicacion: new DateTime (1954, 7, 29),
                        p_numPags: 576,
 p_sinopsis: "Frodo Bolsón hereda un anillo aparentemente inocente, sin saber que es el arma suprema del Señor Oscuro. Para destruirlo, emprende un viaje junto a una Compañía formada por hobbits, humanos, un enano, un elfo y un mago.",
 p_fotoPortada: "/images/portadasLibros/senorAnillos2.webp",
 p_autorPublicador: autorId12
                        );
                var idLibro13 = libro13.Id;
                Console.WriteLine ("Libro 'La Comunidad del Anillo' creado correctamente.");

                // Libro 14 (usa el id del autor anterior)
                var libro14 = librocp.CrearLibro (
 p_titulo: "Las Dos Torres",
 p_genero: "Aventura",
 p_valoracionMedia: 4.5f,
                        p_edadRecomendada: 14,
 p_fechaPublicacion: new DateTime (1954, 11, 11),
                        p_numPags: 352,
 p_sinopsis: "La Comunidad se ha dispersado. Mientras Aragorn, Legolas y Gimli persiguen a los orcos que raptaron a los hobbits, Frodo y Sam continúan su camino hacia Mordor guiados por la misteriosa y peligrosa criatura llamada Gollum.",
 p_fotoPortada: "/images/portadasLibros/senorAnillos3.webp",
 p_autorPublicador: autorId12
                        );
                var idLibro14 = libro14.Id;
                Console.WriteLine ("Libro 'Las Dos Torres' creado correctamente.");

                // Libro 15 (usa el id del autor anterior)
                var libro15 = librocp.CrearLibro (
 p_titulo: "El Retorno del Rey",
 p_genero: "Aventura",
 p_valoracionMedia: 4.5f,
                        p_edadRecomendada: 14,
 p_fechaPublicacion: new DateTime (1955, 10, 20),
                        p_numPags: 416,
 p_sinopsis: "Las fuerzas de Sauron avanzan contra Gondor mientras Frodo y Sam se acercan al Monte del Destino. La batalla final por la Tierra Media se decide cuando el verdadero rey reclama su trono.",
 p_fotoPortada: "/images/portadasLibros/senorAnillos4.webp",
 p_autorPublicador: autorId12
                        );
                var idLibro15 = libro15.Id;
                Console.WriteLine ("Libro 'El Retorno del Rey' creado correctamente.");



                Console.WriteLine ("\n====================================================================================");
                Console.WriteLine ("RESUMEN - CREACIÓN DE AUTORES Y LIBROS:");
                Console.WriteLine ("- 11 Autores creados correctamente");
                Console.WriteLine ("- 12 Libros creados correctamente");
                Console.WriteLine ("- Libros con diferentes géneros, edades recomendadas y valoraciones");
                Console.WriteLine ("====================================================================================");

                /////////////////////////////////////////////////////////////////////////////////////////////////////

                // CREACIÓN DE EVENTOS PARA PRUEBAS DE ReadFilter
                Console.WriteLine ("\n\n------------------ Creación de Eventos ------------------");

                // Inicializar NotificacionCP para crear notificaciones
                SessionCPNHibernate sessionCPNotificacion = new SessionCPNHibernate ();
                var notificacionCP = new NotificacionCP (sessionCPNotificacion);

                int eventoId1 = eventocen.CrearEvento (
 p_nombre: "Feria del Libro 2026",
 p_fecha: new DateTime (2026, 11, 15),
 p_hora: new DateTime (2026, 11, 15, 10, 0, 0),
 p_ubicacion: "Teruel",
 p_descripcion: "Feria anual del libro con firmas de autores",
 p_foto: "/images/imagenEvento/feria_libro_2026.webp",
                        p_aforoMax: 50,
                        p_aforoActual: 0,
 p_administradorEventos: administradorId1
                        );
                Console.WriteLine ("Evento 'Feria del Libro 2026' creado correctamente.");

                int eventoId2 = eventocen.CrearEvento (
 p_nombre: "Encuentro de Lectores",
 p_fecha: new DateTime (2026, 12, 20),
 p_hora: new DateTime (2026, 12, 20, 18, 0, 0),
 p_ubicacion: "Madrid",
 p_descripcion: "Reunión mensual de club de lectura",
 p_foto: "/images/imagenEvento/encuentro_lectores.webp",
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
 p_foto: "/images/imagenEvento/conferencia_escritores.webp",
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
 p_foto: "/images/imagenEvento/presentacion_libro.webp",
                        p_aforoMax: 100,
                        p_aforoActual: 0,
 p_administradorEventos: administradorId1
                        );
                Console.WriteLine ("Evento 'Presentación Libro Nuevo' creado correctamente.");

                int notificacionId3 = notificacionCP.CrearNotificacion (
 p_fecha: new DateTime (2025, 11, 15),
 p_concepto: ConceptoNotificacionEnum.evento_anunciado,
 p_OID_destino: eventoId4,
 p_tituloResumen: "Anuncio de un nuevo evento",
 p_textoCuerpo: "Se ha anunciado un nuevo evento llamado '" + eventocen.DameEventoPorOID (eventoId4).Nombre + "'."
                        ).Id;
                Console.WriteLine ("Notificación 'Nuevo evento anunciado' creada correctamente.");

                Console.WriteLine ("\n====================================================================================");
                Console.WriteLine ("RESUMEN - CREACIÓN DE EVENTOS:");
                Console.WriteLine ("- 4 Eventos creados");
                Console.WriteLine ("- Fechas (hoy o en adelante)");
                Console.WriteLine ("- Aforos: 50, 50, 200, 100 personas");
                Console.WriteLine ("RESUMEN - NOTIFICACIÓN DE EVENTO:");
                Console.WriteLine ("- Notificación creada para el evento 'Presentación Libro Nuevo'");
                Console.WriteLine ("====================================================================================");

                /////////////////////////////////////////////////////////////////////////////////////////////////////

                // CREACIÓN USURIO - Lector 3 (Para asignarle la creación de clubs)
                Console.WriteLine ("\n\n------------------ Creación de Usuario Lector ------------------");

                int usuarioId3 = lectorcen.CrearLector (
 p_email: "niko.lector@email.com",
 p_nombreUsuario: "Niko Lector",
 p_fechaNacimiento: new DateTime (2003, 05, 20),
 p_ciudadResidencia: "Elche",
                        p_numModificaciones: 0,
 p_paisResidencia: "España",
 p_foto: "/images/fotosUsuarios/usuarioDefault.webp",
 p_rol: ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.RolUsuarioEnum.lector,
 p_pass: "passNiko",
                        p_cantLibrosCurso: 0,
                        p_cantLibrosLeidos: 0,
                        p_cantAutoresSeguidos: 0,
                        p_cantClubsSuscritos: 0);
                Console.WriteLine ("Usuario 'Niko Lector' creado correctamente.");

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
                        var lectorCEN = new LectorCEN (sessionCPNHibernate1.UnitRepo.LectorRepository);
                        var clubCEN = new ClubCEN (sessionCPNHibernate1.UnitRepo.ClubRepository);

                        LectorEN propietario1 = lectorCEN.DameLectorPorOID (usuarioId1);

                        clubId1 = clubCEN.CrearClub (
 p_nombre: "Club de Ciencia Ficción",
 p_enlaceDiscord: "https://discord.gg/cienciaficcion",
                                p_miembrosMax: 50,
 p_foto: "/images/imagenClub/club_cienciaficcion.webp",
 p_descripcion: "Un club para los entusiastas de la ciencia ficción, donde exploramos mundos futuristas y tecnologías avanzadas.",
                                p_miembrosActuales: 0,
 p_lectorPropietario: propietario1

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
                        var lectorCEN = new LectorCEN (sessionCPNHibernate1.UnitRepo.LectorRepository);
                        var clubCEN = new ClubCEN (sessionCPNHibernate1.UnitRepo.ClubRepository);

                        LectorEN propietario2 = lectorCEN.DameLectorPorOID (usuarioId3);

                        clubId2 = clubCEN.CrearClub (
 p_nombre: "Club de Misterio y Suspense",
 p_enlaceDiscord: "https://discord.gg/misterio",
                                p_miembrosMax: 30,
 p_foto: "/images/imagenClub/club_misterio.webp",
 p_descripcion: "Amantes del misterio, el suspense y las tramas que te mantienen en vilo hasta la última página.",
                                p_miembrosActuales: 12,
 p_lectorPropietario: propietario2
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
                        var lectorCEN = new LectorCEN (sessionCPNHibernate1.UnitRepo.LectorRepository);
                        var clubCEN = new ClubCEN (sessionCPNHibernate1.UnitRepo.ClubRepository);

                        LectorEN propietario3 = lectorCEN.DameLectorPorOID (usuarioId1);

                        clubId3 = clubCEN.CrearClub (
 p_nombre: "Club de Romance Contemporáneo",
 p_enlaceDiscord: "https://discord.gg/romance",
                                p_miembrosMax: 75,
 p_foto: "/images/imagenClub/club_romance.webp",
 p_descripcion: "Para quienes disfrutan de historias de amor modernas, emotivas y llenas de sentimientos.",
                                p_miembrosActuales: 45,
 p_lectorPropietario: propietario3
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
                        var lectorCEN = new LectorCEN (sessionCPNHibernate1.UnitRepo.LectorRepository);
                        var clubCEN = new ClubCEN (sessionCPNHibernate1.UnitRepo.ClubRepository);

                        LectorEN propietario4 = lectorCEN.DameLectorPorOID (usuarioId3);

                        clubId4 = clubCEN.CrearClub (
 p_nombre: "Club de Aventuras Épicas",
 p_enlaceDiscord: "https://discord.gg/aventuras",
                                p_miembrosMax: 100,
 p_foto: "/images/imagenClub/club_aventuras.webp",
 p_descripcion: "Exploradores literarios que buscan viajes épicos, mundos fantásticos y personajes heroicos.",
                                p_miembrosActuales: 67,
 p_lectorPropietario: propietario4
                                );

                        sessionCPNHibernate1.Commit ();
                        Console.WriteLine ("Club 'Club de Aventuras Épicas' creado correctamente con ID: " + clubId4);

                        // Crear notificación para el club 4
                        int notificacionId4 = notificacionCP.CrearNotificacion (
 p_fecha: new DateTime (2025, 11, 15),
 p_concepto: ConceptoNotificacionEnum.aviso_club_lectura,
 p_OID_destino: clubId4,
 p_tituloResumen: "Aviso de creación de nuevo club de lectura",
 p_textoCuerpo: "Aviso de creación del club '" + clubCEN.DameClubPorOID (clubId4).Nombre + "'."
                                ).Id;
                        Console.WriteLine ("Notificación 'Aviso de creación de nuevo club de lectura' creada correctamente.");
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
                Console.WriteLine ("RESUMEN - CREACIÓN DE NOTIFICACIÓN DE CLUB");
                Console.WriteLine ("Notificación creada para el club 'Aventuras Épicas'");
                Console.WriteLine ("====================================================================================");

                /////////////////////////////////////////////////////////////////////////////////////////////////////

                // CREACIÓN DE RESEÑAS PARA PRUEBAS DE ReadFilter
                Console.WriteLine ("\n\n------------------ Creación de Reseñas ------------------");

                // Obtener objetos Lector para pasarlos como valoradores
                LectorEN lector3 = lectorcen.get_ILectorRepository ().DameLectorPorOID (usuarioId3);

                int noticiaId1 = noticiacen.CrearNoticia (
 p_titulo: "Lanzamiento de nueva plataforma ReadRate",
 p_fechaPublicacion: new DateTime (2024, 10, 15),
 p_foto: "/images/imagenNoticia/noticia_lanzamiento.webp",
 p_textoContenido: "Estamos emocionados de presentar ReadRate, la nueva plataforma de lectura social donde podrás compartir tus opiniones sobre libros y conectar con otros lectores.",
 p_administradorNoticias: administradorId1
                        );

                // Los libros a reseñar ya fueron creados anteriormente

                // Reseña 1 con notificación
                int reseñaId1 = reseñacen.CrearReseña (p_textoOpinion: "Me encantó la trama y los personajes. Una historia muy bien desarrollada.",
 p_valoracion: 5.0f,
 p_lectorValorador: lector3.Id,
 p_libroReseñado: libro1.Id,
 p_fecha: new DateTime (2024, 11, 10));
                Console.WriteLine ("Reseña para libro 1 creada correctamente.");

                int notificacionId1 = notificacionCP.CrearNotificacion (
 p_fecha: new DateTime (2025, 11, 10),
 p_concepto: ConceptoNotificacionEnum.nueva_reseña,
 p_OID_destino: reseñaId1,
 p_tituloResumen: "Nueva reseña creada",
 p_textoCuerpo: "Se ha creado una nueva reseña para el libro '" + libro1.Titulo + "' con una valoración de 5.0 estrellas."
                        ).Id;

                // Reseña 2
                int reseñaId2 = reseñacen.CrearReseña (
 p_textoOpinion: "Recomendado para todos. Lectura muy entretenida.",
 p_valoracion: 4.0f,
 p_lectorValorador: usuarioId3,
 p_libroReseñado: libro1.Id,
 p_fecha: new DateTime (2024, 10, 5)
                        );
                Console.WriteLine ("Reseña para libro 2 creada correctamente.");

                // Reseña 3
                int reseñaId3 = reseñacen.CrearReseña (
 p_textoOpinion: "Tiene sus momentos pero podría mejorar en algunos aspectos.",
 p_valoracion: 3.0f,
 p_lectorValorador: usuarioId3,
 p_libroReseñado: libro3.Id,
 p_fecha: new DateTime (2024, 10, 10)
                        );
                Console.WriteLine ("Reseña para libro 3 creada correctamente.");

                // Reseña 4
                int reseñaId4 = reseñacen.CrearReseña (
 p_textoOpinion: "Esperaba más de este libro. No cumplió mis expectativas.",
 p_valoracion: 2.0f,
 p_lectorValorador: usuarioId3,
 p_libroReseñado: libro4.Id,
 p_fecha: new DateTime (2024, 10, 12)
                        );
                Console.WriteLine ("Reseña para libro 4 creada correctamente.");

                // Reseña 5
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
                Console.WriteLine ("Rango de fechas: 5 de octubre - 10 de noviembre 2024");
                Console.WriteLine ("====================================================================================");

                /////////////////////////////////////////////////////////////////////////////////////////////////////

                // CREACIÓN DE NOTICIAS PARA PRUEBAS DE ReadFilter
                Console.WriteLine ("\n\n------------------ Creación de Noticias ------------------");

                int noticiaId7 = noticiacen.CrearNoticia (
 p_titulo: "Nuevo libro de Brandon Sanderson",
 p_fechaPublicacion: new DateTime (2024, 11, 1),
 p_foto: "/images/imagenNoticia/noticia_nuevo_libro.webp",
 p_textoContenido: "El reconocido autor publicará su nueva obra el próximo mes. Se trata de una novela de misterio que promete mantener a los lectores en vilo hasta la última página.",
 p_administradorNoticias: administradorId1
                        );
                Console.WriteLine ("Noticia 'Nuevo libro de Brandon Sanderson' creada correctamente.");

                int noticiaId2 = noticiacen.CrearNoticia (
 p_titulo: "Feria del Libro 2024",
 p_fechaPublicacion: new DateTime (2024, 11, 5),
 p_foto: "/images/imagenNoticia/noticia_feria_libro.webp",
 p_textoContenido: "Se celebrará la feria anual del libro con la participación de más de 200 autores nacionales e internacionales. Habrá firmas de libros, charlas y actividades para toda la familia.",
 p_administradorNoticias: administradorId1
                        );
                Console.WriteLine ("Noticia 'Feria del Libro 2024' creada correctamente.");

                int noticiaId3 = noticiacen.CrearNoticia (
 p_titulo: "Entrevista exclusiva con Leigh Bardugo",
 p_fechaPublicacion: new DateTime (2024, 11, 10),
 p_foto: "/images/imagenNoticia/noticia_entrevista_leigh.webp",
 p_textoContenido: "Hablamos con la autora sobre su proceso creativo, sus influencias literarias y sus próximos proyectos. Una conversación íntima sobre el arte de escribir.",
 p_administradorNoticias: administradorId1
                        );
                Console.WriteLine ("Noticia 'Entrevista exclusiva con Leigh Bardugo' creada correctamente.");

                int noticiaId4 = noticiacen.CrearNoticia (
 p_titulo: "Bestsellers del mes",
 p_fechaPublicacion: new DateTime (2024, 11, 15),
 p_foto: "/images/imagenNoticia/noticia_bestsellers.webp",
 p_textoContenido: "Los libros más vendidos de este mes incluyen títulos de ficción, romance y ciencia ficción. Descubre cuáles son las lecturas favoritas de los usuarios en noviembre.",
 p_administradorNoticias: administradorId1
                        );
                Console.WriteLine ("Noticia 'Bestsellers del mes' creada correctamente.");

                int notificacionId2 = notificacionCP.CrearNotificacion (
 p_fecha: new DateTime (2025, 11, 15),
 p_concepto: ConceptoNotificacionEnum.noticia_publicada,
 p_OID_destino: noticiaId4,
 p_tituloResumen: "Nueva noticia publicada",
 p_textoCuerpo: "Se ha publicado una nueva noticia titulada '" + noticiacen.DameNoticiaPorOID (noticiaId4).Titulo + "'."
                        ).Id;
                Console.WriteLine ("Notificación 'Nueva noticia publicada' creada correctamente.");

                // Resumen de Noticias creadas:
                Console.WriteLine ("\n====================================================================================");
                Console.WriteLine ("RESUMEN - CREACIÓN DE NOTICIAS");
                Console.WriteLine ("Total: 4 noticias creadas");
                Console.WriteLine ("Títulos: Nuevo libro de Brandon Sanderson, Feria del Libro 2024, Entrevista con Leigh Bardugo, Bestsellers del mes");
                Console.WriteLine ("Fechas de publicación: 1, 5, 10, 15 de noviembre 2024");
                Console.WriteLine ("RESUMEN - CREACIÓN DE NOTIFICACIÓN ASOCIADA A NOTICIA");
                Console.WriteLine ("Total: 1 notificación creada");
                Console.WriteLine ("Título resumen: Nueva noticia publicada");
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

                // PRUEBA 2: Filtro por género "Fantasía"
                Console.WriteLine ("\n------------------ Prueba 2: Filtro por género 'Fantasía' ------------------");
                var librosFantasía = librocen.DameLibrosPorFiltros (p_genero: "Fantasía", p_titulo: null, p_edadRecomendada: null, p_numPags: null, p_valoracionMedia: null, 0, 20);
                Console.WriteLine ("Libros de 'Fantasía' encontrados: " + (librosFantasía != null ? librosFantasía.Count.ToString () : "0"));

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

                // PRUEBA 6: Filtro combinado - Género "Fantasía" y edad > 10
                Console.WriteLine ("\n------------------ Prueba 6: Filtro combinado - Género 'Fantasía' y edad >= 10 ------------------");
                var librosFantasía10 = librocen.DameLibrosPorFiltros (p_genero: "Fantasía", p_titulo: null, p_edadRecomendada: 10, p_numPags: null, p_valoracionMedia: null, 0, 20);
                Console.WriteLine ("Libros de 'Fantasía' para edad >= 10 años encontrados: " + (librosFantasía10 != null ? librosFantasía10.Count.ToString () : "0"));

                // PRUEBA 7: Filtro combinado - Género "Fantasía", valoración >= 1.0, >= 500 páginas
                Console.WriteLine ("\n------------------ Prueba 7: Filtro combinado - Fantasía + valoración >= 4.5, >= 400 páginas ------------------");
                var librosRestrictivo = librocen.DameLibrosPorFiltros (p_genero: "Fantasía", p_titulo: null, p_edadRecomendada: null, p_numPags: 400, p_valoracionMedia: 4.5f, 0, 20);
                Console.WriteLine ("Libros que cumplen todos los criterios propuestos: " + (librosRestrictivo != null ? librosRestrictivo.Count.ToString () : "0"));

                Console.WriteLine ("\n====================================================================================");
                Console.WriteLine ("RESUMEN - PRUEBAS DE FILTRO DE LIBROS:");
                Console.WriteLine ("- Prueba 1: Sin filtros - Todos los libros");
                Console.WriteLine ("- Prueba 2: Por género 'Fantasía'");
                Console.WriteLine ("- Prueba 3: Por edad recomendada >= 12");
                Console.WriteLine ("- Prueba 4: Por número de páginas >= 200");
                Console.WriteLine ("- Prueba 5: Por valoración >= 4.0");
                Console.WriteLine ("- Prueba 6: Filtro combinado género + edad");
                Console.WriteLine ("- Prueba 7: Filtro con tres criterios");
                Console.WriteLine ("=======================================================================");

                /////////////////////////////////////////////////////////////////////////////////////////////////////

                // PRUEBAS DE READFILTERS dameEventosPorFecha()
                Console.WriteLine ("\n\n==================== PRUEBAS DE ReadFilter - dameEventosPorFecha() ====================");

                // PRUEBA 1: Buscar eventos en fecha específica (15 de noviembre de 2026)
                Console.WriteLine ("\n------------------ Prueba 1: Eventos en fecha 15/11/2026 y en adelante ------------------");
                var eventosNov15 = eventocen.DameEventosPorFecha (new DateTime (2026, 11, 15), 0, 20);
                Console.WriteLine ("Eventos encontrados para 15/11/2026 o más: " + (eventosNov15 != null ? eventosNov15.Count.ToString () : "0"));

                // PRUEBA 2: Buscar eventos en fecha sin eventos (1 de enero de 2028)
                Console.WriteLine ("\n------------------ Prueba 2: Eventos en fecha 01/01/2028 y en adelante (sin eventos) ------------------");
                var eventosEne01 = eventocen.DameEventosPorFecha (new DateTime (2028, 1, 1), 0, 20);
                Console.WriteLine ("Eventos encontrados para 01/01/2028 o más: " + (eventosEne01 != null ? eventosEne01.Count.ToString () : "0"));

                Console.WriteLine ("\n====================================================================================");
                Console.WriteLine ("RESUMEN - PRUEBAS dameEventosPorFecha():");
                Console.WriteLine ("- Prueba 1: Buscar eventos el 15/11/2026");
                Console.WriteLine ("- Prueba 2: Buscar eventos el 01/01/2028 (sin resultados esperados)");
                Console.WriteLine ("====================================================================================");

                /////////////////////////////////////////////////////////////////////////////////////////////////////

                Console.WriteLine ("\n\n==================== PRUEBAS DE ReadFilter - dameUsuarioPorEmail() ====================");

                // PRUEBA 1: Buscar usuario lector existente por email
                Console.WriteLine ("\n------------------ Prueba 1: Buscar lector por email 'paco.lector@email.com' ------------------");
                List<UsuarioEN> usuarioPaco = usuariocen.DameUsuarioPorEmail ("paco.lector@email.com") as List<UsuarioEN>;
                Console.WriteLine ("Usuario encontrado: " + (usuarioPaco.Count > 0 ? usuarioPaco [0].NombreUsuario : "No encontrado"));

                // PRUEBA 2: Buscar autor existente por email
                Console.WriteLine ("\n------------------ Prueba 2: Buscar autor por email 'leighbardugo@email.com' ------------------");
                List<UsuarioEN> usuarioAutor = usuariocen.DameUsuarioPorEmail ("leighbardugo@email.com") as List<UsuarioEN>;
                Console.WriteLine ("Usuario encontrado: " + (usuarioAutor.Count > 0 ? usuarioAutor [0].NombreUsuario : "No encontrado"));

                // PRUEBA 3: Buscar usuario que no existe
                Console.WriteLine ("\n------------------ Prueba 3: Buscar usuario no existente 'noexiste@email.com' ------------------");
                List<UsuarioEN> usuarioNoExiste = usuariocen.DameUsuarioPorEmail ("noexiste@email.com") as List<UsuarioEN>;
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
                Console.WriteLine ("\n------------------ Prueba 1: Noticias con título 'Nuevo libro de Brandon Sanderson' ------------------");
                List <NoticiaEN> noticiasLibro = noticiacen.DameNoticiasPorTitulo (p_titulo: "Nuevo libro de Brandon Sanderson") as List<NoticiaEN>;
                Console.WriteLine ("Noticias encontradas con 'Nuevo libro de Brandon Sanderson': " + (noticiasLibro != null ? noticiasLibro.Count.ToString () : "0"));

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
                Console.WriteLine ("- Prueba 2: Buscar 'Feria' en títulos (Se espera 1 resultado encontrado)");
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
                Console.WriteLine ("\n------------------ Prueba 1: Buscar administrador por email 'admin@email.com' ------------------");
                List<AdministradorEN> adminEncontrado = administradorcen.DameAdministradoresPorEmail ("admin@email.com") as List<AdministradorEN>;
                Console.WriteLine ("Administrador encontrado: " + (adminEncontrado.Count > 0 ? adminEncontrado [0].Nombre : "No encontrado"));

                // PRUEBA 2: Buscar con email que no corresponde a un administrador
                Console.WriteLine ("\n------------------ Prueba 2: Buscar con email de lector 'paco.lector@email.com' (no es admin) ------------------");
                List<AdministradorEN> noAdmin = administradorcen.DameAdministradoresPorEmail ("paco.lector@email.com") as List<AdministradorEN>;
                Console.WriteLine ("Administrador encontrado: " + (noAdmin.Count > 0 ? noAdmin [0].Nombre : "No encontrado"));

                // PRUEBA 3: Buscar con email que no existe
                Console.WriteLine ("\n------------------ Prueba 3: Buscar con email inexistente 'admin.falso@email.com' ------------------");
                List<AdministradorEN> adminNoExiste = administradorcen.DameAdministradoresPorEmail ("admin.falso@email.com") as List<AdministradorEN>;
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
 p_email: "marina.lectora@email.com",
 p_nombreUsuario: "Marina Antes de Modificar",
                        p_numModificaciones: 0,
 p_fechaNacimiento: new DateTime (1980, 11, 10),
 p_ciudadResidencia: "Villajoyosa", p_paisResidencia: "España",
 p_foto: "/images/fotosUsuarios/usuarioDefault.webp", p_rol: ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.RolUsuarioEnum.lector,
 p_pass: "passMarina",
                        p_cantLibrosCurso: 0,
                        p_cantLibrosLeidos: 0,
                        p_cantAutoresSeguidos: 0,
                        p_cantClubsSuscritos: 0);

                LectorEN pruebaModificarLector = lectorcen.DameLectorPorOID (usuarioId2);

                // MODIFICACIÓN USURIO - Lector 2 (Cambiar nombre de usuario)
                lectorcen.ModificarLector (
                        usuarioId2,
 p_email: pruebaModificarLector.Email,
 p_nombreUsuario: "Marina Lectora",
 p_numModificaciones: pruebaModificarLector.NumModificaciones + 1,
 p_fechaNacimiento: pruebaModificarLector.FechaNacimiento,
 p_ciudadResidencia: pruebaModificarLector.CiudadResidencia,
 p_paisResidencia: pruebaModificarLector.PaisResidencia,
 p_foto: pruebaModificarLector.Foto,
 p_rol: pruebaModificarLector.Rol,
 p_pass: pruebaModificarLector.Pass,
 p_cantLibrosCurso: pruebaModificarLector.CantLibrosCurso,
 p_cantLibrosLeidos: pruebaModificarLector.CantLibrosLeidos,
 p_cantAutoresSeguidos: pruebaModificarLector.CantAutoresSeguidos,
 p_cantClubsSuscritos: pruebaModificarLector.CantClubsSuscritos
                        );

                Console.WriteLine ("Usuario 'MarinaLectora' creado y modificado correctamente.");

                Console.WriteLine ("\n====================================================================================");
                Console.WriteLine ("RESUMEN - CREACIÓN DE LECTOR PARA PRUEBAS:");
                Console.WriteLine ("- Usuario 'MarinaLectora' creado y modificado correctamente");
                Console.WriteLine ("- Se utilizará para probar asignar/desasignar libros en listas");
                Console.WriteLine ("====================================================================================");

                /////////////////////////////////////////////////////////////////////////////////////////////////////

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

                Console.WriteLine ("\n====================================================================================");
                Console.WriteLine ("RESUMEN - PRUEBAS DE ASIGNAR LIBROS A LISTAS EN CURSO:");
                Console.WriteLine ("- PRUEBA 1: Asignar libro 11 a lista en curso (Correcto)");
                Console.WriteLine ("- PRUEBA 2: Asignar libro 11 a lista en curso (Incorrecto - Duplicado)");
                Console.WriteLine ("- PRUEBA 3: Asignar libro 10 a lista en curso (Correcto)");
                Console.WriteLine ("- PRUEBA 4: Asignar libro 7 a lista en curso (Correcto)");
                Console.WriteLine ("====================================================================================");

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

                Console.WriteLine ("\n====================================================================================");
                Console.WriteLine ("RESUMEN - PRUEBAS DE ASIGNAR LIBROS A LISTA DE GUARDADOS:");
                Console.WriteLine ("- PRUEBA 1: Asignar libro 9 a lista guardados (Correcto)");
                Console.WriteLine ("- PRUEBA 2: Asignar libro 9 a lista guardados (Incorrecto - Duplicado)");
                Console.WriteLine ("- PRUEBA 3: Asignar libro 8 a lista guardados (Correcto)");
                Console.WriteLine ("====================================================================================");

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

                Console.WriteLine ("\n====================================================================================");
                Console.WriteLine ("RESUMEN - PRUEBAS DE DESASIGNAR LIBROS DE LISTA EN CURSO:");
                Console.WriteLine ("- PRUEBA 1: Desasignar libro 10 de lista en curso (Correcto)");
                Console.WriteLine ("- PRUEBA 2: Desasignar libro 10 de lista en curso (Incorrecto - No está en la lista)");
                Console.WriteLine ("- PRUEBA 3: Desasignar libro 11 de lista en curso (Correcto)");
                Console.WriteLine ("====================================================================================");

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

                Console.WriteLine ("\n====================================================================================");
                Console.WriteLine ("RESUMEN - PRUEBAS DE DESASIGNAR LIBROS DE LISTA GUARDADOS:");
                Console.WriteLine ("- PRUEBA 1: Desasignar libro 8 de lista guardados (Correcto)");
                Console.WriteLine ("- PRUEBA 2: Desasignar libro 8 de lista guardados (Incorrecto - No está en la lista)");
                Console.WriteLine ("====================================================================================");

                /////////////////////////////////////////////////////////////////////////////////////////////////////

                Console.WriteLine ("\n\n====================================================================================\n\n");

                Console.WriteLine ("\n\n====================================================================================");
                Console.WriteLine ("ESTADO FINAL DE LAS LISTAS DE LIBROS:");
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
 p_nombre: "Nueva Actividad Cultural",
 p_foto: "/images/imagenEvento/nueva_actividad_cultural.webp",
 p_descripcion: "Una jornada dedicada a explorar diversas formas de arte y cultura local.",
 p_fecha: new DateTime (2026, 7, 1),
 p_hora: new DateTime (2026, 7, 1, 18, 0, 0),
 p_ubicacion: "Alicante",
                        p_aforoMax: 20,
                        p_aforoActual: 20,
 p_administradorEventos: administradorId1
                        );

                Console.WriteLine ("Evento 3 creado correctamente con ID: " + eventoId5);

                //CREAR EVENTO VACIO
                Console.WriteLine ("\n------------------ Crear Evento vacio ------------------");


                int eventoId6 = eventocen.CrearEvento (
 p_nombre: "Inauguración nueva librería en San Sebastián",
 p_foto: "/images/imagenEvento/inauguracion_nueva_libreria.webp",
 p_descripcion: "Únete a nosotros para la apertura de una nueva librería. Habrá descuentos especiales y actividades para todos los amantes de los libros.",
 p_fecha: new DateTime (2026, 7, 1),
 p_hora: new DateTime (2026, 7, 1, 18, 0, 0),
 p_ubicacion: "San Sebastián",
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
 p_ubicacion: "Teruel",
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

                Console.WriteLine ("\n====================================================================================");
                Console.WriteLine ("RESUMEN - PRUEBAS DE CREAR EVENTOS:");
                Console.WriteLine ("- PRUEBA 1: Crear evento lleno (Correcto)");
                Console.WriteLine ("- PRUEBA 2: Crear evento vacío (Correcto)");
                Console.WriteLine ("- PRUEBA 3: Crear evento sin administrador (Incorrecto)");
                Console.WriteLine ("- PRUEBA 4: Crear evento con fecha pasada (Incorrecto)");
                Console.WriteLine ("====================================================================================");


                /////////////////////////////////////////////////////////////////////////////////////////////////////

                Console.WriteLine ("\n\n\n====================================================================================");
                Console.WriteLine ("====================================================================================");
                Console.WriteLine ("====================================================================================");

                Console.WriteLine ("\n\n==================== PRUEBA DE MODIFICACIONES DE LIBRO ====================");

                // MODIFICAR LIBRO - Cambiar diversas caracteristicas
                Console.WriteLine ("\n------------------ Modificación de Libro (Resultado esperado: Correcto) ------------------");

                librocen.ModificarLibro (
                        idLibro10,
 p_titulo: "La Sombra del Viento",
 p_genero: "Misterio",
 p_valoracionMedia: 5.0f,
                        p_edadRecomendada: 15,
 p_fechaPublicacion: new DateTime (2001, 4, 1),
                        p_numPags: 487,
 p_sinopsis: "En la Barcelona de la posguerra, Daniel Sempere descubre un libro olvidado que lo introduce en un misterio literario que cambiará su vida y desvelará secretos del pasado.",
 p_fotoPortada: "/images/portadasLibros/sombraDelViento.webp"
                        );

                Console.WriteLine ("Libro 'La Sombra del Viento' modificado correctamente (ID: " + idLibro10 + ")");

                // MODIFICAR LIBRO - Cambiar número de páginas a un número negativo
                Console.WriteLine ("\n------------------ Modificación de Libro con páginas negativas (Resultado esperado: Incorrecto) ------------------");

                try
                {
                        librocen.ModificarLibro (
                                idLibro10,
 p_titulo: "La Sombra del Viento",
 p_genero: "Misterio",
                                p_edadRecomendada: 15,
 p_fechaPublicacion: new DateTime (2001, 4, 1),
 p_numPags: -50,                // Número de páginas negativo (INVÁLIDO)
 p_sinopsis: "En la Barcelona de la posguerra, Daniel Sempere descubre un libro olvidado que lo introduce en un misterio literario que cambiará su vida y desvelará secretos del pasado.",
 p_fotoPortada: "/images/portadasLibros/sombraDelViento.webp",
 p_valoracionMedia: 5.0f
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
                Console.WriteLine ("    - Título actualizado a 'La Sombra del Viento'");
                Console.WriteLine ("    - Género cambiado a 'Misterio'");
                Console.WriteLine ("    - Valoración media actualizada a 5.0");
                Console.WriteLine ("    - Número de páginas actualizado a 487");
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
 p_nombre: "Actividad en el Club de Lectura de Novela Negra",
 p_foto: "/images/imagenEvento/evento_novela_negra_especial.webp",
 p_descripcion: "Únete a nuestro club para discutir las mejores obras del género negro. Edición especial con autor invitado.",
 p_fecha: new DateTime (2026, 12, 20),                // Fecha futura
 p_hora: new DateTime (2026, 12, 20, 19, 30, 0),
 p_ubicacion: "Teruel",
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
 p_fecha: new DateTime (2020, 1, 1),                         // Fecha pasada (INVÁLIDA)
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
                Console.WriteLine ("  - Se ha intentado establecer fecha = 01/01/2020 (pasada)");
                Console.WriteLine ("  - Resultado: ModelException capturada correctamente");
                Console.WriteLine ("====================================================================================");


                Console.WriteLine ("\n\n==================== PRUEBA DE MODIFICACIONES DE RESEÑA ====================");

                // MODIFICAR RESEÑA - Caso Correcto: Cambiar texto de opinión y valoración
                Console.WriteLine ("\n\n------------------ Modificación de Reseña (Resultado esperado: Correcto) ------------------");

                reseñacen.ModificarReseña (
                        reseñaId1,
 p_textoOpinion: "Me encantó la trama y los personajes. Una historia muy bien desarrollada. Actualización: Sigue siendo excelente tras una segunda lectura.",
 p_valoracion: 4.5f,                // Valoración válida entre 0 y 5
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
 p_valoracion: -2.0f,                        // Valoración negativa (INVÁLIDA)
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
 p_valoracion: 10.0f,                        // Valoración mayor a 5 (INVÁLIDA)
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
                        var libroInvalido1 = librocp.CrearLibro (
 p_titulo: "Libro con Páginas Negativas",
 p_genero: "Prueba",
                                p_edadRecomendada: 12,
 p_fechaPublicacion: new DateTime (2024, 1, 1),
 p_numPags: -100,                         // Número de páginas negativo (INVÁLIDO)
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
                        var libroInvalido2 = librocp.CrearLibro (
 p_titulo: "Libro sin Autor",
 p_genero: "Prueba",
                                p_edadRecomendada: 15,
 p_fechaPublicacion: new DateTime (2024, 2, 1),
                                p_numPags: 200,
 p_sinopsis: "Libro de prueba sin autor asociado",
 p_fotoPortada: "portada_sin_autor.jpg",
 p_autorPublicador: -1,                        // Sin autor asociado (INVÁLIDO)
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
 p_foto: "/images/imagenNoticia/noticia_sin_admin.webp",
 p_textoContenido: "Esta es una noticia de prueba sin administrador asociado",
 p_administradorNoticias: -1                        // Sin administrador asociado (INVÁLIDO)
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
 p_lectorValorador: -1,                        // Sin lector valorador (INVÁLIDO)
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
 p_libroReseñado: -1,                        // Sin libro reseñado (INVÁLIDO)
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

                // INSCRIBIR USUARIO AL EVENTO CREADO CON AFORO ACTUAL = 0
                Console.WriteLine ("\n------------------ Inscripción de Usuario al Evento (Resulatado esperado: Correcto) ------------------");

                SessionCPNHibernate sessionCPNHibernate2 = new SessionCPNHibernate ();
                LectorCP lectorCP1 = new LectorCP (sessionCPNHibernate2);

                try
                {
                        lectorCP1.InscribirLectorAEvento (usuarioId1, new List<int> { eventoId1 });
                        Console.WriteLine ("Usuario inscrito correctamente al evento 'Club de Lectura de Novela Negra'.");
                }
                catch (ModelException ex)
                {
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                        Console.WriteLine ("Se ha capturado una ModelException al intentar inscribirse a un evento: " + ex);
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                }

                // INSCRIBIR USUARIO AL MISMO EVENTO OTRA VEZ
                Console.WriteLine ("\n------------------ Inscripción de Usuario al mismo Evento otra vez (Resulatado: Incorrecto)");

                SessionCPNHibernate sessionCPNHibernate3 = new SessionCPNHibernate ();
                LectorCP lectorCP2 = new LectorCP (sessionCPNHibernate3);

                try
                {
                        lectorCP2.InscribirLectorAEvento (usuarioId1, new List<int> { eventoId1 });
                        Console.WriteLine ("Usuario inscrito correctamente al evento 'Club de Lectura de Novela Negra'.");
                }
                catch (ModelException ex)
                {
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                        Console.WriteLine ("Se ha capturado una ModelException al intentar inscribirse otra vez al mismo evento: " + ex);
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                }

                // INSCRIBIR USUARIO AL EVENTO CREADO CON CAPACIDAD AFORO ACTUAL = AFORO MAXIMO
                Console.WriteLine ("\n------------------ Inscripción de Usuario al Evento lleno (Resultado Esperado: Incorrecto) ------------------");

                SessionCPNHibernate sessionCPNHibernate5 = new SessionCPNHibernate ();
                LectorCP lectorCP3 = new LectorCP (sessionCPNHibernate5);

                try
                {
                        lectorCP3.InscribirLectorAEvento (usuarioId1, new List<int> { eventoId3 });
                        Console.WriteLine ("Usuario inscrito correctamente al evento 'Evento3");
                }
                catch (ModelException ex)
                {
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                        Console.WriteLine ("Se ha capturado una ModelException al intentar inscribirse a un evento con aforo completo: " + ex);
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                }


                // DESINSCRIBIR USUARIO DEL EVENTO CREADO
                Console.WriteLine ("\n------------------ Desinscripción de Usuario del Evento (Resulatado esperado: Correcto) ------------------");
                SessionCPNHibernate sessionCPNHibernate4 = new SessionCPNHibernate ();
                LectorCP lectorCP4 = new LectorCP (sessionCPNHibernate4);

                try
                {
                        lectorCP4.DesinscribirLectorDeEvento (usuarioId1, new List<int> { eventoId1 });
                        Console.WriteLine ("Usuario desinscrito correctamente del evento 'Club de Lectura de Novela Negra'.");
                }
                catch (ModelException ex)
                {
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                        Console.WriteLine ("Se ha capturado una ModelException al intentar desinscribirse de un evento: " + ex);
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                }

                // DESINSCRIBIR USUARIO DE UN EVENTO AL QUE NO ESTA SUSCRITO
                Console.WriteLine ("\n------------------ Desinscripción de Usuario del Evento al que no está suscrito (Resulatado esperado: Inorrecto) ------------------");
                SessionCPNHibernate sessionCPNHibernate6 = new SessionCPNHibernate ();
                LectorCP lectorCP5 = new LectorCP (sessionCPNHibernate6);

                try
                {
                        lectorCP5.DesinscribirLectorDeEvento (usuarioId1, new List<int> { eventoId5 });
                        Console.WriteLine ("Usuario dessuscrito correctamente del evento vacio");
                }
                catch (ModelException ex)
                {
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                        Console.WriteLine ("Se ha capturado una ModelException al intentar desinscribirse de un evento: " + ex);
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                }

                Console.WriteLine ("\n====================================================================================");
                Console.WriteLine ("RESUMEN DE LAS PRUEBAS DE SUSCRIBIR Y DESUSCRIBIR USUARIOS DE EVENTOS:");
                Console.WriteLine ("- PRUEBA 1: Inscripción al evento con aforo disponible (Correcto)");
                Console.WriteLine ("- PRUEBA 2: Inscripción al mismo evento otra vez (Incorrecto)");
                Console.WriteLine ("- PRUEBA 3: Inscripción al evento lleno (Incorrecto)");
                Console.WriteLine ("- PRUEBA 4: Desinscripción del evento (Correcto)");
                Console.WriteLine ("- PRUEBA 5: Desinscripción de evento al que no está suscrito (Incorrecto)");
                Console.WriteLine ("====================================================================================");

                ///////////////////////////////////////////////////////////////////////////////////////////////////////////

                Console.WriteLine ("\n\n\n====================================================================================");
                Console.WriteLine ("====================================================================================");
                Console.WriteLine ("====================================================================================");

                Console.WriteLine ("\n\n==================== PRUEBAS DE CLUBS - SUSCRIBIR Y DESUSCRIBIR PARTICIPANTES ====================");

                // Nota: Para la suscripción y desuscripción de usuarios a club usamos el primer club definido en las pruebas de ReadFilter de Clubs

                // SUSCRIBIR USUARIO AL CLUB CREADO
                Console.WriteLine ("\n------------------ Suscripción de Usuario al Club (Resulatado esperado: Correcto) ------------------");
                SessionCPNHibernate sessionCPNHibernate9 = new SessionCPNHibernate ();
                LectorCP lectorCP6 = new LectorCP (sessionCPNHibernate3);

                try
                {
                        lectorCP6.SuscribirLectorAClub (usuarioId3, new List<int> { clubId1 });
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
                LectorCP lectorCP7 = new LectorCP (sessionCPNHibernate3);
                try
                {
                        lectorCP7.DesuscribirLectorDeClub (usuarioId3, new List<int> { clubId1 });
                        Console.WriteLine ("Usuario desuscrito correctamente del club 'Club de Ciencia Ficción'.");
                }
                catch (ModelException ex)
                {
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                        Console.WriteLine ("Se ha capturado una ModelException al intentar desuscribirse de un club: " + ex);
                        Console.WriteLine ("---------------------------------------------------------------------------------------");
                }

                Console.WriteLine ("\n====================================================================================");
                Console.WriteLine ("RESUMEN DE LAS PRUEBAS DE SUSCRIBIR Y DESUSCRIBIR USUARIOS DE CLUBS:");
                Console.WriteLine ("- PRUEBA 1: Suscripción al club (Correcto)");
                Console.WriteLine ("- PRUEBA 2: Desuscripción del club (Correcto)");
                Console.WriteLine ("====================================================================================");

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
                LectorCP lectorCP8 = new LectorCP (sessionCPNHibernate14);

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

                // PRUEBA 2: Lector sigue a otro autor (Caso Correcto)
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

                // PRUEBA 3: Intentar seguir al mismo autor otra vez (Caso Incorrecto)
                Console.WriteLine ("\n------------------ Prueba 3: Intentar seguir al mismo Autor otra vez (Resultado esperado: Incorrecto) ------------------");

                SessionCPNHibernate sessionCPNHibernate15 = new SessionCPNHibernate ();
                LectorCP lectorCP9 = new LectorCP (sessionCPNHibernate15);

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

                // PRUEBA 4: Intentar seguir a un autor inexistente (Caso Incorrecto)
                Console.WriteLine ("\n------------------ Prueba 4: Intentar seguir a Autor inexistente (Resultado esperado: Incorrecto) ------------------");

                SessionCPNHibernate sessionCPNHibernate16 = new SessionCPNHibernate ();
                LectorCP lectorCP10 = new LectorCP (sessionCPNHibernate16);

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

                // PRUEBA 5: Dejar de seguir a un autor que se está siguiendo (Caso Correcto)
                Console.WriteLine ("\n------------------ Prueba 5: Dejar de seguir a Autor (Resultado esperado: Correcto) ------------------");

                SessionCPNHibernate sessionCPNHibernate17 = new SessionCPNHibernate ();
                LectorCP lectorCP11 = new LectorCP (sessionCPNHibernate17);

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

                // PRUEBA 6: Intentar dejar de seguir a un autor que NO se está siguiendo (Caso Incorrecto)
                Console.WriteLine ("\n------------------ Prueba 6: Intentar dejar de seguir a Autor que NO se sigue (Resultado esperado: Incorrecto) ------------------");

                SessionCPNHibernate sessionCPNHibernate18 = new SessionCPNHibernate ();
                LectorCP lectorCP12 = new LectorCP (sessionCPNHibernate18);

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

                // PRUEBA 7: Intentar dejar de seguir a un autor inexistente (Caso Incorrecto)
                Console.WriteLine ("\n------------------ Prueba 7: Intentar dejar de seguir a Autor inexistente (Resultado esperado: Incorrecto) ------------------");

                SessionCPNHibernate sessionCPNHibernate19 = new SessionCPNHibernate ();
                LectorCP lectorCP13 = new LectorCP (sessionCPNHibernate19);

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
                Console.WriteLine ("====================================================================================\n");

                Console.WriteLine ("\n\n==================== PRUEBAS DE CAMBIO DE CONTRASEÑA DE USUARIO ====================");

                // PRUEBA 1: Cambiar contraseña correctamente (Caso Correcto)
                Console.WriteLine ("\n------------------ Prueba 1: Cambiar contraseña correctamente (Resultado esperado: Correcto) ------------------");

                SessionCPNHibernate sessionCPNHibernate20 = new SessionCPNHibernate ();
                UsuarioCP usuarioCP20 = new UsuarioCP (sessionCPNHibernate20);

                try
                {
                        usuarioCP20.CambiarPassword (usuarioId1, "passPaco", "passPacoLector");
                        Console.WriteLine ("Contraseña cambiada correctamente");
                        Console.WriteLine ("  - Contraseña antigua: 'passPaco'");
                        Console.WriteLine ("  - Contraseña nueva: 'passPacoLector'");
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
                        usuarioCP21.CambiarPassword (usuarioId1, "passwordIncorrecto", "otroPassword123");
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
                        usuarioCP23.CambiarPassword (usuarioId1, "", "nuevaPassword123");
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

                SessionCPNHibernate sessionCPNHibernate22 = new SessionCPNHibernate ();
                LectorCP lectorCP22 = new LectorCP (sessionCPNHibernate22);
                ClubCP clubCP1 = new ClubCP (sessionCPNHibernate22);

                // Suscribir usuarioId2 al club
                try
                {
                        lectorCP22.SuscribirLectorAClub (usuarioId2, new List<int> { clubId1 });
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
                        clubCP1.ExpulsarUsuarioClub (clubId1, usuarioId2);
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
                        clubCP1.ExpulsarUsuarioClub (clubId1, usuarioId2);
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
                        clubCP1.ExpulsarUsuarioClub (99999, usuarioId2);
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
                LectorCP lectorCP16 = new LectorCP (sessionCPNHibernate3);

                // Suscribir usuarioId2 (Marina Lectora)
                try
                {
                        lectorCP16.SuscribirLectorAClub (usuarioId2, new List<int> { clubId1 });
                        Console.WriteLine ("Usuario 2 (Marina Lectora) suscrito al Club de Ciencia Ficción");
                }
                catch (ModelException ex)
                {
                        Console.WriteLine ("Usuario 2 ya estaba suscrito o error: " + ex.Message);
                }

                // PRUEBA 1: Obtener lista de miembros del Club de Ciencia Ficción
                Console.WriteLine ("\n------------------ Prueba 1: Obtener lista de miembros del Club de Ciencia Ficción ------------------");

                IList<LectorEN> miembrosClub1 = clubCP1.ObtenerListaMiembros (clubId1);

                if (miembrosClub1 != null && miembrosClub1.Count > 0) {
                        Console.WriteLine ("Total de miembros en el Club de Ciencia Ficción: " + miembrosClub1.Count);
                        Console.WriteLine ("\nListado de miembros:");
                        for (int i = 0; i < miembrosClub1.Count; i++) {
                                Console.WriteLine ("  " + (i + 1) + ". ID: " + miembrosClub1 [i].Id + " - Nombre: " + miembrosClub1 [i].NombreUsuario + " - Email: " + miembrosClub1 [i].Email);
                        }
                }
                else {
                        Console.WriteLine ("El club no tiene miembros o la lista es nula");
                }

                // PRUEBA 2: Obtener lista de miembros del Club de Misterio y Suspense (Vacío)
                Console.WriteLine ("\n------------------ Prueba 2: Obtener lista de miembros del Club de Misterio y Suspense (Vacío) ------------------");

                IList<LectorEN> miembrosClub2 = clubCP1.ObtenerListaMiembros (clubId2);

                if (miembrosClub2 != null && miembrosClub2.Count > 0) {
                        Console.WriteLine ("Total de miembros en el Club de Misterio y Suspense: " + miembrosClub2.Count);
                        Console.WriteLine ("\nListado de miembros:");
                        for (int i = 0; i < miembrosClub2.Count; i++) {
                                Console.WriteLine ("  " + (i + 1) + ". ID: " + miembrosClub2 [i].Id + " - Nombre: " + miembrosClub2 [i].NombreUsuario);
                        }
                }
                else {
                        Console.WriteLine ("El Club de Misterio y Suspense no tiene miembros (lista vacía o nula)");
                }

                // PRUEBA 3: Intentar obtener lista de miembros de un club que no existe
                Console.WriteLine ("\n------------------ Prueba 3: Obtener lista de miembros de un club inexistente (Resultado esperado: Incorrecto) ------------------");

                try
                {
                        IList<LectorEN> miembrosClubInexistente = clubCP1.ObtenerListaMiembros (99999);
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
                Console.WriteLine ("  - Se listan todos los usuarios miembros del Club de Ciencia Ficción");
                Console.WriteLine ("  - Se muestran sus IDs, nombres de usuario y emails");
                Console.WriteLine ("");
                Console.WriteLine ("Prueba 2 - Obtener miembros de club sin usuarios suscritos");
                Console.WriteLine ("  - Se verifica correctamente el estado del Club de Misterio y Suspense");
                Console.WriteLine ("");
                Console.WriteLine ("Prueba 3 - Intentar obtener miembros de club inexistente");
                Console.WriteLine ("  - Se valida correctamente que el club no existe");
                Console.WriteLine ("  - Se captura la excepción esperada");
                Console.WriteLine ("====================================================================================");

                /////////////////////////////////////////////////////////////////////////////////////////////////////

                Console.WriteLine ("\n\n\n====================================================================================");
                Console.WriteLine ("====================================================================================");
                Console.WriteLine ("====================================================================================");

                Console.WriteLine ("\n\n==================== PRUEBAS DE EventoCEN - ObtenerListaParticipantes() ====================");

                // PREPARACIÓN: Inscribir varios usuarios al evento para tener participantes
                Console.WriteLine ("\n------------------ Preparación: Inscribir usuarios al evento 'Club de Lectura de Novela Negra' ------------------");

                SessionCPNHibernate sessionCPNHibernate12 = new SessionCPNHibernate ();
                LectorCP lectorCP14 = new LectorCP (sessionCPNHibernate3);

                // Inscribir usuarioId2 (Marina Lectora) al evento
                try
                {
                        lectorCP14.InscribirLectorAEvento (usuarioId2, new List<int> { eventoId1 });
                        Console.WriteLine ("Usuario 2 (Marina Lectora) inscrito al evento \n");
                }
                catch (ModelException ex)
                {
                        Console.WriteLine ("Usuario 2 ya estaba inscrito o error: " + ex.Message);
                }

                // Inscribir usuarioId3 (Niko Lector) al evento
                try
                {
                        lectorCP14.InscribirLectorAEvento (usuarioId3, new List<int> { eventoId1 });
                        Console.WriteLine ("Usuario 3 (Niko Lector) inscrito al evento");
                }
                catch (ModelException ex)
                {
                        Console.WriteLine ("Usuario 3 ya estaba inscrito o error: " + ex.Message);
                }

                // PRUEBA 1: Obtener lista de participantes del evento 'Club de Lectura de Novela Negra'
                Console.WriteLine ("\n------------------ Prueba 1: Obtener lista de participantes del evento 'Club de Lectura de Novela Negra' ------------------");

                SessionCPNHibernate sessionCPNHibernate001 = new SessionCPNHibernate ();
                EventoCP eventoCP = new EventoCP (sessionCPNHibernate001);

                IList<UsuarioEN> participantesEvento1 = eventoCP.ObtenerListaParticipantes (eventoId1);

                if (participantesEvento1 != null && participantesEvento1.Count > 0) {
                        Console.WriteLine ("Total de participantes en el evento: " + participantesEvento1.Count);
                        Console.WriteLine ("\nListado de participantes:");
                        for (int i = 0; i < participantesEvento1.Count; i++) {
                                Console.WriteLine ("  " + (i + 1) + ". ID: " + participantesEvento1 [i].Id + " - Nombre: " + participantesEvento1 [i].NombreUsuario + " - Email: " + participantesEvento1 [i].Email);
                        }
                }
                else {
                        Console.WriteLine ("El evento no tiene participantes o la lista es nula");
                }

                // PRUEBA 2: Obtener lista de participantes del evento vacío (Evento5)
                Console.WriteLine ("\n------------------ Prueba 2: Obtener lista de participantes del Evento5 (Sin participantes) ------------------");

                IList<UsuarioEN> participantesEvento5 = eventoCP.ObtenerListaParticipantes (eventoId6);

                if (participantesEvento5 != null && participantesEvento5.Count > 0) {
                        Console.WriteLine ("Total de participantes en Evento5: " + participantesEvento5.Count);
                        Console.WriteLine ("\nListado de participantes:");
                        for (int i = 0; i < participantesEvento5.Count; i++) {
                                Console.WriteLine ("  " + (i + 1) + ". ID: " + participantesEvento5 [i].Id + " - Nombre: " + participantesEvento5 [i].NombreUsuario);
                        }
                }
                else {
                        Console.WriteLine ("El Evento5 no tiene participantes (lista vacía o nula)");
                }

                // PRUEBA 3: Intentar obtener lista de participantes de un evento que no existe
                Console.WriteLine ("\n------------------ Prueba 3: Obtener lista de participantes de un evento inexistente (Resultado esperado: Incorrecto) ------------------");

                try
                {
                        IList<UsuarioEN> participantesEventoInexistente = eventoCP.ObtenerListaParticipantes (99999);
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
                Console.WriteLine ("  - Se listan todos los usuarios participantes del evento");
                Console.WriteLine ("  - Se muestran sus IDs, nombres de usuario y emails");
                Console.WriteLine ("");
                Console.WriteLine ("Prueba 2 - Obtener participantes de evento sin usuarios inscritos");
                Console.WriteLine ("  - Se verifica correctamente que el evento no tiene participantes");
                Console.WriteLine ("");
                Console.WriteLine ("Prueba 3 - Intentar obtener participantes de evento inexistente");
                Console.WriteLine ("  - Se valida correctamente que el evento no existe");
                Console.WriteLine ("  - Se captura la excepción esperada");
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

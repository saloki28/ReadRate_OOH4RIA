
using System;
using System.Text;

using System.Collections.Generic;
using ReadRate_e4Gen.ApplicationCore.Exceptions;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4;



/*PROTECTED REGION ID(usingReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4_Libro_crearLibro) ENABLED START*/
using System.Linq;
/*PROTECTED REGION END*/

namespace ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4
{
public partial class LibroCP : GenericBasicCP
{
public ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LibroEN CrearLibro (string p_titulo, string p_genero, int p_edadRecomendada, Nullable<DateTime> p_fechaPublicacion, int p_numPags, string p_sinopsis, string p_fotoPortada, int p_autorPublicador, float p_valoracionMedia)
{
        /*PROTECTED REGION ID(ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4_Libro_crearLibro) ENABLED START*/

        LibroCEN libroCEN = null;

        ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LibroEN result = null;


        try
        {
                CPSession.SessionInitializeTransaction ();
                libroCEN = new LibroCEN (CPSession.UnitRepo.LibroRepository);
                var autorRepo = CPSession.UnitRepo.AutorRepository;

                int oid;
                // Inicializar LibroEN
                LibroEN libroEN = new LibroEN ();
                libroEN.Titulo = p_titulo;
                libroEN.Genero = p_genero;
                libroEN.EdadRecomendada = p_edadRecomendada;
                libroEN.FechaPublicacion = p_fechaPublicacion ?? DateTime.Now;

                if (p_numPags <= 0) {
                        throw new ModelException ("El numero de paginas debe ser mayor que 0");
                }

                libroEN.NumPags = p_numPags;
                libroEN.Sinopsis = p_sinopsis;
                libroEN.FotoPortada = p_fotoPortada;

                if (p_autorPublicador != -1) {
                        libroEN.AutorPublicador = new AutorEN ();
                        libroEN.AutorPublicador.Id = p_autorPublicador;
                }
                else{
                        throw new ModelException ("Un libro debe estar asociado a un autor");
                }

                libroEN.ValoracionMedia = p_valoracionMedia;

                // Guardar el libro
                oid = libroCEN.get_ILibroRepository ().CrearLibro (libroEN);
                result = libroCEN.get_ILibroRepository ().ReadOIDDefault (oid);

                // Actualizar el autor: +1 a CantidadLibrosPublicados y recalcular ValoracionMedia
                var autor = autorRepo.ReadOIDDefault (p_autorPublicador);
                if (autor != null) {
                        autor.CantidadLibrosPublicados = autor.CantidadLibrosPublicados + 1;

                        // Obtener todos los libros del autor
                        var librosAutor = libroCEN.get_ILibroRepository ().DameTodosLibros (0, int.MaxValue)
                                          .Where (l => l.AutorPublicador != null && l.AutorPublicador.Id == autor.Id)
                                          .ToList ();

                        // Calcular la valoración media del autor
                        if (librosAutor.Count > 0)
                                autor.ValoracionMedia = (float)librosAutor.Average (l => l.ValoracionMedia);
                        else
                                autor.ValoracionMedia = 0;

                        autorRepo.ModificarAutor (autor);
                }

                CPSession.Commit ();
        }
        catch (Exception)
        {
                CPSession.RollBack ();
                throw;
        }
        finally
        {
                CPSession.SessionClose ();
        }
        return result;


        /*PROTECTED REGION END*/
}
}
}

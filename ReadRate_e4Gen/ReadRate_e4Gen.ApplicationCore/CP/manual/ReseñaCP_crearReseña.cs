
using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using ReadRate_e4Gen.ApplicationCore.Exceptions;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4;
using System.Linq;



/*PROTECTED REGION ID(usingReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4_Reseña_crearReseña) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4
{
public partial class ReseñaCP : GenericBasicCP
{
public ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ReseñaEN CrearReseña (string p_textoOpinion, float p_valoracion, int p_lectorValorador, int p_libroReseñado, Nullable<DateTime> p_fecha)
{
        /*PROTECTED REGION ID(ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4_Reseña_crearReseña) ENABLED START*/

        ReseñaCEN reseñaCEN = null;

        ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ReseñaEN result = null;


        try
        {
                CPSession.SessionInitializeTransaction ();
                reseñaCEN = new  ReseñaCEN (CPSession.UnitRepo.ReseñaRepository);
                var libroRepo = CPSession.UnitRepo.LibroRepository;




                int oid;
                //Initialized ReseñaEN
                ReseñaEN reseñaEN;
                reseñaEN = new ReseñaEN ();
                reseñaEN.TextoOpinion = p_textoOpinion;

                reseñaEN.Valoracion = p_valoracion;

                if (p_lectorValorador != -1) {
                        reseñaEN.LectorValorador = new ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN ();
                        reseñaEN.LectorValorador.Id = p_lectorValorador;
                }
                else{
                        throw new ModelException ("Una resña debe tener un lector valorador asociado");
                }


                if (p_libroReseñado != -1) {
                        reseñaEN.LibroReseñado = new ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LibroEN ();
                        reseñaEN.LibroReseñado.Id = p_libroReseñado;
                }
                else{
                        throw new ModelException ("Una reseña debe tener un libro reseñado asociado");
                }

                reseñaEN.Fecha = p_fecha ?? DateTime.Now;



                oid = reseñaCEN.get_IReseñaRepository ().CrearReseña (reseñaEN);

                result = reseñaCEN.get_IReseñaRepository ().ReadOIDDefault (oid);

                // Actualizar el libro, recalcular ValoracionMedia
                var libro = libroRepo.ReadOIDDefault (p_libroReseñado);
                if (libro != null) {
                        // Obtener todas las reseñas del libro
                        var reseñasLibro = reseñaCEN.get_IReseñaRepository ().DameTodosReseñas (0, int.MaxValue)
                                           .Where (r => r.LibroReseñado != null && r.LibroReseñado.Id == libro.Id)
                                           .ToList ();

                        // Calcular la valoración media del libro
                        if (reseñasLibro.Count > 0)
                                libro.ValoracionMedia = (float)reseñasLibro.Average (r => r.Valoracion);
                        else
                                libro.ValoracionMedia = 0;

                        libroRepo.ModificarLibro (libro);

                        // Actualizar el autor, recalcular ValoracionMedia
                        // Verificar que el libro tenga un autor asignado
                        if (libro.AutorPublicador != null) {
                                var autorRepo = CPSession.UnitRepo.AutorRepository;
                                var autor = autorRepo.ReadOIDDefault (libro.AutorPublicador.Id);

                                if (autor != null) {
                                        // Obtener todos los libros del autor
                                        var librosAutor = libroRepo.DameTodosLibros (0, int.MaxValue)
                                                          .Where (l => l.AutorPublicador != null && l.AutorPublicador.Id == autor.Id)
                                                          .ToList ();

                                        // Calcular la valoración media del autor
                                        if (librosAutor.Count > 0)
                                                autor.ValoracionMedia = (float)librosAutor.Average (l => l.ValoracionMedia);
                                        else
                                                autor.ValoracionMedia = 0;

                                        autorRepo.ModificarAutor (autor);
                                }
                        }
                }

                CPSession.Commit ();
        }
        catch (Exception ex)
        {
                CPSession.RollBack ();
                throw ex;
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

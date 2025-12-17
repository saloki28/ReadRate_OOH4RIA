using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using ReadRate_e4Gen.ApplicationCore.Exceptions;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4;

/*PROTECTED REGION ID(usingReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4_Autor_eliminarAutor) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4
{
    public partial class AutorCP : GenericBasicCP
    {
        public void EliminarAutor(int p_Autor_OID)
        {
            /*PROTECTED REGION ID(ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4_Autor_eliminarAutor) ENABLED START*/

            // Defino los CENs necesarios para poder eliminar el autor
            AutorCEN autorCEN = null;
            LectorCEN lectorCEN = null;
            LibroCEN libroCEN = null;
            ReseñaCEN reseñaCEN = null;

            try
            {
                CPSession.SessionInitializeTransaction();

                System.Diagnostics.Debug.WriteLine($"=== INICIO ELIMINACIÓN AUTOR {p_Autor_OID} ===");

                autorCEN = new AutorCEN(CPSession.UnitRepo.AutorRepository);
                AutorEN autor = autorCEN.DameAutorPorOID(p_Autor_OID); // Obtener el autor a eliminar

                // Verificar si el autor existe
                if (autor == null)
                {
                    throw new ModelException($"El autor con ID {p_Autor_OID} no existe.");
                }

                System.Diagnostics.Debug.WriteLine($"Autor encontrado: {autor.NombreUsuario}");

                // ============================================================
                // ELIMINAR LIBROS PUBLICADOS POR EL AUTOR
                // ============================================================
                libroCEN = new LibroCEN(CPSession.UnitRepo.LibroRepository);
                var todosLibros = libroCEN.DameTodosLibros(0, -1);
                var librosDelAutor = todosLibros.Where(l => l.AutorPublicador != null && l.AutorPublicador.Id == p_Autor_OID).ToList();

                if (librosDelAutor.Count > 0)
                {
                    System.Diagnostics.Debug.WriteLine($"Eliminando {librosDelAutor.Count} libro(s) publicado(s) por el autor...");

                    lectorCEN = new LectorCEN(CPSession.UnitRepo.LectorRepository);
                    reseñaCEN = new ReseñaCEN(CPSession.UnitRepo.ReseñaRepository);

                    foreach (var libro in librosDelAutor)
                    {
                        System.Diagnostics.Debug.WriteLine($"  Procesando libro ID: {libro.Id} - '{libro.Titulo}'");

                        // ============================================================
                        // 1.1. Eliminar RESEÑAS de libro
                        // ============================================================

                        var todasReseñas = reseñaCEN.DameTodosReseñas(0, -1);
                        var reseñasDelLibro = todasReseñas.Where(r => r.LibroReseñado?.Id == libro.Id).ToList();

                        if (reseñasDelLibro.Count > 0)
                        {
                            System.Diagnostics.Debug.WriteLine($"    Eliminando {reseñasDelLibro.Count} reseña(s) del libro...");

                            foreach (var reseña in reseñasDelLibro)
                            {
                                reseñaCEN.EliminarReseña(reseña.Id);
                            }
                            System.Diagnostics.Debug.WriteLine($"    ✅ {reseñasDelLibro.Count} reseña(s) eliminada(s)");
                        }

                        // 1.2. DESASIGNAR LIBROS DE LISTAS
                        var todosLectores = lectorCEN.DameTodosLectores(0, -1); // Obtener todos los lectores para desasignar el libro de sus listas

                        foreach (var lector in todosLectores)
                        {
                            // Libros Guardados
                            if (lector.LibroLeido != null && lector.LibroLeido.Any(l => l.Id == libro.Id))
                            {
                                lectorCEN.get_ILectorRepository().DesasignarLibroListaGuardados(lector.Id, new List<int> { libro.Id });

                                System.Diagnostics.Debug.WriteLine($"    Libro desasignado de lista guardados del lector {lector.Id}");
                            }

                            // Libros En Curso
                            if (lector.LibroEnCurso != null && lector.LibroEnCurso.Any(l => l.Id == libro.Id))
                            {
                                lectorCEN.get_ILectorRepository().DesasignarLibroListaEnCurso(lector.Id, new List<int> { libro.Id });

                                System.Diagnostics.Debug.WriteLine($"    Libro desasignado de lista en curso del lector {lector.Id}");
                            }
                        }

                        // 1.3. ELIMINAR LIBRO
                        libroCEN.EliminarLibro(libro.Id);
                        System.Diagnostics.Debug.WriteLine($"  ✅ Libro '{libro.Titulo}' eliminado");
                    }

                    System.Diagnostics.Debug.WriteLine($"✅ {librosDelAutor.Count} libro(s) eliminado(s)");
                }

                // ============================================================
                // DESVINCULAR LECTORES QUE SIGUEN AL AUTOR
                // ============================================================
                if (autor.LectorSeguidor != null && autor.LectorSeguidor.Count > 0)
                {
                    System.Diagnostics.Debug.WriteLine($"Desvinculando {autor.LectorSeguidor.Count} lector(es) que siguen al autor...");

                    if (lectorCEN == null)
                    {
                        lectorCEN = new LectorCEN(CPSession.UnitRepo.LectorRepository); // Inicializar LectorCEN para obtener lectores
                    }

                    List<int> lectoresIds = autor.LectorSeguidor.Select(l => l.Id).ToList();

                    foreach (int lectorId in lectoresIds)
                    {
                        lectorCEN.get_ILectorRepository().DejarDeSeguirAutor(lectorId, new List<int> { p_Autor_OID });
                    }

                    System.Diagnostics.Debug.WriteLine($"✅ {lectoresIds.Count} lector(es) dejaron de seguir al autor");
                }

                // ============================================================
                // DESINSCRIBIR AUTOR DE EVENTOS
                // ============================================================
                if (autor.EventoAutor != null && autor.EventoAutor.Count > 0)
                {
                    System.Diagnostics.Debug.WriteLine($"Desinscribiendo autor de {autor.EventoAutor.Count} evento(s)...");

                    List<int> eventosIds = new List<int>();

                    foreach (EventoEN evento in autor.EventoAutor)
                    {
                        eventosIds.Add(evento.Id);
                    }

                    if (eventosIds.Count > 0)
                    {
                        autorCEN.get_IAutorRepository().DesinscribirAutorDeEvento(p_Autor_OID, eventosIds);
                    }

                    System.Diagnostics.Debug.WriteLine($"✅ Desinscrito de {eventosIds.Count} evento(s)");
                }

                // ============================================================
                // ELIMINAR NOTIFICACIONES DEL AUTOR
                // ============================================================
                if (autor.NotificacionAutor != null && autor.NotificacionAutor.Count > 0)
                {
                    System.Diagnostics.Debug.WriteLine($"Eliminando {autor.NotificacionAutor.Count} notificación(es) del autor...");

                    NotificacionCEN notificacionCEN = new NotificacionCEN(CPSession.UnitRepo.NotificacionRepository);
                    List<int> notificacionesIds = autor.NotificacionAutor.Select(n => n.Id).ToList();

                    foreach (int notifId in notificacionesIds)
                    {
                        notificacionCEN.EliminarNotificacion(notifId);
                    }

                    System.Diagnostics.Debug.WriteLine($"✅ {notificacionesIds.Count} notificación(es) eliminada(s)");
                }

                // ============================================================
                // FINALMENTE: ELIMINAR EL AUTOR
                // ============================================================
                System.Diagnostics.Debug.WriteLine($"Eliminando autor {autor.NombreUsuario}...");
                autorCEN.get_IAutorRepository().EliminarAutor(p_Autor_OID);
                System.Diagnostics.Debug.WriteLine($"✅ Autor eliminado correctamente");

                CPSession.Commit();
                System.Diagnostics.Debug.WriteLine($"=== FIN ELIMINACIÓN AUTOR {p_Autor_OID} ===");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ Error en EliminarAutor: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"StackTrace: {ex.StackTrace}");
                CPSession.RollBack();
                throw;
            }
            finally
            {
                CPSession.SessionClose();
            }


            /*PROTECTED REGION END*/
        }
    }
}

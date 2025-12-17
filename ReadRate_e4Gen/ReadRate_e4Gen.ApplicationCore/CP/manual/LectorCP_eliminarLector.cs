
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using ReadRate_e4Gen.ApplicationCore.Exceptions;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4;

/*PROTECTED REGION ID(usingReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4_Lector_eliminarLector) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4
{
    public partial class LectorCP : GenericBasicCP
    {
        public void EliminarLector(int p_Lector_OID)
        {
            /*PROTECTED REGION ID(ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4_Lector_eliminarLector) ENABLED START*/

            // Defino los CENs necesarios para poder eliminar el lector
            LectorCEN lectorCEN = null;
            AutorCEN autorCEN = null;
            ClubCEN clubCEN = null;
            MensajeCEN mensajeCEN = null;
            ReseñaCEN reseñaCEN = null;

            try
            {
                CPSession.SessionInitializeTransaction();

                lectorCEN = new LectorCEN(CPSession.UnitRepo.LectorRepository);
                LectorEN lector = lectorCEN.DameLectorPorOID(p_Lector_OID); // Obtener el lector a eliminar

                // Verificar si el lector existe
                if (lector == null)
                {
                    throw new ModelException("----------------El lector con ID {p_Lector_OID} no existe. --------------------");
                }


                // ============================================================
                // ELIMINAR CLUBES --> LECTOR PROPIETARIO
                // ============================================================

                if (lector.ClubCreado != null && lector.ClubCreado.Count > 0)
                {

                    clubCEN = new ClubCEN(CPSession.UnitRepo.ClubRepository);
                    mensajeCEN = new MensajeCEN(CPSession.UnitRepo.MensajeRepository);

                    // Nueva lista para evitar modificación durante la iteración
                    List<int> clubsIds = lector.ClubCreado.Select(c => c.Id).ToList();

                    foreach (int clubId in clubsIds)
                    {

                        ClubEN club = clubCEN.DameClubPorOID(clubId);

                        if (club != null)
                        {
                            // 1.1. Eliminar todos sus MENSAJES dentro del club del que es propietario
                            var todosMensajes = mensajeCEN.DameTodosMensajes(0, -1);
                            var mensajesDelClub = todosMensajes.Where(m => m.Club?.Id == clubId).ToList();

                            foreach (var mensaje in mensajesDelClub)
                            {
                                mensajeCEN.EliminarMensaje(mensaje.Id);
                            }

                            // 1.2. Desvincular todos los MIEMBROS del club, ya que el club va a ser eliminado
                            if (club.LectorMiembro != null && club.LectorMiembro.Count > 0)
                            {
                                List<int> miembrosIds = club.LectorMiembro.Select(m => m.Id).ToList();

                                foreach (int miembroId in miembrosIds)
                                {
                                    if (miembroId != p_Lector_OID)
                                    {
                                        lectorCEN.get_ILectorRepository().DesuscribirLectorDeClub(miembroId, new List<int> { clubId });
                                    }
                                }

                            }

                            // 1.3. Desvincular al PROPIETARIO
                            club.LectorPropietario = null;

                            // 1.4. ELIMINAR CLUB al final, ya que si no hay propietario no hay club
                            clubCEN.EliminarClub(clubId);
                        }
                    }
                }

                // ============================================================
                // DESUSCRIBIR DE CLUBES DONDE ES MIEMBRO (No propietario)
                // ============================================================
                if (lector.ClubSuscritoLector != null && lector.ClubSuscritoLector.Count > 0)
                {


                    // Obtengo los IDs de los clubes a los que está suscrito el lector
                    List<int> clubsIds = lector.ClubSuscritoLector.Select(c => c.Id).ToList();

                    foreach (int clubId in clubsIds)
                    {
                        lectorCEN.get_ILectorRepository().DesuscribirLectorDeClub(p_Lector_OID, new List<int> { clubId });
                    }
                }

                // ============================================================
                // DESINSCRIBIR DE EVENTOS
                // ============================================================
                if (lector.EventoLector != null && lector.EventoLector.Count > 0)
                {

                    // Obtengo los IDs de los eventos a los que está inscrito el lector
                    List<int> eventosIds = lector.EventoLector.Select(e => e.Id).ToList();

                    foreach (int eventoId in eventosIds)
                    {
                        lectorCEN.get_ILectorRepository().DesinscribirLectorDeEvento(p_Lector_OID, new List<int> { eventoId });
                    }
                }

                // ============================================================
                // DEJAR DE SEGUIR AUTORES
                // ============================================================
                if (lector.AutorSeguido != null && lector.AutorSeguido.Count > 0)
                {

                    // Obtengo los IDs de los autores que el lector sigue
                    List<int> autoresIds = lector.AutorSeguido.Select(a => a.Id).ToList();

                    lectorCEN.get_ILectorRepository().DejarDeSeguirAutor(p_Lector_OID, autoresIds);

                }

                // ============================================================
                // DESASIGNAR LIBROS DE LISTAS
                // ============================================================
                
                // 1.1. Libros Guardados
                if (lector.LibroLeido != null && lector.LibroLeido.Count > 0)
                {

                    // Obtengo los IDs de los libros guardados
                    List<int> librosIds = lector.LibroLeido.Select(l => l.Id).ToList();

                    lectorCEN.get_ILectorRepository().DesasignarLibroListaGuardados(p_Lector_OID, librosIds);

                }

                // 1.2. Libros En Curso
                if (lector.LibroEnCurso != null && lector.LibroEnCurso.Count > 0)
                {
                    // Obtengo los IDs de los libros en curso
                    List<int> librosIds = lector.LibroEnCurso.Select(l => l.Id).ToList();

                    lectorCEN.get_ILectorRepository().DesasignarLibroListaEnCurso(p_Lector_OID, librosIds);

                }

                // ============================================================
                // ELIMINAR TODOS LOS MENSAJES EN TODOS LOS CLUBES
                // ============================================================
                if (mensajeCEN == null)
                {
                    mensajeCEN = new MensajeCEN(CPSession.UnitRepo.MensajeRepository);
                }

                // Obtener todos los mensajes del lector
                var todosMensajesLector = mensajeCEN.DameTodosMensajes(0, -1);
                var mensajesDelLector = todosMensajesLector.Where(m => m.Lector?.Id == p_Lector_OID).ToList();

                if (mensajesDelLector.Count > 0)
                {

                    foreach (var mensaje in mensajesDelLector)
                    {
                        mensajeCEN.EliminarMensaje(mensaje.Id);
                    }
                }

                // ============================================================
                // ELIMINAR RESEÑAS DEL LECTOR
                // ============================================================
                reseñaCEN = new ReseñaCEN(CPSession.UnitRepo.ReseñaRepository);

                // Obtener todas las reseñas del lector
                var todasReseñas = reseñaCEN.DameTodosReseñas(0, -1);
                var reseñasDelLector = todasReseñas.Where(r => r.LectorValorador?.Id == p_Lector_OID).ToList();

                if (reseñasDelLector.Count > 0)
                {

                    foreach (var reseña in reseñasDelLector)
                    {
                        reseñaCEN.EliminarReseña(reseña.Id);
                    }
                }

                // ============================================================
                // ELIMINAR NOTIFICACIONES DEL LECTOR
                // ============================================================
                if (lector.NotificacionLector != null && lector.NotificacionLector.Count > 0)
                {

                    NotificacionCEN notificacionCEN = new NotificacionCEN(CPSession.UnitRepo.NotificacionRepository);

                    // Obtengo los IDs de las notificaciones del lector
                    List<int> notificacionesIds = lector.NotificacionLector.Select(n => n.Id).ToList();

                    foreach (int notifId in notificacionesIds)
                    {
                        notificacionCEN.EliminarNotificacion(notifId);
                    }
                }

                // ============================================================
                // FINALMENTE: ELIMINAR EL LECTOR
                // ============================================================
                lectorCEN.get_ILectorRepository().EliminarLector(p_Lector_OID);

                CPSession.Commit();
            }
            catch (Exception ex)
            {
                CPSession.RollBack();
                throw ex;
            }
            finally
            {
                CPSession.SessionClose();
            }

            /*PROTECTED REGION END*/
        }
    }
}


using System;
using System.Text;

using System.Collections.Generic;
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
public void EliminarAutor (int p_Autor_OID)
{
        /*PROTECTED REGION ID(ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4_Autor_eliminarAutor) ENABLED START*/

        AutorCEN autorCEN = null;
        EventoCEN eventoCEN = null;


        try
        {
                CPSession.SessionInitializeTransaction ();
                autorCEN = new  AutorCEN (CPSession.UnitRepo.AutorRepository);
                AutorEN autor = autorCEN.DameAutorPorOID (p_Autor_OID); // Obtener el autor a eliminar

                //-------------------------------------------------------------------
                List<EventoEN> eventosInscritos = new List<EventoEN>(); // Lista para almacenar los eventos en los que el usuario está inscrito

                if (autor.EventoAutor != null) { // Verificar si el usuario tiene eventos inscritos
                        eventosInscritos = autor.EventoAutor as List<EventoEN>;

                        // Eliminar al usuario de los eventos en los que está inscrito
                        foreach (EventoEN evento in eventosInscritos) {
                                autorCEN.get_IAutorRepository ().DesinscribirAutorDeEvento (p_Autor_OID, new List<int>() {
                                                evento.Id
                                        });
                        }
                }

                //-------------------------------------------------------------------

                autorCEN.get_IAutorRepository ().EliminarAutor (p_Autor_OID); // Eliminar el autor

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


        /*PROTECTED REGION END*/
}
}
}

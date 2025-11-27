
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

                // Verificar si el autor tiene eventos inscritos y desinscribirlo de ellos
                if (autor.EventoAutor != null && autor.EventoAutor.Count > 0) {
                        List<int> eventosIds = new List<int>();

                        foreach (EventoEN evento in autor.EventoAutor) {
                                eventosIds.Add (evento.Id);
                        }

                        if (eventosIds.Count > 0) {
                                autorCEN.get_IAutorRepository ().DesinscribirAutorDeEvento (p_Autor_OID, eventosIds);
                        }
                }

                autorCEN.get_IAutorRepository ().EliminarAutor (p_Autor_OID); // Eliminar el autor

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


        /*PROTECTED REGION END*/
}
}
}

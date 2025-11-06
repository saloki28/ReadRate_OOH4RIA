
using System;
using System.Text;
using System.Collections.Generic;
using ReadRate_e4Gen.ApplicationCore.Exceptions;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;


/*PROTECTED REGION ID(usingReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4_Lector_comprobarSiEstaEnLista) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4
{
public partial class LectorCEN
{
public bool ComprobarSiEstaEnLista (int p_id_libro, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LibroEN> p_lista_libros)
{
        /*PROTECTED REGION ID(ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4_Lector_comprobarSiEstaEnLista) ENABLED START*/

        var result = false;

        foreach (var libro in p_lista_libros) {
                if (libro.Id == p_id_libro) {
                        result = true;
                        break;
                }
        }
        return result;

        /*PROTECTED REGION END*/
}
}
}

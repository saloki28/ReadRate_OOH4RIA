
using System;
using System.Text;
using System.Collections.Generic;
using ReadRate_e4Gen.ApplicationCore.Exceptions;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;


/*PROTECTED REGION ID(usingReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4_Libro_dameCantidadTotalLibros) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4
{
public partial class LibroCEN
{
public int DameCantidadTotalLibros ()
{
        /*PROTECTED REGION ID(ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4_Libro_dameCantidadTotalLibros) ENABLED START*/

        // Obtener todos los libros
        IList<LibroEN> listaLibros = this.DameTodosLibros (0, -1);

        return listaLibros.Count;

        /*PROTECTED REGION END*/
}
}
}

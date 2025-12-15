
using System;
using System.Text;
using System.Collections.Generic;
using ReadRate_e4Gen.ApplicationCore.Exceptions;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;


/*PROTECTED REGION ID(usingReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4_Usuario_dameCantidadTotalUsuarios) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4
{
public partial class UsuarioCEN
{
public int DameCantidadTotalUsuarios ()
{
        /*PROTECTED REGION ID(ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4_Usuario_dameCantidadTotalUsuarios) ENABLED START*/

        // Pedimos todos los usuarios sin limite
        IList<UsuarioEN> listaUsuarios = this.DameTodosUsuarios (0, -1);

        return listaUsuarios.Count;

        /*PROTECTED REGION END*/
}
}
}

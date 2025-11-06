
using System;
using System.Text;
using System.Collections.Generic;
using ReadRate_e4Gen.ApplicationCore.Exceptions;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;


/*PROTECTED REGION ID(usingReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4_Club_obtenerListaMiembros) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4
{
public partial class ClubCEN
{
public System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.UsuarioEN> ObtenerListaMiembros (int p_oid)
{
        /*PROTECTED REGION ID(ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4_Club_obtenerListaMiembros) ENABLED START*/

        ClubCEN clubCEN = new ClubCEN (_IClubRepository);
        ClubEN clubEN = clubCEN.DameClubPorOID (p_oid);

        if (clubEN == null) {
                throw new ModelException ("El club con ID " + p_oid + " no existe");
        }

        return clubEN.UsuarioMiembro;

        /*PROTECTED REGION END*/
}
}
}

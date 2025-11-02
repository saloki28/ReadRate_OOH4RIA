

using System;
using System.Text;
using System.Collections.Generic;

using ReadRate_e4Gen.ApplicationCore.Exceptions;

using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;


namespace ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4
{
/*
 *      Definition of the class ClubCEN
 *
 */
public partial class ClubCEN
{
private IClubRepository _IClubRepository;

public ClubCEN(IClubRepository _IClubRepository)
{
        this._IClubRepository = _IClubRepository;
}

public IClubRepository get_IClubRepository ()
{
        return this._IClubRepository;
}

public void EliminarClub (int id
                          )
{
        _IClubRepository.EliminarClub (id);
}

public ClubEN DameClubPorOID (int id
                              )
{
        ClubEN clubEN = null;

        clubEN = _IClubRepository.DameClubPorOID (id);
        return clubEN;
}

public System.Collections.Generic.IList<ClubEN> DameTodosClubs (int first, int size)
{
        System.Collections.Generic.IList<ClubEN> list = null;

        list = _IClubRepository.DameTodosClubs (first, size);
        return list;
}
public System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ClubEN> DameClubPorFiltros (int first, int size)
{
        return _IClubRepository.DameClubPorFiltros (first, size);
}
}
}

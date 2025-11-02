
using System;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4;

namespace ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4
{
public partial interface IClubRepository
{
void setSessionCP (GenericSessionCP session);

ClubEN ReadOIDDefault (int id
                       );

void ModifyDefault (ClubEN club);

System.Collections.Generic.IList<ClubEN> ReadAllDefault (int first, int size);



int CearClub (ClubEN club);

void ModificarClub (ClubEN club);


void EliminarClub (int id
                   );


ClubEN DameClubPorOID (int id
                       );


System.Collections.Generic.IList<ClubEN> DameTodosClubs (int first, int size);


System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ClubEN> DameClubPorFiltros (int first, int size);
}
}

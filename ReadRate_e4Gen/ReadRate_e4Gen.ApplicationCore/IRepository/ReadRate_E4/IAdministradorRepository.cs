
using System;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4;

namespace ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4
{
public partial interface IAdministradorRepository
{
void setSessionCP (GenericSessionCP session);

AdministradorEN ReadOIDDefault (int id
                                );

void ModifyDefault (AdministradorEN administrador);

System.Collections.Generic.IList<AdministradorEN> ReadAllDefault (int first, int size);



int CrearAdministador (AdministradorEN administrador);

void ModificarAdministador (AdministradorEN administrador);


void EliminarAdministador (int id
                           );


AdministradorEN DameAdministradorPorOID (int id
                                         );


System.Collections.Generic.IList<AdministradorEN> DameTodosAdministradores (int first, int size);
}
}

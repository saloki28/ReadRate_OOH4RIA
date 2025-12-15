
using System;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4;

namespace ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4
{
public partial interface IUsuarioRepository
{
void setSessionCP (GenericSessionCP session);

UsuarioEN ReadOIDDefault (int id
                          );

void ModifyDefault (UsuarioEN usuario);

System.Collections.Generic.IList<UsuarioEN> ReadAllDefault (int first, int size);



int CrearUsuario (UsuarioEN usuario);

void ModificarUsuario (UsuarioEN usuario);


void EliminarUsuario (int id
                      );


UsuarioEN DameUsuarioPorOID (int id
                             );


System.Collections.Generic.IList<UsuarioEN> DameTodosUsuarios (int first, int size);



System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.UsuarioEN> DameUsuarioPorEmail (string p_email);
}
}

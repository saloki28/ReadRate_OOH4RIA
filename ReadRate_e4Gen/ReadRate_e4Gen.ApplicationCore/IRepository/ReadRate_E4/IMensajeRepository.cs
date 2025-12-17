
using System;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4;

namespace ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4
{
public partial interface IMensajeRepository
{
void setSessionCP (GenericSessionCP session);

MensajeEN ReadOIDDefault (int id
                          );

void ModifyDefault (MensajeEN mensaje);

System.Collections.Generic.IList<MensajeEN> ReadAllDefault (int first, int size);



int CrearMensaje (MensajeEN mensaje);

void ModificarMensaje (MensajeEN mensaje);


void EliminarMensaje (int id
                      );


MensajeEN DameMensajePorOID (int id
                             );


System.Collections.Generic.IList<MensajeEN> DameTodosMensajes (int first, int size);
}
}

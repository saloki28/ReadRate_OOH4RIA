
using System;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4;

namespace ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4
{
public partial interface IReseñaRepository
{
void setSessionCP (GenericSessionCP session);

ReseñaEN ReadOIDDefault (int id
                         );

void ModifyDefault (ReseñaEN reseña);

System.Collections.Generic.IList<ReseñaEN> ReadAllDefault (int first, int size);



int CrearReseña (ReseñaEN reseña);

void ModificarReseña (ReseñaEN reseña);


void EliminarReseña (int id
                     );


ReseñaEN DameReseñaPorOID (int id
                           );


System.Collections.Generic.IList<ReseñaEN> DameTodosReseñas (int first, int size);


System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ReseñaEN> DameReseñasOrdenadasPorValoracionDesc (int first, int size);


System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ReseñaEN> DameReseñasOrdenadasPorValoracionAsc (int first, int size);
}
}


using System;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4;

namespace ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4
{
public partial interface ILectorRepository
{
void setSessionCP (GenericSessionCP session);

LectorEN ReadOIDDefault (int id
                         );

void ModifyDefault (LectorEN lector);

System.Collections.Generic.IList<LectorEN> ReadAllDefault (int first, int size);



int CrearLector (LectorEN lector);

void ModificarLector (LectorEN lector);


void EliminarLector (int id
                     );


void SeguirAutor (int p_Lector_OID, System.Collections.Generic.IList<int> p_autorSeguido_OIDs);

void DejarDeSeguirAutor (int p_Lector_OID, System.Collections.Generic.IList<int> p_autorSeguido_OIDs);

LectorEN DameLectorPorOID (int id
                           );


System.Collections.Generic.IList<LectorEN> DameTodosLectores (int first, int size);



void AsignarLibroListaGuardados (int p_Lector_OID, System.Collections.Generic.IList<int> p_libroLeido_OIDs);

void DesasignarLibroListaGuardados (int p_Lector_OID, System.Collections.Generic.IList<int> p_libroLeido_OIDs);

void AsignarLibroListaEnCurso (int p_Lector_OID, System.Collections.Generic.IList<int> p_libroEnCurso_OIDs);

void DesasignarLibroListaEnCurso (int p_Lector_OID, System.Collections.Generic.IList<int> p_libroEnCurso_OIDs);
}
}

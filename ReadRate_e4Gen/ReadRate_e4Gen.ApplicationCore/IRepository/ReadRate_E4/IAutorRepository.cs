
using System;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4;

namespace ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4
{
public partial interface IAutorRepository
{
void setSessionCP (GenericSessionCP session);

AutorEN ReadOIDDefault (int id
                        );

void ModifyDefault (AutorEN autor);

System.Collections.Generic.IList<AutorEN> ReadAllDefault (int first, int size);



int CrearAutor (AutorEN autor);

void ModificarAutor (AutorEN autor);


void EliminarAutor (int id
                    );


AutorEN DameAutorPorOID (int id
                         );


System.Collections.Generic.IList<AutorEN> DameTodosAutores (int first, int size);


void InscribirAutorAEvento (int p_Autor_OID, System.Collections.Generic.IList<int> p_eventoAutor_OIDs);

void DesinscribirAutorDeEvento (int p_Autor_OID, System.Collections.Generic.IList<int> p_eventoAutor_OIDs);
}
}


using System;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4;

namespace ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4
{
public partial interface ILibroRepository
{
void setSessionCP (GenericSessionCP session);

LibroEN ReadOIDDefault (int id
                        );

void ModifyDefault (LibroEN libro);

System.Collections.Generic.IList<LibroEN> ReadAllDefault (int first, int size);



int CrearLibro (LibroEN libro);

void ModificarLibro (LibroEN libro);


void EliminarLibro (int id
                    );


LibroEN DameLibroPorOID (int id
                         );


System.Collections.Generic.IList<LibroEN> DameTodosLibros (int first, int size);


System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LibroEN> DameLibrosPorFiltros (string p_genero, string p_titulo, int? p_edadRecomendada, int? p_numPags, float? p_valoracionMedia, int first, int size);



System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LibroEN> DameLibrosOrdenadosFecha ();
}
}

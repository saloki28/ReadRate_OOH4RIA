
using System;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4;

namespace ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4
{
public partial interface INoticiaRepository
{
void setSessionCP (GenericSessionCP session);

NoticiaEN ReadOIDDefault (int id
                          );

void ModifyDefault (NoticiaEN noticia);

System.Collections.Generic.IList<NoticiaEN> ReadAllDefault (int first, int size);



int CrearNoticia (NoticiaEN noticia);

void ModificarNoticia (NoticiaEN noticia);


void EliminarNoticia (int id
                      );


NoticiaEN DameNoticiaPorOID (int id
                             );


System.Collections.Generic.IList<NoticiaEN> DameTodosNoticias (int first, int size);


System.Collections.Generic.IList<string> DameTodosTitulosNoticias (int first, int size);


System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NoticiaEN> DameNoticiasPorTitulo ();
}
}

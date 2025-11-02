

using System;
using System.Text;
using System.Collections.Generic;

using ReadRate_e4Gen.ApplicationCore.Exceptions;

using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;


namespace ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4
{
/*
 *      Definition of the class NoticiaCEN
 *
 */
public partial class NoticiaCEN
{
private INoticiaRepository _INoticiaRepository;

public NoticiaCEN(INoticiaRepository _INoticiaRepository)
{
        this._INoticiaRepository = _INoticiaRepository;
}

public INoticiaRepository get_INoticiaRepository ()
{
        return this._INoticiaRepository;
}

public void EliminarNoticia (int id
                             )
{
        _INoticiaRepository.EliminarNoticia (id);
}

public NoticiaEN DameNoticiaPorOID (int id
                                    )
{
        NoticiaEN noticiaEN = null;

        noticiaEN = _INoticiaRepository.DameNoticiaPorOID (id);
        return noticiaEN;
}

public System.Collections.Generic.IList<NoticiaEN> DameTodosNoticias (int first, int size)
{
        System.Collections.Generic.IList<NoticiaEN> list = null;

        list = _INoticiaRepository.DameTodosNoticias (first, size);
        return list;
}
public System.Collections.Generic.IList<string> DameTodosTitulosNoticias (int first, int size)
{
        return _INoticiaRepository.DameTodosTitulosNoticias (first, size);
}
public System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NoticiaEN> DameNoticiasPorTitulo ()
{
        return _INoticiaRepository.DameNoticiasPorTitulo ();
}
}
}



using System;
using System.Text;
using System.Collections.Generic;

using ReadRate_e4Gen.ApplicationCore.Exceptions;

using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;


namespace ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4
{
/*
 *      Definition of the class ReseñaCEN
 *
 */
public partial class ReseñaCEN
{
private IReseñaRepository _IReseñaRepository;

public ReseñaCEN(IReseñaRepository _IReseñaRepository)
{
        this._IReseñaRepository = _IReseñaRepository;
}

public IReseñaRepository get_IReseñaRepository ()
{
        return this._IReseñaRepository;
}

public void EliminarReseña (int id
                            )
{
        _IReseñaRepository.EliminarReseña (id);
}

public ReseñaEN DameReseñaPorOID (int id
                                  )
{
        ReseñaEN reseñaEN = null;

        reseñaEN = _IReseñaRepository.DameReseñaPorOID (id);
        return reseñaEN;
}

public System.Collections.Generic.IList<ReseñaEN> DameTodosReseñas (int first, int size)
{
        System.Collections.Generic.IList<ReseñaEN> list = null;

        list = _IReseñaRepository.DameTodosReseñas (first, size);
        return list;
}
public System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ReseñaEN> DameReseñasOrdenadasPorValoracionDesc (int first, int size)
{
        return _IReseñaRepository.DameReseñasOrdenadasPorValoracionDesc (first, size);
}
public System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ReseñaEN> DameReseñasOrdenadasPorValoracionAsc (int first, int size)
{
        return _IReseñaRepository.DameReseñasOrdenadasPorValoracionAsc (first, size);
}
}
}



using System;
using System.Text;
using System.Collections.Generic;

using ReadRate_e4Gen.ApplicationCore.Exceptions;

using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;


namespace ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4
{
/*
 *      Definition of the class EventoCEN
 *
 */
public partial class EventoCEN
{
private IEventoRepository _IEventoRepository;

public EventoCEN(IEventoRepository _IEventoRepository)
{
        this._IEventoRepository = _IEventoRepository;
}

public IEventoRepository get_IEventoRepository ()
{
        return this._IEventoRepository;
}

public void EliminarEvento (int id
                            )
{
        _IEventoRepository.EliminarEvento (id);
}

public EventoEN DameEventoPorOID (int id
                                  )
{
        EventoEN eventoEN = null;

        eventoEN = _IEventoRepository.DameEventoPorOID (id);
        return eventoEN;
}

public System.Collections.Generic.IList<EventoEN> DameTodosEventos (int first, int size)
{
        System.Collections.Generic.IList<EventoEN> list = null;

        list = _IEventoRepository.DameTodosEventos (first, size);
        return list;
}
public System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.EventoEN> DameEventosPorFecha (Nullable<DateTime> p_fecha, int first, int size)
{
        return _IEventoRepository.DameEventosPorFecha (p_fecha, first, size);
}
}
}


using System;
using System.Text;
using System.Collections.Generic;
using ReadRate_e4Gen.ApplicationCore.Exceptions;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;


/*PROTECTED REGION ID(usingReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4_Evento_modificarEvento) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4
{
public partial class EventoCEN
{
public void ModificarEvento (int p_Evento_OID, string p_nombre, string p_foto, string p_descripcion, Nullable<DateTime> p_fecha, Nullable<DateTime> p_hora, string p_ubicacion, int p_aforoMax, int p_aforoActual)
{
        /*PROTECTED REGION ID(ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4_Evento_modificarEvento_customized) START*/

        EventoEN eventoEN = null;

        //Initialized EventoEN
        eventoEN = new EventoEN ();
        eventoEN.Id = p_Evento_OID;
        eventoEN.Nombre = p_nombre;
        eventoEN.Foto = p_foto;
        eventoEN.Descripcion = p_descripcion;
        eventoEN.Fecha = p_fecha;
        eventoEN.Hora = p_hora;
        eventoEN.Ubicacion = p_ubicacion;
        eventoEN.AforoMax = p_aforoMax;
        eventoEN.AforoActual = p_aforoActual;
        //Call to EventoRepository

        _IEventoRepository.ModificarEvento (eventoEN);

        /*PROTECTED REGION END*/
}
}
}

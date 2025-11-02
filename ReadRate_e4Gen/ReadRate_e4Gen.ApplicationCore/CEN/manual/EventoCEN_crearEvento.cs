
using System;
using System.Text;
using System.Collections.Generic;
using ReadRate_e4Gen.ApplicationCore.Exceptions;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;


/*PROTECTED REGION ID(usingReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4_Evento_crearEvento) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4
{
public partial class EventoCEN
{
public int CrearEvento (string p_nombre, string p_foto, string p_descripcion, Nullable<DateTime> p_fecha, Nullable<DateTime> p_hora, string p_ubicacion, int p_aforoMax, int p_administradorEventos, int p_aforoActual)
{
        /*PROTECTED REGION ID(ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4_Evento_crearEvento_customized) START*/

        EventoEN eventoEN = null;

        int oid;

        //Initialized EventoEN
        eventoEN = new EventoEN ();
        eventoEN.Nombre = p_nombre;

        eventoEN.Foto = p_foto;

        eventoEN.Descripcion = p_descripcion;

        eventoEN.Fecha = p_fecha;

        eventoEN.Hora = p_hora;

        eventoEN.Ubicacion = p_ubicacion;

        eventoEN.AforoMax = p_aforoMax;


        if (p_administradorEventos != -1) {
                eventoEN.AdministradorEventos = new ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AdministradorEN ();
                eventoEN.AdministradorEventos.Id = p_administradorEventos;
        }

        eventoEN.AforoActual = p_aforoActual;

        //Call to EventoRepository

        oid = _IEventoRepository.CrearEvento (eventoEN);
        return oid;
        /*PROTECTED REGION END*/
}
}
}

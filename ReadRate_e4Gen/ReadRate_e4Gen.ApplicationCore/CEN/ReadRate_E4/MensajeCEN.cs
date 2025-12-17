

using System;
using System.Text;
using System.Collections.Generic;

using ReadRate_e4Gen.ApplicationCore.Exceptions;

using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;


namespace ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4
{
/*
 *      Definition of the class MensajeCEN
 *
 */
public partial class MensajeCEN
{
private IMensajeRepository _IMensajeRepository;

public MensajeCEN(IMensajeRepository _IMensajeRepository)
{
        this._IMensajeRepository = _IMensajeRepository;
}

public IMensajeRepository get_IMensajeRepository ()
{
        return this._IMensajeRepository;
}

public int CrearMensaje (string p_texto, Nullable<DateTime> p_fecha, int p_lector, int p_club)
{
        MensajeEN mensajeEN = null;
        int oid;

        //Initialized MensajeEN
        mensajeEN = new MensajeEN ();
        mensajeEN.Texto = p_texto;

        mensajeEN.Fecha = p_fecha;


        if (p_lector != -1) {
                // El argumento p_lector -> Property lector es oid = false
                // Lista de oids id
                mensajeEN.Lector = new ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN ();
                mensajeEN.Lector.Id = p_lector;
        }


        if (p_club != -1) {
                // El argumento p_club -> Property club es oid = false
                // Lista de oids id
                mensajeEN.Club = new ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ClubEN ();
                mensajeEN.Club.Id = p_club;
        }



        oid = _IMensajeRepository.CrearMensaje (mensajeEN);
        return oid;
}

public void ModificarMensaje (int p_Mensaje_OID, string p_texto, Nullable<DateTime> p_fecha)
{
        MensajeEN mensajeEN = null;

        //Initialized MensajeEN
        mensajeEN = new MensajeEN ();
        mensajeEN.Id = p_Mensaje_OID;
        mensajeEN.Texto = p_texto;
        mensajeEN.Fecha = p_fecha;
        //Call to MensajeRepository

        _IMensajeRepository.ModificarMensaje (mensajeEN);
}

public void EliminarMensaje (int id
                             )
{
        _IMensajeRepository.EliminarMensaje (id);
}

public MensajeEN DameMensajePorOID (int id
                                    )
{
        MensajeEN mensajeEN = null;

        mensajeEN = _IMensajeRepository.DameMensajePorOID (id);
        return mensajeEN;
}

public System.Collections.Generic.IList<MensajeEN> DameTodosMensajes (int first, int size)
{
        System.Collections.Generic.IList<MensajeEN> list = null;

        list = _IMensajeRepository.DameTodosMensajes (first, size);
        return list;
}
}
}

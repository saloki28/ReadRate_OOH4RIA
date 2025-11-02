
using System;
using System.Text;
using System.Collections.Generic;
using ReadRate_e4Gen.ApplicationCore.Exceptions;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;


/*PROTECTED REGION ID(usingReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4_Reseña_crearReseña) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4
{
public partial class ReseñaCEN
{
public int CrearReseña (string p_textoOpinion, float p_valoracion, int p_lectorValorador, int p_libroReseñado, int p_notificacionReseña, Nullable<DateTime> p_fecha)
{
        /*PROTECTED REGION ID(ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4_Reseña_crearReseña_customized) START*/

        ReseñaEN reseñaEN = null;

        int oid;

        //Initialized ReseñaEN
        reseñaEN = new ReseñaEN ();
        reseñaEN.TextoOpinion = p_textoOpinion;

        reseñaEN.Valoracion = p_valoracion;


        if (p_lectorValorador != -1) {
                reseñaEN.LectorValorador = new ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN ();
                reseñaEN.LectorValorador.Id = p_lectorValorador;
        }


        if (p_libroReseñado != -1) {
                reseñaEN.LibroReseñado = new ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LibroEN ();
                reseñaEN.LibroReseñado.Id = p_libroReseñado;
        }


        if (p_notificacionReseña != -1) {
                reseñaEN.NotificacionReseña = new ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NotificacionEN ();
                reseñaEN.NotificacionReseña.Id = p_notificacionReseña;
        }

        reseñaEN.Fecha = p_fecha;

        //Call to ReseñaRepository

        oid = _IReseñaRepository.CrearReseña (reseñaEN);
        return oid;
        /*PROTECTED REGION END*/
}
}
}

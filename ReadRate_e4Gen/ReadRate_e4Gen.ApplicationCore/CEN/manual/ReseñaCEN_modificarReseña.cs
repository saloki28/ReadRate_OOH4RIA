
using System;
using System.Text;
using System.Collections.Generic;
using ReadRate_e4Gen.ApplicationCore.Exceptions;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;


/*PROTECTED REGION ID(usingReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4_Reseña_modificarReseña) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4
{
public partial class ReseñaCEN
{
public void ModificarReseña (int p_Reseña_OID, string p_textoOpinion, float p_valoracion, Nullable<DateTime> p_fecha)
{
        /*PROTECTED REGION ID(ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4_Reseña_modificarReseña_customized) ENABLED START*/

        // Validar que la valoración esté en el rango válido (De 0 a 5)
        if (p_valoracion < 0) {
                throw new ModelException ("La valoración no puede ser negativa. Debe estar entre 0 y 5.");
        }
        if (p_valoracion > 5) {
                throw new ModelException ("La valoración no puede ser mayor a 5. Debe estar entre 0 y 5.");
        }

        // Validar que la fecha no sea mayor que la fecha actual
        DateTime fechaActual = DateTime.Now;
        DateTime fechaReseña = p_fecha.HasValue ? p_fecha.Value : fechaActual;
        if (fechaReseña > fechaActual) {
                throw new ModelException ("La fecha de la reseña no puede ser mayor que la fecha actual.");
        }

        ReseñaEN reseñaEN = null;
        ReseñaCEN reseñaCEN = new ReseñaCEN (_IReseñaRepository);
        ReseñaEN reseñaFecha = reseñaCEN.DameReseñaPorOID (p_Reseña_OID);
        DateTime fecha = (DateTime)reseñaFecha.Fecha;

        //Initialized ReseñaEN
        reseñaEN = new ReseñaEN ();
        reseñaEN.Id = p_Reseña_OID;
        reseñaEN.TextoOpinion = p_textoOpinion;
        reseñaEN.Valoracion = p_valoracion;
        reseñaEN.Fecha = fecha;

        //Call to ReseñaRepository

        _IReseñaRepository.ModificarReseña (reseñaEN);

        /*PROTECTED REGION END*/
}
}
}

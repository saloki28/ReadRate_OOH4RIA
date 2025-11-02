
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
        /*PROTECTED REGION ID(ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4_Reseña_modificarReseña_customized) START*/

        ReseñaEN reseñaEN = null;

        //Initialized ReseñaEN
        reseñaEN = new ReseñaEN ();
        reseñaEN.Id = p_Reseña_OID;
        reseñaEN.TextoOpinion = p_textoOpinion;
        reseñaEN.Valoracion = p_valoracion;
        reseñaEN.Fecha = p_fecha;
        //Call to ReseñaRepository

        _IReseñaRepository.ModificarReseña (reseñaEN);

        /*PROTECTED REGION END*/
}
}
}

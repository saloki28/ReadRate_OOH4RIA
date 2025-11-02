
using System;
using System.Text;
using System.Collections.Generic;
using ReadRate_e4Gen.ApplicationCore.Exceptions;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;


/*PROTECTED REGION ID(usingReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4_Noticia_modificarNoticia) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4
{
public partial class NoticiaCEN
{
public void ModificarNoticia (int p_Noticia_OID, string p_titulo, Nullable<DateTime> p_fechaPublicacion, string p_foto, string p_textoContenido)
{
        /*PROTECTED REGION ID(ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4_Noticia_modificarNoticia_customized) START*/

        NoticiaEN noticiaEN = null;

        //Initialized NoticiaEN
        noticiaEN = new NoticiaEN ();
        noticiaEN.Id = p_Noticia_OID;
        noticiaEN.Titulo = p_titulo;
        noticiaEN.FechaPublicacion = p_fechaPublicacion;
        noticiaEN.Foto = p_foto;
        noticiaEN.TextoContenido = p_textoContenido;
        //Call to NoticiaRepository

        _INoticiaRepository.ModificarNoticia (noticiaEN);

        /*PROTECTED REGION END*/
}
}
}

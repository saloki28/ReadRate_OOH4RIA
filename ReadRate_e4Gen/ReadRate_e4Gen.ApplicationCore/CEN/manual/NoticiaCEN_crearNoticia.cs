
using System;
using System.Text;
using System.Collections.Generic;
using ReadRate_e4Gen.ApplicationCore.Exceptions;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;


/*PROTECTED REGION ID(usingReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4_Noticia_crearNoticia) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4
{
public partial class NoticiaCEN
{
public int CrearNoticia (string p_titulo, Nullable<DateTime> p_fechaPublicacion, string p_foto, string p_textoContenido, int p_administradorNoticias)
{
        /*PROTECTED REGION ID(ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4_Noticia_crearNoticia_customized) ENABLED START*/

        NoticiaEN noticiaEN = null;

        int oid;

        //Initialized NoticiaEN
        noticiaEN = new NoticiaEN ();
        noticiaEN.Titulo = p_titulo;

        noticiaEN.FechaPublicacion = p_fechaPublicacion;

        noticiaEN.Foto = p_foto;

        noticiaEN.TextoContenido = p_textoContenido;


        if (p_administradorNoticias != -1) {
                noticiaEN.AdministradorNoticias = new ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AdministradorEN ();
                noticiaEN.AdministradorNoticias.Id = p_administradorNoticias;
        }
        else{
                throw new ModelException ("Una noticia debe estar asociada a un administrador");
        }

        oid = _INoticiaRepository.CrearNoticia (noticiaEN);
        return oid;
        /*PROTECTED REGION END*/
}
}
}


using System;
using System.Text;
using System.Collections.Generic;
using ReadRate_e4Gen.ApplicationCore.Exceptions;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;


/*PROTECTED REGION ID(usingReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4_Libro_crearLibro) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4
{
public partial class LibroCEN
{
public int CrearLibro (string p_titulo, string p_genero, int p_edadRecomendada, Nullable<DateTime> p_fechaPublicacion, int p_numPags, string p_sinopsis, string p_fotoPortada, int p_autorPublicador, float p_valoracionMedia)
{
        /*PROTECTED REGION ID(ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4_Libro_crearLibro_customized) START*/

        LibroEN libroEN = null;

        int oid;

        //Initialized LibroEN
        libroEN = new LibroEN ();
        libroEN.Titulo = p_titulo;

        libroEN.Genero = p_genero;

        libroEN.EdadRecomendada = p_edadRecomendada;

        libroEN.FechaPublicacion = p_fechaPublicacion;

        libroEN.NumPags = p_numPags;

        libroEN.Sinopsis = p_sinopsis;

        libroEN.FotoPortada = p_fotoPortada;


        if (p_autorPublicador != -1) {
                libroEN.AutorPublicador = new ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AutorEN ();
                libroEN.AutorPublicador.Id = p_autorPublicador;
        }

        libroEN.ValoracionMedia = p_valoracionMedia;

        //Call to LibroRepository

        oid = _ILibroRepository.CrearLibro (libroEN);
        return oid;
        /*PROTECTED REGION END*/
}
}
}

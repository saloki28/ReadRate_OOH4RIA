
using System;
using System.Text;
using System.Collections.Generic;
using ReadRate_e4Gen.ApplicationCore.Exceptions;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;


/*PROTECTED REGION ID(usingReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4_Libro_modificarLibro) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4
{
public partial class LibroCEN
{
public void ModificarLibro (int p_Libro_OID, string p_titulo, string p_genero, int p_edadRecomendada, Nullable<DateTime> p_fechaPublicacion, int p_numPags, string p_sinopsis, string p_fotoPortada, float p_valoracionMedia)
{
        /*PROTECTED REGION ID(ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4_Libro_modificarLibro_customized) ENABLED START*/

        LibroEN libroEN = null;
        LibroCEN libroCEN = new LibroCEN (_ILibroRepository);
        LibroEN libroFecha = libroCEN.DameLibroPorOID (p_Libro_OID);
        DateTime fecha = (DateTime)libroFecha.FechaPublicacion;

        //Initialized LibroEN
        libroEN = new LibroEN ();
        libroEN.Id = p_Libro_OID;
        libroEN.Titulo = p_titulo;
        libroEN.Genero = p_genero;
        libroEN.EdadRecomendada = p_edadRecomendada;
        libroEN.FechaPublicacion = fecha;

        if (p_numPags <= 0) {
                throw new ModelException ("El numero de paginas debe ser mayor que 0");
        }
        libroEN.NumPags = p_numPags;

        libroEN.Sinopsis = p_sinopsis;
        libroEN.FotoPortada = p_fotoPortada;
        libroEN.ValoracionMedia = p_valoracionMedia;
        //Call to LibroRepository

        _ILibroRepository.ModificarLibro (libroEN);

        /*PROTECTED REGION END*/
}
}
}

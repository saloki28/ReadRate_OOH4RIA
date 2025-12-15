

using System;
using System.Text;
using System.Collections.Generic;

using ReadRate_e4Gen.ApplicationCore.Exceptions;

using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;


namespace ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4
{
/*
 *      Definition of the class LibroCEN
 *
 */
public partial class LibroCEN
{
private ILibroRepository _ILibroRepository;

public LibroCEN(ILibroRepository _ILibroRepository)
{
        this._ILibroRepository = _ILibroRepository;
}

public ILibroRepository get_ILibroRepository ()
{
        return this._ILibroRepository;
}

public void EliminarLibro (int id
                           )
{
        _ILibroRepository.EliminarLibro (id);
}

public LibroEN DameLibroPorOID (int id
                                )
{
        LibroEN libroEN = null;

        libroEN = _ILibroRepository.DameLibroPorOID (id);
        return libroEN;
}

public System.Collections.Generic.IList<LibroEN> DameTodosLibros (int first, int size)
{
        System.Collections.Generic.IList<LibroEN> list = null;

        list = _ILibroRepository.DameTodosLibros (first, size);
        return list;
}
public System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LibroEN> DameLibrosPorFiltros (string p_genero, string p_titulo, int? p_edadRecomendada, int? p_numPags, float? p_valoracionMedia, int first, int size)
{
        return _ILibroRepository.DameLibrosPorFiltros (p_genero, p_titulo, p_edadRecomendada, p_numPags, p_valoracionMedia, first, size);
}
public System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LibroEN> DameLibrosOrdenadosFecha ()
{
        return _ILibroRepository.DameLibrosOrdenadosFecha ();
}
}
}

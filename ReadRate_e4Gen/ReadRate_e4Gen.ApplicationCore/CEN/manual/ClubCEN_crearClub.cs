
using System;
using System.Text;
using System.Collections.Generic;
using ReadRate_e4Gen.ApplicationCore.Exceptions;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;


/*PROTECTED REGION ID(usingReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4_Club_crearClub) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4
{
public partial class ClubCEN
{
public int CrearClub (string p_nombre, string p_enlaceDiscord, int p_miembrosMax, string p_foto, string p_descripcion, ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN p_lectorPropietario, int p_miembrosActuales)
{
        /*PROTECTED REGION ID(ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4_Club_crearClub_customized) START*/

        ClubEN clubEN = null;

        int oid;

        //Initialized ClubEN
        clubEN = new ClubEN ();
        clubEN.Nombre = p_nombre;

        clubEN.EnlaceDiscord = p_enlaceDiscord;

        clubEN.MiembrosMax = p_miembrosMax;

        clubEN.Foto = p_foto;

        clubEN.Descripcion = p_descripcion;

        clubEN.LectorPropietario = p_lectorPropietario;

        clubEN.MiembrosActuales = p_miembrosActuales;

        //Call to ClubRepository

        oid = _IClubRepository.CrearClub (clubEN);
        return oid;
        /*PROTECTED REGION END*/
}
}
}

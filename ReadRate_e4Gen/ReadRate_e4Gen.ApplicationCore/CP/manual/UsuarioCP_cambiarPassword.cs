
using System;
using System.Text;

using System.Collections.Generic;
using ReadRate_e4Gen.ApplicationCore.Exceptions;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4;



/*PROTECTED REGION ID(usingReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4_Usuario_cambiarPassword) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4
{
public partial class UsuarioCP : GenericBasicCP
{
public void CambiarPassword (int p_oid, string p_oldPass, string p_newPass)
{
        /*PROTECTED REGION ID(ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4_Usuario_cambiarPassword) ENABLED START*/

        UsuarioCEN usuarioCEN = null;
        LectorCEN lectorCEN = null;
        AutorCEN autorCEN = null;

        try
        {
                CPSession.SessionInitializeTransaction ();
                usuarioCEN = new UsuarioCEN (CPSession.UnitRepo.UsuarioRepository);
                lectorCEN = new LectorCEN (CPSession.UnitRepo.LectorRepository);
                autorCEN = new AutorCEN (CPSession.UnitRepo.AutorRepository);


                UsuarioEN usuario = usuarioCEN.DameUsuarioPorOID (p_oid);

                if (p_oldPass == null || p_oldPass.Trim () == "" || p_newPass == null || p_newPass.Trim () == "") {
                        throw new ModelException ("Las contrasenas no pueden estar vacias.");
                }

                if (p_oldPass == p_newPass) {
                        throw new ModelException ("La nueva contrasena no puede ser igual a la antigua.");
                }

                string oldHash = Utils.Util.GetEncondeMD5 (p_oldPass);

                if (usuario.Pass != oldHash) {
                        throw new ModelException ("La contraseNa no es valida");
                }

                usuario.Pass = Utils.Util.GetEncondeMD5 (p_newPass);

                //Console.WriteLine("---------------");
                //Console.WriteLine(usuario.GetType().Name);
                //Console.WriteLine(typeof(LectorEN));
                //Console.WriteLine(usuario.GetType().Name == "LectorNH");

                if (usuario.GetType ().Name == "LectorNH") {
                        lectorCEN.get_ILectorRepository ().ModificarLector (usuario as LectorEN);
                }
                else{
                        autorCEN.get_IAutorRepository ().ModificarAutor (usuario as AutorEN);
                }
                //usuarioCEN.get_IUsuarioRepository().ModificarUsuario (usuario);

                CPSession.Commit ();
        }
        catch (Exception ex)
        {
                CPSession.RollBack ();
                throw ex;
        }
        finally
        {
                CPSession.SessionClose ();
        }


        /*PROTECTED REGION END*/
}
}
}

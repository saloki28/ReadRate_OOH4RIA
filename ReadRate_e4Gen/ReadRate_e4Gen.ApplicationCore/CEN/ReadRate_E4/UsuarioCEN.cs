

using System;
using System.Text;
using System.Collections.Generic;

using ReadRate_e4Gen.ApplicationCore.Exceptions;

using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;
using Newtonsoft.Json;


namespace ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4
{
/*
 *      Definition of the class UsuarioCEN
 *
 */
public partial class UsuarioCEN
{
private IUsuarioRepository _IUsuarioRepository;

public UsuarioCEN(IUsuarioRepository _IUsuarioRepository)
{
        this._IUsuarioRepository = _IUsuarioRepository;
}

public IUsuarioRepository get_IUsuarioRepository ()
{
        return this._IUsuarioRepository;
}

private void ModificarUsuario (int p_Usuario_OID, string p_email, string p_nombreUsuario, Nullable<DateTime> p_fechaNacimiento, string p_ciudadResidencia, string p_paisResidencia, string p_foto, ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.RolUsuarioEnum p_rol, String p_pass)
{
        UsuarioEN usuarioEN = null;

        //Initialized UsuarioEN
        usuarioEN = new UsuarioEN ();
        usuarioEN.Id = p_Usuario_OID;
        usuarioEN.Email = p_email;
        usuarioEN.NombreUsuario = p_nombreUsuario;
        usuarioEN.FechaNacimiento = p_fechaNacimiento;
        usuarioEN.CiudadResidencia = p_ciudadResidencia;
        usuarioEN.PaisResidencia = p_paisResidencia;
        usuarioEN.Foto = p_foto;
        usuarioEN.Rol = p_rol;
        usuarioEN.Pass = Utils.Util.GetEncondeMD5 (p_pass);
        //Call to UsuarioRepository

        _IUsuarioRepository.ModificarUsuario (usuarioEN);
}

public UsuarioEN DameUsuarioPorOID (int id
                                    )
{
        UsuarioEN usuarioEN = null;

        usuarioEN = _IUsuarioRepository.DameUsuarioPorOID (id);
        return usuarioEN;
}

public System.Collections.Generic.IList<UsuarioEN> DameTodosUsuarios (int first, int size)
{
        System.Collections.Generic.IList<UsuarioEN> list = null;

        list = _IUsuarioRepository.DameTodosUsuarios (first, size);
        return list;
}
public string Login (int p_Usuario_OID, string p_pass)
{
        string result = null;
        UsuarioEN en = _IUsuarioRepository.ReadOIDDefault (p_Usuario_OID);

        if (en != null && en.Pass.Equals (Utils.Util.GetEncondeMD5 (p_pass)))
                result = this.GetToken (en.Id);

        return result;
}

public System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.UsuarioEN> DameUsuarioPorEmail (string p_email)
{
        return _IUsuarioRepository.DameUsuarioPorEmail (p_email);
}



private string Encode (int id, string email)
{
        var payload = new Dictionary<string, object>(){
                { "id", id }, { "email", email }
        };
        string token = Jose.JWT.Encode (payload, Utils.Util.getKey (), Jose.JwsAlgorithm.HS256);

        return token;
}

public string GetToken (int id)
{
        UsuarioEN en = _IUsuarioRepository.ReadOIDDefault (id);
        string token = Encode (en.Id, en.Email);

        return token;
}
public int CheckToken (string token)
{
        int result = -1;

        try
        {
                string decodedToken = Utils.Util.Decode (token);



                int id = (int)ObtenerID (decodedToken);

                UsuarioEN en = _IUsuarioRepository.ReadOIDDefault (id);

                if (en != null && ((long)en.Id).Equals (ObtenerID (decodedToken))
                    && ((string)en.Email).Equals (ObtenerEMAIL (decodedToken))) {
                        result = id;
                }
                else throw new ModelException ("El token es incorrecto");
        } catch (Exception)
        {
                throw new ModelException ("El token es incorrecto");
        }

        return result;
}


public long ObtenerID (string decodedToken)
{
        try
        {
                Dictionary<string, object> results = JsonConvert.DeserializeObject<Dictionary<string, object> >(decodedToken);
                long id = (long)results ["id"];
                return id;
        }
        catch
        {
                throw new Exception ("El token enviado no es correcto");
        }
}

public string ObtenerEMAIL (string decodedToken)
{
        try
        {
                Dictionary<string, object> results = JsonConvert.DeserializeObject<Dictionary<string, object> >(decodedToken);
                string email = (string)results ["email"];
                return email;
        }
        catch
        {
                throw new Exception ("El token enviado no es correcto");
        }
}
}
}

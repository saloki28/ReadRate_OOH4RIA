

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
 *      Definition of the class AdministradorCEN
 *
 */
public partial class AdministradorCEN
{
private IAdministradorRepository _IAdministradorRepository;

public AdministradorCEN(IAdministradorRepository _IAdministradorRepository)
{
        this._IAdministradorRepository = _IAdministradorRepository;
}

public IAdministradorRepository get_IAdministradorRepository ()
{
        return this._IAdministradorRepository;
}

public int CrearAdministador (string p_nombre, String p_pass, string p_email)
{
        AdministradorEN administradorEN = null;
        int oid;

        //Initialized AdministradorEN
        administradorEN = new AdministradorEN ();
        administradorEN.Nombre = p_nombre;

        administradorEN.Pass = Utils.Util.GetEncondeMD5 (p_pass);

        administradorEN.Email = p_email;



        oid = _IAdministradorRepository.CrearAdministador (administradorEN);
        return oid;
}

public void ModificarAdministador (int p_Administrador_OID, string p_nombre, String p_pass, string p_email)
{
        AdministradorEN administradorEN = null;

        //Initialized AdministradorEN
        administradorEN = new AdministradorEN ();
        administradorEN.Id = p_Administrador_OID;
        administradorEN.Nombre = p_nombre;
        administradorEN.Pass = Utils.Util.GetEncondeMD5 (p_pass);
        administradorEN.Email = p_email;
        //Call to AdministradorRepository

        _IAdministradorRepository.ModificarAdministador (administradorEN);
}

public void EliminarAdministador (int id
                                  )
{
        _IAdministradorRepository.EliminarAdministador (id);
}

public AdministradorEN DameAdministradorPorOID (int id
                                                )
{
        AdministradorEN administradorEN = null;

        administradorEN = _IAdministradorRepository.DameAdministradorPorOID (id);
        return administradorEN;
}

public System.Collections.Generic.IList<AdministradorEN> DameTodosAdministradores (int first, int size)
{
        System.Collections.Generic.IList<AdministradorEN> list = null;

        list = _IAdministradorRepository.DameTodosAdministradores (first, size);
        return list;
}
public string Login (int p_Administrador_OID, string p_pass)
{
        string result = null;
        AdministradorEN en = _IAdministradorRepository.ReadOIDDefault (p_Administrador_OID);

        if (en != null && en.Pass.Equals (Utils.Util.GetEncondeMD5 (p_pass)))
                result = this.GetToken (en.Id);

        return result;
}

public System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AdministradorEN> DameAdministradoresPorEmail (string p_email)
{
        return _IAdministradorRepository.DameAdministradoresPorEmail (p_email);
}



private string Encode (int id)
{
        var payload = new Dictionary<string, object>(){
                { "id", id }
        };
        string token = Jose.JWT.Encode (payload, Utils.Util.getKey (), Jose.JwsAlgorithm.HS256);

        return token;
}

public string GetToken (int id)
{
        AdministradorEN en = _IAdministradorRepository.ReadOIDDefault (id);
        string token = Encode (en.Id);

        return token;
}
public int CheckToken (string token)
{
        int result = -1;

        try
        {
                string decodedToken = Utils.Util.Decode (token);



                int id = (int)ObtenerID (decodedToken);

                AdministradorEN en = _IAdministradorRepository.ReadOIDDefault (id);

                if (en != null && ((long)en.Id).Equals (ObtenerID (decodedToken))
                    ) {
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
}
}

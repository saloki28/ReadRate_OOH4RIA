using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;

namespace WebApplication_ReadRate.Models.Assemblers
{
    public class UsuarioAssembler
    {
        public UsuarioViewModel ConvertirENToViewModel(UsuarioEN usuarioEN)
        {
            UsuarioViewModel usuarioVM = new UsuarioViewModel();
            
            usuarioVM.Id = usuarioEN.Id;
            usuarioVM.Email = usuarioEN.Email;
            usuarioVM.NombreUsuario = usuarioEN.NombreUsuario;
            usuarioVM.FechaNacimiento = usuarioEN.FechaNacimiento;
            usuarioVM.CiudadResidencia = usuarioEN.CiudadResidencia;
            usuarioVM.PaisResidencia = usuarioEN.PaisResidencia;
            usuarioVM.FotoUrl = usuarioEN.Foto;
            usuarioVM.Rol = usuarioEN.Rol.ToString();
            
            return usuarioVM;
        }

        public IList<UsuarioViewModel> ConvertirListENToViewModel(IList<UsuarioEN> usuarioENList)
        {
            IList<UsuarioViewModel> usuarioVMList = new List<UsuarioViewModel>();
            foreach (UsuarioEN usuarioEN in usuarioENList)
            {
                usuarioVMList.Add(ConvertirENToViewModel(usuarioEN));
            }
            return usuarioVMList;
        }
    }
}

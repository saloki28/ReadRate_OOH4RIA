using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;

namespace WebApplication_ReadRate.Models.Assemblers
{
    public class AdministradorAssembler
    {
        public AdministradorViewModel ConvertirENToViewModel(AdministradorEN en)
        {
            AdministradorViewModel admin = new AdministradorViewModel();
            admin.Id = en.Id;
            admin.Nombre = en.Nombre;
            admin.Password = en.Pass;
            admin.Email = en.Email;
            admin.FotoUrl = en.Foto;

            return admin;
        }

        public IList<AdministradorViewModel> ConvertirListENToViewModel(IList<AdministradorEN> ens)
        {
            IList<AdministradorViewModel> ress = new List<AdministradorViewModel>();
            foreach (AdministradorEN en in ens)
            {
                ress.Add(ConvertirENToViewModel(en));
            }
            return ress;
        }
    }
}

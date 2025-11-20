using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;

namespace WebApplication_ReadRate.Models.Assemblers
{
    public class LibroAssembler
    {
        public LibroViewModel ConvertirENToViewModel(LibroEN libroEN)
        {
            LibroViewModel libroVM = new LibroViewModel();
            libroVM.Id = libroEN.Id;
            libroVM.Titulo = libroEN.Titulo;
            libroVM.Genero = libroEN.Genero;
            libroVM.EdadRecomendada = libroEN.EdadRecomendada;
            libroVM.FechaPublicacion = libroEN.FechaPublicacion.HasValue ? libroEN.FechaPublicacion.Value : DateTime.MinValue;
            libroVM.NumPags = libroEN.NumPags;
            libroVM.Sinopsis = libroEN.Sinopsis;
            libroVM.FotoPortada = libroEN.FotoPortada;
            libroVM.ValoracionMedia = libroEN.ValoracionMedia;
            return libroVM;
        }

        public IList<LibroViewModel> ConvertirListENToViewModel(IList<LibroEN> libroENList)
        {
            IList<LibroViewModel> libroVMList = new List<LibroViewModel>();
            foreach (LibroEN libroEN in libroENList)
            {
                libroVMList.Add(ConvertirENToViewModel(libroEN));
            }
            return libroVMList;
        }
    }
}

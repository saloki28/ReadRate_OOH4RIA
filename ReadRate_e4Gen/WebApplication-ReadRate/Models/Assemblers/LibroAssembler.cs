using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;

namespace WebApplication_ReadRate.Models.Assemblers
{
    public class LibroAssembler
    {
        public LibroViewModel ConvertirENToViewModel(LibroEN libroEN)
        {
            LibroViewModel libroVM = new LibroViewModel();
            LibroFiltrosViewModel libroFiltrosVM = new LibroFiltrosViewModel();

            libroVM.Id = libroEN.Id;
            libroVM.Titulo = libroEN.Titulo;
            libroVM.Genero = libroEN.Genero;

            // Select Generos en el formulario de busqueda
            libroFiltrosVM.GeneroFiltro = libroEN.Genero != null ? libroEN.Genero : (string?)null;

            libroVM.EdadRecomendada = libroEN.EdadRecomendada;
            libroVM.FechaPublicacion = libroEN.FechaPublicacion ?? DateTime.MinValue;
            libroVM.NumPags = libroEN.NumPags;
            libroVM.Sinopsis = libroEN.Sinopsis;
            libroVM.FotoPortadaUrl = libroEN.FotoPortada; // String de la BD
            libroVM.ValoracionMedia = libroEN.ValoracionMedia;
            libroVM.AutorId = libroEN.AutorPublicador.Id;
            libroVM.NombreAutor = libroEN.AutorPublicador.NombreUsuario != null ? libroEN.AutorPublicador.NombreUsuario : string.Empty;
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

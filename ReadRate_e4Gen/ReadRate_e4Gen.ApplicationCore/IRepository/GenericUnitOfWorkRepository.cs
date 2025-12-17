
using System;
using System.Collections.Generic;
using System.Text;

namespace ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4
{
public abstract class GenericUnitOfWorkRepository
{
protected IUsuarioRepository usuariorepository;
protected ILibroRepository librorepository;
protected IReseñaRepository reseñarepository;
protected IClubRepository clubrepository;
protected IAdministradorRepository administradorrepository;
protected INoticiaRepository noticiarepository;
protected IEventoRepository eventorepository;
protected INotificacionRepository notificacionrepository;
protected IAutorRepository autorrepository;
protected ILectorRepository lectorrepository;
protected IMensajeRepository mensajerepository;


public abstract IUsuarioRepository UsuarioRepository {
        get;
}
public abstract ILibroRepository LibroRepository {
        get;
}
public abstract IReseñaRepository ReseñaRepository {
        get;
}
public abstract IClubRepository ClubRepository {
        get;
}
public abstract IAdministradorRepository AdministradorRepository {
        get;
}
public abstract INoticiaRepository NoticiaRepository {
        get;
}
public abstract IEventoRepository EventoRepository {
        get;
}
public abstract INotificacionRepository NotificacionRepository {
        get;
}
public abstract IAutorRepository AutorRepository {
        get;
}
public abstract ILectorRepository LectorRepository {
        get;
}
public abstract IMensajeRepository MensajeRepository {
        get;
}
}
}

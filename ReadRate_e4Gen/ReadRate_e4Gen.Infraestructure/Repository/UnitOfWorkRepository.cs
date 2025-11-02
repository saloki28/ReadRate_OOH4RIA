

using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;
using ReadRate_e4Gen.Infraestructure.Repository.ReadRate_E4;
using ReadRate_e4Gen.Infraestructure.CP;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReadRate_e4Gen.Infraestructure.Repository
{
public class UnitOfWorkRepository : GenericUnitOfWorkRepository
{
SessionCPNHibernate session;


public UnitOfWorkRepository(SessionCPNHibernate session)
{
        this.session = session;
}

public override IUsuarioRepository UsuarioRepository {
        get
        {
                this.usuariorepository = new UsuarioRepository ();
                this.usuariorepository.setSessionCP (session);
                return this.usuariorepository;
        }
}

public override ILibroRepository LibroRepository {
        get
        {
                this.librorepository = new LibroRepository ();
                this.librorepository.setSessionCP (session);
                return this.librorepository;
        }
}

public override IReseñaRepository ReseñaRepository {
        get
        {
                this.reseñarepository = new ReseñaRepository ();
                this.reseñarepository.setSessionCP (session);
                return this.reseñarepository;
        }
}

public override IClubRepository ClubRepository {
        get
        {
                this.clubrepository = new ClubRepository ();
                this.clubrepository.setSessionCP (session);
                return this.clubrepository;
        }
}

public override IAdministradorRepository AdministradorRepository {
        get
        {
                this.administradorrepository = new AdministradorRepository ();
                this.administradorrepository.setSessionCP (session);
                return this.administradorrepository;
        }
}

public override INoticiaRepository NoticiaRepository {
        get
        {
                this.noticiarepository = new NoticiaRepository ();
                this.noticiarepository.setSessionCP (session);
                return this.noticiarepository;
        }
}

public override IEventoRepository EventoRepository {
        get
        {
                this.eventorepository = new EventoRepository ();
                this.eventorepository.setSessionCP (session);
                return this.eventorepository;
        }
}

public override INotificacionRepository NotificacionRepository {
        get
        {
                this.notificacionrepository = new NotificacionRepository ();
                this.notificacionrepository.setSessionCP (session);
                return this.notificacionrepository;
        }
}

public override IAutorRepository AutorRepository {
        get
        {
                this.autorrepository = new AutorRepository ();
                this.autorrepository.setSessionCP (session);
                return this.autorrepository;
        }
}

public override ILectorRepository LectorRepository {
        get
        {
                this.lectorrepository = new LectorRepository ();
                this.lectorrepository.setSessionCP (session);
                return this.lectorrepository;
        }
}
}
}


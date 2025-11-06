

using System;
using System.Collections.Generic;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.Utils;

namespace ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4
{
public abstract class GenericBasicCP
{
protected GenericSessionCP CPSession;
protected GenericUnitOfWorkRepository unitRepo;
protected GenericUnitOfWorkUtils unitUtils;

protected GenericBasicCP (GenericSessionCP currentSession)
{
        this.CPSession = currentSession;
        this.unitRepo = this.CPSession.UnitRepo;
}
protected GenericBasicCP(GenericSessionCP currentSession, GenericUnitOfWorkUtils unitUtils)
{
        this.CPSession = currentSession;
        this.unitRepo = this.CPSession.UnitRepo;
        this.unitUtils = unitUtils;
}
protected GenericBasicCP()
{
        this.CPSession = null;
        this.unitRepo = null;
}
}
}



using System;
using System.Text;
using System.Collections.Generic;
using ReadRate_e4Gen.ApplicationCore.Exceptions;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.Utils;



namespace ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4
{
public partial class NoticiaCP : GenericBasicCP
{
public NoticiaCP(GenericSessionCP currentSession)
        : base (currentSession)
{
}

public NoticiaCP(GenericSessionCP currentSession, GenericUnitOfWorkUtils unitUtils)
        : base (currentSession, unitUtils)
{
}
}
}

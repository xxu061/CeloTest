using CeloTest.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CeloTest.Repo
{
    public interface IUserRepo : IRepo<User>, IDisposable
    {

    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void CommitAsync();
    }
}

using DataAccess.Abstract;
using DataAccess.Contexts;

namespace DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WebTemplateContext _db;

        public UnitOfWork(WebTemplateContext db)
        {
            _db = db;
        }

        public void Commit()
        {
            var code = _db.GetHashCode();
            _db.SaveChanges();
        }

        public void CommitAsync()
        {
            _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            if(_db != null)
            {
                _db.Dispose();
            }
        }
    }
}

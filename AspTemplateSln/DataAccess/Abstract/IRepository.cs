using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IRepository<T>
    {
        // funcs đồng bộ
        void Add(T entity);
        /// <summary>
        /// Chỉ sử dụng khi context không theo dõi entity
        /// Tức là entity không được tạo ra từ context (First, FristOrDefault, Find, ...)
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);
        void Delete(object id, string user = "");
        IQueryable<T> Select(Expression<Func<T, bool>> expr);
        IQueryable<T> SelectAll();
        T FindById(int id);

        // funcs bất đồng bộ
        Task AddAsync(T entity);
        Task<T> FindByIdAsync(int id);
    }
}

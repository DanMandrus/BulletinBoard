using BulletinBoard.DataAccess.Entities;
using BulletinBoard.DataAccess.Entities;

namespace BulletinBoard.DataAccess;

public interface IRepository<T> where T : BaseEntity
{
    IQueryable<T> GetAll();
    T? GetById(int id);
    T? GetById(Guid id);
    T Save(T entity);
    void Delete(T entity);
}
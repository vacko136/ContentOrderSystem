// Application/Interfaces/IRepository.cs
using System;
using System.Collections.Generic;

namespace ContentOrderSystem.Application.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T GetById(Guid id);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}

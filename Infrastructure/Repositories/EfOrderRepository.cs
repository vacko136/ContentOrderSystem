// Infrastructure/Repositories/EfOrderRepository.cs
using System;
using System.Collections.Generic;
using System.Linq;
using ContentOrderSystem.Domain;
using ContentOrderSystem.Application.Interfaces;

namespace ContentOrderSystem.Infrastructure.Repositories
{
    public class EfOrderRepository : IRepository<Order>
    {
        private readonly List<Order> _orders = new(); // Simulated DB

        public void Add(Order entity) => _orders.Add(entity);

        public void Delete(Order entity) => _orders.Remove(entity);

        public IEnumerable<Order> GetAll() => _orders;

        public Order GetById(Guid id) => _orders.Find(o => o.Id == id);

        public void Update(Order entity)
        {
            var index = _orders.FindIndex(o => o.Id == entity.Id);
            if (index >= 0) _orders[index] = entity;
        }
    }
}

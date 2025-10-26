using System;
using System.Collections.Generic;
using ContentOrderSystem.Domain;
using ContentOrderSystem.Application.Interfaces;
using ContentOrderSystem.Infrastructure.Repositories;

namespace ContentOrderSystem.Infrastructure.UnitOfWork
{
    public class InMemoryUnitOfWork : IUnitOfWork
    {
        // In-memory repositories
        public IRepository<Article> Articles { get; }
        public IRepository<SubscriptionPackage> SubscriptionPackages { get; }
        public IRepository<Order> Orders { get; }

        public InMemoryUnitOfWork()
        {
            Articles = new EfArticleRepository();
            SubscriptionPackages = new EfArticleRepository() as IRepository<SubscriptionPackage> ?? new EfSubscriptionRepository();
            Orders = new EfOrderRepository();
        }

        public void Commit()
        {
            // In-memory, so nothing to commit
            // In a real DB, this would save changes
        }

        public void Dispose()
        {
            // Nothing to dispose for in-memory
        }
    }

    public class EfSubscriptionRepository : IRepository<SubscriptionPackage>
    {
        private readonly List<SubscriptionPackage> _packages = new();

        public void Add(SubscriptionPackage entity) => _packages.Add(entity);
        public void Delete(SubscriptionPackage entity) => _packages.Remove(entity);
        public IEnumerable<SubscriptionPackage> GetAll() => _packages;
        public SubscriptionPackage? GetById(Guid id) => _packages.Find(p => p.Id == id);
        public void Update(SubscriptionPackage entity)
        {
            var index = _packages.FindIndex(p => p.Id == entity.Id);
            if (index >= 0) _packages[index] = entity;
        }
    }
}

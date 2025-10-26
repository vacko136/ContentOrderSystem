// Application/Interfaces/IUnitOfWork.cs
using ContentOrderSystem.Domain;
using System;

namespace ContentOrderSystem.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Article> Articles { get; }
        IRepository<SubscriptionPackage> SubscriptionPackages { get; }
        IRepository<Order> Orders { get; }
        void Commit();
    }
}

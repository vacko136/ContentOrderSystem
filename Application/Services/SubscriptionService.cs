// Application/Services/SubscriptionService.cs
using ContentOrderSystem.Domain;
using ContentOrderSystem.Application.Interfaces;
using System;
using System.Collections.Generic;

namespace ContentOrderSystem.Application.Services
{
    public class SubscriptionService
    {
        private readonly IRepository<SubscriptionPackage> _repo;
        private readonly IUnitOfWork _unitOfWork;

        public SubscriptionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repo = unitOfWork.SubscriptionPackages;
        }

        public IEnumerable<SubscriptionPackage> GetAllSubscriptions()
        {
            return _repo.GetAll();
        }

        public void AddSubscription(SubscriptionPackage package)
        {
            if (string.IsNullOrEmpty(package.Name))
                throw new ArgumentException("Package name is required.");

            _repo.Add(package);
            _unitOfWork.Commit();
        }

        public void UpdateSubscription(SubscriptionPackage package)
        {
            _repo.Update(package);
            _unitOfWork.Commit();
        }

        public void DeleteSubscription(SubscriptionPackage package)
        {
            package.IsArchived = true;
            _repo.Update(package);
            _unitOfWork.Commit();
        }
    }
}

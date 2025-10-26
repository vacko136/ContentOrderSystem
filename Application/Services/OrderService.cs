// Application/Services/OrderService.cs
using ContentOrderSystem.Domain;
using ContentOrderSystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContentOrderSystem.Application.Services
{
    public class OrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void PlaceOrder(Order order)
        {
            // Validate customer rules
            var customerPurchases = _unitOfWork.Orders.GetAll()
                .Where(o => o.CustomerPhone == order.CustomerPhone);

            if (order.Subscription != null && customerPurchases.Any(o => o.Subscription != null))
                throw new InvalidOperationException("Customer already has a subscription.");

            foreach (var article in order.Articles)
            {
                if (customerPurchases.Any(o => o.Articles.Any(a => a.ArticleId == article.ArticleId)))
                    throw new InvalidOperationException($"Customer already purchased article {article.ArticleName}.");
            }

            // Calculate total price
            decimal totalPrice = order.Articles.Sum(a => a.ArticlePrice * a.Quantity);
            if (order.Subscription != null) totalPrice += order.Subscription.PackagePrice;
            order.TotalPrice = totalPrice;
            order.Status = OrderStatus.Pending;
            order.DateCreated = DateTime.Now;

            // Add to repository
            _unitOfWork.Orders.Add(order);
            _unitOfWork.Commit();
        }
    }
}

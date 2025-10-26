// Domain/Entities.cs
using System;
using System.Collections.Generic;

namespace ContentOrderSystem.Domain
{
    // Article entity
    public class Article
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string SupplierEmail { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsArchived { get; set; }
    }

    // Subscription package entity
    public class SubscriptionPackage
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IncludesMonthlyPhysicalMagazine { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsArchived { get; set; }
    }

    // Order entity
    public class Order
    {
        public Guid Id { get; set; }
        public string OrderNumber { get; set; }
        public string CustomerPhone { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime DateCreated { get; set; }

        public List<OrderArticle> Articles { get; set; } = new();
        public OrderSubscription Subscription { get; set; }
    }

    public class OrderArticle
    {
        public Guid Id { get; set; }
        public Guid? ArticleId { get; set; }
        public string ArticleName { get; set; }
        public decimal ArticlePrice { get; set; }
        public int Quantity { get; set; }
    }

    public class OrderSubscription
    {
        public Guid Id { get; set; }
        public Guid? SubscriptionPackageId { get; set; }
        public string PackageName { get; set; }
        public decimal PackagePrice { get; set; }
    }
}

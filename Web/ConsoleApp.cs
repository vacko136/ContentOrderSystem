using ContentOrderSystem.Application.Services;
using ContentOrderSystem.Domain;
using ContentOrderSystem.Infrastructure.Sms;
using ContentOrderSystem.Web.Controllers;
using ContentOrderSystem.Infrastructure.UnitOfWork;
using System;

namespace ContentOrderSystem.Web
{
    class ConsoleApp
    {
        static void Main(string[] args)
        {
            var unitOfWork = new InMemoryUnitOfWork(); // you can implement a simple in-memory version
            var articleService = new ArticleService(unitOfWork);
            var subscriptionService = new SubscriptionService(unitOfWork);
            var orderService = new OrderService(unitOfWork);
            var smsGateway = new SmsGateway();

            var articlesController = new ArticlesController(articleService);
            var ordersController = new OrdersController(orderService, smsGateway);

            while (true)
            {
                Console.WriteLine("\n--- Menu ---");
                Console.WriteLine("1: List Articles");
                Console.WriteLine("2: Add Article");
                Console.WriteLine("3: Edit Article");
                Console.WriteLine("4: Delete Article");
                Console.WriteLine("5: Place Order");
                Console.WriteLine("6: List Orders");
                Console.WriteLine("0: Exit");
                Console.Write("Select option: ");

                var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        foreach (var a in articlesController.Index())
                            Console.WriteLine($"{a.Id} | {a.Name} | {a.Price:C}");
                        break;

                    case "2":
                        var newArticle = new Article
                        {
                            Id = Guid.NewGuid(),
                            Name = Prompt("Article Name: "),
                            Description = Prompt("Description: "),
                            Price = decimal.Parse(Prompt("Price: ")),
                            SupplierEmail = Prompt("Supplier Email: "),
                            DateCreated = DateTime.Now
                        };
                        articlesController.Add(newArticle);
                        break;

                    case "3":
                        var editId = Guid.Parse(Prompt("Enter Article Id to edit: "));
                        var editArticle = articleService.GetAllArticles().FirstOrDefault(a => a.Id == editId);
                        if (editArticle != null)
                        {
                            editArticle.Name = Prompt($"Name ({editArticle.Name}): ", editArticle.Name);
                            editArticle.Description = Prompt($"Description ({editArticle.Description}): ", editArticle.Description);
                            editArticle.Price = decimal.Parse(Prompt($"Price ({editArticle.Price}): ", editArticle.Price.ToString()));
                            articlesController.Edit(editArticle);
                        }
                        else
                        {
                            Console.WriteLine("Article not found.");
                        }
                        break;

                    case "4":
                        var delId = Guid.Parse(Prompt("Enter Article Id to delete: "));
                        articlesController.Delete(delId);
                        break;

                    case "5":
                        var order = new Order
                        {
                            Id = Guid.NewGuid(),
                            OrderNumber = Guid.NewGuid().ToString(),
                            CustomerPhone = Prompt("Customer Phone: "),
                            Articles = new List<OrderArticle>(),
                            Subscription = null
                        };

                        Console.Write("Add article to order? (y/n): ");
                        if (Console.ReadLine()?.ToLower() == "y")
                        {
                            var artId = Guid.Parse(Prompt("Article Id: "));
                            var art = articleService.GetAllArticles().FirstOrDefault(a => a.Id == artId);
                            if (art != null)
                            {
                                order.Articles.Add(new OrderArticle
                                {
                                    Id = Guid.NewGuid(),
                                    ArticleId = art.Id,
                                    ArticleName = art.Name,
                                    ArticlePrice = art.Price,
                                    Quantity = int.Parse(Prompt("Quantity: "))
                                });
                            }
                        }

                        Console.Write("Add subscription package? (y/n): ");
                        if (Console.ReadLine()?.ToLower() == "y")
                        {
                            // For simplicity, pick first subscription
                            var sub = subscriptionService.GetAllSubscriptions().FirstOrDefault();
                            if (sub != null)
                            {
                                order.Subscription = new OrderSubscription
                                {
                                    Id = Guid.NewGuid(),
                                    SubscriptionPackageId = sub.Id,
                                    PackageName = sub.Name,
                                    PackagePrice = sub.Price
                                };
                            }
                        }

                        ordersController.PlaceOrder(order);
                        break;

                    case "6":
                        foreach (var o in ordersController.Index())
                            Console.WriteLine($"{o.OrderNumber} | {o.CustomerPhone} | {o.TotalPrice:C} | {o.Status}");
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        static string Prompt(string message, string defaultValue = "")
        {
            Console.Write(message);
            var input = Console.ReadLine();
            return string.IsNullOrEmpty(input) ? defaultValue : input;
        }
    }
}

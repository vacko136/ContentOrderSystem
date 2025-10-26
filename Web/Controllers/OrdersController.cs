// Web/Controllers/OrdersController.cs
using System;
using System.Collections.Generic;
using ContentOrderSystem.Domain;
using ContentOrderSystem.Application.Services;
using ContentOrderSystem.Infrastructure.Sms;

namespace ContentOrderSystem.Web.Controllers
{
    public class OrdersController
    {
        private readonly OrderService _orderService;
        private readonly SmsGateway _smsGateway;

        public OrdersController(OrderService orderService, SmsGateway smsGateway)
        {
            _orderService = orderService;
            _smsGateway = smsGateway;
        }

        // Place a new order
        public void PlaceOrder(Order order)
        {
            try
            {
                _orderService.PlaceOrder(order);
                Console.WriteLine("Order placed successfully.");

                // Send SMS notification
                _smsGateway.SendSms(order.CustomerPhone, "Your order has been successfully placed!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error placing order: {ex.Message}");
            }
        }

        // List all orders
        public IEnumerable<Order> Index()
        {
            return _orderService.GetAllOrders();
        }
    }
}

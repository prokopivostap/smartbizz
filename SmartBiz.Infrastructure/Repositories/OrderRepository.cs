using SmartBiz.Application.DTO;
using SmartBiz.Application.Interfaces;
using SmartBiz.Domain;
using System.Collections.Generic;
using System.Linq;

namespace SmartBiz.Infrastructure.Repositories
{
    public class OrderRepository : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddOrder(OrderDto dto)
        {
            var order = new Order
            {
                Id = dto.Id,
                CustomerName = dto.CustomerName,
                OrderDate = DateTime.SpecifyKind(dto.OrderDate, DateTimeKind.Utc),
                TotalAmount = dto.TotalAmount,
                Status = dto.Status
            };
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public void DeleteOrder(int id)
        {
            var order = _context.Orders.Find(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }
        }

        public IEnumerable<OrderDto> GetAllOrders()
        {
            return _context.Orders.Select(o => new OrderDto
            {
                Id = o.Id,
                CustomerName = o.CustomerName,
                OrderDate = o.OrderDate,
                TotalAmount = o.TotalAmount,
                Status = o.Status
            }).ToList();
        }

        public OrderDto GetOrderById(int id)
        {
            var order = _context.Orders.FirstOrDefault(o => o.Id == id);
            if (order == null) return null;

            return new OrderDto
            {
                Id = order.Id,
                CustomerName = order.CustomerName,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Status = order.Status
            };
        }

        public void UpdateOrder(OrderDto dto)
        {
            var order = _context.Orders.Find(dto.Id);
            if (order == null) return;

            order.Id = dto.Id;
            order.CustomerName = dto.CustomerName;
            order.OrderDate = DateTime.SpecifyKind(dto.OrderDate, DateTimeKind.Utc);
            order.TotalAmount = dto.TotalAmount;
            order.Status = dto.Status;

            _context.Orders.Update(order);
            _context.SaveChanges();
        }

        public void UpdateOrderStatus(int id)
        {
            var order = _context.Orders.Find(id);
            if (order == null) return;

            // Можна реалізувати логіку: наприклад, змінювати статус циклічно
            order.Status = order.Status == "Очікує" ? "Виконано" : "Очікує";

            _context.SaveChanges();
        }
    }
}

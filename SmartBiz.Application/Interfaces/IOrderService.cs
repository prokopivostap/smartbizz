using SmartBiz.Application.DTO;
using System.Collections.Generic;

namespace SmartBiz.Application.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<OrderDto> GetAllOrders();
        OrderDto GetOrderById(int id);
        void AddOrder(OrderDto order);
        void UpdateOrder(OrderDto order);
        void DeleteOrder(int id);
        void UpdateOrderStatus(int id);
    }
}

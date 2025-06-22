using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartBiz.Application.DTO;
using SmartBiz.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartBiz.Web.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            return View(_orderService.GetAllOrders());
        }

        
        [HttpPost]
        public IActionResult Add(OrderDto order)
        {
            _orderService.AddOrder(order);
            return RedirectToAction(nameof(Index));
        }

        
        [HttpPost]
        public IActionResult Update(OrderDto order)
        {
            _orderService.UpdateOrder(order);
            return RedirectToAction(nameof(Index));
        }

        
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _orderService.DeleteOrder(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

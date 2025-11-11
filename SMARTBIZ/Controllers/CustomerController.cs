using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartBiz.Application.DTO;
using SmartBiz.Application.Interfaces;

namespace SmartBiz.Web.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public IActionResult Index()
        {
            var customers = _customerService.GetAllCustomers();
            return View(customers);
        }

        [HttpPost]
        public IActionResult AddInline(CustomerDto dto)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Index));

            _customerService.AddCustomer(dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult UpdateInline(CustomerDto dto)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Index));

            _customerService.UpdateCustomer(dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _customerService.DeleteCustomer(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

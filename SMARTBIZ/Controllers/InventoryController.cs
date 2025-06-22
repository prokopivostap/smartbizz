using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartBiz.Application.DTO;
using SmartBiz.Application.Interfaces;

namespace SmartBiz.Web.Controllers
{
    [Authorize]
    public class InventoryController : Controller
    {
        private readonly IInventoryService _service;

        public InventoryController(IInventoryService service)
        {
            _service = service;
        }

        
        public IActionResult Index(string search)
        {
            var items = _service.GetAllItems(search);
            ViewData["Search"] = search;
            return View(items);
        }

        
        [HttpPost]
        public IActionResult AddInline(InventoryDto dto)
        {
            if (!ModelState.IsValid)
            {
                var items = _service.GetAllItems();
                return View("Index", items);
            }
            _service.AddItem(dto);
            return RedirectToAction(nameof(Index));
        }

        
        [HttpPost]
        public IActionResult UpdateInline(InventoryDto dto)
        {
            if (!ModelState.IsValid)
            {
                var items = _service.GetAllItems();
                return View("Index", items);
            }
            _service.UpdateItem(dto);
            return RedirectToAction(nameof(Index));
        }

        
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _service.DeleteItem(id);
            return RedirectToAction(nameof(Index));
        }

        
        public IActionResult Edit(int id)
        {
            var item = _service.GetItemById(id);
            if (item == null)
                return NotFound();

            return View(item); 
        }

        
        [HttpPost]
        public IActionResult UpdateQuantity(int id, int quantity)
        {
            _service.UpdateQuantity(id, quantity);
            return RedirectToAction(nameof(Index));
        }

        
        [HttpPost]
        public IActionResult AutomateReplenishment()
        {
            _service.AutomateReplenishment();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new InventoryDto());
        }

        [HttpPost]
        public IActionResult Create(InventoryDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            _service.AddItem(dto);
            return RedirectToAction(nameof(Index));
        }
    }
}

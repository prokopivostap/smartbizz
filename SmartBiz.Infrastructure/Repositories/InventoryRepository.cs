using System.Collections.Generic;
using System.Linq;
using SmartBiz.Application.DTO;
using SmartBiz.Application.Interfaces;

namespace SmartBiz.Infrastructure.Repositories
{
    public class InventoryRepository : IInventoryService
    {
        private readonly ApplicationDbContext _context;

        public InventoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddItem(InventoryDto itemDto)
        {
            var inventory = new Inventory
            {
                Id = itemDto.Id,
                StockStatus = itemDto.StockStatus,
                Price = itemDto.Price,
                ProductName = itemDto.ProductName,
                Quantity = itemDto.Quantity
            };

            _context.Inventories.Add(inventory);
            _context.SaveChanges();
        }

        public InventoryDto GetItemById(int id)
        {
            return _context.Inventories
                .Select(t => new InventoryDto
                {
                    Id = t.Id,
                    StockStatus = t.StockStatus,
                    Price = t.Price,
                    ProductName = t.ProductName,
                    Quantity = t.Quantity
                }).FirstOrDefault(i => i.Id == id)!;
        }

        public IEnumerable<InventoryDto> GetAllItems(string search = null)
        {
            var query = _context.Inventories.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(i => i.ProductName.Contains(search));
            }

            return query
                .Select(t => new InventoryDto
                {
                    Id = t.Id,
                    StockStatus = t.StockStatus,
                    Price = t.Price,
                    ProductName = t.ProductName,
                    Quantity = t.Quantity
                })
                .ToList();
        }

        public void UpdateItem(InventoryDto itemDto)
        {
            var existing = _context.Inventories.FirstOrDefault(i => i.Id == itemDto.Id);
            if (existing == null) return;

            existing.ProductName = itemDto.ProductName;
            existing.Quantity = itemDto.Quantity;
            existing.Price = itemDto.Price;
            existing.StockStatus = itemDto.StockStatus;

            _context.SaveChanges();
        }

        public void UpdateQuantity(int id, int newQuantity)
        {
            var item = _context.Inventories.FirstOrDefault(i => i.Id == id);
            if (item != null)
            {
                item.Quantity = newQuantity;
            }

            _context.SaveChanges();
        }

        public void AutomateReplenishment()
        {
            foreach (var item in _context.Inventories.Where(i => i.Quantity < 10))
            {
                item.Quantity = 100;
                item.StockStatus = "Автоматично поповнено";
            }

            _context.SaveChanges();
        }

        public void DeleteItem(int id)
        {
            var item = _context.Inventories.FirstOrDefault(i => i.Id == id);
            if (item != null)
            {
                _context.Inventories.Remove(item);
                _context.SaveChanges();
            }
        }
    }
}

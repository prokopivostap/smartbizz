using System.Collections.Generic;
using SmartBiz.Application.DTO;

namespace SmartBiz.Application.Interfaces
{
    public interface IInventoryService
    {
        IEnumerable<InventoryDto> GetAllItems(string search = null);
        InventoryDto GetItemById(int id);
        void AddItem(InventoryDto dto);
        void UpdateItem(InventoryDto dto);
        void UpdateQuantity(int id, int newQuantity);
        void AutomateReplenishment();
        void DeleteItem(int id);
    }
}
using System.Collections.Generic;
using SmartBiz.Application.DTO; 

namespace SmartBiz.Application.Interfaces 
{
    public interface ICustomerService
    {
        void AddCustomer(CustomerDto customer);                  
        CustomerDto GetCustomerById(int id);
        IEnumerable<CustomerDto> GetAllCustomers();
        void UpdateCustomer(CustomerDto customer);
        void DeleteCustomer(int id);
    }
}

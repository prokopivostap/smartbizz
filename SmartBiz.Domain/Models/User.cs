using Microsoft.AspNetCore.Identity;

namespace SmartBiz.Application.DTO
{
    public class User : IdentityUser<int>
    {
        public string FullName { get; set; }
        public int Age  { get; set; }

    }
}

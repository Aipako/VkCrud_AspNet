using Microsoft.EntityFrameworkCore;

namespace VkCrud2.Models
{
    [PrimaryKey("Id")]
    public class UserGroupClass
    {
        
        public uint Id { get; init; }
        public UserGroupCode Code { get; init; }
        public string? Description { get; set; }
    }

    public enum UserGroupCode 
    {
        Admin,
        User
    }
}

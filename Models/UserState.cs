namespace VkCrud2.Models
{
    public class UserStateClass
    {
        public uint Id { get; init; }
        public UserStateCode Code { get; init; }
        public string? Description { get; set; }
    }

    public enum UserStateCode
    {
        Active,
        Blocked
    }
}
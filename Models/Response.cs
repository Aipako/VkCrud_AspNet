

namespace VkCrud2.Models
{
    public class Response
    {
        public Response(int status, Object? content = null)
        {
            Status = status;
            Content = content;
        }
        public int Status { get; init; }
        public Object? Content { get; init; }
    }
}

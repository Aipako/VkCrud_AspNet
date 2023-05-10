using VkCrud2.Models;
namespace VkCrud2.Services
{
    public class HttpResponseManager
    {
        public static Response GetResponse400()
        {
            return new Response(400);
        }

        public static Response GetResponse200(object content)
        {
            return new Response(200, content);
        }

        public static Response GetResponse501()
        {
            return new Response(501);
        }
    }
}

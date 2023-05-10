using System.Runtime.ConstrainedExecution;
using System.Text.Json;
using VkCrud2.Models;

namespace VkCrud2.Services
{

    public class JsonManager
    {
        public static async Task<User?> StringToUser(string userString)
        {
            User? user;
            using (var userStream = await GenerateStreamFromString(userString))
            {
                user = await JsonSerializer.DeserializeAsync<User?>(userStream);
            }
            return user;
        }

        public static async Task<string> UsersToString(List<User> users)
        {
            string? usersString;
            using (var usersStream = new MemoryStream())
            {
                await JsonSerializer.SerializeAsync<List<User>>(usersStream, users);
                usersStream.Position = 0;
                var reader = new StreamReader(usersStream);
                usersString = await reader.ReadToEndAsync();
            }
            return usersString;
        }

        public static async Task<string> UserToString(User user)
        {
            string? userString;
            using (var userStream = new MemoryStream())
            {
                await JsonSerializer.SerializeAsync<User?>(userStream, user);
                userStream.Position = 0;
                var reader = new StreamReader(userStream);
                userString = await reader.ReadToEndAsync();
            }
            return userString;
        }
        

        private static async Task<Stream> GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            await writer.WriteAsync(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

       

    }
}

using Microsoft.EntityFrameworkCore;
using System.Linq;
using VkCrud2.Data;
using VkCrud2.Models;

namespace VkCrud2.Services
{
    public class UserManager
    {
        public static async Task<Response> GetUser(string userString)
        {
            User? user;
            try
            {
                user = await JsonManager.StringToUser(userString);
                if (user == null)
                {
                    return HttpResponseManager.GetResponse400();
                }
            }
            catch (Exception ex)
            {
                return HttpResponseManager.GetResponse400();
            }

            try
            {
                using (UserDbContext context = new UserDbContext())
                {
                    var users = await context.Users.ToListAsync();
                    User? foundUser = users.Find(x => x.Id == user.Id || x.Login == user.Login);
                    return HttpResponseManager.GetResponse200(/*await JsonManager.UserToString(*/foundUser);
                }
            }
            catch (Exception ex)
            {
                return HttpResponseManager.GetResponse501();
            }
        }

        public static async Task<Response> GetUsers(int page, int count)
        {
            try
            {
                using (UserDbContext context = new UserDbContext())
                {
                    var users = await context.Users.Include(u => u.UserGroup).Include(u => u.UserState).ToListAsync();
                    List<User> foundUsers;
                    if (page == 0)
                    {
                        foundUsers = users;
                    }
                    else
                    {
                        foundUsers = users.Skip(count * (page - 1)).Take(count).ToList();
                    }
                    
                    return HttpResponseManager.GetResponse200(foundUsers);
                }
            }
            catch (Exception ex)
            {
                return HttpResponseManager.GetResponse501();
            }
        }

        public static async Task<Response> AddUser(string userString)
        {
            User? user;
            try
            {
                user = await JsonManager.StringToUser(userString);
                if (user == null)
                {
                    return HttpResponseManager.GetResponse400();
                }
            }
            catch (Exception ex)
            {
                return HttpResponseManager.GetResponse400();
            }

            try
            {
                using (UserDbContext context = new UserDbContext())
                {
                    if(await context.Users.Where(x => x.Login == user.Login).FirstOrDefaultAsync() == null)
                    {
                        if(user.UserGroup.Code == UserGroupCode.Admin && await context.Users.Where(x => x.UserGroup.Code == UserGroupCode.Admin && x.UserState.Code == UserStateCode.Active).FirstOrDefaultAsync() != null)
                        {
                            return HttpResponseManager.GetResponse200("User exist");
                        }
                        else
                        {
                            context.Add(user);
                            await context.SaveChangesAsync();
                            return HttpResponseManager.GetResponse200("User Created");
                        }

                    }
                    else
                    {
                        return HttpResponseManager.GetResponse200("User exist");
                    }
                }
            }
            catch (Exception ex)
            {
                return HttpResponseManager.GetResponse501();
            }
        }

        public static async Task<Response> DeleteUser(string userString)
        {
            User? user;
            try
            {
                user = await JsonManager.StringToUser(userString);
                if (user == null)
                {
                    return HttpResponseManager.GetResponse400();
                }
            }
            catch (Exception ex)
            {
                return HttpResponseManager.GetResponse400();
            }

            try
            {
                using (UserDbContext context = new UserDbContext())
                {
                    User? gettedUser = await context.Users.Where(x => x.Login == user.Login && x.UserState.Code == UserStateCode.Active).FirstOrDefaultAsync();
                    if (gettedUser != null)
                    {
                        gettedUser.UserState = await context.UserStates.Where(x => x.Code == UserStateCode.Blocked).FirstOrDefaultAsync();
                        context.Update(user);
                        context.SaveChangesAsync();
                        return HttpResponseManager.GetResponse200("User deleted");
                    }
                    else
                    {
                        return HttpResponseManager.GetResponse200("User does not exist");
                    }
                }
            }
            catch (Exception ex)
            {
                return HttpResponseManager.GetResponse501();
            }
        }
    }
}

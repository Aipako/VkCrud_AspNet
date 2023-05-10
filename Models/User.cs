using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using VkCrud2.Data;

namespace VkCrud2.Models
{
    public class User
    {
        public Guid Id { get; init; }
        public string Login { get; set; }
        public string? Password { get; set; }
        public DateTime CreatedDate { get; init; }

        //[ForeignKey("UserGroups")]
        //public int UserGroupId { get; set; }
        public UserGroupClass UserGroup { get; set; }


        //[ForeignKey("UserState")]
        //public int UserStateId { get; set; }       
        public UserStateClass UserState { get; set; }

        [JsonConstructor]
        public User(string login, string password, int userGroupId, int? userStateId)
        {
            Id = Guid.NewGuid();
            Login = login;
            Password = password;
            CreatedDate = DateTime.Now;
            using (UserDbContext context = new UserDbContext())
            {
                UserGroupClass? gettedGroup = context.UserGroups.Find(userGroupId);
                if(gettedGroup != null) 
                {
                    UserGroup = gettedGroup;
                }
                else
                    throw new IndexOutOfRangeException();

                if(userStateId != null)
                {
                    UserStateClass? gettedState = context.UserStates.Find(userStateId);
                    if (gettedState != null)
                    {
                        UserState = gettedState;
                    }
                }
                else
                {
                    UserState = context.UserStates.Where(x => x.Code == UserStateCode.Active).FirstOrDefault();
                }
            }
        }




    }
}

using System.Linq;
using Dapper;
using DrinkAPI.Infrastructure;
using DrinkAPI.Models;

namespace DrinkAPI.Data.Account
{
    public class UserService
    {
        public User GetUser(string email)
        {
            using (var connection = ConnectionFactory.GetOpenConnection())
            {
                const string query = @"
                    SELECT [Id]
                        ,[Email]
                        ,[FirstName]
                        ,[LastName]
                        ,[Token]
                        ,[Password]
                        ,[Salt]
                    FROM [dbo].[User]
                    WHERE Email = @Email";

                return connection.Query<User>(query, new { Email = email }).FirstOrDefault();
            }
        }
    }
}
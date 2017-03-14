using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContactList.Infrastructure;
using Dapper;
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

        public string Register(string email, string password, string firstName, string lastName)
        {
            using (var connection = ConnectionFactory.GetOpenConnection())
            {
                const string query = @"";
                return null;
                //return connection.Query<User>(query, new { Email = email }).FirstOrDefault();
            }
        }
    }
}
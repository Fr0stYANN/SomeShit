using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Business.Interfaces;
using Server.Business.Entities;
using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;

namespace Server.MSSQL.Repositories
{
    public class UserRepository : IUserRepository
    {
        string ConnectionString = @"Data Source=sql.bsite.net\MSSQL2016;Initial Catalog=fr0styann_sana;User ID=fr0styann_sana;Password=Sana2022;TrustServerCertificate=true";
        public int Create(User user)
        {
            string sqlQuery = @"INSERT INTO Users
                              (Email, FirstName, LastName, Password) 
                              OUTPUT INSERTED.Id 
                              VALUES(@Email, @FirstName,@LastName, @Password)";
            using IDbConnection connection = new SqlConnection(ConnectionString);
                  var userId = connection.ExecuteScalar<int>(sqlQuery, user);
              return userId;
        }

        public void Delete(int userId)
        {
            string sqlQuery = @"DELETE FROM Users 
                                WHERE Id = @Id";
            using IDbConnection connection = new SqlConnection(ConnectionString);
                connection.Execute(sqlQuery, new { Id = userId });
        }

        public User GetByEmail(string email)
        {
            string sqlQuery = @"SELECT * FROM Users
                                WHERE Email = @Email";
            using IDbConnection connection = new SqlConnection(ConnectionString);
                  var user = connection.QueryFirstOrDefault<User>(sqlQuery, new { Email = email });
            return user;
        }
        public void Update(User user)
        {
            string sqlQuery = @"UPDATE Users 
                             SET Email = @Email, FirstName = @FirstName, LastName = @LastName,Password = @Password 
                             WHERE Id = @Id";
            using IDbConnection connection = new SqlConnection(ConnectionString);
                  connection.Execute(sqlQuery, user);  
        }
        public User GetById(int userId)
        {
            string sqlQuery = @"SELECT * FROM Users 
                                WHERE Id = @Id";
            using IDbConnection connection = new SqlConnection(ConnectionString);
                  var user = connection.QueryFirstOrDefault<User>(sqlQuery, new { Id = userId });
            return user;
        }
    }
}

using Server.Business.Interfaces;
using Server.Business.Entities;
using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Server.MSSQL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string connectionString;
        public UserRepository(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("MsSql");
        }
        public int Create(User user)
        {
            string sqlQuery = @"INSERT INTO Users
                              (Email, Firstname, Lastname, Password) 
                              OUTPUT INSERTED.Id 
                              VALUES(@Email, @Firstname,@Lastname, @Password)";
            using IDbConnection connection = new SqlConnection(connectionString);
                  var userId = connection.ExecuteScalar<int>(sqlQuery, user);
              return userId;
        }

        public void Delete(int userId)
        {
            string sqlQuery = @"DELETE FROM Users 
                                WHERE Id = @Id";
            using IDbConnection connection = new SqlConnection(connectionString);
                connection.Execute(sqlQuery, new { Id = userId });
        }

        public User GetByEmail(string email)
        {
            string sqlQuery = @"SELECT * FROM Users
                                WHERE Email = @Email";
            using IDbConnection connection = new SqlConnection(connectionString);
                  var user = connection.QueryFirstOrDefault<User>(sqlQuery, new { Email = email });
            return user;
        }
        public void Update(User user)
        {
            string sqlQuery = @"UPDATE Users 
                             SET Email = @Email, Firstname = @Firstname, Lastname = @Lastname,Password = @Password 
                             WHERE Id = @Id";
            using IDbConnection connection = new SqlConnection(connectionString);
                  connection.Execute(sqlQuery, user);  
        }
        public User GetById(int userId)
        {
            string sqlQuery = @"SELECT * FROM Users 
                                WHERE Id = @Id";
            using IDbConnection connection = new SqlConnection(connectionString);
                  var user = connection.QueryFirstOrDefault<User>(sqlQuery, new { Id = userId });
            return user;
        }
    }
}

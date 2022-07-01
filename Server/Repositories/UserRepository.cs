using Server.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Server.Models;
using System.Data;

namespace Server.Repositories
{
    public class UserRepository : IUserRepository
    {
        //public SqlConnectionStringBuilder ConnectionStringBuilder = new SqlConnectionStringBuilder()
        //{
        //    DataSource = @"sql.bsite.net\MSSQL2016",
        //    UserID = "fr0styann_sana",
        //    Password = "Sana2022",
        //    InitialCatalog = "fr0styann_sana"
        //};
        string ConnectionString = @"Data Source=sql.bsite.net\MSSQL2016;Initial Catalog=fr0styann_sana;User ID=fr0styann_sana;Password=Sana2022;TrustServerCertificate=true";
        public int CreateUser(User user)
        {
            var sqlQuery = "Insert into Users(Email, FirstName, SecondName, Password) OUTPUT INSERTED.Id VALUES(@Email, @FirstName," +
                 "@SecondName, @Password)";
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var userId = db.ExecuteScalar<int>(sqlQuery, user);
                return userId;
            }
        }

        public void DeleteUser(int userId)
        {
            var sqlQuery = "DELETE from Users Where Id = @Id";
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                db.Execute(sqlQuery, new { Id = userId });
            }
        }

        public User GetUserById(int userId)
        {
            var sqlQuery = "SELECT * from Users Where Id = @Id";
            using(IDbConnection db = new SqlConnection(ConnectionString))
            {
                var user = db.QueryFirstOrDefault<User>(sqlQuery, new { Id = userId });
                return user;
            }
        }

        public void UpdateUser(User user)
        {
            var sqlQuery = "Update Users Set Email = @Email, FirstName = @FirstName, SecondName = @SecondName," +
                "Password = @Password Where Id = @Id";
            using(IDbConnection db = new SqlConnection(ConnectionString))
            {
                db.Execute(sqlQuery, user);
            }
        }
    }
}

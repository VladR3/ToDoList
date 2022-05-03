using Business.entities;
using Business.providers;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ToDoList.Models
{
    
    public class CategoryRepository : ICategoryProvider
    {
        string connectionString = null;
        public CategoryRepository(string connection)
        {
            connectionString = connection;
        }
        public List<Category> GetCategories()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Category>("SELECT * FROM Category").ToList();
            }
        }

        public Category Get(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Category>("SELECT * FROM Category WHERE idCategory = @id", new { id }).FirstOrDefault();
            }
        }

        public void Create(Category category)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO Category (nameCategory) VALUES(@nameCategory)";
                db.Execute(sqlQuery, category);

                // если мы хотим получить id добавленного пользователя
                //var sqlQuery = "INSERT INTO Users (Name, Age) VALUES(@Name, @Age); SELECT CAST(SCOPE_IDENTITY() as int)";
                //int? userId = db.Query<int>(sqlQuery, user).FirstOrDefault();
                //user.Id = userId.Value;
            }
        }

        public void Update(Category category)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE Category SET nameCategory = @nameCategory WHERE idCategory = @idCategory";
                db.Execute(sqlQuery, category);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Category WHERE idCategory = @id";
                db.Execute(sqlQuery, new { id });
            }
        }
    }
}

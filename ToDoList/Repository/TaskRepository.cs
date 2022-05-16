using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System;
using Business.providers;
using Business.entities;

namespace ToDoList.Models
{
    
        public class TaskRepository : ITaskProvider
        {
            string connectionString = null;
            public TaskRepository(string connection)
            {
                connectionString = connection;
            }
            public List<Task> GetActiveTasks()
            {
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    return db.Query<Task>("SELECT * FROM Task WHERE status = 0 ORDER BY deadLine, mark").ToList();
                }
            }
            public List<Task> GetCompletedTasks()
            {
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    return db.Query<Task>("SELECT * FROM Task WHERE status = 1 ORDER BY deadLine, mark").ToList();
                }
            }

            public Task Get(int id)
            {
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    return db.Query<Task>("SELECT * FROM Task WHERE idTask = @id", new { id }).FirstOrDefault();
                }
            }

            public void Create(Task task)
            {
            task.dateOfCreate = DateTime.Now;
            using (IDbConnection db = new SqlConnection(connectionString))
                {
                    var sqlQuery = "INSERT INTO Task (text, dateOfCreate, mark, idCategory, status, deadline) VALUES(@text, @dateOfCreate, @mark, @idCategory, @status, @deadline)";
                    db.Execute(sqlQuery, task);

                    
                    //var sqlQuery = "INSERT INTO Users (Name, Age) VALUES(@Name, @Age); SELECT CAST(SCOPE_IDENTITY() as int)";
                    //int? userId = db.Query<int>(sqlQuery, user).FirstOrDefault();
                    //user.Id = userId.Value;
                }
            }

            public void Update(Task task)
            {
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    var sqlQuery = "UPDATE Task SET text = @text, dateOfCreate = @dateOfCreate, mark = @mark, idCategory = @idCategory, status = @status, deadline = @deadline WHERE idTask = @idTask";
                    db.Execute(sqlQuery, task);
                }
            }

            public void Delete(int id)
            {
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    var sqlQuery = "DELETE FROM Task WHERE idTask = @id";
                    db.Execute(sqlQuery, new { id });
                }
            }

        public void Perform(Task task, int id, string status)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "";
                if (status == "notDone")
                {
                    sqlQuery = "UPDATE Task SET status = 1 WHERE idTask = @idTask";
                }
                else
                {
                    sqlQuery = "UPDATE Task SET status = 0 WHERE idTask = @idTask";
                }
                db.Execute(sqlQuery, task);
            }
        }
    }
    }


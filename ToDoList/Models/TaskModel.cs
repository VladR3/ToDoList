using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class TaskModel
    {
       public int idTask { get; set; }
       public string text { get; set; }
       public int mark { get; set; }
       public int idCategory { get; set; }
       public int status { get; set; }
       public DateTime deadLine { get; set; }
    }
}

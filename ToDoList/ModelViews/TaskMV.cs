using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ToDoList.Models
{
    public class TaskMV
    {
        public TaskModel task { get; set; }
        public List<TaskModel> activeTaskList { get; set; }
        public List<TaskModel> completedTaskList { get; set; }
        public List<CategoryModel> categoryModels { get; set; }

        public TaskMV(TaskModel task, List<TaskModel> activeTaskList, List<TaskModel> completedTaskLis, List<CategoryModel> categoryModels)
        {
            this.task = task;
            this.activeTaskList = activeTaskList;
            this.completedTaskList = completedTaskLis;
            this.categoryModels = categoryModels;
        }
    }
}

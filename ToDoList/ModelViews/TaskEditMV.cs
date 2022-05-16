using System.Collections.Generic;
using ToDoList.Models;

namespace ToDoList.ModelViews
{
    public class TaskEditMV
    {
        public TaskModel task { get; set; }
        public List<CategoryModel> categories { get; set; }
    }
}

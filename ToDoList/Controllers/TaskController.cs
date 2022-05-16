using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using ToDoList.ModelViews;
using System.Collections.Generic;
using Business.providers;
using AutoMapper;
using Business.entities;
using ToDoList.Validation;
using System;


namespace ToDoList.Controllers
{
    public class TaskController : Controller
    {

        //private readonly IConfiguration configuration;

        private readonly ITaskProvider taskProvider;
        private readonly ICategoryProvider categoryProvider;
        private readonly IMapper mapper;
        private ValidationTask validationTask = new();
        public TaskController(ITaskProvider taskProvider, ICategoryProvider categoryProvider, IMapper mapper)
        {
            this.taskProvider = taskProvider;
            this.categoryProvider = categoryProvider;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var activeTaskList = taskProvider.GetActiveTasks();
            var completedTaskList = taskProvider.GetCompletedTasks();
            var categoryList = categoryProvider.GetCategories();

            return View(new TaskMV
            {
                task = new(),
                activeTaskList = mapper.Map<List<TaskModel>>(activeTaskList),
                completedTaskList = mapper.Map<List<TaskModel>>(completedTaskList),
                categoryModels = mapper.Map<List<CategoryModel>>(categoryList)
            });

        }

        //public ActionResult Details(int id)
        //{
        //    TaskModel task = taskR.Get(id);
        //    if (task != null)
        //        return View(task);
        //    return NotFound();
        //}

        [HttpPost]
        public IActionResult Create(TaskModel task)
        {
            List<string> errors = validationTask.isValidTask(mapper.Map<Task>(task));
            if (errors.Count > 0)
            {
                var activeTaskList = taskProvider.GetActiveTasks();
                var completedTaskList = taskProvider.GetCompletedTasks();
                var categoryList = categoryProvider.GetCategories();

                foreach (var error in errors)
                {
                    ModelState.AddModelError("", error);
                }

                return View("Index", new TaskMV
                {
                    task = task,
                    activeTaskList = mapper.Map<List<TaskModel>>(activeTaskList),
                    completedTaskList = mapper.Map<List<TaskModel>>(completedTaskList),
                    categoryModels = mapper.Map<List<CategoryModel>>(categoryList)
                });
            }
            taskProvider.Create(mapper.Map<Task>(task));
            return RedirectToAction("Index");

        }


        [HttpGet]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int id, string ok)
        {
            if (ok == "delete")
            {
                taskProvider.Delete(id);
                return RedirectToAction("Index");
            }
            Task task = taskProvider.Get(id);
            if (task != null)
            {
                //TaskModel taskModel = new TaskModel();
                //taskModel.idTask = task.idTask;
                //taskModel.text = task.text;
                //taskModel.mark = task.mark;
                //taskModel.idCategory = task.idCategory;
                //taskModel.status = task.status;
                //taskModel.deadLine = task.deadLine;
                return View("InformDelete", mapper.Map<TaskModel>(task));
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var task = taskProvider.Get(id);
            if(task != null)
            {
                var categoryList = categoryProvider.GetCategories();
                return View(new TaskEditMV
                {
                    task = mapper.Map<TaskModel>(task),
                    categories = mapper.Map<List<CategoryModel>>(categoryList)
                });
            }
            return NotFound();
        }

        [HttpPost]
        [ActionName("Edit")]
        public IActionResult EditIsDone(TaskModel task)
        {
            List<string> errors = validationTask.isValidTask(mapper.Map<Task>(task));
            if (errors.Count == 0)
            {
                var newTask = mapper.Map<Task>(task);
                newTask.dateOfCreate = DateTime.Now;
                taskProvider.Update(newTask);
                return RedirectToAction("Index");
            }
            foreach(var error in errors)
            {
                ModelState.AddModelError("", error);
            }
            var categoryList = categoryProvider.GetCategories();
            return View(new TaskEditMV
            {
                task = mapper.Map<TaskModel>(task),
                categories = mapper.Map<List<CategoryModel>>(categoryList)
            });
        }

        [HttpGet]
        [ActionName("Completed")]
        public IActionResult CompleteTask(int idTaskComplete)
        {
            Task task = taskProvider.Get(idTaskComplete);
            if (task.status == 0)
            {
                taskProvider.Perform(task, idTaskComplete, "notDone");
            }
            else
            {
                taskProvider.Perform(task, idTaskComplete, "Done");
            }
            return RedirectToAction("Index");
        }
    }
}



//var taskList1 = taskProvider.GetTasks();
//var categoryList = categoryProvider.GetCategories();
//return View(new TaskMV
//{
//    task = new(),
//    taskList = mapper.Map<List<TaskModel>>(taskList1),
//    categoryModels = mapper.Map<List<CategoryModel>>(categoryList)
//});


//var activeTaskList = taskProvider.GetTasks();
//List<TaskModel> taskListM = new List<TaskModel>(taskList.Count);

//for (int i = 0; i < taskList.Count; i++)
//{
//    TaskModel taskModel = new TaskModel();
//    taskModel.idTask = taskList[i].idTask;
//    taskModel.text = taskList[i].text;
//    taskModel.mark = taskList[i].mark;
//    taskModel.idCategory = taskList[i].idCategory;
//    taskModel.status = taskList[i].status;
//    taskModel.deadLine = taskList[i].deadLine;
//    taskListM.Add(taskModel);
//}
//var categoryList = categoryProvider.GetCategories();
//List<CategoryModel> categoryListM = new List<CategoryModel>(categoryList.Count);
//for (int i = 0; i < categoryList.Count; i++)
//{
//    CategoryModel categoryModel = new CategoryModel();
//    categoryModel.idCategory = categoryList[i].idCategory;
//    categoryModel.nameCategory = categoryList[i].nameCategory;
//    categoryListM.Add(categoryModel);
//}
//TaskMV taskMV = new(new(), taskListM, categoryListM);
//return View("Index", taskMV);



//public actionresult edit(int id)
//{
//    taskmodel task = taskr.get(id);
//    if (task != null)
//        return view(task);
//    return notfound();
//}

// [HttpPost]
// public ActionResult Edit(TaskModel task)
// {
//     taskR.Update(task);
//     return RedirectToAction("Index");
// }
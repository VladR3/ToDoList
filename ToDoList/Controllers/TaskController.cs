using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;

using ToDoList.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using Business.providers;
using AutoMapper;
using Business.entities;
using DocumentFormat.OpenXml.Presentation;


namespace ToDoList.Controllers
{
    public class TaskController : Controller
    {

        //private readonly IConfiguration configuration;
        
        private readonly ITaskProvider taskProvider;
        private readonly ICategoryProvider categoryProvider;
        //private readonly IMapper mapper;
        public TaskController(ITaskProvider taskProvider, ICategoryProvider categoryProvider)
        {
            this.taskProvider = taskProvider;
            this.categoryProvider = categoryProvider;
            //this.mapper = mapper;
        }

        public IActionResult Index()
        {
            //var taskList1 = taskProvider.GetTasks();
            //var categoryList = categoryProvider.GetCategories();
            //return View(new TaskMV
            //{
            //    task = new(),
            //    taskList = mapper.Map<List<TaskModel>>(taskList1),
            //    categoryModels = mapper.Map<List<CategoryModel>>(categoryList)
            //});

            var activeTaskList = taskProvider.GetActiveTasks();
            List<TaskModel> activeTaskListM = new List<TaskModel>(activeTaskList.Count);
            
            for(int i = 0; i < activeTaskList.Count; i++)
            {
                TaskModel taskModel = new TaskModel();
                taskModel.idTask = activeTaskList[i].idTask;
                taskModel.text = activeTaskList[i].text;
                taskModel.mark = activeTaskList[i].mark;
                taskModel.idCategory = activeTaskList[i].idCategory;
                taskModel.status = activeTaskList[i].status;
                taskModel.deadLine = activeTaskList[i].deadLine;
                activeTaskListM.Add(taskModel);
            }
            var completedTaskList = taskProvider.GetCompletedTasks();
            List<TaskModel> completedTaskListM = new List<TaskModel>(completedTaskList.Count);

            for (int i = 0; i < completedTaskList.Count; i++)
            {
                TaskModel taskModel = new TaskModel();
                taskModel.idTask = completedTaskList[i].idTask;
                taskModel.text = completedTaskList[i].text;
                taskModel.mark = completedTaskList[i].mark;
                taskModel.idCategory = completedTaskList[i].idCategory;
                taskModel.status = completedTaskList[i].status;
                taskModel.deadLine = completedTaskList[i].deadLine;
                completedTaskListM.Add(taskModel);
            }
            var categoryList = categoryProvider.GetCategories();
            List<CategoryModel> categoryListM = new List<CategoryModel>(categoryList.Count);
            for(int i = 0; i < categoryList.Count; i++)
            {
                CategoryModel categoryModel = new CategoryModel();
                categoryModel.idCategory=categoryList[i].idCategory;
                categoryModel.nameCategory = categoryList[i].nameCategory;
                categoryListM.Add(categoryModel);
            }
            TaskMV taskMV = new(new(), activeTaskListM, completedTaskListM, categoryListM);
            return View(taskMV);
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
            Task taskDB = new Task();
            taskDB.idTask = task.idTask;
            taskDB.text = task.text;
            taskDB.mark = task.mark;
            taskDB.idCategory = task.idCategory;
            taskDB.status = task.status;
            taskDB.deadLine = task.deadLine;
            taskProvider.Create(taskDB);
            return RedirectToAction("Index");
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
        }

        // public ActionResult Edit(int id)
        // {
        //     TaskModel task = taskR.Get(id);
        //     if (task != null)
        //         return View(task);
        //     return NotFound();
        // }

        // [HttpPost]
        // public ActionResult Edit(TaskModel task)
        // {
        //     taskR.Update(task);
        //     return RedirectToAction("Index");
        // }

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
                TaskModel taskModel = new TaskModel();
                taskModel.idTask = task.idTask;
                taskModel.text = task.text;
                taskModel.mark = task.mark;
                taskModel.idCategory = task.idCategory;
                taskModel.status = task.status;
                taskModel.deadLine = task.deadLine;
                return View("InformDelete", taskModel);
            }
            return NotFound();
        }

        [HttpGet]
        [ActionName("Completed")]
        public IActionResult CompleteTask(int idTaskComplete)
        {
            if (idTaskComplete != null)
            {
                Task task = taskProvider.Get(idTaskComplete);
                taskProvider.Perform(task, idTaskComplete);
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}

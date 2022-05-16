using AutoMapper;
using Business.entities;
using Business.providers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ToDoList.Models;
using ToDoList.ModelViews;
using ToDoList.Validation;

namespace ToDoList.Controllers
{
    public class CategoryController : Controller
    {
        //private readonly IConfiguration configuration;

        private readonly ITaskProvider taskProvider;
        private readonly ICategoryProvider categoryProvider;
        private readonly IMapper mapper;
        private ValidationCategory validationCategory = new();
        public CategoryController(ITaskProvider taskProvider, ICategoryProvider categoryProvider, IMapper mapper)
        {
            this.taskProvider = taskProvider;
            this.categoryProvider = categoryProvider;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var categoryList = categoryProvider.GetCategories();
            List<CategoryModel> categoryListM = new List<CategoryModel>(categoryList.Count);
            for (int i = 0; i < categoryList.Count; i++)
            {
                CategoryModel categoryModel = new CategoryModel();
                categoryModel.idCategory = categoryList[i].idCategory;
                categoryModel.nameCategory = categoryList[i].nameCategory;
                categoryListM.Add(categoryModel);
            }
            CategoryMV categoryMV = new(new(), categoryListM);
            return View(categoryMV);
        }

        //public ActionResult Details(int id)
        //{
        //    TaskModel task = taskR.Get(id);
        //    if (task != null)
        //        return View(task);
        //    return NotFound();
        //}

        [HttpPost]
        public IActionResult Create(CategoryModel category)
        {
            Category categoryDB = new Category();
            categoryDB.idCategory = category.idCategory;
            categoryDB.nameCategory = category.nameCategory;
            categoryProvider.Create(categoryDB);
            var categoryList = categoryProvider.GetCategories();
            List<CategoryModel> categoryListM = new List<CategoryModel>(categoryList.Count);
            for (int i = 0; i < categoryList.Count; i++)
            {
                CategoryModel categoryModel = new CategoryModel();
                categoryModel.idCategory = categoryList[i].idCategory;
                categoryModel.nameCategory = categoryList[i].nameCategory;
                categoryListM.Add(categoryModel);
            }
            CategoryMV categoryMV = new(new(), categoryListM);
            return View("Index", categoryMV);
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
                categoryProvider.Delete(id);
                return RedirectToAction("Index");
            }
                Category category = categoryProvider.Get(id);
            if (category != null)
            {
                CategoryModel categoryModel = new CategoryModel();
                categoryModel.idCategory = category.idCategory;
                categoryModel.nameCategory = category.nameCategory;
                return View("InformDelete", categoryModel);
            }
            return NotFound();
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            categoryProvider.Delete(id);
            return RedirectToAction("Index");
        }
    }
}


//var categoryList = categoryProvider.GetCategories();
//List<CategoryModel> categoryListM = new List<CategoryModel>(categoryList.Count);
//for (int i = 0; i < categoryList.Count; i++)
//{
//    CategoryModel categoryModel = new CategoryModel();
//    categoryModel.idCategory = categoryList[i].idCategory;
//    categoryModel.nameCategory = categoryList[i].nameCategory;
//    categoryListM.Add(categoryModel);
//}
//CategoryMV categoryMV = new(new(), categoryListM);
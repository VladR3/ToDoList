using System.Collections.Generic;
using ToDoList.Models;

namespace ToDoList.ModelViews
{
    public class CategoryMV
    {
        public CategoryModel category { get; set; }
        public List<CategoryModel> categoryList { get; set; }

        public CategoryMV(CategoryModel category, List<CategoryModel> categoryList)
        {
            this.category = category;
            this.categoryList = categoryList;
        }
    }
}

using Business.entities;
using System.Collections.Generic;

namespace ToDoList.Validation
{
    public class ValidationCategory
    {
        public List<string> isValidTask(Category category)
        {
            List<string> errors = new List<string>();
            if (string.IsNullOrEmpty(category.nameCategory))
                errors.Add("Category must have name");
            return errors;
        }
    }
}

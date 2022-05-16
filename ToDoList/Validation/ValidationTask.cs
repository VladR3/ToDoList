using Business.entities;
using System;
using System.Collections.Generic;

namespace ToDoList.Validation
{
    public class ValidationTask
    {
        public List<string> isValidTask(Task task)
        {
            List<string> errors = new List<string>();
            if (string.IsNullOrEmpty(task.text))
                errors.Add("Task must have text");
            if (task.deadLine < DateTime.Now)
                errors.Add("Date must be greater than today");

            return errors;
        }
    }
}

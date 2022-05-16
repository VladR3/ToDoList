using AutoMapper;
using Business.entities;
using System.Collections.Generic;
using ToDoList.Models;

namespace ToDoList
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            
            CreateMap<Task,TaskModel>();
            CreateMap<Category, CategoryModel>();
            CreateMap<TaskModel, Task>();
        }
    }
}

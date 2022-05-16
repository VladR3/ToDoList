using Business.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.providers
{
    public interface ITaskProvider
    {
        void Create(Task task);
        void Delete(int id);
        Task Get(int id);
        List<Task> GetActiveTasks();
        List<Task> GetCompletedTasks();
        void Update(Task task);
        void Perform(Task task,int id, string status);
    }
}

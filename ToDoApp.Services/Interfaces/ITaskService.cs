using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Data.Models;

namespace ToDoApp.Services.Interfaces
{
    public interface ITaskService
    {
        Task<TaskItem> CreateTaskAsync(TaskItem task);
        Task<TaskItem> GetTaskByIdAsync(int taskId);
        Task<IEnumerable<TaskItem>> GetAllTasksAsync();
        Task UpdateTaskAsync(TaskItem task);
        Task DeleteTaskAsync(int taskId);
    }
}

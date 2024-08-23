using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Data.Context;
using ToDoApp.Data.Models;
using ToDoApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ToDoApp.Services.Services
{
    public class TaskService : ITaskService
    {
        private readonly ToDoContext _context;

        public TaskService(ToDoContext context)
        {
            _context = context;
        }

        public async Task<TaskItem> CreateTaskAsync(TaskItem task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<TaskItem> GetTaskByIdAsync(int taskId)
        {
            return await _context.Tasks
                                 .Include(t => t.Status)
                                 .Include(t => t.Assignee)
                                 .Include(t => t.Board)
                                 .FirstOrDefaultAsync(t => t.Id == taskId);
        }

        public async Task<IEnumerable<TaskItem>> GetAllTasksAsync()
        {
            return await _context.Tasks
                                 .Include(t => t.Status)
                                 .Include(t => t.Assignee)
                                 .Include(t => t.Board)
                                 .ToListAsync();

        }

        public async Task UpdateTaskAsync(TaskItem task)
        {
            var existingTask = await _context.Tasks.FindAsync(task.Id);
            if (existingTask == null)
            {
                throw new Exception("Task not found");
            }

            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            existingTask.AssigneeId = task.AssigneeId;

            // Валідація статусу
            ValidateStatusTransition(existingTask.StatusId, task.StatusId);

            existingTask.StatusId = task.StatusId;

            _context.Tasks.Update(existingTask);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(int taskId)
        {
            var task = await _context.Tasks.FindAsync(taskId);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Task not found");
            }
        }

        private void ValidateStatusTransition(int currentStatusId, int newStatusId)
        {
            // Ваш код валідації статусів. Наприклад:
            if (currentStatusId == 1 && newStatusId != 2) // To Do -> In Progress
                throw new Exception("Invalid status transition");
            if (currentStatusId == 2 && (newStatusId != 1 && newStatusId != 3)) // In Progress -> To Do or Done
                throw new Exception("Invalid status transition");
        }
    }
}

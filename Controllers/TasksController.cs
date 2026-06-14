using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Data;
using TaskManagementApi.Models;

namespace TaskManagementApi.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TasksController(AppDbContext context)
        {
            _context = context;
        }
        // GET request to retrieve all tasks
        [HttpGet]
        public async Task<ActionResult<List<TaskItem>>> GetTasks()
        {
            return await _context.Tasks.ToListAsync();
        }
        // GET request (id) - find task by id
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItem>> GetTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            return task is null ? NotFound() : task;
        }
        // POST request to create a new task
        [HttpPost]
        public async Task<ActionResult<TaskItem>> CreateTask(TaskItem task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }
        // PUT request for update task title, description, status (IsCompleted)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, TaskItem updatedTask)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task is null) return NotFound();

            task.Title = updatedTask.Title;
            task.Description = updatedTask.Description;
            task.IsCompleted = updatedTask.IsCompleted;

            await _context.SaveChangesAsync();
            //204 - No Content
            return NoContent();
        }
        // PATCH request to mark as completed task
        [HttpPatch("{id}/complete")]
        public async Task<IActionResult> MarkComplete(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task is null) return NotFound();

            task.IsCompleted = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        // DELETE request for task deletion
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id) 
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task is null) return NotFound();

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

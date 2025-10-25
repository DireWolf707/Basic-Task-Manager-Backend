using System.Collections.Concurrent;
using Basic_Task_Manager.Data;
using Basic_Task_Manager.DTOs;
using Basic_Task_Manager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Basic_Task_Manager.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TaskController(TaskDbContext context) : ControllerBase
    {
        private readonly TaskDbContext db = context;

        [HttpGet]
        public async Task<ActionResult<ICollection<TaskItem>>> GetAllTask()
        {
            var tasks = await db.Tasks.ToListAsync();
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(TaskCreateDTO taskDTO)
        {
            if (string.IsNullOrWhiteSpace(taskDTO.Description))
                return BadRequest("Description is required!");

            var task = new TaskItem
            {
                Description = taskDTO.Description,
                IsCompleted = false
            };

            db.Add(task);
            await db.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(Guid id, TaskUpdateDTO taskDTO)
        {
            var task = await db.Tasks.FindAsync(id);
            if (task == null)
                return NotFound();

            task.IsCompleted = taskDTO.IsCompleted;
            await db.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            var task = await db.Tasks.FindAsync(id);
            if (task == null)
                return NotFound();

            db.Tasks.Remove(task);
            await db.SaveChangesAsync();

            return NoContent();
        }

    }
}
using System.Collections.Concurrent;
using Basic_Task_Manager.DTOs;
using Basic_Task_Manager.Models;
using Microsoft.AspNetCore.Mvc;

namespace Basic_Task_Manager.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TaskController : ControllerBase
    {
        private static readonly ConcurrentDictionary<Guid, TaskItem> tasks = new();

        [HttpGet]
        public ActionResult<ICollection<TaskItem>> GetAllTask()
        {
            return Ok(tasks.Values);
        }

        [HttpPost]
        public ActionResult<TaskItem> CreateTask(TaskCreateDTO taskDTO)
        {
            if (string.IsNullOrWhiteSpace(taskDTO.Description))
                return BadRequest("Description is required!");

            var newTask = new TaskItem
            {
                Id = Guid.NewGuid(),
                Description = taskDTO.Description,
                IsCompleted = false
            };

            tasks[newTask.Id] = newTask;

            return Ok(newTask);
        }

        [HttpPut("{id}")]
        public ActionResult<TaskItem> UpdateTask(Guid id, TaskUpdateDTO taskDTO)
        {
            if (!tasks.ContainsKey(id))
                return NotFound();

            tasks[id].IsCompleted = taskDTO.IsCompleted;

            return Ok(tasks[id]);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTask(Guid id)
        {
            if (!tasks.TryRemove(id, out _))
                return NotFound();

            return NoContent();
        }

    }
}
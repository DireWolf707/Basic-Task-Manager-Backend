namespace Basic_Task_Manager.Models
{
    public class TaskItem
    {
        public Guid Id { get; set; }
        public required string Description { get; set; }
        public bool IsCompleted { get; set; }
    }

}
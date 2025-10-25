namespace Basic_Task_Manager.DTOs
{
    public record TaskCreateDTO(string Description);

    public record TaskUpdateDTO(bool IsCompleted);
}
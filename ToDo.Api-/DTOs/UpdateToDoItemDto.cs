namespace ToDo.Api_.DTOs
{
    public class UpdateToDoItemDto
    {
        public string Name { get; set; } = string.Empty;
        public bool IsCompleted { get; set; } = false;
    }
}

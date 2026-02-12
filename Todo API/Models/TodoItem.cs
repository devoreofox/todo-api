namespace Todo_API.Models
{
    public record TodoItem
    {
        public Guid Id { get; init; }
        public string Title { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public bool IsComplete { get; init; }
        public DateTimeOffset? DueDate { get; init; }
        public DateTimeOffset? CreatedAt { get; init; }
        public DateTimeOffset? UpdatedAt { get; init; }
    }
}

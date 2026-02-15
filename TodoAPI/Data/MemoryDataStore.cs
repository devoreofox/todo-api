using TodoAPI.Models;

namespace TodoAPI.Data
{
    public static class MemoryDataStore
    {
        public static List<TodoProject> Projects { get; } = new();
    }
}

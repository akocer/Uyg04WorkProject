using Uyg04WorkProject.API.Models;

namespace Uyg04WorkProject.API.DTOs
{
    public class WorkDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string? AppUserId { get; set; }
        public int Score { get; set; }
        public int Order { get; set; }
    }
}

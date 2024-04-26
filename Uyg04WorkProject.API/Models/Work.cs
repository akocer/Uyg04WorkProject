namespace Uyg04WorkProject.API.Models
{
    public class Work
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int Score { get; set; }
        public int Order { get; set; }
        public List<WorkStep> WorkSteps { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}

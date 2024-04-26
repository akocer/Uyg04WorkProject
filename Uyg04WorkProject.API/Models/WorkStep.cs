namespace Uyg04WorkProject.API.Models
{
    public class WorkStep
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
        public int Score { get; set; }
        public int Order { get; set; }
        public int WorkId { get; set; }
        public Work Work { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}

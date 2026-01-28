namespace AcademicInfoHub.Api.Models
{
    public class InfoImage
    {
        public int Id { get; set; }
        public int InfoPostId { get; set; }
        public string ImageUrl { get; set; }

        public DateTime CreatedAt { get; set; }

        public InfoPost InfoPost { get; set; }
    }
}

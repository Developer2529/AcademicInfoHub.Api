namespace AcademicInfoHub.Api.DTOs
{
    public class InfoPostFeedDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Summary { get; set; }
        public string Category { get; set; }
        public bool HasExternalRedirect { get; set; }

        public bool IsActive { get; set; }
        public string? ExternalUrl { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

namespace AcademicInfoHub.Api.Models
{
    public class InfoPost
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Summary { get; set; }
        public string Content { get; set; } = string.Empty;
        public string? ExternalUrl { get; set; }
        public bool HasExternalRedirect { get; set; }
        public bool IsActive { get; set; }

        public string Category { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public ICollection<InfoImage>? Images { get; set; }
    }
}

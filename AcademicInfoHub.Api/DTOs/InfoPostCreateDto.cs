namespace AcademicInfoHub.Api.DTOs
{
    public class InfoPostCreateDto
    {
        public string Title { get; set; }
        public string? Summary { get; set; }
        public string Content { get; set; }
        public string Category { get; set; }
        public string? ExternalUrl { get; set; }
        public bool HasExternalRedirect { get; set; }
        public bool IsActive { get; set; } = true;
    }
}

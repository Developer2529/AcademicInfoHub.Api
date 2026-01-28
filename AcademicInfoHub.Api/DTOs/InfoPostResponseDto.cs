namespace AcademicInfoHub.Api.DTOs
{
    public class InfoPostResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Summary { get; set; }
        public string Content { get; set; }
        public string Category { get; set; }
        public string? ExternalUrl { get; set; }
        public bool HasExternalRedirect { get; set; }
        public bool IsActive { get; set; }  
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public List<InfoImageResponseDto> Images { get; set; }
    }
}

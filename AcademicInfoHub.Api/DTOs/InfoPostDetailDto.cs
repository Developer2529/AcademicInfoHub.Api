namespace AcademicInfoHub.Api.DTOs
{
    public class InfoPostDetailDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Summary { get; set; }
        public string Content { get; set; }
        public string Category { get; set; }
        public bool HasExternalRedirect { get; set; }
        public string? ExternalUrl { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<ImageDto> Images { get; set; }
    }

    public class ImageDto
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
    }
}

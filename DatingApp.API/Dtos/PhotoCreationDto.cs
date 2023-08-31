namespace DatingApp.API.Dtos
{
    public class PhotoCreationDto
    {
        public string? Url { get; set; }
        public IFormFile File { get; set; } // this is the photo we are uploading
        public string? Description { get; set; }
        public DateTime DateAdded { get; set; }
        public string? PublicId { get; set; }

        public PhotoCreationDto()
        {
            DateAdded = DateTime.Now;
        }
    }
}

namespace ShopTARge24.Core.Dto
{
    public class FileToApiDto
    {
        public Guid Id { get; set; }
        public Guid ImageId { get; set; } = Guid.Empty;
        public string? ExistingFilePath { get; set; }
        public string? Filepath { get; set; }
        public string? ImageTitle { get; set; }
        public byte[]? ImageData { get; set; }
        public Guid? SpaceshipId { get; set; }
        public Guid? KindergartenId { get; set; }
    }
}

namespace TechLekh.Application.Interfaces.Repositories
{
    public interface IImageRepository
    {
        Task<string> UploadAsync(Stream stream, string fileName);
    }
}

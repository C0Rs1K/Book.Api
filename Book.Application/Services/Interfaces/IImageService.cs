namespace Book.Application.Services.Interfaces;

public interface IImageService
{
    Task<string> UploadImageAsync(string fullbase64);
}

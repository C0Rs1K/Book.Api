using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Book.Application.Options;
using MediatR;
using Microsoft.Extensions.Options;

namespace Book.Application.UseCases.Image.UploadImage;

public class UploadImageHandler(IOptions<S3Settings> s3Settings) : IRequestHandler<UploadImageCommand, string>
{
    private readonly S3Settings _s3Settings = s3Settings.Value ?? new S3Settings();

    public async Task<string> Handle(UploadImageCommand request, CancellationToken cancellationToken)
    {
        var s3Client = new AmazonS3Client(_s3Settings.ConfigAccess,
            _s3Settings.ConfigSecret,
            RegionEndpoint.EUCentral1);

        var position = request.fullbase64.Split(new string[] { ";base64," }, StringSplitOptions.None);

        if (position.Length <= 1)
        {
            throw new BadImageFormatException("This is not valid base 64 format");
        }

        var base64 = position[1];
        var filename = GetFileName(request.fullbase64);

        await UploadImageIntoS3BucketAsync(s3Client, base64, filename);

        return $"https://s3.amazonaws.com/{_s3Settings.BucketName}/{filename}";
    }

    private async Task UploadImageIntoS3BucketAsync(AmazonS3Client s3Client, string base64, string filename)
    {
        byte[] bytes = Convert.FromBase64String(base64);

        using (s3Client)
        {
            var request = new PutObjectRequest
            {
                BucketName = _s3Settings.BucketName,
                CannedACL = S3CannedACL.PublicRead,
                Key = filename
            };

            using var stream = new MemoryStream(bytes);
            request.InputStream = stream;
            await s3Client.PutObjectAsync(request);
        }
    }

    private string GetFileName(string base64)
    {
        var extension = !string.IsNullOrEmpty(base64)
            ? base64.Split(new string[] { ";base64," }, StringSplitOptions.None)[0]
                    .Replace("data:image/", "")
            : "";

        string filename = $"{DateTime.UtcNow.ToString("yyyy-MM-dd-HH-mm-ss")}.{extension}";

        return filename;
    }
}

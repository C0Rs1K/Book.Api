using MediatR;

namespace Book.Application.UseCases.Image.UploadImage;

public record class UploadImageCommand(string fullbase64) : IRequest<string>;

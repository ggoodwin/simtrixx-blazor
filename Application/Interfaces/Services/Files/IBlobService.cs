using Application.Requests;

namespace Application.Interfaces.Services.Files
{
    public interface IBlobService
    {
        Task<string> UploadDocAsync(UploadRequest uploadRequest);
        Task<string> UploadPicAsync(UploadRequest uploadRequest);
    }
}

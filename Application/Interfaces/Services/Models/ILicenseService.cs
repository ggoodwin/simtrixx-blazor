using Common.Wrapper;

namespace Application.Interfaces.Services.Models
{
    public interface ILicenseService
    {
        Task<IResult<string>> ExportToExcelAsync(string searchString = "");
    }
}

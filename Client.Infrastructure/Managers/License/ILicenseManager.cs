using Application.Features.Licenses.Commands.AddEdit;
using Application.Features.Licenses.Commands.Import;
using Application.Features.Licenses.Queries.GetAll;
using Application.Features.Licenses.Queries.GetById;
using Application.Features.Licenses.Queries.GetByUser;
using Common.Wrapper;

namespace Client.Infrastructure.Managers.License
{
    public interface ILicenseManager : IManager
    {
        Task<IResult<IEnumerable<GetAllLicensesResponse>>> GetAllLicensesAsync();

        Task<IResult<IEnumerable<GetLicensesByUserResponse>>> GetLicensesByUserAsync();

        Task<IResult<GetLicenseByIdResponse>> GetLicenseByIdAsync(GetLicenseByIdQuery request);

        Task<IResult<string>> DownloadFileAsync(string searchString);

        Task<IResult<int>> ImportAsync(ImportLicensesCommand request);

        Task<IResult<int>> SaveAsync(AddEditLicenseCommand request);

        Task<IResult<int>> DeleteAsync(int id);
    }
}

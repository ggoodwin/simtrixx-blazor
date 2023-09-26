using System.Globalization;
using Application.Extensions;
using Application.Interfaces.Services.Documents;
using Application.Interfaces.Services.Models;
using Application.Specifications.Licenses;
using AutoMapper;
using Common.Wrapper;
using Domain.Entities;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Models
{
    public class LicenseService : ILicenseService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IExcelService _excelService;

        public LicenseService(
            IMapper mapper,
            DataContext context,
            IExcelService excelService)
        {
            _mapper = mapper;
            _context = context;
            _excelService = excelService;
        }

        public async Task<IResult<string>> ExportToExcelAsync(string searchString = "")
        {
            var licenseSpec = new LicenseFilterSpecification(searchString);
            var licenses = await _context.Licenses
                .Specify(licenseSpec)
                .OrderByDescending(a => a.Expiration)
                .ToListAsync();
            var data = await _excelService.ExportAsync(licenses, sheetName: "Licenses",
                mappers: new Dictionary<string, Func<License, object>>
                {
                    { "Key", item => item.Key },
                    { "Expiration Date", item => item.Expiration.ToString() },
                    { "User Email", item => item.SimtrixxUser.Email },
                    { "User Name", item => item.SimtrixxUser.UserName }
                });

            return await Result<string>.SuccessAsync(data: data);
        }
    }
}

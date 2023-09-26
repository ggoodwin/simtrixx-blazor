﻿using System.Security.Claims;
using Application.Interfaces.Services.Users;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services.Users
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            Claims = httpContextAccessor.HttpContext?.User?.Claims.AsEnumerable().Select(item => new KeyValuePair<string, string>(item.Type, item.Value)).ToList();
        }

        public string UserId { get; }
        public List<KeyValuePair<string, string>> Claims { get; set; }
    }
}

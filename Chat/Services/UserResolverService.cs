using Chat.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace Chat.Services
{
    public class UserResolverService : IUserResolver
    {
        private readonly IHttpContextAccessor _context;

        public UserResolverService(IHttpContextAccessor context)
        {
            _context = context;
        }

        public string GetCurrentUserId()
        {
            if (_context.HttpContext?.User == null)
                throw new UnauthorizedAccessException("Not authorized");

            return _context.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public string GetCurrentUserName()
        {
            if (_context.HttpContext?.User == null)
                throw new UnauthorizedAccessException("Not authorized");

            return _context.HttpContext?.User.Identity.Name;
        }
    }
}

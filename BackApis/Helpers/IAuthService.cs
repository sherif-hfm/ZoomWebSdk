﻿using System.Security.Claims;
using System.Collections.Generic;
using BackApis.Models;

namespace BackApis.Helpers
{
    public interface IAuthService
    {
        string SecretKey { get; set; }

        bool IsTokenValid(string token);
        string GenerateToken(IAuthContainerModel model);
        IEnumerable<Claim> GetTokenClaims(string token);
    }
}

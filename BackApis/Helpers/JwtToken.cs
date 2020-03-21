using BackApis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace BackApis.Helpers
{
    public static class JwtToken
    {
        public static string GenerateToken()
        {
            IAuthContainerModel model = new JWTContainerModel()
            {
                Claims = new Claim[]
                {
                    new Claim("iss", Settings.ApiKey)
                }
            };
            IAuthService authService = new JWTService(model.SecretKey);

            string token = authService.GenerateToken(model);
            return token;
        }
    }
}
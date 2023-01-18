﻿using AutoMapper;
using B2BWebApi.Models;
using BL.Constants;
using BL.Models.User;
using BL.Options;
using Extensions.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BL.Services.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly IMapper mapper;
        private readonly JwtOptions jwtOptions;

        public IdentityService(IMapper mapper, IOptions<JwtOptions> options)
        {
            this.mapper = mapper;
            jwtOptions = options.Value;
        }

        public IdentityModel GetIdentity(UserDto user)
        {
            var identity = user.Map<IdentityModel>(mapper);
            var claims = new[] {
                        new Claim(ClaimTypes.Role, user.IsAdmin ? Roles.Admin : Roles.Parent),
                        new Claim("UserID", identity.Id)
                    };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                        jwtOptions.Issuer,
                        jwtOptions.Audience,
                        claims,
                        expires: DateTime.UtcNow.AddDays(1),
                        signingCredentials: signIn);
            identity.Token = new JwtSecurityTokenHandler().WriteToken(token);
            return identity;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace BusinessLayer.Services.Token
{
    public interface ITokenService
    {
        Task<string> CreateToken(User user);
    }
}
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface ITokenRepository
    {
        string GenerateJwtToken(User user);
    }
}

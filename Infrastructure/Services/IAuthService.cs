using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Infrastructure.Services
{
    public interface IAuthService
    {
        void Register(User user, string password);
        User Login(string username, string password);
    }
}

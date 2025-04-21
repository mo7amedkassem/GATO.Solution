using Gato.Repository.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gato.Core.Service_Contract
{
    public interface IAuthServices
    {
        Task<string> CreateTokenAsync(User user , UserManager<User> userManager);


    }
}

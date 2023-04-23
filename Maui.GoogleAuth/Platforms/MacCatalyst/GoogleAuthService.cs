using Maui.GoogleAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui.GoogleAuth.Services
{

    public partial class GoogleAuthService 
    {
        public async Task<UserDTO> AuthenticateAsync()
        {
            return new UserDTO
            {
                Email = Guid.NewGuid().ToString(),
                FullName = Guid.NewGuid().ToString(),
                Id = Guid.NewGuid().ToString(),
                UserName = Guid.NewGuid().ToString()
            };
        }
        public async Task<UserDTO> GetCurrentUserAsync()
        {
           
            return new UserDTO
            {
                Email = Guid.NewGuid().ToString(),
                FullName = Guid.NewGuid().ToString(),
                Id = Guid.NewGuid().ToString(),
                UserName = Guid.NewGuid().ToString()
            };
        }
        public async Task LogoutAsync()
        { 
        
        }

    }
}

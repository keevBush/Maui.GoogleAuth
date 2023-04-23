using Maui.GoogleAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui.GoogleAuth.Services
{
    public interface IGoogleAuthService
    {
        Task<UserDTO> AuthenticateAsync();
        Task LogoutAsync();
        Task<UserDTO> GetCurrentUserAsync();

        private static IGoogleAuthService _instance;
        public static IGoogleAuthService Instance
        {
            get => _instance ?? (_instance = new GoogleAuthService());
        }
    }
    public partial class GoogleAuthService : IGoogleAuthService
    {
        
    }
}

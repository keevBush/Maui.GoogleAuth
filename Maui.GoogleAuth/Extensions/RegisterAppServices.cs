using Maui.GoogleAuth.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui.GoogleAuth.Extensions
{
    public static class RegisterAppServices
    {
        public static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddSingleton<IGoogleAuthService, GoogleAuthService>();

            return mauiAppBuilder;
        }
    }
}

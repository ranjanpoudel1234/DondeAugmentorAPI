using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace Donde.Augmentor.Web.Identity
{
    public static class SignInKeyExtension
    {     
        public static IIdentityServerBuilder AddCertificateFromFile(this IIdentityServerBuilder builder, 
            SignInKeyCredentialSettings settings)
        {
            var keyFilePath = settings.KeyFilePath;
            var keyFilePassword = settings.KeyFilePassword;

            if (File.Exists(keyFilePath))
            {
                var certificate = new X509Certificate2(keyFilePath, keyFilePassword);
                builder.AddSigningCredential(certificate);
            }

            return builder;
        }
    }
}

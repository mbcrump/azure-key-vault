using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;

namespace KeyVaultApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    var builtConfig = config.Build();

                    var authCallback = new KeyVaultClient.AuthenticationCallback
                        (new AzureServiceTokenProvider().KeyVaultTokenCallback);
                    var kvClient = new KeyVaultClient(authCallback);

                    config.AddAzureKeyVault(
                        $"https://{builtConfig["Vault"]}.vault.azure.net/",
                        kvClient,
                        new DefaultKeyVaultSecretManager()
                    );
                })
                .UseStartup<Startup>();
    }
}
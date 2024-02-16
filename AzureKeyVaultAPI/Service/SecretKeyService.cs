using Azure.Security.KeyVault.Secrets;

namespace AzureKeyVaultAPI.Service
{
    public class SecretKeyService : ISecretKeyService
    {
        public SecretClient SecretClient;
        public SecretKeyService(SecretClient secretClient) {

            this.SecretClient = secretClient;

        }
        public string GetDBKeyValue()
        {
            return this.SecretClient.GetSecret("DBConnectionString").ToString();
        }
    }
}

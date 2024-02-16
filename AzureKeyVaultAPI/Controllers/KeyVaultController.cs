using Azure.Security.KeyVault.Secrets;
using AzureKeyVaultAPI.Service;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;

namespace AzureKeyVaultAPI.Controllers
{
    public class KeyVaultController : Controller
    {
       public ISecretKeyService keyManager;
       public KeyVaultController(ISecretKeyService keyManager) {
            this.keyManager = keyManager;
        }

        [HttpGet]
        public IActionResult GetDBConnectioString()
        {
            try
            {
                return Ok(this.keyManager.GetDBKeyValue());
            }
            catch (Exception ex)
            {
              return  BadRequest();
            }
        }
    }
}

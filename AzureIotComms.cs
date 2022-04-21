using Microsoft.Azure.Devices;
using Microsoft.Extensions.Configuration;
using System;
using System.Text;
using System.Threading.Tasks;

namespace GA_FA
{
    public class AzureIotComms
    {
        private ServiceClient serviceClient;
        private string connectionString;
        private string targetDevice;
        private IConfiguration _config;

        public AzureIotComms(String cs, String dev)
        {
            this.connectionString = cs;
            this.targetDevice = dev;
        }

        public async Task SendCloudToDeviceMessageAsync(String data)
        {
            serviceClient = ServiceClient.CreateFromConnectionString(connectionString);
            Console.WriteLine(connectionString);
            Console.WriteLine(targetDevice);
            var commandMessage = new Message(Encoding.ASCII.GetBytes(data));
            await serviceClient.SendAsync(targetDevice, commandMessage);
        }

        public string getData() { 
            return connectionString;
        }
    }
}

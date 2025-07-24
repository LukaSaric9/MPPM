using FTN.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Connection
{
    public class GDAProxy
    {
        private readonly ChannelFactory<INetworkModelGDAContract> channelFactory;

        private static readonly Lazy<GDAProxy> instance = new Lazy<GDAProxy>(() => new GDAProxy());

        public static GDAProxy Instance => instance.Value;

        private GDAProxy()
        {
            try
            {
                var binding = new NetTcpBinding
                {
                    Security = { Mode = SecurityMode.None }
                };
                var endpointAddress = new EndpointAddress("net.tcp://localhost:10000/NetworkModelService/GDA/");

                channelFactory = new ChannelFactory<INetworkModelGDAContract>(binding, endpointAddress);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Failed to initialize ChannelFactory: " + ex.Message);
            }
        }

        public INetworkModelGDAContract GetProxy()
        {
            if (channelFactory == null)
            {
                throw new InvalidOperationException("ChannelFactory is not initialized.");
            }

            return channelFactory.CreateChannel();
        }

        public void Dispose()
        {
            if (channelFactory != null)
            {
                channelFactory.Close();
            }
        }
    }
}

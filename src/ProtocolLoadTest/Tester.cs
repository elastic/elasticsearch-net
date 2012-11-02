using System;
using Nest;

namespace ProtocolLoadTest
{
    internal abstract class Tester
    {
        protected void Connect(ElasticClient client, ConnectionSettings settings)
        {
            ConnectionStatus indexConnectionStatus;

            if (!client.TryConnect(out indexConnectionStatus))
            {
                Console.Error.WriteLine("Could not connect to {0}:\r\n{1}",
                    settings.Host, indexConnectionStatus.Error.OriginalException.Message);
                Console.Read();
                return;
            }
        }
    }
}

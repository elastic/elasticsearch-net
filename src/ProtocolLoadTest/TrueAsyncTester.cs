using System;
using System.Collections.Generic;
using System.Threading;
using Nest;

namespace ProtocolLoadTest
{
    internal class TrueAsyncTester : Tester, ITester
    {
        public void Run(string indexName, int port, int numMessages, int bufferSize)
        {
			var settings = this.CreateSettings(indexName, port);
			var connection = new NoTaskHttpConnection(settings);
            var client = new ElasticClient(settings, connection);
            
            Connect(client, settings);

			GenerateAndIndex(client, indexName, numMessages, bufferSize);
        }
    }
}

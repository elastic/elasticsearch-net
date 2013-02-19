using System;
using System.Collections.Generic;
using System.Threading;
using Nest;

namespace ProtocolLoadTest
{
    internal class HttpTester : Tester, ITester
    {
        public void Run(string indexName, int port, int numMessages, int bufferSize)
        {
			var settings = this.CreateSettings(indexName, port);
            var client = new ElasticClient(settings);
            
            Connect(client, settings);

			GenerateAndIndex(client, indexName, numMessages, bufferSize);
        }
    }
}

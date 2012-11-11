using System;
using System.Collections.Generic;
using System.Threading;
using Nest;
using Nest.Thrift;

namespace ProtocolLoadTest
{
    internal class ThriftTester : Tester, ITester
    {
        

        public void Run(string indexName, int port, int numMessages, int bufferSize)
        {
			// refresh == false is default on elasticsearch's side.
            var bulkParms = new SimpleBulkParameters() { Refresh = false };

            var settings = new ConnectionSettings("localhost", port)
                .SetDefaultIndex(indexName);

            var client = new ElasticClient(settings, new ThriftConnection(settings));
            
            Connect(client, settings);

			GenerateAndIndex(indexName, numMessages, bufferSize, bulkParms, client);
        }

		
    }
}

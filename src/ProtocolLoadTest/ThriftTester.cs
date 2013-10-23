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
			var settings = this.CreateSettings(indexName, port);
            var client = new ElasticClient(settings, new ThriftConnection(settings));
            
            Connect(client, settings);

			GenerateAndIndex(client, indexName, numMessages, bufferSize);
        }

	    public void SearchUsingSingleClient(string indexName, int port, int numMessages, int bufferSize)
	    {
		    throw new NotImplementedException();
	    }

	    public void SearchUsingMultipleClients(string indexName, int port, int numMessages, int bufferSize)
	    {
		    throw new NotImplementedException();
	    }
    }
}

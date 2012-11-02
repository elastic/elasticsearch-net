using System;
using System.Collections.Generic;
using System.Threading;
using Nest;
using Nest.Thrift;

namespace ProtocolLoadTest
{
    internal class ThriftTester : Tester, ITester
    {
        // Number of messages sent by all ThriftTester instances
        private static int NumSent;

        public void Run(string indexName, int port, int numMessages, int bufferSize)
        {
            var bulkParms = new SimpleBulkParameters() { Refresh = false };

            var settings = new ConnectionSettings("localhost", port)
                .SetDefaultIndex(indexName);

            var client = new ElasticClient(settings, new ThriftConnection(settings));
            
            Connect(client, settings);

            IList<Message> msgBuffer = new List<Message>(bufferSize);

            var msgGenerator = new MessageGenerator();

            foreach (var msg in msgGenerator.Generate(numMessages))
            {
                msgBuffer.Add(msg);

                // Flush buffer once max size reached
                if (msgBuffer.Count >= bufferSize)
                {
                    client.IndexMany(msgBuffer, indexName, bulkParms);
                    msgBuffer.Clear();

                    Interlocked.Add(ref NumSent, bufferSize);

                    // Output how many messages sent so far
                    if (NumSent % 10000 == 0)
                    {
                        Console.WriteLine("Sent {0:0,0} messages over Thrift", NumSent);
                    }
                }
            }
        }
    }
}

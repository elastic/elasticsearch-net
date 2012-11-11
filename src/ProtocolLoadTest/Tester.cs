using System;
using Nest;
using System.Collections.Generic;
using System.Threading;

namespace ProtocolLoadTest
{
    internal abstract class Tester
    {
		// Number of messages sent by all ThriftTester instances
		private static int NumSent;

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
		protected static void GenerateAndIndex(string indexName, int numMessages, int bufferSize, SimpleBulkParameters bulkParms, ElasticClient client)
		{
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

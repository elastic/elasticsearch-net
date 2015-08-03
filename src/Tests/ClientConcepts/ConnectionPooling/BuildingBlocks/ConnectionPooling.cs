using System;
using System.Collections.Specialized;
using System.Net;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.ConnectionPool;
using Nest;
using System.Text;
using Elasticsearch.Net.Providers;
using FluentAssertions;
using Tests.Framework;
using System.Linq;
using System.Collections.Generic;

namespace Tests.ClientConcepts.LowLevel
{
	public class ConnectionPooling
	{
		/** = Connection Pooling
		 * Connection pooling is the internal mechanism that takes care of registering what nodes there are in the cluster and which
		 * we can use to issue client calls on.
		 */
		
		/* == SingleNodeConnectionPool 
		* The simplest of all connection pools, this takes a single `Uri` and uses that to connect to elasticsearch for all the calls
		* It doesn't opt in to sniffing and pinging behavior, and will never mark nodes dead or alive. The one `Uri` it holds is always
		* ready to go. 
		*/
		[U] public void DefaultNowBehaviour()
		{
			var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9201"));
			pool.Nodes.Should().HaveCount(1);
			var node = pool.Nodes.First();
			node.Uri.Port.Should().Be(9201);
		}

	}
}

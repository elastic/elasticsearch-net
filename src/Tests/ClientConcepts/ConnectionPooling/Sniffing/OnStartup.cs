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
	public class OnStartupSniffing
	{
		/** == Sniffing on startup
		* 
		* Connection pools that return true for `SupportsReseeding` by default sniff on startup.
		*/

		[U] public void RequestPipeline()
		{
			var cluster = Cluster.Nodes(10)
				.Sniff(s => s.FailAlways())
				.Sniff(s => s.OnPort(9202).SucceedAlways())
				.SniffingConnectionPool().AllDefaults();

			var response = cluster.ClientCall();
		}
	}
}

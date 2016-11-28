using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Framework;
using System.Net;

namespace Tests.ClientConcepts.Connection
{
	public class CustomConnections
	{
		/**== Custom Connection Implementations
		*
		* The client abstracts sending the request and creating a response behind `IConnection`
		*
		* By default the client will use a WebRequest based version on the desktop CLR (.NET 4.5 and up targets)
		* and a HttpClient based HttpConnection specifically build for the Core CLR (netstandard 1.6).
		*
		* The reason for the split is because WebRequest and ServicePoint are not directly available on netstandard 1.6
		*
		* However the implementation written against WebRequest is the most matured implementation that we weren't ready to it give up.
		* There are also a couple of important toggles that are easy to set against a `ServicePoint` that we'd have to give up
		* had we jumped on the `HttpClient` completely.
		*
		* Another limitation is that `HttpClient` has no synchronous code paths and supporting that means doing hacky async patches which definitely
		* need time to bake.
		*
		* So why would you ever want to pass your own `IConnection`? Let's look at a couple of examples
		*
		*/

		public void OverrideHow()
		{
			var connection = new InMemoryConnection();
			var connectionPool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var settings = new ConnectionSettings(connectionPool, connection);
			var client = new ElasticClient(settings);
		}

		/**
		 * Here we create a new `ConnectionSettings` by using the overload that takes a `IConnectionPool` and an `IConnection`.
		 * We pass it an `InMemoryConnection` which using this constructor will return 200 for everything and never actually perform any IO.
		 *
		 * `InMemoryConnection` is great to write unit tests with as there is another overload that takes a fixed response stream to return.
		 *
		 *
		 * === ServicePoint hacking
		 *
		 * If you are running on the desktop CLR you can override specific properties for the current `ServicePoint` easily by overriding
		 * `AlterServicePoint` e.g to change the default connection limit to a specific endpoint from 80 to something higher.
		 * Remember though that we reuse TCP connections so changing this to something really high should only be done with careful consideration.
		 */

#if !DOTNETCORE
		public class MySpecialHttpConnection : HttpConnection
		{
			protected override void AlterServicePoint(ServicePoint requestServicePoint, RequestData requestData)
			{
				base.AlterServicePoint(requestServicePoint, requestData);
				requestServicePoint.ConnectionLimit = 10000;
				requestServicePoint.UseNagleAlgorithm = true;
			}

		}
	}
#endif
}

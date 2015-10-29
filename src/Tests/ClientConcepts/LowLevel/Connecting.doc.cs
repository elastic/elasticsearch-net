using System;
using System.Collections.Specialized;
using System.Net;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.ConnectionPool;
using Nest;

namespace Tests.ClientConcepts.LowLevel
{
	public class Connecting
	{
		/** # Connecting 
		 * Connecting to *Elasticsearch* with `Elasticsearch.Net` is quite easy but has a few toggles and options worth knowing.
		 * 
		 * # Choosing the right connection strategy
		 * If you simply new an `ElasticsearchClient`, it will be a non-failover connection to `http://localhost:9200`
		 */

		public void InstantiateUsingAllDefaults()
		{
			var client = new ElasticsearchClient();
			var tokenizers = new TokenizersDescriptor();

		}
		/**
		 * If your Elasticsearch node does not live at `http://localhost:9200` but i.e `http://mynode.example.com:8082/apiKey`, then 
		 * you will need to pass in some instance of `IConnectionConfigurationValues`.
		 * 
		 * The easiest way to do this is:
		 */

		public void InstantiatingASingleNodeClient()
		{
			var node = new Uri("http://mynode.example.com:8082/apiKey");
			var config = new ConnectionConfiguration(node);
			var client = new ElasticsearchClient(config);
		}

		/** 
		 * This however is still a non-failover connection. Meaning if that `node` goes down the operation will not be retried on any other nodes in the cluster.
		 * 
		 * To get a failover connection we have to pass an `IConnectionPool` instance instead of a `Uri`.
		 */

		public void InstantiatingAConnectionPoolClient()
		{
			var node = new Uri("http://mynode.example.com:8082/apiKey");
			var connectionPool = new SniffingConnectionPool(new[] { node });
			var config = new ConnectionConfiguration(connectionPool);
			var client = new ElasticsearchClient(config);
		}
		
		/** 
		 * Here instead of directly passing `node`, we pass a `SniffingConnectionPool` which will use our `node` to find out the rest of the available cluster nodes.
		 * Be sure to read more about [Connection Pooling and Cluster Failover here](/elasticsearch-net/cluster-failover.html)
		 * 
		 * ## Options
		 * 
		 *  Besides either passing a `Uri` or `IConnectionPool` to `ConnectionConfiguration`, you can also fluently control many more options. For instance:
		 */

		public void SpecifyingClientOptions()
		{
			//hide
			var node = new Uri("http://mynode.example.com:8082/apiKey");
			var connectionPool = new SniffingConnectionPool(new[] { node });
			//endhide

			var config = new ConnectionConfiguration(connectionPool)
				.EnableTrace()
				.DisableDirectStreaming()
				.SetBasicAuthentication("user", "pass")
				.SetTimeout(TimeSpan.FromSeconds(5));

		}
		/**
		 * The following is a list of available connection configuration options:
		 */

		public void AvailableOptions()
		{
			//hide
			var client = new ElasticsearchClient();
			//endhide

			var config = new ConnectionConfiguration()

				.DisableAutomaticProxyDetection()
				/** Disable automatic proxy detection.  Defaults to true. */

				.EnableHttpCompression()
				/**
				 * Enable compressed request and reesponses from Elasticsearch (Note that nodes need to be configured 
				 * to allow this.  See the [http module settings](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/modules-http.html) for more info).
				*/

				.EnableMetrics()
				/** Enable more meta data to be returned per API call about requests (ping, sniff, failover, and general stats). */

				.EnableTrace()
				/**
				* Will cause `Elasticsearch.Net` to write connection debug information on the TRACE output of your application.
				*/

				.DisableDirectStreaming()
				/**
				 * By default responses are deserialized off stream to the object you tell it to.
				 * For debugging purposes it can be very useful to keep a copy of the raw response on the result object. 
				 */;

			var result = client.Search<SearchResponse<object>>(new { size = 12 });
			var raw = result.ResponseBodyInBytes;
			/** This will only have a value if the client configuration has ExposeRawResponse set */

			/** 
			 * Please note that this only make sense if you need a mapped response and the raw response at the same time. 
			 * If you need a `string` or `byte[]` response simply call:
			 */
			var stringResult = client.Search<string>(new { });

			//hide
			config = config
				//endhide
				.SetConnectionStatusHandler(s => { })
				/** 
				* Allows you to pass a `Action&lt;IElasticsearchResponse&gt;` that can eaves drop every time a response (good or bad) is created. If you have complex logging needs 
				* this is a good place to add that in.
				*/

				.SetGlobalQueryStringParameters(new NameValueCollection())
				/**
				* Allows you to set querystring parameters that have to be added to every request. For instance, if you use a hosted elasticserch provider, and you need need to pass an `apiKey` parameter onto every request.
				*/

				.SetProxy(new Uri("http://myproxy"), "username", "pass")
				/** Sets proxy information on the connection. */

				.SetTimeout(TimeSpan.FromSeconds(4))
				/**
				* Sets the global maximum time a connection may take.
				 * Please note that this is the request timeout, the builtin .NET `WebRequest` has no way to set connection timeouts 
				 * (see http://msdn.microsoft.com/en-us/library/system.net.httpwebrequest.timeout(v=vs.110).aspx).
				*/

				.ThrowOnElasticsearchServerExceptions()
				/**
				* As an alternative to the C/go like error checking on `response.IsValid`, you can instead tell the client to always throw 
				 * an `ElasticsearchServerException` when a call resulted in an exception on the Elasticsearch server. Reasons for 
				 * such exceptions could be search parser errors and index missing exceptions.
				*/

				.PrettyJson()
				/**
				* Forces all serialization to be indedented and appends `pretty=true` to all the requests so that the responses are indented as well
				*/

				.SetBasicAuthentication("username", "password")
				/** Sets the HTTP basic authentication credentials to specify with all requests. */;

			/**
			* **Note:** This can alternatively be specified on the node URI directly:
			 */

			var uri = new Uri("http://username:password@localhost:9200");
			var settings = new ConnectionConfiguration(uri);

			/**
			*  ...but may become tedious when using connection pooling with multiple nodes.
			*/
		}

		public void ConfiguringSSL()
		{
			/**
			 * ## Configuring SSL
			 * SSL must be configured outside of the client using .NET's 
			 * [ServicePointManager](http://msdn.microsoft.com/en-us/library/system.net.servicepointmanager%28v=vs.110%29.aspx)
			 * class and setting the [ServerCertificateValidationCallback](http://msdn.microsoft.com/en-us/library/system.net.servicepointmanager.servercertificatevalidationcallback.aspx)
			 * property.
			 * 
			 * The bare minimum to make .NET accept self-signed SSL certs that are not in the Window's CA store would be to have the callback simply return `true`:
			 */

			ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, errors) => true;
			/**
			 * However, this will accept all requests from the AppDomain to untrusted SSL sites, 
			 * therefore we recommend doing some minimal introspection on the passed in certificate.
			 */
		}
	}
}

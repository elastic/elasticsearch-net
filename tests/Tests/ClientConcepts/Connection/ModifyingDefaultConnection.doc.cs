// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using System.Runtime.Serialization;
using Tests.Core.Extensions;
using Tests.Domain;
using Tests.Framework;
using Newtonsoft.Json;

namespace Tests.ClientConcepts.Connection
{
	/**[[modifying-default-connection]]
	 * === Modifying the default connection
	 *
	 * The client abstracts sending the request and creating a response behind `IConnection` and the default
	 * implementation uses https://msdn.microsoft.com/en-us/library/system.net.http.httpclient(v=vs.118).aspx[`System.Net.Http.HttpClient`].
	 *
	 * Why would you ever want to pass your own `IConnection`? Let's look at a couple of examples
	 */
	public class ModifyingTheDefaultConnection
	{
		/**==== Using InMemoryConnection
		 *
		 * `InMemoryConnection` is an in-built `IConnection` that makes it easy to write unit tests against. It can be
		 * configured to respond with default response bytes, HTTP status code and an exception when a call is made.
		 *
		 * `InMemoryConnection` **doesn't actually send any requests or receive any responses from Elasticsearch**;
		 * requests are still serialized and the request bytes can be obtained on the response if `.DisableDirectStreaming` is
		 * set to `true` on the request or globally
		 */
		public void InMemoryConnectionDefaultCtor()
		{
			var connection = new InMemoryConnection();
			var connectionPool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var settings = new ConnectionSettings(connectionPool, connection);
			var client = new ElasticClient(settings);
		}

		/**
		 * Here we create a new `ConnectionSettings` by using the overload that takes a `IConnectionPool` and an `IConnection`.
		 * We pass it an `InMemoryConnection` which, using the default parameterless constructor,
		 * will return 200 for everything and never actually perform any IO.
		 *
		 * Let's see a more complex example
		 */
		[U]
		public void InMemoryConnectionOverloadedCtor()
		{
			var response = new
			{
				took = 1,
				timed_out = false,
				_shards = new
				{
					total = 2,
					successful = 2,
					failed = 0
				},
				hits = new
				{
					total = new { value = 25 },
					max_score = 1.0,
					hits = Enumerable.Range(1, 25).Select(i => (object)new
					{
						_index = "project",
						_type = "project",
						_id = $"Project {i}",
						_score = 1.0,
						_source = new { name = $"Project {i}" }
					}).ToArray()
				}
			};

			var responseBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response));
			var connection = new InMemoryConnection(responseBytes, 200); // <1> `InMemoryConnection` is configured to **always** return `responseBytes` along with a 200 HTTP status code
			var connectionPool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var settings = new ConnectionSettings(connectionPool, connection).DefaultIndex("project");
			var client = new ElasticClient(settings);

			var searchResponse = client.Search<Project>(s => s.MatchAll());

			/**
			 * We can now assert that the `searchResponse` is valid and contains documents deserialized
			 * from our fixed `InMemoryConnection` response
			 */
			searchResponse.ShouldBeValid();
			searchResponse.Documents.Count.Should().Be(25);
		}

		/**
		* ==== Changing HttpConnection
		*
		* There may be a need to change how the default `HttpConnection` works, for example, to add an X509 certificate
		* to the request, change the maximum number of connections allowed to an endpoint, etc.
		*
		* By deriving from `HttpConnection`, it is possible to change the behaviour of the connection. The following
		* provides some examples
		*
		*/
#if DOTNETCORE
		public class MyCustomHttpConnection : HttpConnection
		{
			protected override void SetAuthenticationIfNeeded(HttpRequestMessage requestMessage, RequestData requestData)

			{
				var header = string.Empty;
				requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Negotiate", header);
			}
		}
#endif
#if !DOTNETCORE
		/**
		 * On .NET full framework the overrides on HttpConnection are different as they are geared towards using HttpWebRequest.
		 * Here are two examples for .NET full framework
		 */
		public class MyCustomHttpConnection : HttpConnection
		{
			protected override void AlterServicePoint(ServicePoint requestServicePoint, RequestData requestData)
			{
				base.AlterServicePoint(requestServicePoint, requestData);
				requestServicePoint.ConnectionLimit = 10000;
				requestServicePoint.UseNagleAlgorithm = true;
			}
		}
		public class X509CertificateHttpConnection : HttpConnection
		{
			protected override HttpWebRequest CreateHttpWebRequest(RequestData requestData)
			{
				var request = base.CreateHttpWebRequest(requestData);
				request.ClientCertificates.Add(new X509Certificate("file_path_to_cert"));
				return request;
			}
		}

		/**
		 * As before, a new instance of the custom connection is passed to `ConnectionSettings` in order to
		 * use
		 */
		public void UseX509CertificateHttpConnection()
		{
			var connection = new X509CertificateHttpConnection();
			var connectionPool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var settings = new ConnectionSettings(connectionPool, connection);
			var client = new ElasticClient(settings);
		}
		/**
		 * See <<working-with-certificates, Working with certificates>> for further details.
		 */
#endif
#if DOTNETCORE

		/*
		* [[kerberos-authentication]]
		* ===== Kerberos Authentication
		*
		* For a lot of use cases subclassing HttpConnection is a great way to customize the http connection for your needs.
		* E.g if you want to authenticate with Kerberos, creating a custom HttpConnection as followed allows you to set the right HTTP headers.
		*
		*
		* TIP use something like https://www.nuget.org/packages/Kerberos.NET/ to fill in the actual blanks of this implementation
		*/
		public class KerberosConnection : HttpConnection
		{
			protected override void SetAuthenticationIfNeeded(HttpRequestMessage requestMessage, RequestData requestData)
			{
				var header = string.Empty;
				requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Negotiate", header);
			}

		}
		/**
		 * See <<working-with-certificates, Working with certificates>> for further details.
		 */
#endif
	}
}

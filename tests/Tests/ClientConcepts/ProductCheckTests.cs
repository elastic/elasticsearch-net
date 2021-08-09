// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Net;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;

namespace Tests.ClientConcepts
{
	public class ProductCheckTests
	{
		[U] public void MissingProductNameHeaderCausesExceptionOnVersion7_14()
		{
			var productCheckResponse = InMemoryConnection.ValidProductCheckResponse();
			productCheckResponse.Headers.Clear();

			var connectionPool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var connectionSettings = new ConnectionSettings(connectionPool, new InMemoryConnection(productCheckResponse));
			var client = new ElasticClient(connectionSettings);

			client.Invoking(y => y.Cluster.Health())
				.Should()
				.Throw<UnsupportedProductException>();
		}

		[U] public void InvalidProductNameHeaderCausesException()
		{
			var productCheckResponse = InMemoryConnection.ValidProductCheckResponse();
			productCheckResponse.Headers.Clear();
			productCheckResponse.Headers.Add("X-elastic-product", new List<string> { "Something Unexpected" });

			var connectionPool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var connectionSettings = new ConnectionSettings(connectionPool, new InMemoryConnection(productCheckResponse));
			var client = new ElasticClient(connectionSettings);

			client.Invoking(y => y.Cluster.Health())
				.Should()
				.Throw<UnsupportedProductException>();
		}

		[U] public void UnauthorizedStatusCodeFromRootPathDoesNotThrowException_WithExpectedDataOnApiCall()
		{
			var productCheckResponse = ValidResponseFor713();
			productCheckResponse.StatusCode = (int)HttpStatusCode.Unauthorized;

			var connectionPool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var connectionSettings = new ConnectionSettings(connectionPool, new InMemoryConnection(productCheckResponse));
			var client = new ElasticClient(connectionSettings);

			var response = client.Cluster.Health();

			response.ApiCall.AuditTrail.Should().Contain(x => x.Event == AuditEvent.ProductCheckOnStartup);
			response.ApiCall.AuditTrail.Should().Contain(x => x.Event == AuditEvent.ProductCheckSuccess);
			response.ApiCall.DebugInformation.Should().Contain(RequestPipeline.UndeterminedProductWarning);
		}

		[U] public void ForbiddenStatusCodeFromRootPathDoesNotThrowException_WithExpectedDataOnApiCall()
		{
			var productCheckResponse = ValidResponseFor713();
			productCheckResponse.StatusCode = (int)HttpStatusCode.Forbidden;

			var connectionPool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var connectionSettings = new ConnectionSettings(connectionPool, new InMemoryConnection(productCheckResponse));
			var client = new ElasticClient(connectionSettings);

			var response = client.Cluster.Health();

			response.ApiCall.AuditTrail.Should().Contain(x => x.Event == AuditEvent.ProductCheckOnStartup);
			response.ApiCall.AuditTrail.Should().Contain(x => x.Event == AuditEvent.ProductCheckSuccess);
			response.ApiCall.DebugInformation.Should().Contain(RequestPipeline.UndeterminedProductWarning);
		}

		[U] public void OldVersionsThrowException()
		{
			var responseJson = new { version = new { number = "5.9.999", build_flavor = "default", }, tagline = "You Know, for Search" };

			var productCheckResponse = CreateResponseWithNoHeader(responseJson);

			var connectionPool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var connectionSettings = new ConnectionSettings(connectionPool, new InMemoryConnection(productCheckResponse));
			var client = new ElasticClient(connectionSettings);

			client.Invoking(y => y.Cluster.Health())
				.Should()
				.Throw<UnsupportedProductException>();
		}

		[U] public void MissingTaglineOnVersionSixThrowsException()
		{
			var responseJson = new { version = new { number = "6.10.0", build_flavor = "default" } };

			var productCheckResponse = CreateResponseWithNoHeader(responseJson);

			var connectionPool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var connectionSettings = new ConnectionSettings(connectionPool, new InMemoryConnection(productCheckResponse));
			var client = new ElasticClient(connectionSettings);

			client.Invoking(y => y.Cluster.Health())
				.Should()
				.Throw<UnsupportedProductException>();
		}

		[U] public void InvalidTaglineOnVersionSixThrowsException()
		{
			var responseJson = new { version = new { number = "6.10.0", build_flavor = "default" }, tagline = "unexpected" };

			var productCheckResponse = CreateResponseWithNoHeader(responseJson);

			var connectionPool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var connectionSettings = new ConnectionSettings(connectionPool, new InMemoryConnection(productCheckResponse));
			var client = new ElasticClient(connectionSettings);

			client.Invoking(y => y.Cluster.Health())
				.Should()
				.Throw<UnsupportedProductException>();
		}

		[U] public void ExpectedTaglineOnVersionSixDoesNotThrowException_WithExpectedDataOnApiCall()
		{
			var responseJson = new { version = new { number = "6.8.0", build_flavor = "default" }, tagline = "You Know, for Search" };

			var productCheckResponse = CreateResponseWithNoHeader(responseJson);

			var connectionPool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var connectionSettings = new ConnectionSettings(connectionPool, new InMemoryConnection(productCheckResponse));
			var client = new ElasticClient(connectionSettings);

			var response = client.Cluster.Health();

			response.ApiCall.AuditTrail.Should().Contain(x => x.Event == AuditEvent.ProductCheckOnStartup);
			response.ApiCall.AuditTrail.Should().Contain(x => x.Event == AuditEvent.ProductCheckSuccess);
		}

		[U] public void MissingBuildFlavorOnVersionSevenThrowsException()
		{
			var responseJson = new { version = new { number = "7.13.0" }, tagline = "You Know, for Search" };

			var productCheckResponse = CreateResponseWithNoHeader(responseJson);

			var connectionPool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var connectionSettings = new ConnectionSettings(connectionPool, new InMemoryConnection(productCheckResponse));
			var client = new ElasticClient(connectionSettings);

			client.Invoking(y => y.Cluster.Health())
				.Should()
				.Throw<UnsupportedProductException>();
		}

		[U] public void InvalidBuildFlavorMissingOnVersionSevenThrowsException()
		{
			var responseJson = new { version = new { number = "7.13.0", build_flavor = "unexpected" }, tagline = "You Know, for Search" };

			var productCheckResponse = CreateResponseWithNoHeader(responseJson);

			var connectionPool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var connectionSettings = new ConnectionSettings(connectionPool, new InMemoryConnection(productCheckResponse));
			var client = new ElasticClient(connectionSettings);

			client.Invoking(y => y.Cluster.Health())
				.Should()
				.Throw<UnsupportedProductException>();
		}

		[U] public void ExpectedBuildFlavorOnVersionSixDoesNotThrowException_WithExpectedDataOnApiCall()
		{
			var responseJson = new { version = new { number = "7.13.0", build_flavor = "default" }, tagline = "You Know, for Search" };

			var productCheckResponse = CreateResponseWithNoHeader(responseJson);

			var connectionPool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var connectionSettings = new ConnectionSettings(connectionPool, new InMemoryConnection(productCheckResponse));
			var client = new ElasticClient(connectionSettings);

			var response = client.Cluster.Health();

			response.ApiCall.AuditTrail.Should().Contain(x => x.Event == AuditEvent.ProductCheckOnStartup);
			response.ApiCall.AuditTrail.Should().Contain(x => x.Event == AuditEvent.ProductCheckSuccess);
		}

		private static InMemoryHttpResponse ValidResponseFor713()
		{
			var responseJson = new
			{
				name = "es01",
				cluster_name = "elasticsearch-test-cluster",
				version = new
				{
					number = "7.13.2",
					build_flavor = "default",
					build_hash = "af1dc6d8099487755c3143c931665b709de3c764",
					build_timestamp = "2020-08-11T21:36:48.204330Z",
					build_snapshot = false,
					lucene_version = "8.6.0"
				},
				tagline = "You Know, for Search"
			};

			using var ms = RecyclableMemoryStreamFactory.Default.Create();
			LowLevelRequestResponseSerializer.Instance.Serialize(responseJson, ms);

			return new InMemoryHttpResponse { ResponseBytes = ms.ToArray() };
		}

		private static InMemoryHttpResponse CreateResponseWithNoHeader(object json)
		{
			using var ms = RecyclableMemoryStreamFactory.Default.Create();
			LowLevelRequestResponseSerializer.Instance.Serialize(json, ms);

			return new InMemoryHttpResponse { ResponseBytes = ms.ToArray() };
		}
	}
}

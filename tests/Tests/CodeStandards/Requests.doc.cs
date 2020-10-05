// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Domain;

namespace Tests.CodeStandards
{
	/**
	 * Combining base URI with the API path results in a URI that respects the relative path defined in base URI
	 */
	public class Requests
	{
		[U]
		public void BaseUriIsRespected()
		{
			var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var settings = new ConnectionSettings(pool, new InMemoryConnection());
			var client = new ElasticClient(settings);
			var searchResponse = client.Search<Project>(s => s.AllIndices());

			searchResponse.ApiCall.Uri.ToString().Should().Be("http://localhost:9200/_all/_search?typed_keys=true");
		}

		[U]
		public void BaseUriWithTrailingSlashIsRespected()
		{
			var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200/"));
			var settings = new ConnectionSettings(pool, new InMemoryConnection());
			var client = new ElasticClient(settings);
			var searchResponse = client.Search<Project>(s => s.AllIndices());

			searchResponse.ApiCall.Uri.ToString().Should().Be("http://localhost:9200/_all/_search?typed_keys=true");
		}

		[U]
		public void BaseUriWithRelativePathIsRespected()
		{
			var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200/elasticsearch"));
			var settings = new ConnectionSettings(pool, new InMemoryConnection());
			var client = new ElasticClient(settings);
			var searchResponse = client.Search<Project>(s => s.AllIndices());

			searchResponse.ApiCall.Uri.ToString().Should().Be("http://localhost:9200/elasticsearch/_all/_search?typed_keys=true");
		}

		[U]
		public void BaseUriWithRelativePathAndTrailingSlashIsRespected()
		{
			var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200/elasticsearch/"));
			var settings = new ConnectionSettings(pool, new InMemoryConnection());
			var client = new ElasticClient(settings);
			var searchResponse = client.Search<Project>(s => s.AllIndices());

			searchResponse.ApiCall.Uri.ToString().Should().Be("http://localhost:9200/elasticsearch/_all/_search?typed_keys=true");
		}
	}
}

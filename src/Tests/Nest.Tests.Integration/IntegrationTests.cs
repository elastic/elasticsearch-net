using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration
{
	public class IntegrationTests
	{

		protected IElasticClient Client { get { return ElasticsearchConfiguration.Client.Value; } }
		protected IElasticClient ClientThatThrows { get { return ElasticsearchConfiguration.ClientThatThrows.Value; } }
		protected IElasticClient ClientNoRawResponse { get { return ElasticsearchConfiguration.ClientNoRawResponse.Value; } }

		protected IConnectionSettingsValues Settings { get { return ElasticsearchConfiguration.Settings(); } }

		protected ISearchResponse<T> SearchRaw<T>(string query) where T : class
		{
			var index = this.Client.Infer.IndexName<T>();
			var typeName = this.Client.Infer.TypeName<T>();

			var connectionStatus = this.Client.Raw.Search<SearchResponse<T>>(index, typeName, query);
			var serializer = connectionStatus.Serializer as INestSerializer;
			return connectionStatus.Response;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Nest;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;
using Nest.Resolvers;

namespace Nest.Tests.Integration
{
	public class IntegrationTests
	{

		protected IElasticClient Client { get { return ElasticsearchConfiguration.Client; } }
		protected IElasticClient ClientThatThrows { get { return ElasticsearchConfiguration.ClientThatThrows; } }
		protected IElasticClient ClientNoRawResponse { get { return ElasticsearchConfiguration.ClientNoRawResponse; } }
		protected ElasticClient ThriftClient { get { return ElasticsearchConfiguration.ThriftClient; } }

		protected IConnectionSettingsValues Settings { get { return ElasticsearchConfiguration.Settings(); } }

		protected virtual void ResetIndexes()
		{

		}

		protected ISearchResponse<T> SearchRaw<T>(string query) where T : class
		{
			var index = this.Client.Infer.IndexName<T>();
			var typeName = this.Client.Infer.TypeName<T>();

			var connectionStatus = this.Client.Raw.Search<SearchResponse<T>>(index, typeName, query);
			var serializer = connectionStatus.Serializer as INestSerializer;
			return connectionStatus.Response;
		}

		public void DoFilterTest(Func<FilterDescriptor<ElasticsearchProject>, FilterContainer> filter, ElasticsearchProject project, bool queryMustHaveResults)
		{
			var filterId = Filter<ElasticsearchProject>.Term(e => e.Id, project.Id);

			var results = this.Client.Search<ElasticsearchProject>(
			  s => s.Filter(ff => ff.And(
				  f => f.Term(e => e.Id, project.Id),
				  filter
				))
			  );

			var rawResponse = results.ConnectionStatus.ResponseRaw.Utf8String();

			Assert.True(results.IsValid, rawResponse);
			Assert.True(results.ConnectionStatus.Success, rawResponse);
			Assert.AreEqual(queryMustHaveResults ? 1 : 0, results.Total);
		}

	}
}

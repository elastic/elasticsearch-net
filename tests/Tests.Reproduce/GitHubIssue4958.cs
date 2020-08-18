// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Text;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;

namespace Tests.Reproduce
{
	public class GitHubIssue4958
	{
		[U]
		public void SearchUsesConfiguredDefaultMappingFor()
		{
			var defaultIndex = "documents";
			var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));

			var settings = new ConnectionSettings(pool, new InMemoryConnection())
				.DefaultIndex(defaultIndex)
				.DisableDirectStreaming()
				.DefaultMappingFor<AccountAddressElasticInfo>(i => i
					.IndexName(defaultIndex)
					.PropertyName(p => p.AccountId, "accountid")
					.IdProperty(p => p.AccountAddressVersionId)
				);

			var client = new ElasticClient(settings);

			var response = client.DeleteByQuery<AccountAddressElasticInfo>(d => d
				.Query(q => q
					.Term(f => f.AccountId, "value")
				)
				.MaximumDocuments(100000)
			);

			var json = Encoding.UTF8.GetString(response.ApiCall.RequestBodyInBytes);
			json.Should().Contain("\"accountid\"");
		}

		public class AccountAddressInfo
		{
			public string AccountAddressVersionId { get; set; }
			public string AccountId { get; set; }
		}

		public class AccountAddressElasticInfo : AccountAddressInfo
		{

			public DateTime Timestamp => DateTime.Now;
		}
	}
}

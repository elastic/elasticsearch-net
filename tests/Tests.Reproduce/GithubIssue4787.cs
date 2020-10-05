// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Text;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;

namespace Tests.Reproduce
{
	public class GithubIssue4787
	{
		[U]
		public void DoNotSerializeNullSourceFilter()
		{
			var connectionSettings = new ConnectionSettings(new InMemoryConnection()).DisableDirectStreaming();
			var client = new ElasticClient(connectionSettings);

			Func<ISearchResponse<object>> action = () =>
				client.Search<object>(s => s
					.Query(q => q
						.MatchAll()
					)
					.Index("index")
					.Source(sfd => null)
				);

			var response = action.Should().NotThrow().Subject;

			var json = Encoding.UTF8.GetString(response.ApiCall.RequestBodyInBytes);
			json.Should().Be(@"{""query"":{""match_all"":{}}}");
		}
	}
}

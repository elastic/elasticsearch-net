// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Text;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Client;

namespace Tests.Reproduce
{
	public class GitHubIssue4573
	{
		[U]
		public void SerializePercentageScore()
		{
			Func<ISearchResponse<object>> action = () => TestClient.DefaultInMemoryClient.Search<object>(b => b
				.Aggregations(a => a
					.SignificantTerms("related_organisations", sigTerms => sigTerms
						.Field("organisations.keyword")
						.Size(10)
						.PercentageScore(p => p)
						.MinimumDocumentCount(5)
					)
				)
			);

			action.Should().NotThrow();

			var response = action();

			var json = Encoding.UTF8.GetString(response.ApiCall.RequestBodyInBytes);
			json.Should().Contain("\"percentage\":{}");
		}
	}
}

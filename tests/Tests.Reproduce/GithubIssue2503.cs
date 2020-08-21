// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using static Tests.Core.Serialization.SerializationTestHelper;

namespace Tests.Reproduce
{
	public class GithubIssue2503
	{
		[U] public void DeserializeTermsIncludeExcludeValues() => Expect(new
			{
				aggs = new
				{
					sizes = new
					{
						terms = new
						{
							field = "size",
							size = 20,
							include = new[] { "35", "50", "70", "75", "100" }
						}
					}
				}
			})
			.DeserializesTo<SearchRequest>((message, request) =>
			{
				request.Should().NotBeNull(message);
				request.Aggregations.Should().NotBeNull(message).And.HaveCount(1, message);
				var termsAggregation = request.Aggregations["sizes"].Terms;
				termsAggregation.Should().NotBeNull(message);
				termsAggregation.Include.Should().NotBeNull(message);
				termsAggregation.Include.Values.Should().NotBeNull(message).And.HaveCount(5, message);
			});

		[U] public void DeserializeTermsIncludeExcludePattern() => Expect(@"{
			  ""aggs"": {
				""sizes"": {
				  ""terms"": {
					""field"": ""size"",
					""size"": 20,
					""include"": ""\\d+""
				  }
				}
			  }
			}")
			.DeserializesTo<SearchRequest>((message, request) =>
			{
				request.Should().NotBeNull(message);
				request.Aggregations.Should().NotBeNull(message).And.HaveCount(1, message);
				var termsAggregation = request.Aggregations["sizes"].Terms;
				termsAggregation.Should().NotBeNull(message);
				termsAggregation.Include.Should().NotBeNull(message);
				termsAggregation.Include.Pattern.Should().NotBeNull(message);
			});
	}
}

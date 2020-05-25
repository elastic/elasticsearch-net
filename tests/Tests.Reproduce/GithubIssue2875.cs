// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using static Tests.Core.Serialization.SerializationTestHelper;

namespace Tests.Reproduce
{
	public class GithubIssue2875
	{
		[U] public void ReusingQueryDescriptorOutSideOfSelector() => Expect(new
			{
				query = new
				{
					@bool = new
					{
						must = new[]
						{
							new { term = new { field = new { value = "value" } } }
						}
					}
				}
			})
			.FromRequest(ReuseQueryDescriptorUnexpected);

		private static ISearchResponse<Project> ReuseQueryDescriptorUnexpected(IElasticClient client) => client.Search<Project>(s => s
			.Query(q => q
				.Bool(b => b
					.Must(q.Term("field", "value"))
				)
			)
		);
	}
}

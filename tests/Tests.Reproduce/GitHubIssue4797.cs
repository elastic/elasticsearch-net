// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Text;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Client;

namespace Tests.Reproduce
{
	public class GitHubIssue4797
	{
		[U]
		public void DeserializeSortWithOnlyFieldAndOrder()
		{
			var json = @"
			{
			    ""sort"" : [
			        { ""post_date"" : {""order"" : ""asc""}},
			        ""user"",
			        { ""name"" : ""desc"" },
			        { ""age"" : ""desc"" },
			        ""_score""
			    ],
			    ""query"" : {
			        ""term"" : { ""user"" : ""kimchy"" }
			    }
			}
			";

			var bytes = Encoding.UTF8.GetBytes(json);
			var client = TestClient.FixedInMemoryClient(bytes);

			using var stream = client.ConnectionSettings.MemoryStreamFactory.Create(bytes);
			var request = client.RequestResponseSerializer.Deserialize<SearchRequest>(stream);

			request.Should().NotBeNull();
			request.Sort.Should().NotBeNull().And.HaveCount(5);
			request.Sort[1].SortKey.Should().Be("user");
			request.Sort[2].SortKey.Should().Be("name");
			request.Sort[2].Order.Should().Be(SortOrder.Descending);
		}
	}
}

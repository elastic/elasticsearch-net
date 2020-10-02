// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Client;

namespace Tests.Reproduce
{
	public class GithubIssue4733
	{
		[U]
		public void CanSerializeIdsQueryWithIEnumerableString()
		{
			var client = TestClient.DefaultInMemoryClient;

			Func<ISearchResponse<object>> func = () => client.Search<object>(s => s
				.From(0)
				.Size(25)
				.Source(ss => ss.Excludes(e => e.Field("events")))
				.Query(q => q.Pinned(p => p
						.Organic(o => o.MatchAll())
						.Ids(new [] { "387c2c78-95b1-42f8-a965-e09a73f7cff6" })
					)
				)
			);

			func.Should().NotThrow();
		}
	}
}

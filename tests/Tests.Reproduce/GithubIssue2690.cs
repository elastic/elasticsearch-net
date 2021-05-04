// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Tests.Core.Client;
using Tests.Core.Extensions;

namespace Tests.Reproduce
{
	public class GithubIssue2690
	{
		[U] public void EmptyPolicyCausesNullReferenceException()
		{
			var client = TestClient.DefaultInMemoryClient;
			var response = client.Indices.Create("foo", c => c
				.Settings(s => s
					.Merge(m => m
						.Scheduler(sch => sch.MaxThreadCount(1))
					)
				)
			);
			response.ShouldBeSuccess();
		}
	}
}

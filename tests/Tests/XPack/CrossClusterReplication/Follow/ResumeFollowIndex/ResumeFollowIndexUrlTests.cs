// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;

namespace Tests.XPack.CrossClusterReplication.Follow.ResumeFollowIndex
{
	public class ResumeFollowIndexUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			var name = "x";
			await UrlTester.POST($"/{name}/_ccr/resume_follow")
				.Fluent(c => c.CrossClusterReplication.ResumeFollowIndex(name, d => d))
				.Request(c => c.CrossClusterReplication.ResumeFollowIndex(new ResumeFollowIndexRequest(name)))
				.FluentAsync(c => c.CrossClusterReplication.ResumeFollowIndexAsync(name, d => d))
				.RequestAsync(c => c.CrossClusterReplication.ResumeFollowIndexAsync(new ResumeFollowIndexRequest(name)));

		}
	}
}

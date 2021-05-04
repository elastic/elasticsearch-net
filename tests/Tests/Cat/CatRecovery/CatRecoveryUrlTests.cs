// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Cat.CatRecovery
{
	public class CatRecoveryUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await GET("/_cat/recovery")
					.Fluent(c => c.Cat.Recovery())
					.Request(c => c.Cat.Recovery(new CatRecoveryRequest()))
					.FluentAsync(c => c.Cat.RecoveryAsync())
					.RequestAsync(c => c.Cat.RecoveryAsync(new CatRecoveryRequest()))
				;

			await GET("/_cat/recovery/project")
				.Fluent(c => c.Cat.Recovery(r => r.Index<Project>()))
				.Request(c => c.Cat.Recovery(new CatRecoveryRequest(Nest.Indices.Index<Project>())))
				.FluentAsync(c => c.Cat.RecoveryAsync(r => r.Index<Project>()))
				.RequestAsync(c => c.Cat.RecoveryAsync(new CatRecoveryRequest(Nest.Indices.Index<Project>())));
		}
	}
}

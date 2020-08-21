// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Security.GetBuiltinPrivileges
{
	public class GetBuiltinPrivilegesUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() =>
			await GET("/_security/privilege/_builtin")
				.Fluent(c => c.Security.GetBuiltinPrivileges())
				.Request(c => c.Security.GetBuiltinPrivileges(new GetBuiltinPrivilegesRequest()))
				.FluentAsync(c => c.Security.GetBuiltinPrivilegesAsync())
				.RequestAsync(c => c.Security.GetBuiltinPrivilegesAsync(new GetBuiltinPrivilegesRequest()));
	}
}

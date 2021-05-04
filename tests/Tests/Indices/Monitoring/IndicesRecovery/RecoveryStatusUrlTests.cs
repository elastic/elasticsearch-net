// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Nest.Indices;

namespace Tests.Indices.Monitoring.IndicesRecovery
{
	public class RecoveryStatusUrlTests
	{
		[U] public async Task Urls()
		{
			await UrlTester.GET($"/_recovery")
					.Request(c => c.Indices.RecoveryStatus(new RecoveryStatusRequest()))
					.RequestAsync(c => c.Indices.RecoveryStatusAsync(new RecoveryStatusRequest()))
				;

			await UrlTester.GET($"/_all/_recovery")
					.Fluent(c => c.Indices.RecoveryStatus(All))
					.Request(c => c.Indices.RecoveryStatus(new RecoveryStatusRequest(All)))
					.FluentAsync(c => c.Indices.RecoveryStatusAsync(All))
					.RequestAsync(c => c.Indices.RecoveryStatusAsync(new RecoveryStatusRequest(All)))
				;

			var index = "index1,index2";
			await UrlTester.GET($"/index1%2Cindex2/_recovery")
					.Fluent(c => c.Indices.RecoveryStatus(index))
					.Request(c => c.Indices.RecoveryStatus(new RecoveryStatusRequest(index)))
					.FluentAsync(c => c.Indices.RecoveryStatusAsync(index))
					.RequestAsync(c => c.Indices.RecoveryStatusAsync(new RecoveryStatusRequest(index)))
				;
		}
	}
}

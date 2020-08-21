// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Nest.Indices;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Indices.IndexSettings.UpdateIndicesSettings
{
	public class UpdateIndexSettingsUrlTests
	{
		[U] public async Task Urls()
		{
			var index = "index1,index2";
			Nest.Indices indices = index;
			await PUT($"/index1%2Cindex2/_settings")
					.Fluent(c => c.Indices.UpdateSettings(indices, s => s))
					.Request(c => c.Indices.UpdateSettings(new UpdateIndexSettingsRequest(index)))
					.FluentAsync(c => c.Indices.UpdateSettingsAsync(indices, s => s))
					.RequestAsync(c => c.Indices.UpdateSettingsAsync(new UpdateIndexSettingsRequest(index)));

			await PUT($"/_all/_settings")
					.Fluent(c => c.Indices.UpdateSettings(AllIndices, s => s))
					.Request(c => c.Indices.UpdateSettings(new UpdateIndexSettingsRequest(All)))
					.FluentAsync(c => c.Indices.UpdateSettingsAsync(AllIndices, s => s))
					.RequestAsync(c => c.Indices.UpdateSettingsAsync(new UpdateIndexSettingsRequest(All)))
				;

			await PUT($"/_settings")
					.Request(c => c.Indices.UpdateSettings(new UpdateIndexSettingsRequest()))
					.RequestAsync(c => c.Indices.UpdateSettingsAsync(new UpdateIndexSettingsRequest()))
				;
		}
	}
}

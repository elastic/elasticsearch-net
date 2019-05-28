using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Nest.Indices;
using static Tests.Framework.UrlTester;

namespace Tests.Indices.IndexSettings.UpdateIndicesSettings
{
	public class UpdateIndexSettingsUrlTests
	{
		[U] public async Task Urls()
		{
			var index = "index1,index2";
			Nest.Indices indices = index;
			await PUT($"/index1%2Cindex2/_settings")
					.Fluent(c => c.Indices.UpdateIndexSettings(indices, s => s))
					.Request(c => c.Indices.UpdateIndexSettings(new UpdateIndexSettingsRequest(index)))
					.FluentAsync(c => c.Indices.UpdateIndexSettingsAsync(indices, s => s))
					.RequestAsync(c => c.Indices.UpdateIndexSettingsAsync(new UpdateIndexSettingsRequest(index)));
			
			await PUT($"/_all/_settings")
					.Fluent(c => c.Indices.UpdateIndexSettings(AllIndices, s => s))
					.Request(c => c.Indices.UpdateIndexSettings(new UpdateIndexSettingsRequest(All)))
					.FluentAsync(c => c.Indices.UpdateIndexSettingsAsync(AllIndices, s => s))
					.RequestAsync(c => c.Indices.UpdateIndexSettingsAsync(new UpdateIndexSettingsRequest(All)))
				;
			
			await PUT($"/_settings")
					.Request(c => c.Indices.UpdateIndexSettings(new UpdateIndexSettingsRequest()))
					.RequestAsync(c => c.Indices.UpdateIndexSettingsAsync(new UpdateIndexSettingsRequest()))
				;
		}
	}
}

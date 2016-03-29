using System.Threading.Tasks;
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
				.Fluent(c => c.UpdateIndexSettings(indices, s=>s))
				.Request(c => c.UpdateIndexSettings(new UpdateIndexSettingsRequest(index)))
				.FluentAsync(c => c.UpdateIndexSettingsAsync(indices, s=>s))
				.RequestAsync(c => c.UpdateIndexSettingsAsync(new UpdateIndexSettingsRequest(index)))
				;
			await PUT($"/_settings")
				.Fluent(c => c.UpdateIndexSettings(AllIndices, s=>s))
				.Request(c => c.UpdateIndexSettings(new UpdateIndexSettingsRequest()))
				.FluentAsync(c => c.UpdateIndexSettingsAsync(AllIndices, s=>s))
				.RequestAsync(c => c.UpdateIndexSettingsAsync(new UpdateIndexSettingsRequest()))
				;
		}
	}
}

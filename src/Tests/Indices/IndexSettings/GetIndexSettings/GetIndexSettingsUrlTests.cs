using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using static Nest.Indices;
using static Tests.Framework.UrlTester;

namespace Tests.Indices.IndexSettings.GetIndexSettings
{
	public class GetIndexSettingsUrlTests
	{
		[U] public async Task Urls()
		{
			var index = "index1,index2";
			Nest.Indices indices = index;
			var name = "name";
			Name n = name;
			await GET($"/index1%2Cindex2/_settings/{name}")
				.Fluent(c => c.GetIndexSettings(m=>m.Index(index).Name(name)))
				.Request(c => c.GetIndexSettings(new GetIndexSettingsRequest(index, name)))
				.FluentAsync(c => c.GetIndexSettingsAsync(m=>m.Index(index).Name(name)))
				.RequestAsync(c => c.GetIndexSettingsAsync(new GetIndexSettingsRequest(index, name)))
				;
			
			await GET($"/index1%2Cindex2/_settings")
				.Fluent(c => c.GetIndexSettings(m=>m.Index(index)))
				.Request(c => c.GetIndexSettings(new GetIndexSettingsRequest(indices)))
				.FluentAsync(c => c.GetIndexSettingsAsync(m=>m.Index(index)))
				.RequestAsync(c => c.GetIndexSettingsAsync(new GetIndexSettingsRequest(indices)))
				;

			await GET($"/_settings/{name}")
				.Fluent(c => c.GetIndexSettings(m=>m.Name(name)))
				.Request(c => c.GetIndexSettings(new GetIndexSettingsRequest(n)))
				.FluentAsync(c => c.GetIndexSettingsAsync(m=>m.Name(name)))
				.RequestAsync(c => c.GetIndexSettingsAsync(new GetIndexSettingsRequest(n)))
				;
			await GET($"/_settings")
				.Fluent(c => c.GetIndexSettings(m=>m.Index(AllIndices)))
				.Request(c => c.GetIndexSettings(new GetIndexSettingsRequest(AllIndices)))
				.FluentAsync(c => c.GetIndexSettingsAsync(m=>m.Index(AllIndices)))
				.RequestAsync(c => c.GetIndexSettingsAsync(new GetIndexSettingsRequest(AllIndices)))
				;
		}
	}
}

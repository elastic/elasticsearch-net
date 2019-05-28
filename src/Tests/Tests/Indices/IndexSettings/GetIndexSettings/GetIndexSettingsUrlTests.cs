using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
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
					.Fluent(c => c.Indices.GetIndexSettings(index, m => m.Name(name)))
					.Request(c => c.Indices.GetIndexSettings(new GetIndexSettingsRequest(index, name)))
					.FluentAsync(c => c.Indices.GetIndexSettingsAsync(index, m => m.Name(name)))
					.RequestAsync(c => c.Indices.GetIndexSettingsAsync(new GetIndexSettingsRequest(index, name)))
				;

			await GET($"/index1%2Cindex2/_settings")
					.Fluent(c => c.Indices.GetIndexSettings(index))
					.Request(c => c.Indices.GetIndexSettings(new GetIndexSettingsRequest(indices)))
					.FluentAsync(c => c.Indices.GetIndexSettingsAsync(index))
					.RequestAsync(c => c.Indices.GetIndexSettingsAsync(new GetIndexSettingsRequest(indices)))
				;

			await GET($"/_settings/{name}")
					.Fluent(c => c.Indices.GetIndexSettings(null, m => m.Name(name)))
					.Request(c => c.Indices.GetIndexSettings(new GetIndexSettingsRequest(n)))
					.FluentAsync(c => c.Indices.GetIndexSettingsAsync(null, m => m.Name(name)))
					.RequestAsync(c => c.Indices.GetIndexSettingsAsync(new GetIndexSettingsRequest(n)))
				;
			await GET($"/_all/_settings")
					.Fluent(c => c.Indices.GetIndexSettings(AllIndices))
					.Request(c => c.Indices.GetIndexSettings(new GetIndexSettingsRequest(AllIndices)))
					.FluentAsync(c => c.Indices.GetIndexSettingsAsync(AllIndices))
					.RequestAsync(c => c.Indices.GetIndexSettingsAsync(new GetIndexSettingsRequest(AllIndices)))
				;

			await GET($"/_settings")
					.Request(c => c.Indices.GetIndexSettings(new GetIndexSettingsRequest()))
					.RequestAsync(c => c.Indices.GetIndexSettingsAsync(new GetIndexSettingsRequest()))
				;
		}
	}
}

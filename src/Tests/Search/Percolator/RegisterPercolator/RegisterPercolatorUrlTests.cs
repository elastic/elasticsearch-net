using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.Search.Percolator.RegisterPercolator
{
	public class RegisterPercolatorUrlTests
	{
		[U] public async Task Urls()
		{
			var name = "name-of-perc";
			var index = "indexx";
			await POST($"/{index}/.percolator/{name}")
				.Fluent(c=>c.RegisterPercolator<Project>(name, s=>s.Index(index)))
				.Request(c=>c.RegisterPercolator(new RegisterPercolatorRequest(index, name)))
				.FluentAsync(c=>c.RegisterPercolatorAsync<Project>(name, s=>s.Index(index)))
				.RequestAsync(c=>c.RegisterPercolatorAsync(new RegisterPercolatorRequest(index, name)))
				;

			await POST($"/project/.percolator/{name}")
				.Fluent(c=>c.RegisterPercolator<Project>(name,s=>s))
				.Request(c=>c.RegisterPercolator(new RegisterPercolatorRequest(typeof(Project), name)))
				.FluentAsync(c=>c.RegisterPercolatorAsync<Project>(name, s=>s))
				.RequestAsync(c=>c.RegisterPercolatorAsync(new RegisterPercolatorRequest(IndexName.From<Project>(), name)))
				;
		}
	}
}

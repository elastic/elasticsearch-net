using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Cat.CatFielddata
{
	public class CatFielddataUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await GET("/_cat/fielddata")
					.Fluent(c => c.Cat.Fielddata())
					.Request(c => c.Cat.Fielddata(new CatFielddataRequest()))
					.FluentAsync(c => c.Cat.FielddataAsync())
					.RequestAsync(c => c.Cat.FielddataAsync(new CatFielddataRequest()))
				;

			var fields = new[] { "name", "startedOn" };

			await GET("/_cat/fielddata/name%2CstartedOn")
					.Fluent(c => c.Cat.Fielddata(f => f.Fields<Project>(p => p.Name, p => p.StartedOn)))
					.Request(c => c.Cat.Fielddata(new CatFielddataRequest(fields)))
					.FluentAsync(c => c.Cat.FielddataAsync(f => f.Fields<Project>(p => p.Name, p => p.StartedOn)))
					.RequestAsync(c => c.Cat.FielddataAsync(new CatFielddataRequest(fields)))
				;
		}
	}
}

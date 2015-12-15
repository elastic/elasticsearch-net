using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.Cat.CatRecovery
{
	public class CatRecoveryUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await GET("/_cat/recovery")
				.Fluent(c => c.CatRecovery())
				.Request(c => c.CatRecovery(new CatRecoveryRequest()))
				.FluentAsync(c => c.CatRecoveryAsync())
				.RequestAsync(c => c.CatRecoveryAsync(new CatRecoveryRequest()))
				;

			await GET("/_cat/recovery/project")
				.Fluent(c => c.CatRecovery(r => r.Index<Project>()))
				.Request(c => c.CatRecovery(new CatRecoveryRequest(Nest.Indices.Index<Project>())))
				.FluentAsync(c => c.CatRecoveryAsync(r => r.Index<Project>()))
				.RequestAsync(c => c.CatRecoveryAsync(new CatRecoveryRequest(Nest.Indices.Index<Project>())));
		}
	}
}

using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.Search.FieldCapabilities
{
	public class FieldCapabilitiesUrlTests
	{
		[U] public async Task Urls()
		{
			await GET("/project/_field_caps")
				.Fluent(c => c.FieldCapabilities(Nest.Indices.Index<Project>()))
				.Request(c => c.FieldCapabilities(new FieldCapabilitiesRequest("project") {}))
				.FluentAsync(c => c.FieldCapabilitiesAsync(typeof(Project)))
				.RequestAsync(c => c.FieldCapabilitiesAsync(new FieldCapabilitiesRequest("project")))
				;
		}
	}
}

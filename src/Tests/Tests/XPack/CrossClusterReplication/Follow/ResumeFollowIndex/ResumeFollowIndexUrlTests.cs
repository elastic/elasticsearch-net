using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;

namespace Tests.XPack.CrossClusterReplication.Follow.ResumeFollowIndex
{
	public class ResumeFollowIndexUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			var name = "x";
			await UrlTester.POST($"/{name}/_ccr/resume_follow")
				.Fluent(c => c.ResumeFollowIndex(name, d => d))
				.Request(c => c.ResumeFollowIndex(new ResumeFollowIndexRequest(name)))
				.FluentAsync(c => c.ResumeFollowIndexAsync(name, d => d))
				.RequestAsync(c => c.ResumeFollowIndexAsync(new ResumeFollowIndexRequest(name)));

		}
	}
}

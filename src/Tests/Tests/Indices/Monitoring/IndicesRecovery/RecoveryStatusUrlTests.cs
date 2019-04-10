using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Nest.Indices;

namespace Tests.Indices.Monitoring.IndicesRecovery
{
	public class RecoveryStatusUrlTests
	{
		[U] public async Task Urls()
		{
			await UrlTester.GET($"/_recovery")
					.Request(c => c.RecoveryStatus(new RecoveryStatusRequest()))
					.RequestAsync(c => c.RecoveryStatusAsync(new RecoveryStatusRequest()))
				;
			
			await UrlTester.GET($"/_all/_recovery")
					.Fluent(c => c.RecoveryStatus(All))
					.Request(c => c.RecoveryStatus(new RecoveryStatusRequest(All)))
					.FluentAsync(c => c.RecoveryStatusAsync(All))
					.RequestAsync(c => c.RecoveryStatusAsync(new RecoveryStatusRequest(All)))
				;

			var index = "index1,index2";
			await UrlTester.GET($"/index1%2Cindex2/_recovery")
					.Fluent(c => c.RecoveryStatus(index))
					.Request(c => c.RecoveryStatus(new RecoveryStatusRequest(index)))
					.FluentAsync(c => c.RecoveryStatusAsync(index))
					.RequestAsync(c => c.RecoveryStatusAsync(new RecoveryStatusRequest(index)))
				;
		}
	}
}

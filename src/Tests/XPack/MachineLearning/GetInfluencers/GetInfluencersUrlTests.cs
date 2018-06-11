using System;
using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.MachineLearning.GetInfluencers
{
	public class GetInfluencersUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await POST("/_xpack/ml/anomaly_detectors/job_id/results/influencers")
				.Fluent(c => c.GetInfluencers("job_id"))
				.Request(c => c.GetInfluencers(new GetInfluencersRequest("job_id")))
				.FluentAsync(c => c.GetInfluencersAsync("job_id"))
				.RequestAsync(c => c.GetInfluencersAsync(new GetInfluencersRequest("job_id")))
				;
		}
	}
}

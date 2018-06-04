using System;
using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.MachineLearning.GetAnomalyRecords
{
	public class GetAnomalyRecordsUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await POST("/_xpack/ml/anomaly_detectors/job_id/results/records")
				.Fluent(c => c.GetAnomalyRecords("job_id"))
				.Request(c => c.GetAnomalyRecords(new GetAnomalyRecordsRequest("job_id")))
				.FluentAsync(c => c.GetAnomalyRecordsAsync("job_id"))
				.RequestAsync(c => c.GetAnomalyRecordsAsync(new GetAnomalyRecordsRequest("job_id")))
				;
		}
	}
}

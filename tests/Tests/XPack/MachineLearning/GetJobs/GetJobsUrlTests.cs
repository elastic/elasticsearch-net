// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.MachineLearning.GetJobs
{
	public class GetJobsUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await GET("/_ml/anomaly_detectors")
					.Fluent(c => c.MachineLearning.GetJobs())
					.Request(c => c.MachineLearning.GetJobs(new GetJobsRequest()))
					.FluentAsync(c => c.MachineLearning.GetJobsAsync())
					.RequestAsync(c => c.MachineLearning.GetJobsAsync(new GetJobsRequest()))
				;

			await GET("/_ml/anomaly_detectors/job_id")
					.Fluent(c => c.MachineLearning.GetJobs(r => r.JobId("job_id")))
					.Request(c => c.MachineLearning.GetJobs(new GetJobsRequest("job_id")))
					.FluentAsync(c => c.MachineLearning.GetJobsAsync(r => r.JobId("job_id")))
					.RequestAsync(c => c.MachineLearning.GetJobsAsync(new GetJobsRequest("job_id")))
				;
		}
	}
}

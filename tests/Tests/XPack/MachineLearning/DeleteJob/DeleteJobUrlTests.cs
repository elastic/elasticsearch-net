// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.MachineLearning.DeleteJob
{
	public class DeleteJobUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await DELETE("/_ml/anomaly_detectors/job_id")
			.Fluent(c => c.MachineLearning.DeleteJob("job_id"))
			.Request(c => c.MachineLearning.DeleteJob(new DeleteJobRequest("job_id")))
			.FluentAsync(c => c.MachineLearning.DeleteJobAsync("job_id"))
			.RequestAsync(c => c.MachineLearning.DeleteJobAsync(new DeleteJobRequest("job_id")));
	}
}

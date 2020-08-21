// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.MachineLearning.GetBuckets
{
	public class GetBucketsUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await POST("/_ml/anomaly_detectors/job_id/results/buckets")
			.Fluent(c => c.MachineLearning.GetBuckets("job_id"))
			.Request(c => c.MachineLearning.GetBuckets(new GetBucketsRequest("job_id")))
			.FluentAsync(c => c.MachineLearning.GetBucketsAsync("job_id"))
			.RequestAsync(c => c.MachineLearning.GetBucketsAsync(new GetBucketsRequest("job_id")));
	}
}

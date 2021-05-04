// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.MachineLearning.PutJob
{
	public class PutJobUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await PUT("/_ml/anomaly_detectors/job_id")
			.Fluent(c => c.MachineLearning.PutJob<object>("job_id", p => p))
			.Request(c => c.MachineLearning.PutJob(new PutJobRequest("job_id")))
			.FluentAsync(c => c.MachineLearning.PutJobAsync<object>("job_id", p => p))
			.RequestAsync(c => c.MachineLearning.PutJobAsync(new PutJobRequest("job_id")));
	}
}

// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.MachineLearning.ValidateJob
{
	public class ValidateJobUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await POST("/_ml/anomaly_detectors/_validate")
			.Fluent(c => c.MachineLearning.ValidateJob<Project>(v => v))
			.Request(c => c.MachineLearning.ValidateJob(new ValidateJobRequest()))
			.FluentAsync(c => c.MachineLearning.ValidateJobAsync<Project>(v => v))
			.RequestAsync(c => c.MachineLearning.ValidateJobAsync(new ValidateJobRequest()));
	}
}

// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.MachineLearning.EstimateModelMemory
{
	public class EstimateModelMemoryUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await POST("/_ml/anomaly_detectors/_estimate_model_memory")
			.Fluent(c => c.MachineLearning.EstimateModelMemory(new EstimateModelMemoryRequest()))
			.Request(c => c.MachineLearning.EstimateModelMemory(new EstimateModelMemoryRequest()))
			.FluentAsync(c => c.MachineLearning.EstimateModelMemoryAsync(new EstimateModelMemoryRequest()))
			.RequestAsync(c => c.MachineLearning.EstimateModelMemoryAsync(new EstimateModelMemoryRequest()));
	}
}

// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;

namespace Tests.XPack.MachineLearning.MachineLearningInfo
{
	public class InfoUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() =>
			await UrlTester.GET("_ml/info")
				.Fluent(c => c.MachineLearning.Info())
				.Request(c => c.MachineLearning.Info(new MachineLearningInfoRequest()))
				.FluentAsync(c => c.MachineLearning.InfoAsync())
				.RequestAsync(c => c.MachineLearning.InfoAsync(new MachineLearningInfoRequest()));
	}
}

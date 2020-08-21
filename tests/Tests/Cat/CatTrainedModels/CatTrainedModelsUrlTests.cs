// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Cat.CatTrainedModels
{
	public class CatTrainedModelsUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await GET("/_cat/ml/trained_models")
					.Fluent(c => c.Cat.TrainedModels())
					.Request(c => c.Cat.TrainedModels(new CatTrainedModelsRequest()))
					.FluentAsync(c => c.Cat.TrainedModelsAsync())
					.RequestAsync(c => c.Cat.TrainedModelsAsync(new CatTrainedModelsRequest()))
				;

			await GET("/_cat/ml/trained_models/model-id")
				.Fluent(c => c.Cat.TrainedModels(r => r.ModelId("model-id")))
				.Request(c => c.Cat.TrainedModels(new CatTrainedModelsRequest("model-id")))
				.FluentAsync(c => c.Cat.TrainedModelsAsync(r => r.ModelId("model-id")))
				.RequestAsync(c => c.Cat.TrainedModelsAsync(new CatTrainedModelsRequest("model-id")));
		}
	}
}

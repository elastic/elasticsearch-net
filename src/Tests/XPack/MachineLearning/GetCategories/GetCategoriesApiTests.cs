using System;
using System.Linq;
using System.Threading;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;

namespace Tests.XPack.MachineLearning.GetCategories
{
	public class GetCategoriesApiTests : MachineLearningIntegrationTestBase<IGetCategoriesResponse, IGetCategoriesRequest, GetCategoriesDescriptor, GetCategoriesRequest>
	{
		public GetCategoriesApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutJob(client, callUniqueValue.Value);
				IndexCategory(client, callUniqueValue.Value);
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.GetCategories(CallIsolatedValue, f),
			fluentAsync: (client, f) => client.GetCategoriesAsync(CallIsolatedValue, f),
			request: (client, r) => client.GetCategories(r),
			requestAsync: (client, r) => client.GetCategoriesAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"_xpack/ml/anomaly_detectors/{CallIsolatedValue}/results/categories/";
		protected override bool SupportsDeserialization => true;
		protected override GetCategoriesDescriptor NewDescriptor() => new GetCategoriesDescriptor(CallIsolatedValue);
		protected override object ExpectJson => null;
		protected override Func<GetCategoriesDescriptor, IGetCategoriesRequest> Fluent => f => f.Page(p => p.From(0).Size(10));

		protected override GetCategoriesRequest Initializer => new GetCategoriesRequest(CallIsolatedValue)
		{
			Page = new Page
			{
				From = 0,
				Size = 10
			}
		};

		protected override void ExpectResponse(IGetCategoriesResponse response)
		{
			response.ShouldBeValid();
			response.Categories.Should().HaveCount(1);
			response.Count.Should().Be(1);

			response.Categories.First().JobId.Should().Be(CallIsolatedValue);
			response.Categories.First().CategoryId.Should().Be(1);
			response.Categories.First().Terms.Should().Be("");
			response.Categories.First().Regex.Should().Be("");
			response.Categories.First().MaxMatchingLength.Should().Be(0);
			response.Categories.First().Examples.Should().HaveCount(0);
		}
	}

	public class GetCategoriesWithCategoriesApiTests : MachineLearningIntegrationTestBase<IGetCategoriesResponse, IGetCategoriesRequest, GetCategoriesDescriptor, GetCategoriesRequest>
	{
		public GetCategoriesWithCategoriesApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutJob(client, callUniqueValue.Value);
				IndexCategory(client, callUniqueValue.Value);
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.GetCategories(CallIsolatedValue, f),
			fluentAsync: (client, f) => client.GetCategoriesAsync(CallIsolatedValue, f),
			request: (client, r) => client.GetCategories(r),
			requestAsync: (client, r) => client.GetCategoriesAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"_xpack/ml/anomaly_detectors/{CallIsolatedValue}/results/categories/1";
		protected override bool SupportsDeserialization => true;
		protected override GetCategoriesDescriptor NewDescriptor() => new GetCategoriesDescriptor(CallIsolatedValue);
		protected override object ExpectJson => null;
		protected override Func<GetCategoriesDescriptor, IGetCategoriesRequest> Fluent => f => f.CategoryId(1);
		protected override GetCategoriesRequest Initializer => new GetCategoriesRequest(CallIsolatedValue, 1);

		protected override void ExpectResponse(IGetCategoriesResponse response)
		{
			response.ShouldBeValid();
			response.Categories.Should().HaveCount(1);
			response.Count.Should().Be(1);

			response.Categories.First().JobId.Should().Be(CallIsolatedValue);
			response.Categories.First().CategoryId.Should().Be(1);
			response.Categories.First().Terms.Should().Be("");
			response.Categories.First().Regex.Should().Be("");
			response.Categories.First().MaxMatchingLength.Should().Be(0);
			response.Categories.First().Examples.Should().HaveCount(0);
		}
	}
}

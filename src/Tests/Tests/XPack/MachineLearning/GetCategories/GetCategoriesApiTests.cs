using System;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.XPack.MachineLearning.GetCategories
{
	public class GetCategoriesApiTests
		: MachineLearningIntegrationTestBase<IGetCategoriesResponse, IGetCategoriesRequest, GetCategoriesDescriptor, GetCategoriesRequest>
	{
		public GetCategoriesApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 200;
		protected override Func<GetCategoriesDescriptor, IGetCategoriesRequest> Fluent => f => f.Page(p => p.From(0).Size(10));
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override GetCategoriesRequest Initializer => new GetCategoriesRequest(CallIsolatedValue)
		{
			Page = new Page
			{
				From = 0,
				Size = 10
			}
		};

		protected override string UrlPath => $"_ml/anomaly_detectors/{CallIsolatedValue}/results/categories/";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutJob(client, callUniqueValue.Value);
				IndexCategory(client, callUniqueValue.Value);
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.GetCategories(CallIsolatedValue, f),
			(client, f) => client.GetCategoriesAsync(CallIsolatedValue, f),
			(client, r) => client.GetCategories(r),
			(client, r) => client.GetCategoriesAsync(r)
		);

		protected override GetCategoriesDescriptor NewDescriptor() => new GetCategoriesDescriptor(CallIsolatedValue);

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

	public class GetCategoriesWithCategoriesApiTests
		: MachineLearningIntegrationTestBase<IGetCategoriesResponse, IGetCategoriesRequest, GetCategoriesDescriptor, GetCategoriesRequest>
	{
		public GetCategoriesWithCategoriesApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 200;
		protected override Func<GetCategoriesDescriptor, IGetCategoriesRequest> Fluent => f => f.CategoryId(1);
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override GetCategoriesRequest Initializer => new GetCategoriesRequest(CallIsolatedValue, 1);
		protected override string UrlPath => $"_ml/anomaly_detectors/{CallIsolatedValue}/results/categories/1";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutJob(client, callUniqueValue.Value);
				IndexCategory(client, callUniqueValue.Value);
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.GetCategories(CallIsolatedValue, f),
			(client, f) => client.GetCategoriesAsync(CallIsolatedValue, f),
			(client, r) => client.GetCategories(r),
			(client, r) => client.GetCategoriesAsync(r)
		);

		protected override GetCategoriesDescriptor NewDescriptor() => new GetCategoriesDescriptor(CallIsolatedValue);

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

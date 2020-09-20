// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.MachineLearning.GetCategories
{
	public class GetCategoriesApiTests
		: MachineLearningIntegrationTestBase<GetCategoriesResponse, IGetCategoriesRequest, GetCategoriesDescriptor, GetCategoriesRequest>
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
			(client, f) => client.MachineLearning.GetCategories(CallIsolatedValue, f),
			(client, f) => client.MachineLearning.GetCategoriesAsync(CallIsolatedValue, f),
			(client, r) => client.MachineLearning.GetCategories(r),
			(client, r) => client.MachineLearning.GetCategoriesAsync(r)
		);

		protected override GetCategoriesDescriptor NewDescriptor() => new GetCategoriesDescriptor(CallIsolatedValue);

		protected override void ExpectResponse(GetCategoriesResponse response)
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
		: MachineLearningIntegrationTestBase<GetCategoriesResponse, IGetCategoriesRequest, GetCategoriesDescriptor, GetCategoriesRequest>
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
			(client, f) => client.MachineLearning.GetCategories(CallIsolatedValue, f),
			(client, f) => client.MachineLearning.GetCategoriesAsync(CallIsolatedValue, f),
			(client, r) => client.MachineLearning.GetCategories(r),
			(client, r) => client.MachineLearning.GetCategoriesAsync(r)
		);

		protected override GetCategoriesDescriptor NewDescriptor() => new GetCategoriesDescriptor(CallIsolatedValue);

		protected override void ExpectResponse(GetCategoriesResponse response)
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

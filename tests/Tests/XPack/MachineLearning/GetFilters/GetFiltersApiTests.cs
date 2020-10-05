// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.MachineLearning.GetFilters
{
	[SkipVersion("<6.4.0", "Filter functions for machine learning stabilised in 6.4.0")]
	public class GetFiltersApiTests : MachineLearningIntegrationTestBase<GetFiltersResponse, IGetFiltersRequest, GetFiltersDescriptor, GetFiltersRequest>
	{
		public GetFiltersApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
				PutFilter(client, callUniqueValue.Value);
		}

		protected override void IntegrationTeardown(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
				DeleteFilter(client, callUniqueValue.Value);
		}

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => null;

		protected override int ExpectStatusCode => 200;

		protected override Func<GetFiltersDescriptor, IGetFiltersRequest> Fluent => f => f.FilterId(CallIsolatedValue);

		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override GetFiltersRequest Initializer => new GetFiltersRequest(CallIsolatedValue);

		protected override bool SupportsDeserialization => false;

		protected override string UrlPath => $"_ml/filters/{CallIsolatedValue}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.MachineLearning.GetFilters(f),
			(client, f) => client.MachineLearning.GetFiltersAsync(f),
			(client, r) => client.MachineLearning.GetFilters(r),
			(client, r) => client.MachineLearning.GetFiltersAsync(r)
		);

		protected override GetFiltersDescriptor NewDescriptor() => new GetFiltersDescriptor().FilterId(CallIsolatedValue);

		protected override void ExpectResponse(GetFiltersResponse response)
		{
			response.ShouldBeValid();
			response.Filters.Should().NotBeEmpty();
			var filter = response.Filters.First();
			filter.FilterId.Should().NotBeNullOrEmpty();
			filter.Items.Should().NotBeNull()
				.And.HaveCount(2)
				.And.Contain("*.google.com")
				.And.Contain("wikipedia.org");
			filter.Description.Should().Be("A list of safe domains");
		}
	}

	[SkipVersion("<6.4.0", "Filter functions for machine learning stabilised in 6.4.0")]
	public class GetFiltersPagingApiTests : MachineLearningIntegrationTestBase<GetFiltersResponse, IGetFiltersRequest, GetFiltersDescriptor, GetFiltersRequest>
	{
		public GetFiltersPagingApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
				for (var i = 0; i < 3; i++)
					PutFilter(client, callUniqueValue.Value + "_" + (i + 1));
		}

		protected override void IntegrationTeardown(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
				for (var i = 0; i < 3; i++)
					DeleteFilter(client, callUniqueValue.Value + "_" + (i + 1));
		}

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => null;

		protected override int ExpectStatusCode => 200;

		protected override Func<GetFiltersDescriptor, IGetFiltersRequest> Fluent => f => f
			.Size(10)
			.From(10);

		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override GetFiltersRequest Initializer => new GetFiltersRequest
		{
			Size = 10,
			From = 10
		};

		protected override bool SupportsDeserialization => false;

		protected override string UrlPath => $"_ml/filters?from=10&size=10";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.MachineLearning.GetFilters(f),
			(client, f) => client.MachineLearning.GetFiltersAsync(f),
			(client, r) => client.MachineLearning.GetFilters(r),
			(client, r) => client.MachineLearning.GetFiltersAsync(r)
		);

		protected override void ExpectResponse(GetFiltersResponse response)
		{
			response.ShouldBeValid();
			response.Filters.Should().NotBeEmpty();
			var filter = response.Filters.First();
			filter.FilterId.Should().NotBeNullOrEmpty();
			filter.Items.Should().NotBeNull()
				.And.HaveCount(2)
				.And.Contain("*.google.com")
				.And.Contain("wikipedia.org");
			filter.Description.Should().Be("A list of safe domains");
		}
	}
}

// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Configuration;
using Tests.Core.Client;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;

namespace Tests.Indices.MappingManagement.GetMapping
{
	public class GetMappingApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, GetMappingResponse, IGetMappingRequest, GetMappingDescriptor<Project>,
			GetMappingRequest>
	{
		public GetMappingApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override Func<GetMappingDescriptor<Project>, IGetMappingRequest> Fluent => d => d
			.IgnoreUnavailable();

		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override GetMappingRequest Initializer => new GetMappingRequest(Index<Project>())
		{
			IgnoreUnavailable = true
		};

		protected override string UrlPath => "/project/_mapping?ignore_unavailable=true";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.GetMapping(f),
			(client, f) => client.Indices.GetMappingAsync(f),
			(client, r) => client.Indices.GetMapping(r),
			(client, r) => client.Indices.GetMappingAsync(r)
		);

		protected override void ExpectResponse(GetMappingResponse response)
		{
			response.ShouldBeValid();

			/** For backwards compatible reasons you can still ask per type mappings, this is obsolete and will be removed in 8.0 **/
#pragma warning disable 618
			response.Indices["project"]["_doc"].Properties.Should().NotBeEmpty();
			response.Indices[Index<Project>()].Mappings.Properties.Should().NotBeEmpty();
			response.Indices[Index<Project>()]["_doc"].Properties.Should().NotBeEmpty();
#pragma warning restore 618

			var mappings = response.Indices[Index<Project>()].Mappings;
			var properties = mappings.Properties;

			var leadDev = properties[Property<Project>(p => p.LeadDeveloper)];
			leadDev.Should().NotBeNull();

			var props = response.Indices["x"]?.Mappings.Properties;
			props.Should().BeNull();

			//hide
			AssertExtensionMethods(response);

			//hide
			AssertVisitedProperties(response);
		}

		//hide
		private static void AssertExtensionMethods(GetMappingResponse response)
		{
			/** The `GetMappingFor` extension method can be used to get a type mapping easily and safely */
			response.GetMappingFor<Project>().Should().NotBeNull();
			response.GetMappingFor(typeof(Project)).Should().NotBeNull();

		}

		//hide
		private static void AssertVisitedProperties(GetMappingResponse response)
		{
			var visitor = new TestVisitor();
			var b = TestClient.Configuration.Random.SourceSerializer;

			var supportsFlattenedType = TestConfiguration.Instance.InRange(">=7.3.0");
			var supportsConstantKeyword = TestConfiguration.Instance.InRange(">=7.7.0");

			var keywordCount = b ? 19 : 18;
			if (!supportsConstantKeyword)
				keywordCount++;

			response.Accept(visitor);
			visitor.CountsShouldContainKeyAndCountBe("type", 1);
			visitor.CountsShouldContainKeyAndCountBe("text", b ? 18 : 17);
			visitor.CountsShouldContainKeyAndCountBe("keyword", keywordCount);
			visitor.CountsShouldContainKeyAndCountBe("object", supportsFlattenedType? 8 : 9);
			visitor.CountsShouldContainKeyAndCountBe("number", 9);
			visitor.CountsShouldContainKeyAndCountBe("ip", 2);
			visitor.CountsShouldContainKeyAndCountBe("geo_point", 3);
			visitor.CountsShouldContainKeyAndCountBe("date", 4);
			visitor.CountsShouldContainKeyAndCountBe("join", 1);
			visitor.CountsShouldContainKeyAndCountBe("completion", 2);
			visitor.CountsShouldContainKeyAndCountBe("date_range", 1);
			visitor.CountsShouldContainKeyAndCountBe("double_range", 1);
			visitor.CountsShouldContainKeyAndCountBe("float_range", 1);
			visitor.CountsShouldContainKeyAndCountBe("integer_range", 1);
			visitor.CountsShouldContainKeyAndCountBe("long_range", 1);
			visitor.CountsShouldContainKeyAndCountBe("ip_range", 1);
			visitor.CountsShouldContainKeyAndCountBe("nested", 1);
			visitor.CountsShouldContainKeyAndCountBe("rank_feature", 1);

			if (supportsFlattenedType)
				visitor.CountsShouldContainKeyAndCountBe("flattened", 1);

			if (supportsConstantKeyword)
				visitor.CountsShouldContainKeyAndCountBe("constant_keyword", 1);
		}
	}

	public class GetMappingNonExistentIndexApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, GetMappingResponse, IGetMappingRequest,
			GetMappingDescriptor<Project>, GetMappingRequest>
	{
		private readonly string _nonExistentIndex = "non-existent-index";

		public GetMappingNonExistentIndexApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => false;
		protected override int ExpectStatusCode => 404;

		protected override Func<GetMappingDescriptor<Project>, IGetMappingRequest> Fluent => d => d.Index(_nonExistentIndex);

		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override GetMappingRequest Initializer => new GetMappingRequest(_nonExistentIndex);
		protected override string UrlPath => $"/{_nonExistentIndex}/_mapping";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.GetMapping(f),
			(client, f) => client.Indices.GetMappingAsync(f),
			(client, r) => client.Indices.GetMapping(r),
			(client, r) => client.Indices.GetMappingAsync(r)
		);

		protected override void ExpectResponse(GetMappingResponse response)
		{
			response.Indices.Should().BeEmpty();
			response.ServerError.Should().NotBeNull();
		}
	}

	internal class TestVisitor : IMappingVisitor
	{
		public TestVisitor() => Counts = new Dictionary<string, int>();

		public Dictionary<string, int> Counts { get; }

		public int Depth { get; set; }

		public void Visit(IDateProperty mapping) => Increment("date");

		public void Visit(IDateNanosProperty mapping) => Increment("date_nanos");

		public void Visit(IBinaryProperty mapping) => Increment("binary");

		public void Visit(INestedProperty mapping) => Increment("nested");

		public void Visit(IGeoPointProperty mapping) => Increment("geo_point");

		public void Visit(ICompletionProperty mapping) => Increment("completion");

		public void Visit(ITokenCountProperty mapping) => Increment("token_count");

		public void Visit(IPercolatorProperty property) => Increment("percolator");

		public void Visit(IIntegerRangeProperty property) => Increment("integer_range");

		public void Visit(IFloatRangeProperty property) => Increment("float_range");

		public void Visit(ILongRangeProperty property) => Increment("long_range");

		public void Visit(IDoubleRangeProperty property) => Increment("double_range");

		public void Visit(IDateRangeProperty property) => Increment("date_range");

		public void Visit(IIpRangeProperty property) => Increment("ip_range");

		public void Visit(IJoinProperty property) => Increment("join");

		public void Visit(IMurmur3HashProperty mapping) => Increment("murmur3");

		public void Visit(INumberProperty mapping) => Increment("number");

		public void Visit(IGeoShapeProperty mapping) => Increment("geo_shape");

		public void Visit(IShapeProperty mapping) => Increment("shape");

		public void Visit(IPointProperty mapping) => Increment("point");

		public void Visit(IIpProperty mapping) => Increment("ip");

		public void Visit(IObjectProperty mapping) => Increment("object");

		public void Visit(IBooleanProperty mapping) => Increment("boolean");

		public void Visit(ITextProperty mapping) => Increment("text");

		public void Visit(IKeywordProperty mapping) => Increment("keyword");

		public void Visit(ITypeMapping mapping) => Increment("type");

		public void Visit(IRankFeatureProperty mapping) => Increment("rank_feature");

		public void Visit(IRankFeaturesProperty mapping) => Increment("rank_features");

		public void Visit(ISearchAsYouTypeProperty property) => Increment("search_as_you_type");

		public void Visit(IFlattenedProperty property) => Increment("flattened");

		public void Visit(IHistogramProperty property) => Increment("histogram");

		public void Visit(IConstantKeywordProperty property) => Increment("constant_keyword");

		private void Increment(string key)
		{
			if (!Counts.ContainsKey(key)) Counts.Add(key, 0);

			Counts[key] += 1;
		}

		public void CountsShouldContainKeyAndCountBe(string key, int count)
		{
			Counts.ContainsKey(key).Should().BeTrue($"did not see {key}");
			var sb = new StringBuilder()
				.AppendLine($"because there should be {count} {key} properties");
			var because = Counts.Aggregate(sb, (s, kv) => s.AppendLine($"{kv.Key} = {kv.Value}"), s => s.ToString());
			Counts[key].Should().Be(count, because);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Client;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Xunit;
using static Nest.Infer;

namespace Tests.Indices.MappingManagement.GetMapping
{
	public class GetMappingApiTests : ApiIntegrationTestBase<ReadOnlyCluster, IGetMappingResponse, IGetMappingRequest, GetMappingDescriptor<Project>,
		GetMappingRequest>
	{
		public GetMappingApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => this.Calls(
			fluent: (client, f) => client.GetMapping<Project>(f),
			fluentAsync: (client, f) => client.GetMappingAsync<Project>(f),
			request: (client, r) => client.GetMapping(r),
			requestAsync: (client, r) => client.GetMappingAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/project/_mapping/doc?ignore_unavailable=true";

		protected override Func<GetMappingDescriptor<Project>, IGetMappingRequest> Fluent => d => d
			.IgnoreUnavailable();

		protected override GetMappingRequest Initializer => new GetMappingRequest(Index<Project>(), Type<Project>())
		{
			IgnoreUnavailable = true
		};

		protected override void ExpectResponse(IGetMappingResponse response)
		{
			response.ShouldBeValid();

			response.Indices["project"]["doc"].Properties.Should().NotBeEmpty();
			response.Indices[Index<Project>()].Mappings[Type<Project>()].Properties.Should().NotBeEmpty();
			response.Indices[Index<Project>()][Type<Project>()].Properties.Should().NotBeEmpty();
			var properties = response.Indices[Index<Project>()][Type<Project>()].Properties;

			var leadDev = properties[Property<Project>(p => p.LeadDeveloper)];
			leadDev.Should().NotBeNull();

			var props = response.Indices["x"]?["y"].Properties;
			props.Should().BeNull();

			//hide
			AssertExtensionMethods(response);

			//hide
			AssertVisitedProperies(response);
		}

		//hide
		private static void AssertExtensionMethods(IGetMappingResponse response)
		{
			/** The `GetMappingFor` extension method can be used to get a type mapping easily and safely */
			response.GetMappingFor<Project>().Should().NotBeNull();
			response.GetMappingFor(typeof(Project), typeof(Project)).Should().NotBeNull();
			response.GetMappingFor(typeof(Project)).Should().NotBeNull();

			/** The following should all return a `null` because we had asked for the mapping of type `doc` in index `project` */
			response.GetMappingFor<Developer>().Should().BeNull();
			response.GetMappingFor("dev", "dev").Should().BeNull();
			response.GetMappingFor(typeof(Project), "x").Should().BeNull();
			response.GetMappingFor("dev").Should().BeNull();



		}
		//hide
		private static void AssertVisitedProperies(IGetMappingResponse response)
		{
			var visitor = new TestVisitor();
			var b = TestClient.Configuration.Random.SourceSerializer;
			response.Accept(visitor);
			visitor.CountsShouldContainKeyAndCountBe("type", 1);
			visitor.CountsShouldContainKeyAndCountBe("text", b ? 19 : 18);
			visitor.CountsShouldContainKeyAndCountBe("keyword", b ? 20 : 19);
			visitor.CountsShouldContainKeyAndCountBe("object", 8);
			visitor.CountsShouldContainKeyAndCountBe("number", 8);
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
		}
	}

	public class GetMappingNonExistentIndexApiTests : ApiIntegrationTestBase<ReadOnlyCluster, IGetMappingResponse, IGetMappingRequest,
		GetMappingDescriptor<Project>, GetMappingRequest>
	{
		private string _nonExistentIndex = "non-existent-index";

		public GetMappingNonExistentIndexApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => this.Calls(
			fluent: (client, f) => client.GetMapping<Project>(f),
			fluentAsync: (client, f) => client.GetMappingAsync<Project>(f),
			request: (client, r) => client.GetMapping(r),
			requestAsync: (client, r) => client.GetMappingAsync(r)
		);

		protected override bool ExpectIsValid => false;
		protected override int ExpectStatusCode => 404;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"/{_nonExistentIndex}/_mapping";

		protected override Func<GetMappingDescriptor<Project>, IGetMappingRequest> Fluent => d => d
			.Index(_nonExistentIndex)
			.AllTypes();

		protected override GetMappingRequest Initializer => new GetMappingRequest(_nonExistentIndex, AllTypes);

		protected override void ExpectResponse(IGetMappingResponse response)
		{
			response.Indices.Should().BeEmpty();
			response.ServerError.Should().NotBeNull();
		}
	}

	internal class TestVisitor : IMappingVisitor
	{
		public TestVisitor() => this.Counts = new Dictionary<string, int>();

		public int Depth { get; set; }

		public Dictionary<string, int> Counts { get; }

		private void Increment(string key)
		{
			if (!this.Counts.ContainsKey(key))
			{
				this.Counts.Add(key, 0);
			}

			this.Counts[key] += 1;
		}

		public void CountsShouldContainKeyAndCountBe(string key, int count)
		{
			this.Counts.ContainsKey(key).Should().BeTrue($"did not see {key}");
			var sb = new StringBuilder()
				.AppendLine($"because there should be {count} {key} properties");
			var because = this.Counts.Aggregate(sb, (s, kv) => s.AppendLine($"{kv.Key} = {kv.Value}"), s=>s.ToString());
			this.Counts[key].Should().Be(count, because);
		}

		public void Visit(IDateProperty mapping) => this.Increment("date");

		public void Visit(IBinaryProperty mapping) => this.Increment("binary");

		public void Visit(INestedProperty mapping) => this.Increment("nested");

		public void Visit(IGeoPointProperty mapping) => this.Increment("geo_point");

		public void Visit(ICompletionProperty mapping) => this.Increment("completion");

		public void Visit(ITokenCountProperty mapping) => this.Increment("token_count");

		public void Visit(IPercolatorProperty property) => this.Increment("percolator");

		public void Visit(IIntegerRangeProperty property) => this.Increment("integer_range");

		public void Visit(IFloatRangeProperty property) => this.Increment("float_range");

		public void Visit(ILongRangeProperty property) => this.Increment("long_range");

		public void Visit(IDoubleRangeProperty property) => this.Increment("double_range");

		public void Visit(IDateRangeProperty property) => this.Increment("date_range");

		public void Visit(IIpRangeProperty property) => this.Increment("ip_range");

		public void Visit(IJoinProperty property) => this.Increment("join");

		public void Visit(IMurmur3HashProperty mapping) => this.Increment("murmur3");

		public void Visit(INumberProperty mapping) => this.Increment("number");

		public void Visit(IGeoShapeProperty mapping) => this.Increment("geo_shape");

		public void Visit(IIpProperty mapping) => this.Increment("ip");

		public void Visit(IObjectProperty mapping) => this.Increment("object");

		public void Visit(IBooleanProperty mapping) => this.Increment("boolean");

		public void Visit(ITextProperty mapping) => this.Increment("text");

		public void Visit(IKeywordProperty mapping) => this.Increment("keyword");

		public void Visit(ITypeMapping mapping) => this.Increment("type");
	}
}

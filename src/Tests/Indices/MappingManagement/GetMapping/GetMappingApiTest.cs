using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using Xunit;
using static Nest.Infer;

namespace Tests.Indices.MappingManagement.GetMapping
{
	public class GetMappingApiTests : ApiIntegrationTestBase<ReadOnlyCluster, IGetMappingResponse, IGetMappingRequest, GetMappingDescriptor<Project>,
		GetMappingRequest>
	{
		public GetMappingApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
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

			var visitor = new TestVisitor();
			response.Accept(visitor);
			var b = TestClient.Configuration.UsingCustomSourceSerializer;

			visitor.CountsShouldContainKeyAndCountBe("type", 1);
			visitor.CountsShouldContainKeyAndCountBe("text", b ? 19 : 18);
			visitor.CountsShouldContainKeyAndCountBe("keyword", b ? 19 : 18);
			visitor.CountsShouldContainKeyAndCountBe("object", 8);
			visitor.CountsShouldContainKeyAndCountBe("number", 7);
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
			visitor.CountsShouldContainKeyAndCountBe("nested", 1);
		}
	}

	public class GetMappingNonExistentIndexApiTests : ApiIntegrationTestBase<ReadOnlyCluster, IGetMappingResponse, IGetMappingRequest,
		GetMappingDescriptor<Project>, GetMappingRequest>
	{
		private string _nonExistentIndex = "non-existent-index";

		public GetMappingNonExistentIndexApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.GetMapping<Project>(f),
			fluentAsync: (client, f) => client.GetMappingAsync<Project>(f),
			request: (client, r) => client.GetMapping(r),
			requestAsync: (client, r) => client.GetMappingAsync(r)
		);

		protected override bool ExpectIsValid => false;
		protected override int ExpectStatusCode => 404;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"/{_nonExistentIndex}/_mapping?ignore_unavailable=true";

		protected override Func<GetMappingDescriptor<Project>, IGetMappingRequest> Fluent => d => d
			.Index(_nonExistentIndex)
			.AllTypes()
			.IgnoreUnavailable();

		protected override GetMappingRequest Initializer => new GetMappingRequest(_nonExistentIndex, AllTypes)
		{
			IgnoreUnavailable = true
		};

		protected override void ExpectResponse(IGetMappingResponse response)
		{
			response.Mappings.Should().BeEmpty();
			response.Mapping.Should().BeNull();
			response.ServerError.Should().NotBeNull();
		}
	}

	internal class TestVisitor : IMappingVisitor
	{
		public TestVisitor()
		{
			Counts = new Dictionary<string, int>();
		}

		public int Depth { get; set; }

		public Dictionary<string, int> Counts { get; }

		private void Increment(string key)
		{
			if (!Counts.ContainsKey(key))
			{
				Counts.Add(key, 0);
			}
			Counts[key] += 1;
		}

		public void CountsShouldContainKeyAndCountBe(string key, int count)
		{
			this.Counts.ContainsKey(key).Should().BeTrue($"did not see {key}");
			var sb = new StringBuilder()
				.AppendLine($"because there should be {count} {key} properties");
			var because = this.Counts.Aggregate(sb, (s, kv) => s.AppendLine($"{kv.Key} = {kv.Value}"), s=>s.ToString());
			this.Counts[key].Should().Be(count, because);
		}

#pragma warning disable 618

		public void Visit(IStringProperty mapping)
		{
			Increment("string");
		}

#pragma warning restore 618

		public void Visit(IDateProperty mapping)
		{
			Increment("date");
		}

		public void Visit(IBinaryProperty mapping)
		{
			Increment("binary");
		}

		public void Visit(INestedProperty mapping)
		{
			Increment("nested");
		}

		public void Visit(IGeoPointProperty mapping)
		{
			Increment("geo_point");
		}

		public void Visit(ICompletionProperty mapping)
		{
			Increment("completion");
		}

		public void Visit(ITokenCountProperty mapping)
		{
			Increment("token_count");
		}

		public void Visit(IPercolatorProperty property)
		{
			Increment("percolator");
		}

		public void Visit(IIntegerRangeProperty property)
		{
			Increment("integer_range");
		}

		public void Visit(IFloatRangeProperty property)
		{
			Increment("float_range");
		}

		public void Visit(ILongRangeProperty property)
		{
			Increment("long_range");
		}

		public void Visit(IDoubleRangeProperty property)
		{
			Increment("double_range");
		}

		public void Visit(IDateRangeProperty property)
		{
			Increment("date_range");
		}

		public void Visit(IJoinProperty property)
		{
			Increment("join");
		}

		public void Visit(IMurmur3HashProperty mapping)
		{
			Increment("murmur3");
		}

		public void Visit(INumberProperty mapping)
		{
			Increment("number");
		}

		public void Visit(IGeoShapeProperty mapping)
		{
			Increment("geo_shape");
		}

		public void Visit(IIpProperty mapping)
		{
			Increment("ip");
		}

		public void Visit(IObjectProperty mapping)
		{
			Increment("object");
		}

		public void Visit(IBooleanProperty mapping)
		{
			Increment("boolean");
		}

		public void Visit(ITextProperty mapping)
		{
			Increment("text");
		}

		public void Visit(IKeywordProperty mapping)
		{
			Increment("keyword");
		}

		public void Visit(ITypeMapping mapping)
		{
			Increment("type");
		}
	}
}

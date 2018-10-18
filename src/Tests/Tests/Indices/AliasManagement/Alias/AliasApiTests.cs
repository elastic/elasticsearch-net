using System;
using System.Collections.Generic;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Xunit;

namespace Tests.Indices.AliasManagement.Alias
{
	public class AliasApiTests : ApiIntegrationAgainstNewIndexTestBase<WritableCluster, IBulkAliasResponse, IBulkAliasRequest, BulkAliasDescriptor, BulkAliasRequest>
	{
		public AliasApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Alias(f),
			fluentAsync: (client, f) => client.AliasAsync(f),
			request: (client, r) => client.Alias(r),
			requestAsync: (client, r) => client.AliasAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/_aliases";

		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson => new
		{
			actions = new object[]
			{
				new Dictionary<string, object> { { "add", new { alias = "alias", index = CallIsolatedValue, index_routing="x", search_routing="y"} } },
				new Dictionary<string, object> { { "remove", new { alias = "alias", index = CallIsolatedValue} } },
			}
		};

		protected override Func<BulkAliasDescriptor, IBulkAliasRequest> Fluent => d => d
			.Add(a=>a.Alias("alias").Index(CallIsolatedValue).IndexRouting("x").SearchRouting("y"))
			.Remove(a=>a.Alias("alias").Index(CallIsolatedValue))
		;

		protected override BulkAliasRequest Initializer => new BulkAliasRequest
		{
			Actions = new List<IAliasAction>
			{
				new AliasAddAction { Add = new AliasAddOperation {Alias = "alias", Index = CallIsolatedValue, IndexRouting = "x", SearchRouting = "y"} },
				new AliasRemoveAction {Remove = new AliasRemoveOperation {Alias = "alias", Index = CallIsolatedValue }},
			}
		};
	}

	[SkipVersion("<6.4.0", "is_write_index is a new feature")]
	public class AliasIsWriteIndexApiTests : ApiIntegrationAgainstNewIndexTestBase<WritableCluster, IBulkAliasResponse, IBulkAliasRequest, BulkAliasDescriptor, BulkAliasRequest>
	{
		public AliasIsWriteIndexApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Alias(f),
			fluentAsync: (client, f) => client.AliasAsync(f),
			request: (client, r) => client.Alias(r),
			requestAsync: (client, r) => client.AliasAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/_aliases";
		private string Index => this.CallIsolatedValue;
		private string Alias(int i) => $"alias-{this.CallIsolatedValue}-{i}";

		protected override void OnAfterCall(IElasticClient client)
		{
			var secondAlias = this.Alias(2);
			var aliasResponse = this.Client.GetAlias(a => a.Name(secondAlias));
			aliasResponse.ShouldBeValid();
			aliasResponse.Indices.Should().NotBeEmpty().And.ContainKey(this.Index);
			var indexAliases = aliasResponse.Indices[this.Index].Aliases;

			indexAliases.Should().NotBeEmpty().And.ContainKey(secondAlias);
			var alias = indexAliases[secondAlias];
			alias.IsWriteIndex.Should().HaveValue().And
				.BeTrue($"{secondAlias} was stored is is_write_index, so we need to be able to read it too");

		}

		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson => new
		{
			actions = new object[]
			{
				new Dictionary<string, object> { { "add",
					new { alias = this.Alias(1), index = this.Index, index_routing="x", search_routing="y", is_write_index = false} } },
				new Dictionary<string, object> { { "add",
					new { alias = this.Alias(2), index = this.Index, index_routing="x", search_routing="y", is_write_index = true} } },
			}
		};

		protected override Func<BulkAliasDescriptor, IBulkAliasRequest> Fluent => d => d
			.Add(a=>a.Alias(this.Alias(1)).Index(this.Index).IndexRouting("x").SearchRouting("y").IsWriteIndex(false))
			.Add(a=>a.Alias(this.Alias(2)).Index(this.Index).IndexRouting("x").SearchRouting("y").IsWriteIndex())
		;

		protected override BulkAliasRequest Initializer => new BulkAliasRequest
		{
			Actions = new List<IAliasAction>
			{
				new AliasAddAction { Add =
					new AliasAddOperation {Alias = this.Alias(1), Index = this.Index, IndexRouting = "x", SearchRouting = "y", IsWriteIndex = false} },
				new AliasAddAction { Add =
					new AliasAddOperation {Alias = this.Alias(2), Index = this.Index, IndexRouting = "x", SearchRouting = "y", IsWriteIndex = true} },
			}
		};
	}
}

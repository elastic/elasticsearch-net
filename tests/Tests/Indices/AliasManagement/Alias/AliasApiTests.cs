// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Indices.AliasManagement.Alias
{
	public class AliasApiTests
		: ApiIntegrationAgainstNewIndexTestBase<WritableCluster, BulkAliasResponse, IBulkAliasRequest, BulkAliasDescriptor, BulkAliasRequest>
	{
		public AliasApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new
		{
			actions = new object[]
			{
				new Dictionary<string, object>
					{ { "add", new { alias = "alias", index = CallIsolatedValue, index_routing = "x", search_routing = "y" } } },
				new Dictionary<string, object>
					{ { "add", new { aliases = new [] { "alias1", "alias2" }, indices = new [] { CallIsolatedValue } } } },
				new Dictionary<string, object> { { "remove", new { alias = "alias", index = CallIsolatedValue } } },
				new Dictionary<string, object> { { "remove", new { aliases = new [] { "alias1", "alias2" }, indices = new[] { CallIsolatedValue } } } },
			}
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<BulkAliasDescriptor, IBulkAliasRequest> Fluent => d => d
			.Add(a => a.Alias("alias").Index(CallIsolatedValue).IndexRouting("x").SearchRouting("y"))
			.Add(a => a.Aliases("alias1", "alias2").Indices(CallIsolatedValue))
			.Remove(a => a.Alias("alias").Index(CallIsolatedValue))
			.Remove(a => a.Aliases("alias1", "alias2").Indices(CallIsolatedValue));

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override BulkAliasRequest Initializer => new BulkAliasRequest
		{
			Actions = new List<IAliasAction>
			{
				new AliasAddAction
					{ Add = new AliasAddOperation { Alias = "alias", Index = CallIsolatedValue, IndexRouting = "x", SearchRouting = "y" } },
				new AliasAddAction
					{ Add = new AliasAddOperation { Aliases = new[] { "alias1", "alias2" }, Indices = CallIsolatedValue } },
				new AliasRemoveAction { Remove = new AliasRemoveOperation { Alias = "alias", Index = CallIsolatedValue } },
				new AliasRemoveAction { Remove = new AliasRemoveOperation { Aliases = new[] { "alias1", "alias2" }, Indices = CallIsolatedValue } },
			}
		};

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/_aliases";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.BulkAlias(f),
			(client, f) => client.Indices.BulkAliasAsync(f),
			(client, r) => client.Indices.BulkAlias(r),
			(client, r) => client.Indices.BulkAliasAsync(r)
		);
	}

	[SkipVersion("<6.4.0", "is_write_index is a new feature")]
	public class AliasIsWriteIndexApiTests
		: ApiIntegrationAgainstNewIndexTestBase<WritableCluster, BulkAliasResponse, IBulkAliasRequest, BulkAliasDescriptor, BulkAliasRequest>
	{
		public AliasIsWriteIndexApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new
		{
			actions = new object[]
			{
				new Dictionary<string, object>
				{
					{
						"add",
						new { alias = Alias(1), index = Index, index_routing = "x", search_routing = "y", is_write_index = false }
					}
				},
				new Dictionary<string, object>
				{
					{
						"add",
						new { alias = Alias(2), index = Index, index_routing = "x", search_routing = "y", is_write_index = true }
					}
				},
			}
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<BulkAliasDescriptor, IBulkAliasRequest> Fluent => d => d
			.Add(a => a.Alias(Alias(1)).Index(Index).IndexRouting("x").SearchRouting("y").IsWriteIndex(false))
			.Add(a => a.Alias(Alias(2)).Index(Index).IndexRouting("x").SearchRouting("y").IsWriteIndex());

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override BulkAliasRequest Initializer => new BulkAliasRequest
		{
			Actions = new List<IAliasAction>
			{
				new AliasAddAction
				{
					Add =
						new AliasAddOperation { Alias = Alias(1), Index = Index, IndexRouting = "x", SearchRouting = "y", IsWriteIndex = false }
				},
				new AliasAddAction
				{
					Add =
						new AliasAddOperation { Alias = Alias(2), Index = Index, IndexRouting = "x", SearchRouting = "y", IsWriteIndex = true }
				},
			}
		};

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/_aliases";
		private string Index => CallIsolatedValue;

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.BulkAlias(f),
			(client, f) => client.Indices.BulkAliasAsync(f),
			(client, r) => client.Indices.BulkAlias(r),
			(client, r) => client.Indices.BulkAliasAsync(r)
		);

		private string Alias(int i) => $"alias-{CallIsolatedValue}-{i}";

		protected override void OnAfterCall(IElasticClient client)
		{
			var secondAlias = Alias(2);
			var aliasResponse = Client.Indices.GetAlias(secondAlias);
			aliasResponse.ShouldBeValid();
			aliasResponse.Indices.Should().NotBeEmpty().And.ContainKey(Index);
			var indexAliases = aliasResponse.Indices[Index].Aliases;

			indexAliases.Should().NotBeEmpty().And.ContainKey(secondAlias);
			var alias = indexAliases[secondAlias];
			alias.IsWriteIndex.Should()
				.HaveValue()
				.And
				.BeTrue($"{secondAlias} was stored is is_write_index, so we need to be able to read it too");
		}
	}
}

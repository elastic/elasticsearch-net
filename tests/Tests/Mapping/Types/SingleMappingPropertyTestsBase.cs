// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using Elastic.Transport;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Mapping.Types
{
	public abstract class SingleMappingPropertyTestsBase
		: ApiIntegrationTestBase<WritableCluster, PutIndexTemplateResponse, IPutIndexTemplateRequest, PutIndexTemplateDescriptor,
			PutIndexTemplateRequest>
	{
		protected SingleMappingPropertyTestsBase(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new
		{
			order = 1,
			index_patterns = new[] { "nestx-*" },
			settings = new Dictionary<string, object> { { "index.number_of_shards", 1 } },
			mappings = new
			{
				dynamic_templates = new object[]
				{
					new
					{
						@base = new
						{
							match = "*",
							match_pattern = "simple",
							match_mapping_type = "*",
							mapping = SingleMappingJson
						}
					}
				}
			}
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<PutIndexTemplateDescriptor, IPutIndexTemplateRequest> Fluent => d => d
			.Order(1)
			.IndexPatterns("nestx-*")
			.Create(false)
			.Settings(p => p.NumberOfShards(1))
			.Map(tm => tm
				.DynamicTemplates(t => t
					.DynamicTemplate("base", dt => dt
						.Match("*")
						.MatchPattern(MatchType.Simple)
						.MatchMappingType("*")
						.Mapping(FluentSingleMapping)
					)
				)
			);

		protected abstract Func<SingleMappingSelector<object>, IProperty> FluentSingleMapping { get; }

		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override PutIndexTemplateRequest Initializer => new PutIndexTemplateRequest(CallIsolatedValue)
		{
			Order = 1,
			IndexPatterns = new[] { "nestx-*" },
			Create = false,
			Settings = new IndexSettings
			{
				NumberOfShards = 1
			},
			Mappings = new TypeMapping
			{
				DynamicTemplates = new DynamicTemplateContainer
				{
					{
						"base", new DynamicTemplate
						{
							Match = "*",
							MatchPattern = MatchType.Simple,
							MatchMappingType = "*",
							Mapping = InitializerSingleMapping
						}
					}
				}
			}
		};

		protected abstract IProperty InitializerSingleMapping { get; }

		protected abstract object SingleMappingJson { get; }
		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/_template/{CallIsolatedValue}?create=false";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.PutTemplate(CallIsolatedValue, f),
			(client, f) => client.Indices.PutTemplateAsync(CallIsolatedValue, f),
			(client, r) => client.Indices.PutTemplate(r),
			(client, r) => client.Indices.PutTemplateAsync(r)
		);

		protected override PutIndexTemplateDescriptor NewDescriptor() => new PutIndexTemplateDescriptor(CallIsolatedValue);
	}
}

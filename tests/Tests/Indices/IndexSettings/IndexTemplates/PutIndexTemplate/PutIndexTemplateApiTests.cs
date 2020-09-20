// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Indices.IndexSettings.IndexTemplates.PutIndexTemplate
{
	public class PutIndexTemplateApiTests
		: ApiIntegrationTestBase<WritableCluster, PutIndexTemplateResponse, IPutIndexTemplateRequest, PutIndexTemplateDescriptor,
			PutIndexTemplateRequest>
	{
		public PutIndexTemplateApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson { get; } = new
		{
			order = 1,
			version = 2,
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
							match_mapping_type = "*",
							mapping = new
							{
								index = false
							}
						}
					}
				}
			}
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<PutIndexTemplateDescriptor, IPutIndexTemplateRequest> Fluent => d => d
			.Order(1)
			.Version(2)
			.IndexPatterns("nestx-*")
			.Create(false)
			.Settings(p => p.NumberOfShards(1))
			.Map(tm => tm
				.DynamicTemplates(t => t
					.DynamicTemplate("base", dt => dt
						.Match("*")
						.MatchMappingType("*")
						.Mapping(mm => mm
							.Generic(g => g
								.Index(false)
							)
						)
					)
			)
		);

		protected override HttpMethod HttpMethod => HttpMethod.PUT;


		protected override PutIndexTemplateRequest Initializer => new PutIndexTemplateRequest(CallIsolatedValue)
		{
			Order = 1,
			Version = 2,
			IndexPatterns = new[] { "nestx-*" },
			Create = false,
			Settings = new Nest.IndexSettings
			{
				NumberOfShards = 1
			},
			Mappings = new TypeMapping
			{
				DynamicTemplates = new DynamicTemplateContainer
				{
					{ "base", new DynamicTemplate
					{
						Match = "*",
						MatchMappingType = "*",
						Mapping = new GenericProperty { Index = false }
					} }
				}
			}
		};

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/_template/{CallIsolatedValue}?create=false";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.PutTemplate(CallIsolatedValue, f),
			(client, f) => client.Indices.PutTemplateAsync(CallIsolatedValue, f),
			(client, r) => client.Indices.PutTemplate(r),
			(client, r) => client.Indices.PutTemplateAsync(r)
		);

		protected override PutIndexTemplateDescriptor NewDescriptor() => new PutIndexTemplateDescriptor(CallIsolatedValue);

		protected override void ExpectResponse(PutIndexTemplateResponse response)
		{
			response.ShouldBeValid();
			response.Acknowledged.Should().BeTrue();
		}
	}
}

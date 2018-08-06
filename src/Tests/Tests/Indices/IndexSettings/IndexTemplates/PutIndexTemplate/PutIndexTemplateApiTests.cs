using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.Indices.IndexSettings.IndexTemplates.PutIndexTemplate
{
	public class PutIndexTemplateApiTests
		: ApiIntegrationTestBase<WritableCluster, IPutIndexTemplateResponse, IPutIndexTemplateRequest, PutIndexTemplateDescriptor, PutIndexTemplateRequest>
	{
		public PutIndexTemplateApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage)
		{
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.PutIndexTemplate(CallIsolatedValue, f),
			fluentAsync: (client, f) => client.PutIndexTemplateAsync(CallIsolatedValue, f),
			request: (client, r) => client.PutIndexTemplate(r),
			requestAsync: (client, r) => client.PutIndexTemplateAsync(r)
			);

		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override string UrlPath => $"/_template/{CallIsolatedValue}?create=false";
		protected override bool SupportsDeserialization => false;
		protected override int ExpectStatusCode => 200;
		protected override bool ExpectIsValid => true;

		protected override object ExpectJson { get; } = new
		{
			order = 1,
			version = 2,
			index_patterns = new [] {"nestx-*" },
			settings = new Dictionary<string, object> { { "index.number_of_shards", 1 } },
			mappings = new
			{
				doc = new
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
			}
		};

		protected override PutIndexTemplateDescriptor NewDescriptor() => new PutIndexTemplateDescriptor(CallIsolatedValue);

		protected override Func<PutIndexTemplateDescriptor, IPutIndexTemplateRequest> Fluent => d => d
			.Order(1)
			.Version(2)
			.IndexPatterns("nestx-*")
			.Create(false)
			.Settings(p=>p.NumberOfShards(1))
			.Mappings(m => m
				.Map("doc", tm => tm
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
				)
			);



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
			Mappings = new Mappings
			{
				{ "doc", new TypeMapping
					{
						DynamicTemplates = new DynamicTemplateContainer
						{
							{ "base", new DynamicTemplate
								{
									Match = "*",
									MatchMappingType = "*",
									Mapping = new GenericProperty
									{
										Index = false
									}
								}
							}
						}
					}
				}
			}
		};

		protected override void ExpectResponse(IPutIndexTemplateResponse response)
		{
			response.ShouldBeValid();
			response.Acknowledged.Should().BeTrue();
		}
	}
}

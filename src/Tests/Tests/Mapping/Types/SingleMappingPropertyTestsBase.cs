using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.Mapping.Types
{
	public abstract class SingleMappingPropertyTestsBase
		: ApiIntegrationTestBase<WritableCluster, IPutIndexTemplateResponse, IPutIndexTemplateRequest, PutIndexTemplateDescriptor, PutIndexTemplateRequest>
	{
		protected SingleMappingPropertyTestsBase(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.PutIndexTemplate(CallIsolatedValue, f),
			fluentAsync: (client, f) => client.PutIndexTemplateAsync(CallIsolatedValue, f),
			request: (client, r) => client.PutIndexTemplate(r),
			requestAsync: (client, r) => client.PutIndexTemplateAsync(r)
		);

		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override string UrlPath => $"/_template/{CallIsolatedValue}?create=false";
		protected override bool SupportsDeserialization => false;
		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override object ExpectJson => new
		{
			order = 1,
			template = "nestx-*",
			settings = new Dictionary<string, object> { { "index.number_of_shards", 1 } },
			mappings = new
			{
				_default_ = new
				{
					dynamic_templates = new object[]
					{
						new
						{
							@base = new
							{
								match = "*",
								match_mapping_type = "*",
								mapping = this.SingleMappingJson
							}
						}
					}
				}
			}
		};

		protected abstract object SingleMappingJson { get;  }

		protected override PutIndexTemplateDescriptor NewDescriptor() => new PutIndexTemplateDescriptor(CallIsolatedValue);

#pragma warning disable 618 // Use of Template
		protected override Func<PutIndexTemplateDescriptor, IPutIndexTemplateRequest> Fluent => d => d
			.Order(1)
			.Template("nestx-*")
			.Create(false)
			.Settings(p=>p.NumberOfShards(1))
			.Mappings(m => m
				.Map("_default_", tm => tm
					.DynamicTemplates(t => t
						.DynamicTemplate("base", dt => dt
							.Match("*")
							.MatchMappingType("*")
							.Mapping(FluentSingleMapping)
						)
					)
				)
			);
#pragma warning restore 618

		protected abstract Func<SingleMappingDescriptor<object>, IProperty> FluentSingleMapping { get; }
		protected abstract IProperty InitializerSingleMapping { get; }

		protected override PutIndexTemplateRequest Initializer => new PutIndexTemplateRequest(CallIsolatedValue)
		{
			Order = 1,
#pragma warning disable 618
			Template = "nestx-*",
#pragma warning restore 618
			Create = false,
			Settings = new Nest.IndexSettings
			{
				NumberOfShards = 1
			},
			Mappings = new Mappings
			{
				{ "_default_", new TypeMapping
					{
						DynamicTemplates = new DynamicTemplateContainer
						{
							{ "base", new DynamicTemplate
								{
									Match = "*",
									MatchMappingType = "*",
									Mapping = InitializerSingleMapping
								}
							}
						}
					}
				}
			}
		};
	}
}

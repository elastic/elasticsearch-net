using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using Nest;
using Newtonsoft.Json;
using Tests.Framework;
using static Tests.Framework.RoundTripper;

namespace Tests.ClientConcepts.HighLevel.Mapping
{
    /**[[ignoring-properties]]
    * === Ignoring properties
    * Properties on a POCO can be ignored for mapping purposes in a few ways:
    *
    * - Using the `Ignore` property on a derived `ElasticsearchPropertyAttribute` type applied to
    * the property that should be ignored on the POCO
    *
    * - Using the `.DefaultMappingFor<TDocument>(Func<ClrTypeMappingDescriptor<TDocument>, IClrTypeMapping<TDocument>>
    * selector)` on `ConnectionSettings`
    *
    * - Using an ignore attribute applied to the POCO property that is understood by
    * the `IElasticsearchSerializer` used, and inspected inside of the `CreatePropertyMapping()` on
    * the serializer. Using the builtin `SourceSerializer` this would be the `IgnoreProperty`
    *
    * This example demonstrates all ways, using the `Ignore` property on the attribute to ignore the property
    * `PropertyToIgnore`, the infer mapping to ignore the property `AnotherPropertyToIgnore` and the
    * json serializer specific attribute  to ignore the property either `IgnoreProperty` or `JsonIgnoredProperty` depending on which
    * `SourceSerializer` we configured.
    */
    public class IgnoringProperties
	{
		private IElasticClient client = TestClient.GetInMemoryClient(c => c.DisableDirectStreaming());

		[ElasticsearchType(Name = "company")]
		public class CompanyWithAttributesAndPropertiesToIgnore
		{
			public string Name { get; set; }

			[Text(Ignore = true)]
			public string PropertyToIgnore { get; set; }

			public string AnotherPropertyToIgnore { get; set; }

			[Ignore, JsonIgnore]
			public string JsonIgnoredProperty { get; set; }
		}

		[U]
		public void Ignoring()
		{
			/** All of the properties except `Name` have been ignored in the mapping */
			var connectionSettings = new ConnectionSettings(new InMemoryConnection()) // <1> we're using an in-memory connection, but in your application, you'll want to use an `IConnection` that actually sends a request.
				.DisableDirectStreaming() // <2> we disable direct streaming here to capture the request and response bytes. In your application however, you'll like not want to do this in production.
				.DefaultMappingFor<CompanyWithAttributesAndPropertiesToIgnore>(m => m
					.Ignore(p => p.AnotherPropertyToIgnore)
				);

			var client = new ElasticClient(connectionSettings);

			var createIndexResponse = client.CreateIndex("myindex", c => c
				.Mappings(ms => ms
					.Map<CompanyWithAttributesAndPropertiesToIgnore>(m => m
						.AutoMap()
					)
				)
			);

            /**
             * The JSON output for the mapping does not contain the ignored properties
             */
            //json
            var expected = new
			{
				mappings = new
				{
					company = new
					{
						properties = new
						{
							name = new
							{
								type = "text",
								fields = new
								{
									keyword = new
									{
										type = "keyword",
										ignore_above = 256
									}
								}
							}
						}
					}
				}
			};

            //hide
			Expect(expected).NoRoundTrip().WhenSerializing(Encoding.UTF8.GetString(createIndexResponse.ApiCall.RequestBodyInBytes));
		}

        /**==== Ignoring inherited properties
         *
         * By using the infer mapping configuration for a POCO on the `ConnectionSettings`, it is possible to
         * ignore inherited properties too.
         *
         */
		public class Parent
		{
			public int Id { get; set; }
			public string Description { get; set; }
			public string IgnoreMe { get; set; }
		}

		public class Child : Parent { }

		[U]
		public void IgnoringInheritedProperties()
		{
			var connectionSettings = new ConnectionSettings(new InMemoryConnection())
				.DisableDirectStreaming()
				.DefaultMappingFor<Child>(m => m
					.PropertyName(p => p.Description, "desc")
					.Ignore(p => p.IgnoreMe)
				);

			var client = new ElasticClient(connectionSettings);

			var createIndexResponse = client.CreateIndex("myindex", c => c
				.Mappings(ms => ms
                    .Map<Child>(m => m
						.AutoMap()
					)
				)
			);

            /** The property `IgnoreMe` has been ignored for the child mapping */
            //json
            var expected = new
			{
				mappings = new
				{
                    child = new
					{
						properties = new
						{
                            id = new
                            {
                                type = "integer"
                            },
                            desc = new {
								fields = new {
									keyword = new {
										ignore_above = 256,
										type = "keyword"
									}
								},
								type = "text"
							}
						}
					}
				}
			};

            //hide
			Expect(expected).NoRoundTrip().WhenSerializing(Encoding.UTF8.GetString(createIndexResponse.ApiCall.RequestBodyInBytes));
		}
	}
}

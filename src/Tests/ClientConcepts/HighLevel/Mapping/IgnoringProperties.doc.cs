using System;
using System.Collections.Generic;
using System.Reflection;
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
    * - Using the `.InferMappingFor<TDocument>(Func<ClrTypeMappingDescriptor<TDocument>, IClrTypeMapping<TDocument>>
    * selector)` on `ConnectionSettings`
    *
    * - Using an ignore attribute applied to the POCO property that is understood by
    * the `IElasticsearchSerializer` used, and inspected inside of the `CreatePropertyMapping()` on
    * the serializer. In the case of the default `JsonNetSerializer`, this is the Json.NET `JsonIgnoreAttribute`
    *
    * This example demonstrates all ways, using the `Ignore` property on the attribute to ignore the property
    * `PropertyToIgnore`, the infer mapping to ignore the property `AnotherPropertyToIgnore` and the
    * json serializer specific attribute  to ignore the property `JsonIgnoredProperty`
    */
    public class IgnoringProperties
	{
		[ElasticsearchType(Name = "company")]
		public class CompanyWithAttributesAndPropertiesToIgnore
		{
			public string Name { get; set; }

			[String(Ignore = true)]
			public string PropertyToIgnore { get; set; }

			public string AnotherPropertyToIgnore { get; set; }

			[JsonIgnore]
			public string JsonIgnoredProperty { get; set; }
		}

		[U]
		public void Ignoring()
		{
			/** All of the properties except `Name` have been ignored in the mapping */
			var descriptor = new CreateIndexDescriptor("myindex")
				.Mappings(ms => ms
					.Map<CompanyWithAttributesAndPropertiesToIgnore>(m => m
						.AutoMap()
					)
				);

            var settings = WithConnectionSettings(s => s
                .InferMappingFor<CompanyWithAttributesAndPropertiesToIgnore>(i => i
                    .Ignore(p => p.AnotherPropertyToIgnore)
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
								type = "string"
							}
						}
					}
				}
			};

            //hide
			settings.Expect(expected).WhenSerializing((ICreateIndexRequest)descriptor);
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
			var descriptor = new CreateIndexDescriptor("myindex")
				.Mappings(ms => ms
                    .Map<Child>(m => m
						.AutoMap()
					)
				);

            var settings = WithConnectionSettings(s => s
                .InferMappingFor<Child>(m => m
                    .Rename(p => p.Description, "desc")
                    .Ignore(p => p.IgnoreMe)
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
								type = "string"
							}
						}
					}
				}
			};

            //hide
			settings.Expect(expected).WhenSerializing((ICreateIndexRequest)descriptor);
		}
	}
}

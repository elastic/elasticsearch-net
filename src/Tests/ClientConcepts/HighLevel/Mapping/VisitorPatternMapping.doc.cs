using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Newtonsoft.Json;
using Tests.Framework;
using static Tests.Framework.RoundTripper;

namespace Tests.ClientConcepts.HighLevel.Mapping
{
    /**[[visitor-pattern-mapping]]
     * === Applying conventions through the Visitor pattern
     * It is also possible to apply a transformation on all or specific properties.
     *
     * `.AutoMap()` internally implements the https://en.wikipedia.org/wiki/Visitor_pattern[visitor pattern].
     * The default visitor, `NoopPropertyVisitor`, does nothing and acts as a blank canvas for you
     * to implement your own visiting methods.
     *
     * For instance, let's create a custom visitor that disables doc values for numeric and boolean types -
     * __This is not really a good idea in practice, but let's do it anyway for the sake of a clear example.__
     */
    public class VisitorPattern
	{
		private IElasticClient client = TestClient.GetInMemoryClient(c => c.DisableDirectStreaming());

        /**
		* Using the following POCO
		*/
		public class Employee
		{
			public string FirstName { get; set; }
			public string LastName { get; set; }
			public int Salary { get; set; }
			public DateTime Birthday { get; set; }
			public bool IsManager { get; set; }
			public List<Employee> Employees { get; set; }
			public TimeSpan Hours { get; set; }
		}

        /**
         * We first define a visitor; it's easiest to inherit from `NoopPropertyVisitor` and override
         * the `Visit` methods to implement your conventions
         */
        public class DisableDocValuesPropertyVisitor : NoopPropertyVisitor
		{
			public override void Visit(
				INumberProperty type,
				PropertyInfo propertyInfo,
				ElasticsearchPropertyAttributeBase attribute) //<1> Override the `Visit` method on `INumberProperty` and set `DocValues = false`
			{
				type.DocValues = false;
			}

			public override void Visit(
				IBooleanProperty type,
				PropertyInfo propertyInfo,
				ElasticsearchPropertyAttributeBase attribute) //<2> Similarily, override the `Visit` method on `IBooleanProperty` and set `DocValues = false`
			{
				type.DocValues = false;
			}
		}

		[U]
		public void UsingACustomPropertyVisitor()
		{
			/** Now we can pass an instance of our custom visitor to `.AutoMap()` */
			var createIndexResponse = client.CreateIndex("myindex", c => c
				.Mappings(ms => ms
					.Map<Employee>(m => m.AutoMap(new DisableDocValuesPropertyVisitor()))
				)
			);

			/** and any time the client maps a property of the POCO (Employee in this example) as a number (INumberProperty) or boolean (IBooleanProperty),
			 * it will apply the transformation defined in each `Visit()` call respectively, which in this example
			 * disables {ref_current}/doc-values.html[doc_values].
			 */
			// json
			var expected = new
			{
				mappings = new
				{
					employee = new
					{
						properties = new
						{
							birthday = new
							{
								type = "date"
							},
							employees = new
							{
								properties = new { },
								type = "object"
							},
							firstName = new
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
							},
							isManager = new
							{
								doc_values = false,
								type = "boolean"
							},
							lastName = new
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
							},
							salary = new
							{
								doc_values = false,
								type = "integer"
							},
							hours = new
							{
								doc_values = false,
								type = "long"
							}
						}
					}
				}
			};

			// hide
			Expect(expected).NoRoundTrip().WhenSerializing(Encoding.UTF8.GetString(createIndexResponse.ApiCall.RequestBodyInBytes));
		}

        /**
		 * ==== Visiting on PropertyInfo
		 * You can even take the visitor approach a step further, and instead of visiting on `IProperty` types, visit
		 * directly on your POCO reflected `PropertyInfo` properties.
         *
         * As an example, let's create a visitor that maps all CLR types to an Elasticsearch text datatype (`ITextProperty`).
		 */
        public class EverythingIsATextPropertyVisitor : NoopPropertyVisitor
		{
			public override IProperty Visit(PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute) => new TextProperty();
		}

		[U]
		public void UsingACustomPropertyVisitorOnPropertyInfo()
		{
			var createIndexResponse = client.CreateIndex("myindex", c => c
				.Mappings(ms => ms
					.Map<Employee>(m => m.AutoMap(new EverythingIsATextPropertyVisitor()))
				)
			);

            /**
             */
            // json
			var expected = new
			{
				mappings = new
				{
					employee = new
					{
						properties = new
						{
							birthday = new
							{
								type = "text"
							},
							employees = new
							{
								type = "text"
							},
							firstName = new
							{
								type = "text"
							},
							isManager = new
							{
								type = "text"
							},
							lastName = new
							{
								type = "text"
							},
							salary = new
							{
								type = "text"
							},
                            hours = new
							{
								type = "text"
							}
						}
					}
				}
			};

			// hide
			Expect(expected).NoRoundTrip().WhenSerializing(Encoding.UTF8.GetString(createIndexResponse.ApiCall.RequestBodyInBytes));
		}
        /**
		 * ==== Skip properties
         *
         * Through implementing `SkipProperty` on the visitor, you can prevent certain properties from being mapped.
         *
         * In this example, we skip the inherited properties of the type from which `DictionaryDocument` is derived
         *
		 */
		public class DictionaryDocument : SortedDictionary<string, dynamic>
		{
			public int Id { get; set; }
		}

        public class IgnoreInheritedPropertiesVisitor<T>  : NoopPropertyVisitor
		{
			public override bool SkipProperty(PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute)
			{
				return propertyInfo?.DeclaringType != typeof(T);
			}
		}

		[U] public void HidesInheritedMembers()
		{
			var createIndexResponse = client.CreateIndex("myindex", c => c
				.Mappings(ms => ms
					.Map<DictionaryDocument>(m => m.AutoMap(new IgnoreInheritedPropertiesVisitor<DictionaryDocument>()))
				)
			);

            /**
             */
            // json
			var expected = new
			{
				mappings = new
				{
					dictionarydocument = new
					{
						properties = new
						{
							id = new
							{
								type = "integer"
							}
						}
					}
				}
			};

			// hide
			Expect(expected).NoRoundTrip().WhenSerializing(Encoding.UTF8.GetString(createIndexResponse.ApiCall.RequestBodyInBytes));
		}
	}
}

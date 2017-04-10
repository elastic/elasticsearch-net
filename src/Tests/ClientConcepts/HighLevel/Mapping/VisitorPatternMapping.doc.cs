using System;
using System.Collections.Generic;
using System.Reflection;
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
     * For instance, let's create a custom visitor that disables doc values for numeric and boolean types
     * (Not really a good idea in practice, but let's do it anyway for the sake of a clear example.)
     */
    public class VisitorPattern
	{
        /**
		* Using the following two POCOs as in previous examples,
		*/
        public class Company
		{
			public string Name { get; set; }
			public List<Employee> Employees { get; set; }
		}

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
			var descriptor = new CreateIndexDescriptor("myindex")
				.Mappings(ms => ms
					.Map<Employee>(m => m.AutoMap(new DisableDocValuesPropertyVisitor()))
				);

			/** and any time the client maps a property of the POCO (Employee in this example) as a number (INumberProperty) or boolean (IBooleanProperty),
			 * it will apply the transformation defined in each `Visit()` call respectively, which in this example
			 * disables {ref_current}/doc-values.html[doc_values].
			 */
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
								type = "string"
							},
							isManager = new
							{
								doc_values = false,
								type = "boolean"
							},
							lastName = new
							{
								type = "string"
							},
							salary = new
							{
								doc_values = false,
								type = "integer"
							}
						}
					}
				}
			};
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
			var descriptor = new CreateIndexDescriptor("myindex")
				.Mappings(ms => ms
					.Map<Employee>(m => m.AutoMap(new EverythingIsATextPropertyVisitor()))
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
		    Expect(expected).WhenSerializing((ICreateIndexRequest) descriptor);
		}
	}
}

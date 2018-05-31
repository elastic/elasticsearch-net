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
    /**
    * [[attribute-mapping]]
	* === Attribute mapping
    *
    * In <<auto-map, Auto mapping>>, you saw that the type mapping for a POCO can be inferred from the
    * properties of the POCO, using `.AutoMap()`. But what do you do when you want to map differently
    * to the inferred mapping? This is where attribute mapping can help.
    *
	* It is possible to define your mappings using attributes on your POCO type and properties. With
    * attributes on properties and calling `.AutoMap()`, NEST will infer the mappings from the POCO property
    * types **and** take into account the mapping attributes.
    *
    * [IMPORTANT]
    * --
    * When you use attributes, you *must* also call `.AutoMap()` for the attributes to be applied.
    * --
    *
    * Here we define an `Employee` type and use attributes to define the mappings.
	*/
    public class AttributeMapping
	{
		private IElasticClient client = TestClient.GetInMemoryClient(c => c.DisableDirectStreaming());

        [ElasticsearchType(Name = "employee")]
        public class Employee
        {
            [Text(Name = "first_name", Norms = false, Similarity = "LMDirichlet")]
            public string FirstName { get; set; }

            [Text(Name = "last_name")]
            public string LastName { get; set; }

            [Number(DocValues = false, IgnoreMalformed = true, Coerce = true)]
            public int Salary { get; set; }

            [Date(Format = "MMddyyyy")]
            public DateTime Birthday { get; set; }

            [Boolean(NullValue = false, Store = true)]
            public bool IsManager { get; set; }

            [Nested]
            [PropertyName("empl")]
            public List<Employee> Employees { get; set; }

	        [Text(Name = "office_hours")]
	        public TimeSpan? OfficeHours { get; set; }

	        [Object]
	        public List<Skill> Skills { get; set; }
        }

		public class Skill
		{
			[Text]
			public string Name { get; set; }

			[Number(NumberType.Byte, Name = "level")]
			public int Proficiency { get; set; }
		}

		/**Then we map the types by calling `.AutoMap()` */
		[U]
		public void UsingAutoMapWithAttributes()
		{
			var createIndexResponse = client.CreateIndex("myindex", c => c
				.Mappings(ms => ms
					.Map<Employee>(m => m.AutoMap())
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
								format = "MMddyyyy",
								type = "date"
							},
							empl = new
							{
								properties = new {},
								type = "nested"
							},
							first_name = new
							{
								type = "text",
								norms = false,
								similarity = "LMDirichlet"
							},
							isManager = new
							{
								null_value = false,
								store = true,
								type = "boolean"
							},
							last_name = new
							{
								type = "text"
							},
							office_hours = new
							{
								type = "text"
							},
							salary = new
							{
								coerce = true,
								doc_values = false,
								ignore_malformed = true,
								type = "float"
							},
							skills = new
							{
								properties = new
								{
									level = new
									{
										type = "byte"
									},
									name = new
									{
										type = "text"
									}
								},
								type = "object"
							}
						}
					}
				}
			};

            // hide
			Expect(expected).NoRoundTrip().WhenSerializing(Encoding.UTF8.GetString(createIndexResponse.ApiCall.RequestBodyInBytes));
		}
        /**
         * Attribute mapping can be a convenient way to control how POCOs are mapped with minimal code, however
         * there are some mapping features that cannot be expressed with attributes, for example, <<multi-fields, Multi fields>>.
         * In order to have the full power of mapping in NEST at your disposal,
         * take a look at <<fluent-mapping, Fluent Mapping>> next.
         */
	}
}

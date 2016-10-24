using System;
using System.Collections.Generic;
using System.Reflection;
using Nest;
using Newtonsoft.Json;
using Tests.Framework;
using static Tests.Framework.RoundTripper;

namespace Tests.ClientConcepts.HighLevel.Mapping
{
	/**
	* [[auto-map]]
	* == Auto mapping properties
	 *
	 * When creating a mapping (either when creating an index or via the put mapping API),
	* NEST offers a feature called `.AutoMap()`, which will automagically infer the correct
	 * Elasticsearch datatypes of the POCO properties you are mapping.  Alternatively, if
	* you're using attributes to map your properties, then calling `.AutoMap()` is required
	* in order for your attributes to be applied.  We'll look at the features of auto mapping
	* with a number of examples.
	**/
	public class AutoMap
	{
		/**
		* For these examples, we'll define two POCOS, `Company`, which has a name
		* and a collection of Employees, and `Employee` which has various properties of
		* different types, and itself has a collection of `Employee` types.
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

		[U]
		public void MappingManually()
		{
			/** === Manual mapping
			 * To create a mapping for our Company type, we can use the fluent API
			 * and map each property explicitly
			 */
			var descriptor = new CreateIndexDescriptor("myindex")
				.Mappings(ms => ms
					.Map<Company>(m => m
						.Properties(ps => ps
							.Text(s => s
								.Name(c => c.Name) //<1> map `Name` as a `string` type
							)
							.Object<Employee>(o => o  //<2> map `Employees` as an `object` type, mapping each of the properties of `Employee`
								.Name(c => c.Employees)
								.Properties(eps => eps
									.Text(s => s
										.Name(e => e.FirstName)
									)
									.Text(s => s
										.Name(e => e.LastName)
									)
									.Number(n => n
										.Name(e => e.Salary)
										.Type(NumberType.Integer)
									)
								)
							)
						)
					)
				);

			/**
			 * This is all fine and dandy and useful for some use cases however in most cases
			 * this can become verbose and wieldy. The majority of the time you simply just want to map *all*
			 * the properties of a POCO in a single go.
			 */
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
								type = "text"
							},
							employees = new
							{
								type = "object",
								properties = new
								{
									firstName = new
									{
										type = "text"
									},
									lastName = new
									{
										type = "text"
									},
									salary = new
									{
										type = "integer"
									}
								}
							}
						}
					}
				}
			};

			Expect(expected).WhenSerializing((ICreateIndexRequest)descriptor);
		}

		[U]
		public void UsingAutoMap()
		{
			/** === Simple Automapping
			* This is exactly where `.AutoMap()` becomes useful. Instead of manually mapping each property,
			* explicitly, we can instead call `.AutoMap()` for each of our mappings and let NEST do all the work
			*/
			var descriptor = new CreateIndexDescriptor("myindex")
				.Mappings(ms => ms
					.Map<Company>(m => m.AutoMap())
					.Map<Employee>(m => m.AutoMap())
				);

			/**
			* Observe that NEST has inferred the Elasticsearch types based on the CLR type of our POCO properties.
			* In this example,
			* - Birthday was mapped as a `date`,
			* - Hours was mapped as a `long` (ticks)
			* - IsManager was mapped as a `bool`,
			* - Salary as an `integer`
			* - Employees as an `object`
			*
			* and the remaining string properties as `string` types
			*/
			var expected = new
			{
				mappings = new
				{
					company = new
					{
						properties = new
						{
							employees = new
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
										fields = new
										{
											keyword = new
											{
												type = "keyword"
											}
										},
										type = "text"
									},
									hours = new
									{
										type = "long"
									},
									isManager = new
									{
										type = "boolean"
									},
									lastName = new
									{
										fields = new
										{
											keyword = new
											{
												type = "keyword"
											}
										},
										type = "text"
									},
									salary = new
									{
										type = "integer"
									}
								},
								type = "object"
							},
							name = new
							{
								fields = new
								{
									keyword = new
									{
										type = "keyword"
									}
								},
								type = "text"
							}
						}
					},
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
								fields = new
								{
									keyword = new
									{
										type = "keyword"
									}
								},
								type = "text"
							},
							hours = new
							{
								type = "long"
							},
							isManager = new
							{
								type = "boolean"
							},
							lastName = new
							{
								fields = new
								{
									keyword = new
									{
										type = "keyword"
									}
								},
								type = "text"
							},
							salary = new
							{
								type = "integer"
							}
						}
					}
				}
			};


			Expect(expected).WhenSerializing((ICreateIndexRequest)descriptor);
		}

		/**[float]
		* == Auto mapping with overrides
		* In most cases, you'll want to map more than just the vanilla datatypes and also provide
		* various options for your properties (analyzer to use, whether to enable doc_values, etc...).
		* In that case, it's possible to use `.AutoMap()` in conjunction with explicitly mapped properties.
		*/
		[U]
		public void OverridingAutoMappedProperties()
		{
			/**
			* Here we are using `.AutoMap()` to automatically map our company type, but then we're
			* overriding our employee property and making it a `nested` type, since by default,
			* `.AutoMap()` will infer objects as `object`.
			*/
			var descriptor = new CreateIndexDescriptor("myindex")
				.Mappings(ms => ms
					.Map<Company>(m => m
						.AutoMap()
						.Properties(ps => ps
							.Nested<Employee>(n => n
								.Name(c => c.Employees)
							)
						)
					)
				);

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
										type = "keyword"
									}
								}
							},
							employees = new
							{
								type = "nested",
							}
						}
					}
				}
			};

			Expect(expected).WhenSerializing((ICreateIndexRequest)descriptor);

			/**
			 * `.AutoMap()` is idempotent; calling it before or after manually
			 * mapped properties will still yield the same results.
			 */
			descriptor = new CreateIndexDescriptor("myindex")
				.Mappings(ms => ms
					.Map<Company>(m => m
						.Properties(ps => ps
							.Nested<Employee>(n => n
								.Name(c => c.Employees)
							)
						)
						.AutoMap()
					)
				);

			Expect(expected).WhenSerializing((ICreateIndexRequest)descriptor);
		}

		/**[[attribute-mapping]]
		 * [float]
		 * == Attribute mapping
		 * It is also possible to define your mappings using attributes on your POCOs.  When you
		 * use attributes, you *must* use `.AutoMap()` in order for the attributes to be applied.
		 * Here we define the same two types as before, but this time using attributes to define the mappings.
		 */
		[ElasticsearchType(Name = "company")]
		public class CompanyWithAttributes
		{
			[Keyword(NullValue = "null", Similarity = "BM25")]
			public string Name { get; set; }

			[Text(Name = "office_hours")]
			public TimeSpan? HeadOfficeHours { get; set; }

			[Object(Store = false)]
			public List<Employee> Employees { get; set; }
		}

		[ElasticsearchType(Name = "employee")]
		public class EmployeeWithAttributes
		{
			[Text(Name = "first_name")]
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
			[JsonProperty("empl")]
			public List<Employee> Employees { get; set; }
		}

		/**Then we map the types by calling `.AutoMap()` */
		[U]
		public void UsingAutoMapWithAttributes()
		{
			var descriptor = new CreateIndexDescriptor("myindex")
				.Mappings(ms => ms
					.Map<CompanyWithAttributes>(m => m.AutoMap())
					.Map<EmployeeWithAttributes>(m => m.AutoMap())
				);

			var expected = new
			{
				mappings = new
				{
					company = new
					{
						properties = new
						{
							employees = new
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
										fields = new
										{
											keyword = new
											{
												type = "keyword"
											}
										},
										type = "text"
									},
									hours = new
									{
										type = "long"
									},
									isManager = new
									{
										type = "boolean"
									},
									lastName = new
									{
										fields = new
										{
											keyword = new
											{
												type = "keyword"
											}
										},
										type = "text"
									},
									salary = new
									{
										type = "integer"
									}
								},
								store = false,
								type = "object"
							},
							name = new
							{
								null_value = "null",
								similarity = "BM25",
								type = "keyword"
							},
							office_hours = new
							{
								type = "text"
							}
						}
					},
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
										fields = new
										{
											keyword = new
											{
												type = "keyword"
											}
										},
										type = "text"
									},
									hours = new
									{
										type = "long"
									},
									isManager = new
									{
										type = "boolean"
									},
									lastName = new
									{
										fields = new
										{
											keyword = new
											{
												type = "keyword"
											}
										},
										type = "text"
									},
									salary = new
									{
										type = "integer"
									}
								},
								type = "nested"
							},
							first_name = new
							{
								type = "text"
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
							salary = new
							{
								coerce = true,
								doc_values = false,
								ignore_malformed = true,
								type = "float"
							}
						}
					}
				}
			};


			Expect(expected).WhenSerializing(descriptor as ICreateIndexRequest);
		}

		/**
		 * Just as we were able to override the inferred properties in our earlier example, explicit (manual)
		 * mappings also take precedence over attributes.  Therefore we can also override any mappings applied
		 * via any attributes defined on the POCO
		 */
		[U]
		public void OverridingAutoMappedAttributes()
		{
			var descriptor = new CreateIndexDescriptor("myindex")
				.Mappings(ms => ms
					.Map<CompanyWithAttributes>(m => m
						.AutoMap()
						.Properties(ps => ps
							.Nested<Employee>(n => n
								.Name(c => c.Employees)
							)
						)
					)
					.Map<EmployeeWithAttributes>(m => m
						.AutoMap()
						.Properties(ps => ps
							.Text(s => s
								.Name(e => e.FirstName)
								.Fields(fs => fs
									.Keyword(ss => ss
										.Name("firstNameRaw")
									)
									.TokenCount(t => t
										.Name("length")
										.Analyzer("standard")
									)
								)
							)
							.Number(n => n
								.Name(e => e.Salary)
								.Type(NumberType.Double)
								.IgnoreMalformed(false)
							)
							.Date(d => d
								.Name(e => e.Birthday)
								.Format("MM-dd-yy")
							)
						)
					)
				);

			var expected = new
			{
				mappings = new
				{
					company = new
					{
						properties = new
						{
							employees = new
							{
								type = "nested"
							},
							name = new
							{
								null_value = "null",
								similarity = "BM25",
								type = "keyword"
							},
							office_hours = new
							{
								type = "text"
							}
						}
					},
					employee = new
					{
						properties = new
						{
							birthday = new
							{
								format = "MM-dd-yy",
								type = "date"
							},
							empl = new
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
										fields = new
										{
											keyword = new
											{
												type = "keyword"
											}
										},
										type = "text"
									},
									hours = new
									{
										type = "long"
									},
									isManager = new
									{
										type = "boolean"
									},
									lastName = new
									{
										fields = new
										{
											keyword = new
											{
												type = "keyword"
											}
										},
										type = "text"
									},
									salary = new
									{
										type = "integer"
									}
								},
								type = "nested"
							},
							first_name = new
							{
								fields = new
								{
									firstNameRaw = new
									{
										type = "keyword"
									},
									length = new
									{
										analyzer = "standard",
										type = "token_count"
									}
								},
								type = "text"
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
							salary = new
							{
								ignore_malformed = false,
								type = "double"
							}
						}
					}
				}
			};

			Expect(expected).WhenSerializing((ICreateIndexRequest)descriptor);
		}

		/**[float]
		* == Ignoring Properties
		* Properties on a POCO can be ignored in a few ways:
		*
		* - Using the `Ignore` property on a derived `ElasticsearchPropertyAttribute` type applied to the property that should be ignored on the POCO
		*
		* - Using the `.InferMappingFor<TDocument>(Func<ClrTypeMappingDescriptor<TDocument>, IClrTypeMapping<TDocument>> selector)` on the connection settings
		*
		* - Using an ignore attribute applied to the POCO property that is understood by the `IElasticsearchSerializer` used, and inspected inside of the `CreatePropertyMapping()` on the serializer. In the case of the default `JsonNetSerializer`, this is the Json.NET `JsonIgnoreAttribute`
		*
		* This example demonstrates all ways, using the `Ignore` property on the attribute to ignore the property `PropertyToIgnore`, the infer mapping to ignore the
		* property `AnotherPropertyToIgnore` and the json serializer specific attribute  to ignore the property `JsonIgnoredProperty`
		*/
		[ElasticsearchType(Name = "company")]
		public class CompanyWithAttributesAndPropertiesToIgnore
		{
			public string Name { get; set; }

			[Text(Ignore = true)]
			public string PropertyToIgnore { get; set; }

			public string AnotherPropertyToIgnore { get; set; }

			[JsonIgnore]
			public string JsonIgnoredProperty { get; set; }
		}

		[U]
		public void IgnoringProperties()
		{
			/** All of the properties except `Name` have been ignored in the mapping */
			var descriptor = new CreateIndexDescriptor("myindex")
				.Mappings(ms => ms
					.Map<CompanyWithAttributesAndPropertiesToIgnore>(m => m
						.AutoMap()
					)
				);

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
										type = "keyword"
									}
								}
							}
						}
					}
				}
			};

			var settings = WithConnectionSettings(s => s
				.InferMappingFor<CompanyWithAttributesAndPropertiesToIgnore>(i => i
					.Ignore(p => p.AnotherPropertyToIgnore)
				)
			);

			settings.Expect(expected).WhenSerializing((ICreateIndexRequest)descriptor);
		}

		/**[float]
		 * == Mapping Recursion
		 * If you notice in our previous `Company` and `Employee` examples, the `Employee` type is recursive
		 * in that the `Employee` class itself contains a collection of type `Employee`. By default, `.AutoMap()` will only
		 * traverse a single depth when it encounters recursive instances like this.  Hence, in the
		 * previous examples, the collection of type `Employee` on the `Employee` class did not get any of its properties mapped.
		 * This is done as a safe-guard to prevent stack overflows and all the fun that comes with
		 * infinite recursion.  Additionally, in most cases, when it comes to Elasticsearch mappings, it is
		 * often an edge case to have deeply nested mappings like this.  However, you may still have
		 * the need to do this, so you can control the recursion depth of `.AutoMap()`.
		 *
		 * Let's introduce a very simple class, `A`, which itself has a property
		 * Child of type `A`.
		 */
		public class A
		{
			public A Child { get; set; }
		}

		[U]
		public void ControllingRecursionDepth()
		{
			/** By default, `.AutoMap()` only goes as far as depth 1 */
			var descriptor = new CreateIndexDescriptor("myindex")
				.Mappings(ms => ms
					.Map<A>(m => m.AutoMap())
				);

			/** Thus we do not map properties on the second occurrence of our Child property */
			var expected = new
			{
				mappings = new
				{
					a = new
					{
						properties = new
						{
							child = new
							{
								properties = new { },
								type = "object"
							}
						}
					}
				}
			};

			Expect(expected).WhenSerializing((ICreateIndexRequest)descriptor);

			/** Now let's specify a maxRecursion of 3 */
			var withMaxRecursionDescriptor = new CreateIndexDescriptor("myindex")
				.Mappings(ms => ms
					.Map<A>(m => m.AutoMap(3))
				);

			/** `.AutoMap()` has now mapped three levels of our Child property */
			var expectedWithMaxRecursion = new
			{
				mappings = new
				{
					a = new
					{
						properties = new
						{
							child = new
							{
								type = "object",
								properties = new
								{
									child = new
									{
										type = "object",
										properties = new
										{
											child = new
											{
												type = "object",
												properties = new
												{
													child = new
													{
														type = "object",
														properties = new { }
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			};

			Expect(expectedWithMaxRecursion).WhenSerializing((ICreateIndexRequest)withMaxRecursionDescriptor);
		}

		[U]
		//hide
		public void PutMappingAlsoAdheresToMaxRecursion()
		{
			var descriptor = new PutMappingDescriptor<A>().AutoMap();

			var expected = new
			{
				properties = new
				{
					child = new
					{
						properties = new { },
						type = "object"
					}
				}
			};

			Expect(expected).WhenSerializing((IPutMappingRequest)descriptor);

			var withMaxRecursionDescriptor = new PutMappingDescriptor<A>().AutoMap(3);

			var expectedWithMaxRecursion = new
			{
				properties = new
				{
					child = new
					{
						type = "object",
						properties = new
						{
							child = new
							{
								type = "object",
								properties = new
								{
									child = new
									{
										type = "object",
										properties = new
										{
											child = new
											{
												type = "object",
												properties = new { }
											}
										}
									}
								}
							}
						}
					}
				}
			};

			Expect(expectedWithMaxRecursion).WhenSerializing((IPutMappingRequest)withMaxRecursionDescriptor);
		}
		//endhide

		/**[float]
		 * == Applying conventions through the Visitor pattern
		 * It is also possible to apply a transformation on all or specific properties.
		 *
		 * `.AutoMap()` internally implements the https://en.wikipedia.org/wiki/Visitor_pattern[visitor pattern]. The default visitor, `NoopPropertyVisitor`,
		 * does nothing and acts as a blank canvas for you to implement your own visiting methods.
		 *
		 * For instance, let's create a custom visitor that disables doc values for numeric and boolean types
		 * (Not really a good idea in practice, but let's do it anyway for the sake of a clear example.)
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

		/**=== Visiting on PropertyInfo
		 * You can even take the visitor approach a step further, and instead of visiting on `IProperty` types, visit
		 * directly on your POCO properties (PropertyInfo). As an example, let's create a visitor that maps all CLR types
		 * to an Elasticsearch text datatype (ITextProperty).
		 */
		public class EverythingIsAStringPropertyVisitor : NoopPropertyVisitor
		{
			public override IProperty Visit(PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute) => new TextProperty();
		}

		[U]
		public void UsingACustomPropertyVisitorOnPropertyInfo()
		{
			var descriptor = new CreateIndexDescriptor("myindex")
				.Mappings(ms => ms
					.Map<Employee>(m => m.AutoMap(new EverythingIsAStringPropertyVisitor()))
				);

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
							}
						}
					}
				}
			};
		}
	}
}

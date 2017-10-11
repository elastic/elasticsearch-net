using System;
using System.Collections.Generic;
using Nest;
using Tests.Framework;
using static Tests.Framework.RoundTripper;

namespace Tests.ClientConcepts.HighLevel.Mapping
{
	/**
	* [[auto-map]]
	* === Auto mapping
	*
	* When creating a mapping either when creating an index or through the Put Mapping API,
	* NEST offers a feature called auto mapping that can automagically infer the correct
	* Elasticsearch field datatypes from the CLR POCO property types you are mapping.
	**/
	public class AutoMap
	{
		/**
		* We'll look at the features of auto mapping with a number of examples. For this,
		* we'll define two POCOs, `Company`, which has a name
		* and a collection of Employees, and `Employee` which has various properties of
		* different types, and itself has a collection of `Employee` types.
		*/

		public abstract class Document
		{
			public JoinField Join { get; set; }
		}

		public class Company : Document
		{
			public string Name { get; set; }
			public List<Employee> Employees { get; set; }
		}

		public class Employee : Document
		{
			public string LastName { get; set; }
			public int Salary { get; set; }
			public DateTime Birthday { get; set; }
			public bool IsManager { get; set; }
			public List<Employee> Employees { get; set; }
			public TimeSpan Hours { get; set; }
		}

		[U]
		public void UsingAutoMap()
		{
			/**
            * Auto mapping can take the pain out of having to define a manual mapping for all properties
            * on the POCO. In this case we want to index two subclasses into a single index. We call Map
            * for the base class and then call AutoMap foreach of the types we want it it the implement
			*/

			var descriptor = new CreateIndexDescriptor("myindex")
				.Mappings(ms => ms
					.Map<Document>(m => m
						.AutoMap<Company>() // <1> Auto map `Company`
						.AutoMap<Employee>() // <1> Auto map `Employee`
					)
				);

			// json
			var expected = new
			{
				mappings = new
				{
					document = new
					{
						properties = new
						{
							birthday = new {type = "date"},
							employees = new
							{
								properties = new
								{
									birthday = new {type = "date"},
									employees = new
									{
										properties = new { },
										type = "object"
									},
									hours = new {type = "long"},
									isManager = new {type = "boolean"},
									join = new
									{
										properties = new { },
										type = "object"
									},
									lastName = new
									{
										fields = new
										{
											keyword = new
											{
												ignore_above = 256,
												type = "keyword"
											}
										},
										type = "text"
									},
									salary = new {type = "integer"}
								},
								type = "object"
							},
							hours = new {type = "long"},
							isManager = new {type = "boolean"},
							join = new
							{
								properties = new { },
								type = "object"
							},
							lastName = new
							{
								fields = new
								{
									keyword = new
									{
										ignore_above = 256,
										type = "keyword"
									}
								},
								type = "text"
							},
							name = new
							{
								fields = new
								{
									keyword = new
									{
										ignore_above = 256,
										type = "keyword"
									}
								},
								type = "text"
							},
							salary = new {type = "integer"}
						}
					}
				}
			};

			// hide
			Expect(expected).WhenSerializing((ICreateIndexRequest) descriptor);
		}

		public class ParentWithStringId : IgnoringProperties.Parent
		{
			public new string Id { get; set; }
		}

		[U]
		public void OverridingInheritedProperties()
		{
			var descriptor = new CreateIndexDescriptor("myindex")
				.Mappings(ms => ms
					.Map<ParentWithStringId>(m => m
						.AutoMap()
					)
				);

			var expected = new
			{
				mappings = new
				{
					parent = new
					{
						properties = new
						{
							id = new
							{
								type = "text",
								fields = new
								{
									keyword = new
									{
										ignore_above = 256,
										type = "keyword"
									}
								}
							}
						}
					}
				}
			};

			var settings = WithConnectionSettings(s => s
				.InferMappingFor<ParentWithStringId>(m => m
					.TypeName("parent")
					.Ignore(p => p.Description)
					.Ignore(p => p.IgnoreMe)
				)
			);

			settings.Expect(expected).WhenSerializing((ICreateIndexRequest) descriptor);
		}
		/**
		 * Observe that NEST has inferred the Elasticsearch types based on the CLR type of our POCO properties.
		 * In this example,
		 *
		 * - Birthday is mapped as a `date`,
		 * - Hours is mapped as a `long` (`TimeSpan` ticks)
		 * - IsManager is mapped as a `boolean`,
		 * - Salary is mapped as an `integer`
		 * - Employees is mapped as an `object`
		 *
		 * and the remaining string properties as multi field `text` datatypes, each with a `keyword` datatype
         * sub field.
         *
         * NEST has inferred mapping support for the following .NET types
         *
         * - `String` maps to `"text"` with a `"keyword"` sub field. See <<multi-fields, Multi Fields>>.
         * - `Int32` maps to `"integer"`
         * - `UInt16` maps to `"integer"`
         * - `Int16` maps to `"short"`
         * - `Byte` maps to `"short"`
         * - `Int64` maps to `"long"`
         * - `UInt32` maps to `"long"`
         * - `TimeSpan` maps to `"long"`
         * - `Single` maps to `"float"`
         * - `Double` maps to `"double"`
         * - `Decimal` maps to `"double"`
         * - `UInt64` maps to `"double"`
         * - `DateTime` maps to `"date"`
         * - `DateTimeOffset` maps to `"date"`
         * - `Boolean` maps to `"boolean"`
         * - `Char` maps to `"keyword"`
         * - `Guid` maps to `"keyword"`
         *
         * and supports a number of special types defined in NEST
         *
         * - `Nest.GeoLocation` maps to `"geo_point"`
         * - `Nest.CompletionField` maps to `"completion"`
         * - `Nest.Attachment` maps to `"attachment"`
         * - `Nest.DateRange` maps to `"date_range"`
         * - `Nest.DoubleRange` maps to `"double_range"`
         * - `Nest.FloatRange` maps to `"float_range"`
         * - `Nest.IntegerRange` maps to `"integer_range"`
         * - `Nest.LongRange` maps to `"long_range"`
		 *
         * All other types map to `"object"` by default.
         *
         *[IMPORTANT]
		 * --
		 * Some .NET types do not have direct equivalent Elasticsearch types. For example, `System.Decimal` is a type
		 * commonly used to express currencies and other financial calculations that require large numbers of significant
		 * integral and fractional digits and no round-off errors. There is no equivalent type in Elasticsearch, and the
		 * nearest type is {ref_current}/number.html[double], a double-precision 64-bit IEEE 754 floating point.
		 *
		 * When a POCO has a `System.Decimal` property, it is automapped to the Elasticsearch `double` type. With the caveat
		 * of a potential loss of precision, this is generally acceptable for a lot of use cases, but it can however cause
		 * problems in _some_ edge cases.
		 *
		 * As the https://download.microsoft.com/download/3/8/8/388e7205-bc10-4226-b2a8-75351c669b09/csharp%20language%20specification.doc[C# Specification states],
		 *
		 * [quote, C# Specification section 6.2.1]
		 * For a conversion from `decimal` to `float` or `double`, the `decimal` value is rounded to the nearest `double` or `float` value.
		 * While this conversion may lose precision, it never causes an exception to be thrown.
		 *
		 * This conversion causes an exception to be thrown at deserialization time for `Decimal.MinValue` and `Decimal.MaxValue` because, at
		 * serialization time, the nearest `double` value that is converted to is outside of the bounds of `Decimal.MinValue` or `Decimal.MaxValue`,
		 * respectively. In these cases, it is advisable to use `double` as the POCO property type.
		 * --
		 */

		/**[float]
		 * === Mapping Recursion
		 * If you notice in our previous `Company` and `Employee` example, the `Employee` type is recursive
		 * in that the `Employee` class itself contains a collection of type `Employee`. By default, `.AutoMap()` will only
		 * traverse a single depth when it encounters recursive instances like this; the collection of type `Employee`
         * on the `Employee` class did not get any of its properties mapped.
         *
		 * This is done as a safe-guard to prevent stack overflows and all the fun that comes with
		 * __infinite__ recursion.  Additionally, in most cases, when it comes to Elasticsearch mappings, it is
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
			//json
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

			//hide
			Expect(expected).WhenSerializing((ICreateIndexRequest) descriptor);

			/** Now let's specify a maxRecursion of `3` */
			var withMaxRecursionDescriptor = new CreateIndexDescriptor("myindex")
				.Mappings(ms => ms
					.Map<A>(m => m.AutoMap(3))
				);

			/** `.AutoMap()` has now mapped three levels of our Child property */
			//json
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

			//hide
			Expect(expectedWithMaxRecursion).WhenSerializing((ICreateIndexRequest) withMaxRecursionDescriptor);
		}

		//hide
		[U]
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

			Expect(expected).WhenSerializing((IPutMappingRequest) descriptor);

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

			Expect(expectedWithMaxRecursion).WhenSerializing((IPutMappingRequest) withMaxRecursionDescriptor);
		}
	}
}

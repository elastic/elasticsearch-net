using System;
using System.Collections.Generic;
using System.Reflection;
using Nest;
using Newtonsoft.Json;
using Tests.ClientConcepts.HighLevel.Mapping;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.RoundTripper;

namespace Tests.ClientConcepts.HighLevel.Serialization
{
    /**[[extending-nest-types]]
     * === Extending NEST types
     */
    public class ExtendingNestTypes
	{
        public class MyTermQuery : TermQuery
		{
			public string AdditionalProperty { get; set; }
		}

		public class IcuCollationKeywordProperty : IProperty
		{
			public IcuCollationKeywordProperty(string name, string language)
			{
				this.Name = name;
				this.Type = "icu_collation_keyword";
				this.Language = language;
				this.Variant = "@collation=standard";
				this.Strength = "primary";
				this.Numeric = true;
			}

			public string Type { get; set; }
			public PropertyName Name { get; set; }
			public IDictionary<string, object> LocalMetadata { get; set; }

			[Rename("language")]
			public string Language { get; set; }

			[Rename("strength")]
			public string Strength { get; set; }

			[Rename("variant")]
			public string Variant { get; set; }

			[Rename("numeric")]
			public bool Numeric { get; set; }
		}

		[U]
		public void InjectACustomIPropertyImplementation()
		{
			/** Now we can pass an instance of our custom visitor to `.AutoMap()` */
			var descriptor = new CreateIndexDescriptor("myindex")
				.Mappings(ms => ms
					.Map<Project>(m => m
						.Properties(props => props
							.Custom(new IcuCollationKeywordProperty("fieldName", "dutch"))
						)
					)
				);

			/** and any time the client maps a property of the POCO (Employee in this example) as a number (INumberProperty) or boolean (IBooleanProperty),
			 * it will apply the transformation defined in each `Visit()` call respectively, which in this example
			 * disables {ref_current}/doc-values.html[doc_values].
			 */
			var expected = new
			{
				mappings = new
				{
					doc = new
					{
						properties = new
						{
							fieldName = new
							{
								type = "icu_collation_keyword",
								language = "dutch",
								variant = "@collation=standard",
								strength = "primary",
								numeric = true
							}
						}
					}
				}
			};

		    Expect(expected)
			    .NoRoundTrip()
			    .WhenSerializing((ICreateIndexRequest) descriptor);
		}
	}
}

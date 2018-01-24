using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
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
     *
     * Although of course NEST should always be up to date with a 100% API coverage of the Elasticsearch API, sometimes you might want to work around a bug
     * or maybe use types from a third party plugin that NEST does not support.
     */
    public class ExtendingNestTypes
	{
		private IElasticClient client = TestClient.GetInMemoryClient(c => c.DisableDirectStreaming());

		/* === Creating your own property mapping
		*
		 * Here we implement a custom `IProperty` implementation so that we can consume a field mapping type provided by a plugin.
		*/
		public class MyPluginProperty : IProperty
		{
			IDictionary<string, object> IProperty.LocalMetadata { get; set; }
			public string Type { get; set; } = "my_plugin_property";
			public PropertyName Name { get; set; }

			public MyPluginProperty(string name, string language)
			{
				this.Name = name;
				this.Language = language;
				this.Numeric = true;
			}

			[PropertyName("language")]
			public string Language { get; set; }

			[PropertyName("numeric")]
			public bool Numeric { get; set; }
		}

		[U] public void InjectACustomIPropertyImplementation()
		{
			/**
			 * `PropertyNameAttribute` can be used to mark properties that should be serialized. Without this attribute,
			 * NEST won't pick up the property for serialization.
			 *
			 * Now that we have our own `IProperty` implementation we can add it to our propertes mapping when creating an index
			 */
			var createIndexResponse = client.CreateIndex("myindex", c => c
				.Mappings(ms => ms
					.Map<Project>(m => m
						.Properties(props => props
							.Custom(new MyPluginProperty("fieldName", "dutch"))
						)
					)
				)
			);

			/**
			 * which will serialize to the following JSON request
			 */
			// json
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
								type = "my_plugin_property",
								language = "dutch",
								numeric = true
							}
						}
					}
				}
			};

			/**
			 * Whilst NEST can serialize our `my_plugin_property`, it does not know how to deserialize it;
			 * We plan to make this more pluggable in the future
			 */
			// hide
		    Expect(expected)
			    .NoRoundTrip()
			    .WhenSerializing(Encoding.UTF8.GetString(createIndexResponse.ApiCall.RequestBodyInBytes));
		}
	}
}

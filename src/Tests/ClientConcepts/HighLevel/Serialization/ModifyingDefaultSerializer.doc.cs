using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;
using Newtonsoft.Json;
using Tests.Framework.MockData;

namespace Tests.ClientConcepts.HighLevel.Serialization
{
    public class ModifyingTheDefaultSerializer
    {
        /**[[modifying-default-serializer]]
         * === Modifying the default serializer
         *
         * In <<changing-serializers, Changing serializers>>, you saw how it is possible to provide your own serializer
         * implementation to NEST. A more common scenario is the desire to change the settings on the default JSON.Net
         * serializer.
         *
         * There are a couple of ways in which this can be done, depending on what it is you need to change.
         *
         * ==== Modifying settings using SerializerFactory
         *
         * The default implementation of `ISerializerFactory` allows a delegate to be passed that can change
         * the settings for JSON.Net serializers created by the factory
         *
         */
        public void ModifyingJsonNetSettings()
        {
            var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
            var connection = new HttpConnection();
            var connectionSettings =
                new ConnectionSettings(pool, connection, new SerializerFactory((settings, values) => // <1> delegate will be passed `JsonSerializerSettings` and `IConnectionSettingsValues`
                {
                    settings.NullValueHandling = NullValueHandling.Include;
                    settings.TypeNameHandling = TypeNameHandling.Objects;
                }));

            var client = new ElasticClient(connectionSettings);
        }

        /**
         * Here, the JSON.Net serializer is configured to *always* serialize `null` values and
         * include the .NET type name when serializing to a JSON object structure.
         *
         * ==== Modifying settings using a custom ISerializerFactory
         *
         * If you need more control than passing a delegate to `SerializerFactory` provides, you can also
         * implement your own `ISerializerFactory` and derive an `IElasticsearchSerializer` from the
         * default `JsonNetSerializer`.
         *
         * Here's an example of doing so that effectively achieves the same configuration as in the previous example.
         * First, the custom factory and serializer are implemented
         */
        public class CustomJsonNetSerializerFactory : ISerializerFactory
        {
            public IElasticsearchSerializer Create(IConnectionSettingsValues settings)
            {
                return new CustomJsonNetSerializer(settings);
            }
            public IElasticsearchSerializer CreateStateful(IConnectionSettingsValues settings, JsonConverter converter)
            {
                return new CustomJsonNetSerializer(settings, converter);
            }
        }

        public class CustomJsonNetSerializer : JsonNetSerializer
        {
            public CustomJsonNetSerializer(IConnectionSettingsValues settings) : base(settings)
            {
                base.OverwriteDefaultSerializers(ModifyJsonSerializerSettings);
            }
            public CustomJsonNetSerializer(IConnectionSettingsValues settings, JsonConverter statefulConverter) :
                base(settings, statefulConverter)
            {
                base.OverwriteDefaultSerializers(ModifyJsonSerializerSettings);
            }

            private void ModifyJsonSerializerSettings(JsonSerializerSettings settings, IConnectionSettingsValues connectionSettings)
            {
                settings.NullValueHandling = NullValueHandling.Include;
                settings.TypeNameHandling = TypeNameHandling.Objects;
            }
        }

        /**
         * Then, create a new instance of the factory to `ConnectionSettings`
         */
        public void ModifyingJsonNetSettingsWithCustomSerializer()
        {
            var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
            var connection = new HttpConnection();
            var connectionSettings =
                new ConnectionSettings(pool, connection, new CustomJsonNetSerializerFactory());

            var client = new ElasticClient(connectionSettings);
        }

        /**[IMPORTANT]
         * ====
         * Any custom serializer that derives from `JsonNetSerializer` wishing to change the settings for the JSON.Net
         * serializer, must do so using the `OverwriteDefaultSerializers` method in the constructor of the derived
         * serializer.
         *
         * NEST includes many custom changes to the http://www.newtonsoft.com/json/help/html/ContractResolver.htm[`IContractResolver`] that the JSON.Net serializer uses to resolve
         * serialization contracts for types. Examples of such changes are:
         *
         * - Allowing contracts for concrete types to be _inherited_ from interfaces that they implement
         * - Special handling of dictionaries to ensure dictionary keys are serialized verbatim
         * - Explicitly implemented interface properties are serialized in requests
         *
         * It's important therefore that these changes to `IContractResolver` are not overwritten by a serializer derived
         * from `JsonNetSerializer`.
         * ====
         */

         /** ==== Adding contract JsonConverters
         *
         * If you want to register custom json converters without attributing your classes you can register
         * Functions that given a type return a JsonConverter. This is cached as part of the types json contract so once
         * Json.NET knows a type has a certain converter it won't ask anymore for the duration of the application.
         *
         * Override `ContractConverters` getter property and have it return a list of these functions
         */
        public class CustomContractsJsonNetSerializer : CustomJsonNetSerializer
        {
            public CustomContractsJsonNetSerializer(IConnectionSettingsValues settings) : base(settings) { }
            public CustomContractsJsonNetSerializer(IConnectionSettingsValues settings, JsonConverter statefulConverter)
	            : base(settings, statefulConverter) { }

	        protected override IList<Func<Type, JsonConverter>> ContractConverters { get; } = new List<Func<Type, JsonConverter>>
	        {
		        ((t) => t == typeof(Project) ? new MyCustomJsonConverter() : null)
	        };
        }

	    public class MyCustomJsonConverter : JsonConverter
	    {
		    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) { }

		    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) => null;

		    public override bool CanConvert(Type objectType) => false;
	    }
    }
}

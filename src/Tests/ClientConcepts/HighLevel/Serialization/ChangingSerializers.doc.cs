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

namespace Tests.ClientConcepts.HighLevel.Serialization
{
    /**[[changing-serializers]]
     * === Changing serializers
     *
     * NEST uses http://www.newtonsoft.com/json[JSON.Net] to serialize requests to and deserialize responses from JSON.
     *
     * Whilst JSON.Net does a good job of serialization, you may wish to use your own JSON serializer for a particular
     * reason. Elasticsearch.Net and NEST make it easy to replace the default serializer with your own.
     *
     * [NOTE]
     * --
     * If you are looking to change how the default serializer works, check out
     * <<modifying-default-serializer,Modifying the default serializer>>.
     * --
     */
    public class ChangingSerializers
    {
        /**
         * The main component needed is to provide an implementation of `IElasticsearchSerializer`
		 */
        public class CustomSerializer : IElasticsearchSerializer
        {
            public T Deserialize<T>(Stream stream)
            {
                // provide deserialization implementation
                throw new NotImplementedException();
            }

            public Task<T> DeserializeAsync<T>(Stream responseStream, CancellationToken cancellationToken = default(CancellationToken))
            {
                // provide an asynchronous deserialization implementation
                throw new NotImplementedException();
            }

            public void Serialize(object data, Stream writableStream, SerializationFormatting formatting = SerializationFormatting.Indented)
            {
                // provide a serialization implementation
                throw new NotImplementedException();
            }

            public IPropertyMapping CreatePropertyMapping(MemberInfo memberInfo)
            {
                // provide an implementation, if the serializer can decide how properties should be mapped.
				// Otherwise return null.
                return null;
            }
        }

        /**==== Changing serializers in Elasticsearch.Net
         *
         * For Elasticsearch.Net, an implementation of `IElasticsearchSerializer` is all that is needed and a delegate can
		 * be passed to `ConnectionConfiguration` that will be called to construct an instance of the serializer
         */
        public void ConnectionConfigurationWithCustomSerializer()
        {
            var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
            var connection = new HttpConnection();
            var connectionConfiguration =
                new ConnectionConfiguration(pool, connection, configuration => new CustomSerializer()); // <1> delegate gets passed `ConnectionConfiguration` and creates a serializer.

			var lowlevelClient = new ElasticLowLevelClient(connectionConfiguration);
        }

        /**==== Changing serializers in NEST
         *
         * With NEST however, an implementation of `ISerializerFactory` in addition to an implementation
         * of `IElasticsearchSerializer` is required.
         */
        public class CustomSerializerFactory : ISerializerFactory
        {
            public IElasticsearchSerializer Create(IConnectionSettingsValues settings) => new CustomSerializer();

            public IElasticsearchSerializer CreateStateful(IConnectionSettingsValues settings, JsonConverter converter) =>
                new CustomSerializer();
        }

        /**
         * With an implementation of `ISerializerFactory` that can create instances of our custom serializer,
         * hooking this into `ConnectionSettings` is straightfoward
         */
        public void ConnectionSettingsWithCustomSerializer()
        {
            var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
            var connection = new HttpConnection();
            var connectionSettings =
                new ConnectionSettings(pool, connection, new CustomSerializerFactory());

            var client = new ElasticClient(connectionSettings);
        }

        /**[IMPORTANT]
         * --
         * The implementation for how custom serialization is configured within the client is subject to
         * change in the next major release. NEST relies heavily on stateful deserializers that have access to details
         * from the original request for specialized features such a covariant search results.
         *
         * You may have noticed that this requirement leaks into the `ISerializerFactory` abstraction in the form of
         * the `CreateStateful` method signature. There are intentions to replace or at least internalize the usage of
         * JSON.Net within NEST in the future and in the process, simplifying how custom serialization can
         * be integrated.
         * --
         *
         * This has provided you details on how to implement your own custom serialization, but a much more common scenario
         * amongst NEST client users is the desire to change the serialization settings of the default JSON.Net serializer.
         * Take a look at <<modifying-default-serializer, modifying the default serializer>> to see how this can be done.
         */
    }
}

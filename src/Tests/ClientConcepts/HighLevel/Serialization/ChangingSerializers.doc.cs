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
                new ConnectionConfiguration(pool, connection, new CustomSerializer()); // <1> delegate gets passed `ConnectionConfiguration` and creates a serializer.

			var lowlevelClient = new ElasticLowLevelClient(connectionConfiguration);
        }

    }
}

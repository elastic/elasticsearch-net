using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Elasticsearch.Net.Serialization;

namespace Nest.SerializationExtensions
{
	public static class SerializerExtensions
	{
		/// <summary>
		/// This is a convenience method to deserialize to T using the registered deserializer. 
		/// <para>NOTE:</para> If you want to deserialize to a NEST response you need to use the overload that 
		/// takes an ElasticsearchResponse
		/// </summary>
		/// <typeparam name="T">The type to deserialize to</typeparam>
		/// <param name="serializer"></param>
		/// <param name="data">The string representation of the data to be deserialized</param>
		public static T Deserialize<T>(this IElasticClient client, string data)
		{
			using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(data)))
				return client.Serializer.Deserialize<T>(null, ms, null);
		}
		
	}
}

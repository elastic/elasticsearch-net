using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net.Serialization;
using ServiceStack.Text;

namespace Elasticsearch.Net.ServiceStackText
{
	public class ElasticsearchServiceStackSerializer : IElasticsearchSerializer
	{
		static ElasticsearchServiceStackSerializer()
		{
			JsConfig.TryToParsePrimitiveTypeValues = true;
			JsConfig.ConvertObjectTypesIntoStringDictionary = true;
			JsConfig.ExcludeTypeInfo = true;
			JsConfig.TryToParseNumericType = true;
			//JsConfig.TypeI = true;
		}

		public T Deserialize<T>(byte[] bytes) where T : class
		{
			using (var stream = new MemoryStream(bytes))
				return JsonSerializer.DeserializeFromStream<T>(stream);
		}

		public byte[] Serialize(object data, SerializationFormatting formatting = SerializationFormatting.Indented)
		{

			using (var stream = new MemoryStream())
			{
				JsonSerializer.SerializeToStream(data, stream);
				if (formatting == SerializationFormatting.Indented)
					return stream.ToArray();
				return RemoveNewLinesAndTabs(stream.ToArray());
			}
		}

		public static byte[] RemoveNewLinesAndTabs(byte[] input)
		{
			return input
				.Where(c => c != (byte)'\r' && c != (byte)'\n')
				.ToArray();
		}
	}
}

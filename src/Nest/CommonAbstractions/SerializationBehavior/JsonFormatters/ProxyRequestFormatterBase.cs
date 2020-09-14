// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Reflection;
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	internal abstract class ProxyRequestFormatterBase<TRequestInterface, TRequest> : IJsonFormatter<TRequestInterface>
		where TRequestInterface : IProxyRequest
		where TRequest : TRequestInterface
	{
		public TRequestInterface Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			// TODO: Look at optimizing this. It looks like this could be replaced with SourceFormatter<T> on Document and a serialization ctor
			var segment = reader.ReadNextBlockSegment();
			var settings = formatterResolver.GetConnectionSettings();
			using (var ms = settings.MemoryStreamFactory.Create(segment.Array, segment.Offset, segment.Count))
			{
				//not optimized but deserializing create requests is far from common practice
				var genericType = typeof(TRequest).GenericTypeArguments[0];
				var o = settings.SourceSerializer.Deserialize(genericType, ms);

				// TRequest might be an open or closed generic type
				var request = typeof(TRequest).IsGenericTypeDefinition
					? (TRequest)typeof(TRequest).CreateGenericInstance(genericType, o, null, null)
					: (TRequest)typeof(TRequest).CreateInstance(o, null, null);

				return request;
			}
		}

		public void Serialize(ref JsonWriter writer, TRequestInterface value, IJsonFormatterResolver formatterResolver)
		{
			var untypedDocumentRequest = (IProxyRequest)value;
			var settings = formatterResolver.GetConnectionSettings();
			var serializer = settings.SourceSerializer;

			using (var ms = settings.MemoryStreamFactory.Create())
			{
				untypedDocumentRequest.WriteJson(serializer, ms, SerializationFormatting.None);
				writer.WriteRaw(ms);
			}
		}
	}
}

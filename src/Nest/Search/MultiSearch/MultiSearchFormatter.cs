using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;
using Elasticsearch.Net.Utf8Json.Resolvers;

namespace Nest
{
	internal class MultiSearchFormatter : IJsonFormatter<IMultiSearchRequest>
	{
		private const byte Newline = (byte)'\n';

		public IMultiSearchRequest Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver) =>
			DynamicObjectResolver.ExcludeNullCamelCase.GetFormatter<IMultiSearchRequest>().Deserialize(ref reader, formatterResolver);

		public void Serialize(ref JsonWriter writer, IMultiSearchRequest value, IJsonFormatterResolver formatterResolver)
		{
			if (value?.Operations == null)
				return;

			var settings = formatterResolver.GetConnectionSettings();
			var memoryStreamFactory = settings.MemoryStreamFactory;
			var serializer = settings.RequestResponseSerializer;

			foreach (var operation in value.Operations.Values)
			{
				var p = operation.RequestParameters;

				string GetString(string key) => p.GetResolvedQueryStringValue(key, settings);

				IUrlParameter indices = value.Index == null || !value.Index.Equals(operation.Index)
					? operation.Index
					: null;
				var operationIndex = indices?.GetString(settings);

				var searchType = GetString("search_type");
				if (searchType == "query_then_fetch")
					searchType = null;

				var header = new
				{
					index = operationIndex != value.Index ? operationIndex : null,
					search_type = searchType,
					preference = GetString("preference"),
					routing = GetString("routing"),
					ignore_unavailable = GetString("ignore_unavailable")
				};

				var headerBytes = serializer.SerializeToBytes(header, memoryStreamFactory, SerializationFormatting.None);
				writer.WriteRaw(headerBytes);
				writer.WriteRaw(Newline);
				var bodyBytes = serializer.SerializeToBytes(operation, memoryStreamFactory, SerializationFormatting.None);
				writer.WriteRaw(bodyBytes);
				writer.WriteRaw(Newline);
			}
		}
	}
}

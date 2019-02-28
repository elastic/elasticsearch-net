using Elasticsearch.Net;

namespace Nest
{
	internal class MultiSearchTemplateFormatter : IJsonFormatter<IMultiSearchTemplateRequest>
	{
		private const byte Newline = (byte)'\n';

		public IMultiSearchTemplateRequest Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver) =>
			formatterResolver.GetFormatter<MultiSearchTemplateRequest>().Deserialize(ref reader, formatterResolver);

		public void Serialize(ref JsonWriter writer, IMultiSearchTemplateRequest value, IJsonFormatterResolver formatterResolver)
		{
			if (value?.Operations == null) return;

			var settings = formatterResolver.GetConnectionSettings();
			var serializer = settings.RequestResponseSerializer;

			foreach (var operation in value.Operations.Values)
			{
				var p = operation.RequestParameters;

				string GetString(string key)
				{
					return p.GetResolvedQueryStringValue(key, settings);
				}

				IUrlParameter indices = value.Index == null || !value.Index.Equals(operation.Index)
					? operation.Index
					: null;

				var searchType = GetString("search_type");
				if (searchType == "query_then_fetch")
					searchType = null;

				var header = new
				{
					index = indices?.GetString(settings),
					search_type = searchType,
					preference = GetString("preference"),
					routing = GetString("routing"),
					ignore_unavailable = GetString("ignore_unavailable")
				};

				var headerBytes = serializer.SerializeToBytes(header, SerializationFormatting.None);
				writer.WriteRaw(headerBytes);
				writer.WriteRaw(Newline);
				var bodyBytes = serializer.SerializeToBytes(operation, SerializationFormatting.None);
				writer.WriteRaw(bodyBytes);
				writer.WriteRaw(Newline);
			}
		}
	}
}

using Utf8Json;

namespace Elasticsearch.Net
{
	public static class JsonFormatterResolverExtensions
	{
		internal static T ReserializeAndDeserialize<T>(this IJsonFormatterResolver formatterResolver, object value)
		{
			// TODO: Use ArrayPool or MemoryStreamFactory.Create() for byte array / stream
			var bytes = JsonSerializer.Serialize(value);
			var formatter = formatterResolver.GetFormatter<T>();
			var reader = new JsonReader(bytes);
			return formatter.Deserialize(ref reader, formatterResolver);
		}
	}
}

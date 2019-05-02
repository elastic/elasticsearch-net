using Elasticsearch.Net;

namespace Nest
{
	internal class ConcreteBulkIndexResponseItemFormatter<T> : IJsonFormatter<T>
		where T : BulkResponseItemBase
	{
		public void Serialize(ref JsonWriter writer, T value, IJsonFormatterResolver formatterResolver) =>
			throw new System.NotImplementedException();

		public T Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var formatter = DynamicObjectResolver.AllowPrivateExcludeNullCamelCase.GetFormatter<T>();
			return formatter.Deserialize(ref reader, formatterResolver);
		}
	}
}

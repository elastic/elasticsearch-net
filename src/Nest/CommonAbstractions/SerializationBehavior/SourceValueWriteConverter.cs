using Elasticsearch.Net;

namespace Nest
{
	internal class SourceWriteFormatter<T> : SourceFormatter<T>
	{
		public override void Serialize(ref JsonWriter writer, T value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			var nestType = value.GetType().Assembly() == typeof(SourceWriteFormatter<>).Assembly();
			if (nestType)
				formatterResolver.GetFormatter<T>().Serialize(ref writer, value, formatterResolver);
			else
				base.Serialize(ref writer, value, formatterResolver);
		}
	}
}

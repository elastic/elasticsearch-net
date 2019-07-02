using Elasticsearch.Net.Utf8Json;

namespace Elasticsearch.Net 
{
	internal interface IInternalSerializerWithFormatter
	{
		IJsonFormatterResolver FormatterResolver { get; }
	}
}
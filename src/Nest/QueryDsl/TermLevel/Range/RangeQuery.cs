using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[JsonFormatter(typeof(RangeQueryFormatter))]
	public interface IRangeQuery : IFieldNameQuery { }
}

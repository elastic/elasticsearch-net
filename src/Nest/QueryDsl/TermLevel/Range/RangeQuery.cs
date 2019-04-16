using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	[JsonFormatter(typeof(RangeQueryFormatter))]
	public interface IRangeQuery : IFieldNameQuery { }
}

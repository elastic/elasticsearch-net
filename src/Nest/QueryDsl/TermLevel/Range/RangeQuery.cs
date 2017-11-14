using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof (RangeQueryJsonConverter))]
	public interface IRangeQuery : IFieldNameQuery { }
}

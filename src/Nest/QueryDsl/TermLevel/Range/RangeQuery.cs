using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	[JsonConverter(typeof(RangeQueryJsonConverter))]
	public interface IRangeQuery : IFieldNameQuery { }
}

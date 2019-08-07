using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	/// <summary>
	/// For numeric fields it is also possible to cast the values from one type to another using this option. This can be useful for cross-index
	/// search if the sort field is mapped differently on some indices.
	/// </summary>
	[StringEnum]
	public enum NumericType
	{
		[EnumMember(Value = "long")]
		Long,

		[EnumMember(Value = "double")]
		Double,

		[EnumMember(Value = "date")]
		Date,

		[EnumMember(Value = "date_nanos")]
		DateNanos
	}
}

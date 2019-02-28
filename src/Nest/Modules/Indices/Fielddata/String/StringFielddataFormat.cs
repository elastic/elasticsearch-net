using System.Runtime.Serialization;
using Elasticsearch.Net;


namespace Nest
{
	[StringEnum]
	public enum StringFielddataFormat
	{
		[EnumMember(Value = "paged_bytes")]
		PagedBytes,

		[EnumMember(Value = "disabled")]
		Disabled
	}
}

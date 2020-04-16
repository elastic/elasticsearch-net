using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[StringEnum]
	public enum TransformType
	{
		[EnumMember(Value = "batch")]
		Batch,

		[EnumMember(Value = "continuous")]
		Continuous
	}
}

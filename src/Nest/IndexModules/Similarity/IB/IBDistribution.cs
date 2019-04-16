using System.Runtime.Serialization;
using Elasticsearch.Net;


namespace Nest
{
	[StringEnum]
	public enum IBDistribution
	{
		[EnumMember(Value = "ll")]
		LogLogistic,

		[EnumMember(Value = "spl")]
		SmoothPowerLaw,
	}
}

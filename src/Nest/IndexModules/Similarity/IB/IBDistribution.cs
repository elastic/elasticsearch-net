using System.Runtime.Serialization;
using Elasticsearch.Net;


namespace Nest
{
	[StringEnum]
	// ReSharper disable once InconsistentNaming
	public enum IBDistribution
	{
		[EnumMember(Value = "ll")]
		LogLogistic,

		[EnumMember(Value = "spl")]
		SmoothPowerLaw,
	}
}

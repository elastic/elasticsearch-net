using System.Runtime.Serialization;
using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

namespace Nest
{

	public enum IBDistribution
	{
		[EnumMember(Value = "ll")]
		LogLogistic,

		[EnumMember(Value = "spl")]
		SmoothPowerLaw,
	}
}

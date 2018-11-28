using System.Runtime.Serialization;
using System.Runtime.Serialization;


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

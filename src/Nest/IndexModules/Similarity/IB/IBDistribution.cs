using System.Runtime.Serialization;


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

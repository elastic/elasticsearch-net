using System.Runtime.Serialization;
using System.Runtime.Serialization;


namespace Nest
{
	/// <summary>
	/// <see cref="IDFISimilarity" /> independence measure
	/// </summary>

	public enum DFIIndependenceMeasure
	{
		[EnumMember(Value = "standardized")]
		Standardized,

		[EnumMember(Value = "saturated")]
		Saturated,

		[EnumMember(Value = "chisquared")]
		ChiSquared
	}
}

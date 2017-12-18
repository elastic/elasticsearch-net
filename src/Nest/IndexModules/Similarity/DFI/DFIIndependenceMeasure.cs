using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	/// <summary>
	/// <see cref="IDFISimilarity"/> independence measure
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
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

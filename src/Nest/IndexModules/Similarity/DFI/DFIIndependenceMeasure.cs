using System.Runtime.Serialization;
using Elasticsearch.Net;


namespace Nest
{
	/// <summary>
	/// <see cref="IDFISimilarity" /> independence measure
	/// </summary>
	[StringEnum]
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

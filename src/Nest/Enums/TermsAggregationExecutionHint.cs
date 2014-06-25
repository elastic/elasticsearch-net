using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// Determines how the terms aggregation is executed
	/// </summary>
	public enum TermsAggregationExecutionHint
	{
		/// <summary>
		/// Order by using field values directly in order to aggregate data per-bucket 
		/// </summary>
		[EnumMember(Value = "map")]
		Map,
		/// <summary>
		/// Order by using ordinals of the field values instead of the values themselves
		/// </summary>
		[EnumMember(Value = "ordinals")]
		Ordinals
	}
}
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Cause for the anomaly that has been identified for the over field.
	/// </summary>
	[JsonObject]
	public class AnomalyCause
	{
		[JsonProperty("probability")]
		public double Probability { get; internal set; }

		[JsonProperty("over_field_name")]
		public string OverFieldName  { get; internal set; }

		[JsonProperty("over_field_value")]
		public string OverFieldValue  { get; internal set; }

		[JsonProperty("by_field_name")]
		public string ByFieldName  { get; internal set; }

		[JsonProperty("by_field_value")]
		public string ByFieldValue  { get; internal set; }

		[JsonProperty("correlated_by_field_value")]
		public string CorrelatedByFieldValue  { get; internal set; }

		[JsonProperty("partition_field_name")]
		public string PartitionFieldName  { get; internal set; }

		[JsonProperty("partition_field_value")]
		public string PartitionFieldValue  { get; internal set; }

		[JsonProperty("function")]
		public string Function  { get; internal set; }

		[JsonProperty("function_description")]
		public string FunctionDescription { get; internal set; }

		[JsonProperty("typical")]
		public IReadOnlyCollection<double> Typical { get; internal set; } = EmptyReadOnly<double>.Collection;

		[JsonProperty("actual")]
		public IReadOnlyCollection<double> Actual { get; internal set; } = EmptyReadOnly<double>.Collection;

		[JsonProperty("influencers")]
		public IReadOnlyCollection<Influence> Influencers { get; internal set; } = EmptyReadOnly<Influence>.Collection;

		[JsonProperty("field_name")]
		public string FieldName  { get; internal set; }
	}
}

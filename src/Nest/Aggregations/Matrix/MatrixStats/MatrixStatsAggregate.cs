using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class MatrixStatsField
	{
		[JsonProperty("correlation")]
		public Dictionary<string, double> Correlation { get; set; }

		[JsonProperty("count")]
		public int Count { get; set; }

		[JsonProperty("covariance")]
		public Dictionary<string, double> Covariance { get; set; }

		[JsonProperty("kurtosis")]
		public double Kurtosis { get; set; }

		[JsonProperty("mean")]
		public double Mean { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("skewness")]
		public double Skewness { get; set; }

		[JsonProperty("variance")]
		public double Variance { get; set; }
	}

	[JsonObject]
	public class MatrixStatsAggregate : MatrixAggregateBase
	{
		//TODO non nullable in 6.0
		[JsonProperty("fields")]
		public long? DocCount { get; set; }

		[JsonProperty("fields")]
		public List<MatrixStatsField> Fields { get; set; }
	}
}

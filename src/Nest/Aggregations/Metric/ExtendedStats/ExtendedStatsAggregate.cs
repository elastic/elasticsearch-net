// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	public class ExtendedStatsAggregate : StatsAggregate
	{
		/// <summary>
		/// The standard deviation of the collected values
		/// </summary>
		public double? StdDeviation { get; set; }

		/// <summary>
		/// The upper or lower bounds of standard deviation
		/// </summary>
		public StandardDeviationBounds StdDeviationBounds { get; set; }

		/// <summary>
		/// The sum of squares of the collected values
		/// </summary>
		public double? SumOfSquares { get; set; }

		/// <summary>
		/// The variance of the collected values
		/// </summary>
		public double? Variance { get; set; }

		/// <summary>
		/// The population variance of the collected values.
		/// <para />
		/// Valid in Elasticsearch 7.9.0+
		/// </summary>
		public double? VariancePopulation { get; set; }

		/// <summary>
		/// The sampling variance of the collected values.
		/// <para />
		/// Valid in Elasticsearch 7.9.0+
		/// </summary>
		public double? VarianceSampling { get; set; }

		/// <summary>
		/// The population standard deviation of the collected values.
		/// <para />
		/// Valid in Elasticsearch 7.9.0+
		/// </summary>
		public double? StdDeviationPopulation { get; set; }

		/// <summary>
		/// The sampling standard deviation of the collected values.
		/// <para />
		/// Valid in Elasticsearch 7.9.0+
		/// </summary>
		public double? StdDeviationSampling { get; set; }
	}

	[DataContract]
	public class StandardDeviationBounds
	{
		[DataMember(Name = "lower")]
		public double? Lower { get; set; }

		[DataMember(Name = "upper")]
		public double? Upper { get; set; }

		[DataMember(Name = "lower_population")]
		public double? LowerPopulation { get; set; }

		[DataMember(Name = "upper_population")]
		public double? UpperPopulation { get; set; }

		[DataMember(Name = "lower_sampling")]
		public double? LowerSampling { get; set; }

		[DataMember(Name = "upper_sampling")]
		public double? UpperSampling { get; set; }
	}
}

// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;

namespace Nest
{
	public class StringStatsAggregate : MetricAggregateBase
	{
		/// <summary>
		/// The average length computed over all terms.
		/// </summary>
		public double AverageLength { get; set; }

		/// <summary>
		/// The number of non-empty fields counted.
		/// </summary>
		public long Count { get; set; }

		/// <summary>
		/// The length of the longest term.
		/// </summary>
		public int MaxLength { get; set; }

		/// <summary>
		/// The length of the shortest term.
		/// </summary>
		public int MinLength { get; set; }

		/// <summary>
		/// The Shannon Entropy value computed over all terms collected by the aggregation.
		/// Shannon entropy quantifies the amount of information contained in the field.
		/// It is a very useful metric for measuring a wide range of properties of a data set, such as diversity, similarity, randomness etc.
		/// </summary>
		public double Entropy { get; set; }

		/// <summary>
		/// The probability of each character appearing in all terms.
		/// </summary>
		public IReadOnlyDictionary<string, double> Distribution { get; set; } = EmptyReadOnly<string, double>.Dictionary;
	}
}

/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System.Runtime.Serialization;
using Nest.Utf8Json;

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
		[JsonFormatter(typeof(NullableStringDoubleFormatter))]
		public double? LowerSampling { get; set; }

		[DataMember(Name = "upper_sampling")]
		[JsonFormatter(typeof(NullableStringDoubleFormatter))]
		public double? UpperSampling { get; set; }
	}
}

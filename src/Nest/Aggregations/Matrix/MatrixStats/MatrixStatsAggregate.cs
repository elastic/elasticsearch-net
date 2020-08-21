// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class MatrixStatsField
	{
		[DataMember(Name = "correlation")]
		public Dictionary<string, double> Correlation { get; set; }

		[DataMember(Name = "count")]
		public int Count { get; set; }

		[DataMember(Name = "covariance")]
		public Dictionary<string, double> Covariance { get; set; }

		[DataMember(Name = "kurtosis")]
		public double Kurtosis { get; set; }

		[DataMember(Name = "mean")]
		public double Mean { get; set; }

		[DataMember(Name = "name")]
		public string Name { get; set; }

		[DataMember(Name = "skewness")]
		public double Skewness { get; set; }

		[DataMember(Name = "variance")]
		public double Variance { get; set; }
	}

	[DataContract]
	public class MatrixStatsAggregate : MatrixAggregateBase
	{
		[DataMember(Name = "doc_count")]
		public long DocCount { get; set; }

		[DataMember(Name = "fields")]
		public List<MatrixStatsField> Fields { get; set; }
	}
}

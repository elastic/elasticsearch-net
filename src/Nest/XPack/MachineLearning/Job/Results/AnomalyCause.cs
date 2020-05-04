// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	/// <summary>
	/// Cause for the anomaly that has been identified for the over field.
	/// </summary>
	[DataContract]
	public class AnomalyCause
	{
		[DataMember(Name ="actual")]
		public IReadOnlyCollection<double> Actual { get; internal set; } = EmptyReadOnly<double>.Collection;

		[DataMember(Name ="by_field_name")]
		public string ByFieldName { get; internal set; }

		[DataMember(Name ="by_field_value")]
		public string ByFieldValue { get; internal set; }

		[DataMember(Name ="correlated_by_field_value")]
		public string CorrelatedByFieldValue { get; internal set; }

		[DataMember(Name ="field_name")]
		public string FieldName { get; internal set; }

		[DataMember(Name ="function")]
		public string Function { get; internal set; }

		[DataMember(Name ="function_description")]
		public string FunctionDescription { get; internal set; }

		[DataMember(Name ="influencers")]
		public IReadOnlyCollection<Influence> Influencers { get; internal set; } = EmptyReadOnly<Influence>.Collection;

		[DataMember(Name ="over_field_name")]
		public string OverFieldName { get; internal set; }

		[DataMember(Name ="over_field_value")]
		public string OverFieldValue { get; internal set; }

		[DataMember(Name ="partition_field_name")]
		public string PartitionFieldName { get; internal set; }

		[DataMember(Name ="partition_field_value")]
		public string PartitionFieldValue { get; internal set; }

		[DataMember(Name ="probability")]
		public double Probability { get; internal set; }

		[DataMember(Name ="typical")]
		public IReadOnlyCollection<double> Typical { get; internal set; } = EmptyReadOnly<double>.Collection;
	}
}

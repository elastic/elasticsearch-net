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

using System.Collections.Generic;
using System.Runtime.Serialization;

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

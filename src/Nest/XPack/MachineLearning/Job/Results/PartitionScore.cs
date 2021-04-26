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

namespace Nest
{
	[DataContract]
	public class PartitionScore
	{
		/// <summary>
		/// A normalized score between 0-100, which is based on the probability of the anomalousness of this record. This is the initial value that was
		/// calculated at the time the bucket was processed.
		/// </summary>
		[DataMember(Name ="initial_record_score")]
		public double InitialRecordScore { get; internal set; }

		/// <summary>
		/// The name of the partition field that was used in the analysis. This value is present only if it was specified in the detector.
		/// </summary>
		[DataMember(Name ="partition_field_name")]
		public string PartitionFieldName { get; internal set; }

		/// <summary>
		/// The value of partition_field_name. This value is present only if it was specified in the detector.
		/// </summary>
		[DataMember(Name ="partition_field_value")]
		public string PartitionFieldValue { get; internal set; }

		/// <summary>
		/// The probability of the individual anomaly occurring, in the range 0 to 1. This value can be held to a high precision of over 300 decimal
		/// places.
		/// </summary>
		[DataMember(Name ="probability")]
		public double Probability { get; internal set; }

		/// <summary>
		/// A normalized score between 0-100, which is based on the probability of the anomalousness of this record. Unlike initial_record_score, this
		/// value will be updated by a re-normalization process as new data is analyzed.
		/// </summary>
		[DataMember(Name ="record_score")]
		public double RecordScore { get; internal set; }
	}
}

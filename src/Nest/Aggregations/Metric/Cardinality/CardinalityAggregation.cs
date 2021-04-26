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
	[InterfaceDataContract]
	[ReadAs(typeof(CardinalityAggregation))]
	public interface ICardinalityAggregation : IMetricAggregation
	{
		[DataMember(Name ="precision_threshold")]
		int? PrecisionThreshold { get; set; }

		[DataMember(Name ="rehash")]
		bool? Rehash { get; set; }
	}

	public class CardinalityAggregation : MetricAggregationBase, ICardinalityAggregation
	{
		internal CardinalityAggregation() { }

		public CardinalityAggregation(string name, Field field) : base(name, field) { }

		public int? PrecisionThreshold { get; set; }
		public bool? Rehash { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.Cardinality = this;
	}

	public class CardinalityAggregationDescriptor<T>
		: MetricAggregationDescriptorBase<CardinalityAggregationDescriptor<T>, ICardinalityAggregation, T>
			, ICardinalityAggregation
		where T : class
	{
		int? ICardinalityAggregation.PrecisionThreshold { get; set; }

		bool? ICardinalityAggregation.Rehash { get; set; }

		public CardinalityAggregationDescriptor<T> PrecisionThreshold(int? precisionThreshold)
			=> Assign(precisionThreshold, (a, v) => a.PrecisionThreshold = v);

		public CardinalityAggregationDescriptor<T> Rehash(bool? rehash = true) => Assign(rehash, (a, v) => a.Rehash = v);
	}
}

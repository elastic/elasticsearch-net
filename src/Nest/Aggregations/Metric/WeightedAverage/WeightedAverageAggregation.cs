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

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(WeightedAverageAggregation))]
	public interface IWeightedAverageAggregation : IAggregation
	{
		/// <summary> The optional numeric response formatter</summary>
		[DataMember(Name ="format")]
		string Format { get; set; }

		/// <summary> The configuration for the field or script that provides the values</summary>
		[DataMember(Name ="value")]
		IWeightedAverageValue Value { get; set; }

		/// <summary> A hint about the values for pure scripts or unmapped fields </summary>
		[DataMember(Name ="value_type")]
		ValueType? ValueType { get; set; }

		/// <summary> The configuration for the field or script that provides the weights</summary>
		[DataMember(Name ="weight")]
		IWeightedAverageValue Weight { get; set; }
	}

	public class WeightedAverageAggregation : AggregationBase, IWeightedAverageAggregation
	{
		internal WeightedAverageAggregation() { }

		public WeightedAverageAggregation(string name) : base(name) { }

		/// <inheritdoc cref="IWeightedAverageAggregation.Format" />
		public string Format { get; set; }

		/// <inheritdoc cref="IWeightedAverageAggregation.Value" />
		public IWeightedAverageValue Value { get; set; }

		/// <inheritdoc cref="IWeightedAverageAggregation.ValueType" />
		public ValueType? ValueType { get; set; }

		/// <inheritdoc cref="IWeightedAverageAggregation.Weight" />
		public IWeightedAverageValue Weight { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.WeightedAverage = this;
	}

	public class WeightedAverageAggregationDescriptor<T>
		: DescriptorBase<WeightedAverageAggregationDescriptor<T>, IWeightedAverageAggregation>
			, IWeightedAverageAggregation
		where T : class
	{
		string IWeightedAverageAggregation.Format { get; set; }
		IDictionary<string, object> IAggregation.Meta { get; set; }
		string IAggregation.Name { get; set; }
		IWeightedAverageValue IWeightedAverageAggregation.Value { get; set; }
		ValueType? IWeightedAverageAggregation.ValueType { get; set; }
		IWeightedAverageValue IWeightedAverageAggregation.Weight { get; set; }

		/// <inheritdoc cref="IAggregation.Meta" />
		public WeightedAverageAggregationDescriptor<T> Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector) =>
			Assign(selector, (a, v) => a.Meta = v?.Invoke(new FluentDictionary<string, object>()));

		/// <inheritdoc cref="IWeightedAverageAggregation.Value" />
		public WeightedAverageAggregationDescriptor<T> Value(Func<WeightedAverageValueDescriptor<T>, IWeightedAverageValue> selector) =>
			Assign(selector, (a, v) => a.Value = v?.Invoke(new WeightedAverageValueDescriptor<T>()));

		/// <inheritdoc cref="IWeightedAverageAggregation.Weight" />
		public WeightedAverageAggregationDescriptor<T> Weight(Func<WeightedAverageValueDescriptor<T>, IWeightedAverageValue> selector) =>
			Assign(selector, (a, v) => a.Weight = v?.Invoke(new WeightedAverageValueDescriptor<T>()));

		/// <inheritdoc cref="IWeightedAverageAggregation.Format" />
		public WeightedAverageAggregationDescriptor<T> Format(string format) => Assign(format, (a, v) => a.Format = v);

		/// <inheritdoc cref="IWeightedAverageAggregation.ValueType" />
		public WeightedAverageAggregationDescriptor<T> ValueType(ValueType? valueType) => Assign(valueType, (a, v) => a.ValueType = v);
	}
}

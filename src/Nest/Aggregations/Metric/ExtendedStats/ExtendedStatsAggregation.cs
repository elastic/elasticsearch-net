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
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(ExtendedStatsAggregation))]
	public interface IExtendedStatsAggregation : IFormattableMetricAggregation
	{
		[DataMember(Name ="sigma")]
		double? Sigma { get; set; }
	}

	public class ExtendedStatsAggregation : FormattableMetricAggregationBase, IExtendedStatsAggregation
	{
		internal ExtendedStatsAggregation() { }

		public ExtendedStatsAggregation(string name, Field field) : base(name, field) { }

		public double? Sigma { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.ExtendedStats = this;
	}

	public class ExtendedStatsAggregationDescriptor<T>
		: FormattableMetricAggregationDescriptorBase<ExtendedStatsAggregationDescriptor<T>, IExtendedStatsAggregation, T>
			, IExtendedStatsAggregation
		where T : class
	{
		double? IExtendedStatsAggregation.Sigma { get; set; }

		public ExtendedStatsAggregationDescriptor<T> Sigma(double? sigma) =>
			Assign(sigma, (a, v) => a.Sigma = v);
	}
}

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
	/// <summary>
	/// The histogram value source can be applied on numeric values to build fixed size interval over the values.
	/// </summary>
	public interface IHistogramGroupSource : ISingleGroupSource
	{
		/// <summary>
		/// The interval to use when bucketing documents
		/// </summary>
		[DataMember(Name ="interval")]
		double? Interval { get; set; }
	}

	/// <inheritdoc cref="IHistogramGroupSource"/>
	public class HistogramGroupSource : SingleGroupSourceBase, IHistogramGroupSource
	{
		/// <inheritdoc cref="IHistogramGroupSource.Interval"/>
		public double? Interval { get; set; }
	}

	/// <inheritdoc cref="IHistogramGroupSource"/>
	public class HistogramGroupSourceDescriptor<T>
		: SingleGroupSourceDescriptorBase<HistogramGroupSourceDescriptor<T>, IHistogramGroupSource, T>,
			IHistogramGroupSource
	{
		double? IHistogramGroupSource.Interval { get; set; }

		/// <inheritdoc cref="IHistogramGroupSource.Interval"/>
		public HistogramGroupSourceDescriptor<T> Interval(double? interval) => Assign(interval, (a, v) => a.Interval = v);
	}
}

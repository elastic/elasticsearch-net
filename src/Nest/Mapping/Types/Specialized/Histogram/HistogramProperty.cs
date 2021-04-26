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

using System.Diagnostics;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A field to store pre-aggregated numerical data representing a histogram.
	/// <para />
	/// Available in Elasticsearch 7.6.0+ with at least basic license level
	/// </summary>
	[InterfaceDataContract]
	public interface IHistogramProperty : IProperty
	{
		/// <summary>
		/// Whether to ignore malformed input values.
		/// </summary>
		[DataMember(Name = "ignore_malformed")]
		bool? IgnoreMalformed { get; set; }
	}

	/// <inheritdoc cref="IHistogramProperty"/>
	[DebuggerDisplay("{DebugDisplay}")]
	public class HistogramProperty : PropertyBase, IHistogramProperty
	{
		public HistogramProperty() : base(FieldType.Histogram) { }

		/// <inheritdoc />
		public bool? IgnoreMalformed { get; set; }
	}

	/// <inheritdoc cref="IHistogramProperty"/>
	[DebuggerDisplay("{DebugDisplay}")]
	public class HistogramPropertyDescriptor<T>
		: PropertyDescriptorBase<HistogramPropertyDescriptor<T>, IHistogramProperty, T>, IHistogramProperty
		where T : class
	{
		bool? IHistogramProperty.IgnoreMalformed { get; set; }

		public HistogramPropertyDescriptor() : base(FieldType.Histogram) { }

		/// <inheritdoc cref="IHistogramProperty.IgnoreMalformed"/>
		public HistogramPropertyDescriptor<T> IgnoreMalformed(bool? ignoreMalformed = true) =>
			Assign(ignoreMalformed, (a, v) => a.IgnoreMalformed = v);
	}
}

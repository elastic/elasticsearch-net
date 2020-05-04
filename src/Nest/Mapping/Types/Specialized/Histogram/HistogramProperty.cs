// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Diagnostics;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

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

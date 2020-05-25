// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// Matches terms that start with a specified set of characters. This prefix can expand to match at most 128 terms.
	/// If the prefix matches more than 128 terms, Elasticsearch returns an error.
	/// You can use the index-prefixes option in the field mapping to avoid this limit.
	/// <para />
	/// Available in Elasticsearch 7.3.0+
	/// </summary>
	[ReadAs(typeof(IntervalsPrefix))]
	public interface IIntervalsPrefix : IIntervalsNoFilter
	{
		/// <summary>
		/// Analyzer used to normalize the prefix. Defaults to the top-level field's analyzer.
		/// </summary>
		[DataMember(Name = "analyzer")]
		string Analyzer { get; set; }

		/// <summary>
		/// Beginning characters of terms you wish to find in the top-level field
		/// </summary>
		[DataMember(Name = "prefix")]
		string Prefix { get; set; }

		/// <summary>
		/// If specified, then match intervals from this field rather than the top-level field.
		/// The prefix is normalized using the search analyzer from this field, unless a separate analyzer is specified.
		/// </summary>
		[DataMember(Name = "use_field")]
		Field UseField { get; set; }
	}

	/// <inheritdoc cref="IIntervalsPrefix" />
	public class IntervalsPrefix : IntervalsNoFilterBase, IIntervalsPrefix
	{
		/// <inheritdoc />
		public string Analyzer { get; set; }

		/// <inheritdoc />
		public string Prefix { get; set; }

		/// <inheritdoc />
		public Field UseField { get; set; }

		internal override void WrapInContainer(IIntervalsContainer container) => container.Prefix = this;
	}

	/// <inheritdoc cref="IIntervalsPrefix" />
	public class IntervalsPrefixDescriptor : DescriptorBase<IntervalsPrefixDescriptor, IIntervalsPrefix>, IIntervalsPrefix
	{
		string IIntervalsPrefix.Analyzer { get; set; }
		string IIntervalsPrefix.Prefix { get; set; }
		Field IIntervalsPrefix.UseField { get; set; }

		/// <inheritdoc cref="IIntervalsPrefix.Analyzer" />
		public IntervalsPrefixDescriptor Analyzer(string analyzer) => Assign(analyzer, (a, v) => a.Analyzer = v);

		/// <inheritdoc cref="IIntervalsPrefix.Prefix" />
		public IntervalsPrefixDescriptor Prefix(string prefix) => Assign(prefix, (a, v) => a.Prefix = v);

		/// <inheritdoc cref="IIntervalsPrefix.UseField" />
		public IntervalsPrefixDescriptor UseField<T>(Expression<Func<T, object>> objectPath) => Assign(objectPath, (a, v) => a.UseField = v);

		/// <inheritdoc cref="IIntervalsPrefix.UseField" />
		public IntervalsPrefixDescriptor UseField(Field useField) => Assign(useField, (a, v) => a.UseField = v);
	}
}

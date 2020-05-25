// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// Matches terms using a wildcard pattern. This pattern can expand to match at most 128 terms.
	/// If the pattern matches more than 128 terms, Elasticsearch returns an error.
	/// <para />
	/// Available in Elasticsearch 7.3.0+
	/// </summary>
	[ReadAs(typeof(IntervalsWildcard))]
	public interface IIntervalsWildcard : IIntervalsNoFilter
	{
		/// <summary>
		/// Analyzer used to normalize the prefix. Defaults to the top-level field's analyzer.
		/// </summary>
		[DataMember(Name = "analyzer")]
		string Analyzer { get; set; }

		/// <summary>
		/// Wildcard pattern used to find matching terms. Supports two wildcard operators:
		/// <para />?, which matches any single character
		/// <para />*, which can match zero or more characters, including an empty one
		/// <para />Warning: Avoid beginning patterns with * or ?. This can increase the iterations needed to find matching terms and slow search performance.
		/// </summary>
		[DataMember(Name = "pattern")]
		string Pattern { get; set; }

		/// <summary>
		/// If specified, then match intervals from this field rather than the top-level field.
		/// The prefix is normalized using the search analyzer from this field, unless a separate analyzer is specified.
		/// </summary>
		[DataMember(Name = "use_field")]
		Field UseField { get; set; }
	}

	/// <inheritdoc cref="IIntervalsWildcard" />
	public class IntervalsWildcard : IntervalsNoFilterBase, IIntervalsWildcard
	{
		/// <inheritdoc />
		public string Analyzer { get; set; }

		/// <inheritdoc />
		public string Pattern { get; set; }

		/// <inheritdoc />
		public Field UseField { get; set; }

		internal override void WrapInContainer(IIntervalsContainer container) => container.Wildcard = this;
	}

	/// <inheritdoc cref="IIntervalsWildcard" />
	public class IntervalsWildcardDescriptor : DescriptorBase<IntervalsWildcardDescriptor, IIntervalsWildcard>, IIntervalsWildcard
	{
		string IIntervalsWildcard.Analyzer { get; set; }
		string IIntervalsWildcard.Pattern { get; set; }
		Field IIntervalsWildcard.UseField { get; set; }

		/// <inheritdoc cref="IIntervalsWildcard.Analyzer" />
		public IntervalsWildcardDescriptor Analyzer(string analyzer) => Assign(analyzer, (a, v) => a.Analyzer = v);

		/// <inheritdoc cref="IIntervalsWildcard.Pattern" />
		public IntervalsWildcardDescriptor Pattern(string pattern) => Assign(pattern, (a, v) => a.Pattern = v);

		/// <inheritdoc cref="IIntervalsWildcard.UseField" />
		public IntervalsWildcardDescriptor UseField<T>(Expression<Func<T, object>> objectPath) => Assign(objectPath, (a, v) => a.UseField = v);

		/// <inheritdoc cref="IIntervalsWildcard.UseField" />
		public IntervalsWildcardDescriptor UseField(Field useField) => Assign(useField, (a, v) => a.UseField = v);
	}
}

// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// The fuzzy rule matches terms that are similar to the provided term, within an edit distance defined by Fuzziness.
	/// If the fuzzy expansion matches more than 128 terms, Elasticsearch returns an error.
	/// <para />
	/// Available in Elasticsearch 7.6.0+
	/// </summary>
	[ReadAs(typeof(IntervalsFuzzy))]
	public interface IIntervalsFuzzy : IIntervalsNoFilter
	{
		/// <summary>
		/// Analyzer used to normalize the term. Defaults to the top-level field's analyzer.
		/// </summary>
		[DataMember(Name = "analyzer")]
		string Analyzer { get; set; }

		/// <summary>
		/// Number of beginning characters left unchanged when creating expansions. Defaults to <c>0</c>.
		/// </summary>
		[DataMember(Name = "prefix_length")]
		int? PrefixLength { get; set; }

		/// <summary>
		/// Indicates whether edits include transpositions of two adjacent characters (ab → ba). Defaults to <c>true</c>.
		/// </summary>
		[DataMember(Name = "transpositions")]
		bool? Transpositions { get; set; }

		/// <summary>
		/// Maximum edit distance allowed for matching. See Fuzziness for valid values and more information.
		/// Defaults to <see cref="Nest.Fuzziness.Auto"/>.
		/// </summary>
		[DataMember(Name = "fuzziness")]
		Fuzziness Fuzziness { get; set; }

		/// <summary>
		/// The term to match.
		/// </summary>
		[DataMember(Name = "term")]
		string Term { get; set; }

		/// <summary>
		/// If specified, then match intervals from this field rather than the top-level field.
		/// The term is normalized using the search analyzer from this field,
		/// unless analyzer is specified separately.
		/// </summary>
		[DataMember(Name = "use_field")]
		Field UseField { get; set; }
	}

	/// <inheritdoc cref="IIntervalsFuzzy" />
	public class IntervalsFuzzy : IntervalsNoFilterBase, IIntervalsFuzzy
	{
		internal override void WrapInContainer(IIntervalsContainer container) => container.Fuzzy = this;

		/// <inheritdoc />
		public string Analyzer { get; set; }
		/// <inheritdoc />
		public int? PrefixLength { get; set; }
		/// <inheritdoc />
		public bool? Transpositions { get; set; }
		/// <inheritdoc />
		public Fuzziness Fuzziness { get; set; }
		/// <inheritdoc />
		public string Term { get; set; }
		/// <inheritdoc />
		public Field UseField { get; set; }
	}

	/// <inheritdoc cref="IIntervalsFuzzy" />
	public class IntervalsFuzzyDescriptor : DescriptorBase<IntervalsFuzzyDescriptor, IIntervalsFuzzy>, IIntervalsFuzzy
	{
		string IIntervalsFuzzy.Analyzer { get; set; }
		int? IIntervalsFuzzy.PrefixLength { get; set; }
		bool? IIntervalsFuzzy.Transpositions { get; set; }
		Fuzziness IIntervalsFuzzy.Fuzziness { get; set; }
		string IIntervalsFuzzy.Term { get; set; }
		Field IIntervalsFuzzy.UseField { get; set; }

		/// <inheritdoc cref="IIntervalsFuzzy.Analyzer" />
		public IntervalsFuzzyDescriptor Analyzer(string analyzer) => Assign(analyzer, (a, v) => a.Analyzer = v);

		/// <inheritdoc cref="IIntervalsFuzzy.PrefixLength" />
		public IntervalsFuzzyDescriptor PrefixLength(int? prefixLength) => Assign(prefixLength, (a, v) => a.PrefixLength = v);

		/// <inheritdoc cref="IIntervalsFuzzy.Transpositions" />
		public IntervalsFuzzyDescriptor Transpositions(bool? transpositions = true) => Assign(transpositions, (a, v) => a.Transpositions = v);

		/// <inheritdoc cref="IIntervalsFuzzy.Fuzziness" />
		public IntervalsFuzzyDescriptor Fuzziness(Fuzziness fuzziness) => Assign(fuzziness, (a, v) => a.Fuzziness = v);

		/// <inheritdoc cref="IIntervalsFuzzy.Term" />
		public IntervalsFuzzyDescriptor Term(string term) => Assign(term, (a, v) => a.Term = v);


		/// <inheritdoc cref="IIntervalsFuzzy.UseField" />
		public IntervalsFuzzyDescriptor UseField<T>(Expression<Func<T, object>> objectPath) => Assign(objectPath, (a, v) => a.UseField = v);

		/// <inheritdoc cref="IIntervalsFuzzy.UseField" />
		public IntervalsFuzzyDescriptor UseField(Field useField) => Assign(useField, (a, v) => a.UseField = v);
	}
}

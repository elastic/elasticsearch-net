// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// Matches the analyzed text
	/// </summary>
	[ReadAs(typeof(IntervalsMatch))]
	public interface IIntervalsMatch : IIntervals
	{
		/// <summary>
		/// Which analyzer should be used to analyze terms in the query.
		/// By default, the search analyzer of the top-level field will be used.
		/// </summary>
		[DataMember(Name = "analyzer")]
		string Analyzer { get; set; }

		/// <summary>
		/// Specify a maximum number of gaps between the terms in the text. Terms that appear further apart than this will not
		/// match.
		/// If unspecified, or set to -1, then there is no width restriction on the match.
		/// If set to 0 then the terms must appear next to each other.
		/// </summary>
		[DataMember(Name = "max_gaps")]
		int? MaxGaps { get; set; }

		/// <summary>
		/// Whether or not the terms must appear in their specified order. Defaults to <c>false</c>
		/// </summary>
		[DataMember(Name = "ordered")]
		bool? Ordered { get; set; }

		/// <summary>
		/// The text to match.
		/// </summary>
		[DataMember(Name = "query")]
		string Query { get; set; }

		/// <summary>
		/// If specified, then match intervals from this field rather than the top-level field.
		/// Terms will be analyzed using the search analyzer from this field.
		/// This allows you to search across multiple fields as if they were all the same field
		/// </summary>
		[DataMember(Name = "use_field")]
		Field UseField { get; set; }
	}

	/// <inheritdoc cref="IIntervalsMatch" />
	public class IntervalsMatch : IntervalsBase, IIntervalsMatch
	{
		/// <inheritdoc />
		[DataMember(Name = "analyzer")]
		public string Analyzer { get; set; }

		/// <inheritdoc />
		[DataMember(Name = "max_gaps")]
		public int? MaxGaps { get; set; }

		/// <inheritdoc />
		[DataMember(Name = "ordered")]
		public bool? Ordered { get; set; }

		/// <inheritdoc />
		public string Query { get; set; }

		/// <inheritdoc />
		[DataMember(Name = "use_field")]
		public Field UseField { get; set; }

		internal override void WrapInContainer(IIntervalsContainer container) => container.Match = this;
	}

	/// <inheritdoc cref="IIntervalsMatch" />
	public class IntervalsMatchDescriptor : IntervalsDescriptorBase<IntervalsMatchDescriptor, IIntervalsMatch>, IIntervalsMatch
	{
		string IIntervalsMatch.Analyzer { get; set; }
		int? IIntervalsMatch.MaxGaps { get; set; }
		bool? IIntervalsMatch.Ordered { get; set; }
		string IIntervalsMatch.Query { get; set; }
		Field IIntervalsMatch.UseField { get; set; }

		/// <inheritdoc cref="IIntervalsMatch.Analyzer" />
		public IntervalsMatchDescriptor Analyzer(string analyzer) => Assign(analyzer, (a, v) => a.Analyzer = v);

		/// <inheritdoc cref="IIntervalsMatch.MaxGaps" />
		public IntervalsMatchDescriptor MaxGaps(int? maxGaps) => Assign(maxGaps, (a, v) => a.MaxGaps = v);

		/// <inheritdoc cref="IIntervalsMatch.Ordered" />
		public IntervalsMatchDescriptor Ordered(bool? ordered = true) => Assign(ordered, (a, v) => a.Ordered = v);

		/// <inheritdoc cref="IIntervalsMatch.Query" />
		public IntervalsMatchDescriptor Query(string query) => Assign(query, (a, v) => a.Query = v);

		/// <inheritdoc cref="IIntervalsMatch.UseField" />
		public IntervalsMatchDescriptor UseField<T>(Expression<Func<T, object>> objectPath) => Assign(objectPath, (a, v) => a.UseField = v);

		/// <inheritdoc cref="IIntervalsMatch.UseField" />
		public IntervalsMatchDescriptor UseField(Field useField) => Assign(useField, (a, v) => a.UseField = v);
	}
}

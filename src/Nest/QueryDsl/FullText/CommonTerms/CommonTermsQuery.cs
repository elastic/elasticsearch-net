// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[Obsolete("Deprecated in 7.3.0. Use MatchQuery instead, which skips blocks of documents efficiently, without any configuration, provided that the total number of hits is not tracked.")]
	[InterfaceDataContract]
	[JsonFormatter(typeof(FieldNameQueryFormatter<CommonTermsQuery, ICommonTermsQuery>))]
	public interface ICommonTermsQuery : IFieldNameQuery
	{
		[DataMember(Name = "analyzer")]
		string Analyzer { get; set; }

		[DataMember(Name = "cutoff_frequency")]
		double? CutoffFrequency { get; set; }

		[DataMember(Name = "high_freq_operator")]

		Operator? HighFrequencyOperator { get; set; }

		[DataMember(Name = "low_freq_operator")]

		Operator? LowFrequencyOperator { get; set; }

		[DataMember(Name = "minimum_should_match")]
		MinimumShouldMatch MinimumShouldMatch { get; set; }

		[DataMember(Name = "query")]
		string Query { get; set; }
	}

	[Obsolete("Deprecated in 7.3.0. Use MatchQuery instead, which skips blocks of documents efficiently, without any configuration, provided that the total number of hits is not tracked.")]
	public class CommonTermsQuery : FieldNameQueryBase, ICommonTermsQuery
	{
		public string Analyzer { get; set; }
		public double? CutoffFrequency { get; set; }
		public Operator? HighFrequencyOperator { get; set; }
		public Operator? LowFrequencyOperator { get; set; }
		public MinimumShouldMatch MinimumShouldMatch { get; set; }
		public string Query { get; set; }
		protected override bool Conditionless => IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.CommonTerms = this;

		internal static bool IsConditionless(ICommonTermsQuery q) => q.Field.IsConditionless() || q.Query.IsNullOrEmpty();
	}

	[Obsolete("Deprecated in 7.3.0. Use MatchQuery instead, which skips blocks of documents efficiently, without any configuration, provided that the total number of hits is not tracked.")]
	public class CommonTermsQueryDescriptor<T>
		: FieldNameQueryDescriptorBase<CommonTermsQueryDescriptor<T>, ICommonTermsQuery, T>
			, ICommonTermsQuery
		where T : class
	{
		protected override bool Conditionless => CommonTermsQuery.IsConditionless(this);
		string ICommonTermsQuery.Analyzer { get; set; }
		double? ICommonTermsQuery.CutoffFrequency { get; set; }
		Field IFieldNameQuery.Field { get; set; }
		Operator? ICommonTermsQuery.HighFrequencyOperator { get; set; }
		Operator? ICommonTermsQuery.LowFrequencyOperator { get; set; }
		MinimumShouldMatch ICommonTermsQuery.MinimumShouldMatch { get; set; }
		string IQuery.Name { get; set; }
		string ICommonTermsQuery.Query { get; set; }

		/// <inheritdoc />
		public CommonTermsQueryDescriptor<T> Query(string query) => Assign(query, (a, v) => a.Query = v);

		/// <inheritdoc />
		public CommonTermsQueryDescriptor<T> HighFrequencyOperator(Operator? op) => Assign(op, (a, v) => a.HighFrequencyOperator = v);

		public CommonTermsQueryDescriptor<T> LowFrequencyOperator(Operator? op) => Assign(op, (a, v) => a.LowFrequencyOperator = v);

		/// <inheritdoc />
		public CommonTermsQueryDescriptor<T> Analyzer(string analyzer) => Assign(analyzer, (a, v) => a.Analyzer = v);

		/// <inheritdoc />
		public CommonTermsQueryDescriptor<T> CutoffFrequency(double? cutOffFrequency) => Assign(cutOffFrequency, (a, v) => a.CutoffFrequency = v);

		/// <inheritdoc />
		public CommonTermsQueryDescriptor<T> MinimumShouldMatch(MinimumShouldMatch minimumShouldMatch) =>
			Assign(minimumShouldMatch, (a, v) => a.MinimumShouldMatch = v);
	}
}

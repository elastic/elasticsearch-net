// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(CombinedFieldsQuery))]
	public interface ICombinedFieldsQuery : IQuery
	{
		/// <summary>
		/// The query to execute
		/// </summary>
		[DataMember(Name = "query")]
		string Query { get; set; }

		/// <summary>
		/// The fields to perform the query against.
		/// </summary>
		[DataMember(Name = "fields")]
		Fields Fields { get; set; }

		/// <summary>
		/// A value controlling how many "should" clauses in the resulting boolean query should match.
		/// It can be an absolute value, a percentage or a combination of both.
		/// </summary>
		[DataMember(Name = "minimum_should_match")]
		MinimumShouldMatch MinimumShouldMatch { get; set; }

		/// <summary>
		/// If `true`, match phrase queries are automatically created for multi-term synonyms.
		/// </summary>
		[DataMember(Name = "auto_generate_synonyms_phrase_query")]
		bool? AutoGenerateSynonymsPhraseQuery { get; set; }

		/// <summary>
		/// The operator used if no explicit operator is specified.
		/// The default operator is <see cref="Nest.Operator.Or" />
		/// </summary>
		/// <remarks>
		/// <see cref="TextQueryType.BestFields" /> and <see cref="TextQueryType.MostFields" /> types are field-centric?;
		/// they generate a match query per field. This means that <see cref="Operator" /> and <see cref="MinimumShouldMatch" />
		/// are applied to each field individually, which is probably not what you want.
		/// Consider using <see cref="TextQueryType.CrossFields" />.
		/// </remarks>
		[DataMember(Name = "operator")]
		Operator? Operator { get; set; }

		/// <summary>
		/// If the analyzer used removes all tokens in a query like a stop filter does, the default behavior is
		/// to match no documents at all. In order to change that, <see cref="Nest.ZeroTermsQuery" /> can be used,
		/// which accepts <see cref="Nest.ZeroTermsQuery.None" /> (default) and <see cref="Nest.ZeroTermsQuery.All" />
		/// which corresponds to a match_all query.
		/// </summary>
		[DataMember(Name = "zero_terms_query")]
		ZeroTermsQuery? ZeroTermsQuery { get; set; }
	}

	/// <inheritdoc cref="ICombinedFieldsQuery" />
	[DataContract]
	public class CombinedFieldsQuery : QueryBase, ICombinedFieldsQuery
	{
		/// <inheritdoc />
		public string Query { get; set; }
		/// <inheritdoc />
		public Fields Fields { get; set; }
		/// <inheritdoc />
		public MinimumShouldMatch MinimumShouldMatch { get; set; }
		/// <inheritdoc />
		public bool? AutoGenerateSynonymsPhraseQuery { get; set; }
		/// <inheritdoc />
		public Operator? Operator { get; set; }
		/// <inheritdoc />
		public ZeroTermsQuery? ZeroTermsQuery { get; set; }

		protected override bool Conditionless => IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.CombinedFields = this;

		internal static bool IsConditionless(ICombinedFieldsQuery q) => q.Fields.IsConditionless() || q.Query.IsNullOrEmpty();
	}

	public class CombinedFieldsQueryDescriptor<T>
		: QueryDescriptorBase<CombinedFieldsQueryDescriptor<T>, ICombinedFieldsQuery>, ICombinedFieldsQuery where T : class
	{
		protected override bool Conditionless => CombinedFieldsQuery.IsConditionless(this);
		
		string ICombinedFieldsQuery.Query { get; set; }
		Fields ICombinedFieldsQuery.Fields { get; set; }
		MinimumShouldMatch ICombinedFieldsQuery.MinimumShouldMatch { get; set; }
		bool? ICombinedFieldsQuery.AutoGenerateSynonymsPhraseQuery { get; set; }
		Operator? ICombinedFieldsQuery.Operator { get; set; }
		ZeroTermsQuery? ICombinedFieldsQuery.ZeroTermsQuery { get; set; }

		/// <inheritdoc cref="ICombinedFieldsQuery.Query" />
		public CombinedFieldsQueryDescriptor<T> Query(string query) => Assign(query, (a, v) => a.Query = v);

		/// <inheritdoc cref="ICombinedFieldsQuery.Fields" />
		public CombinedFieldsQueryDescriptor<T> Fields(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) =>
			Assign(fields, (a, v) => a.Fields = v?.Invoke(new FieldsDescriptor<T>())?.Value);

		/// <inheritdoc cref="ICombinedFieldsQuery.Fields" />
		public CombinedFieldsQueryDescriptor<T> Fields(Fields fields) => Assign(fields, (a, v) => a.Fields = v);

		/// <inheritdoc cref="ICombinedFieldsQuery.MinimumShouldMatch" />
		public CombinedFieldsQueryDescriptor<T> MinimumShouldMatch(MinimumShouldMatch minimumShouldMatch)
			=> Assign(minimumShouldMatch, (a, v) => a.MinimumShouldMatch = v);

		/// <inheritdoc cref="ICombinedFieldsQuery.Operator" />
		public CombinedFieldsQueryDescriptor<T> Operator(Operator? op) => Assign(op, (a, v) => a.Operator = v);

		/// <inheritdoc cref="ICombinedFieldsQuery.ZeroTermsQuery" />
		public CombinedFieldsQueryDescriptor<T> ZeroTermsQuery(ZeroTermsQuery? zeroTermsQuery) => Assign(zeroTermsQuery, (a, v) => a.ZeroTermsQuery = v);

		/// <inheritdoc cref="ICombinedFieldsQuery.AutoGenerateSynonymsPhraseQuery" />
		public CombinedFieldsQueryDescriptor<T> AutoGenerateSynonymsPhraseQuery(bool? autoGenerateSynonymsPhraseQuery = true) =>
			Assign(autoGenerateSynonymsPhraseQuery, (a, v) => a.AutoGenerateSynonymsPhraseQuery = v);
	}
}

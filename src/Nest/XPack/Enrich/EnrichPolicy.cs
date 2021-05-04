// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// An enrich policy
	/// </summary>
	[InterfaceDataContract]
	[ReadAs(typeof(EnrichPolicy))]
	public interface IEnrichPolicy
	{
		/// <summary>
		/// Source indices used to create the enrich index.
		/// If multiple indices are provided, they must share a common <see cref="MatchField"/>, which the enrich processor can use
		/// to match incoming documents.
		/// </summary>
		[DataMember(Name = "indices")]
		[JsonFormatter(typeof(IndicesFormatter))]
		Indices Indices { get; set; }

		/// <summary>
		/// Field in the source indices used to match incoming documents.
		/// </summary>
		[DataMember(Name = "match_field")]
		Field MatchField { get; set; }

		/// <summary>
		/// Fields to add to matching incoming documents. These fields must be present in the source indices.
		/// </summary>
		[DataMember(Name = "enrich_fields")]
		Fields EnrichFields { get; set; }

		/// <summary>
		/// Query type used to filter documents in the enrich index for matching.
		/// </summary>
		[DataMember(Name = "query")]
		string Query { get; set; }
	}


	/// <inheritdoc />
	[DataContract]
	public class EnrichPolicy : IEnrichPolicy
	{
		/// <inheritdoc />
		public Indices Indices { get; set; }

		/// <inheritdoc />
		public Field MatchField { get; set; }

		/// <inheritdoc />
		public Fields EnrichFields { get; set; }

		/// <inheritdoc />
		public string Query { get; set; }
	}

	public class EnrichPolicyDescriptor<TDocument> : DescriptorBase<EnrichPolicyDescriptor<TDocument>, IEnrichPolicy>, IEnrichPolicy where TDocument : class
	{
		Indices IEnrichPolicy.Indices { get; set; }
		Field IEnrichPolicy.MatchField { get; set; }
		Fields IEnrichPolicy.EnrichFields { get; set; }
		string IEnrichPolicy.Query { get; set; }

		/// <inheritdoc cref="IEnrichPolicy.Indices"/>
		public EnrichPolicyDescriptor<TDocument> Indices(Indices indices) => Assign(indices, (a, v) => a.Indices = v);

		/// <inheritdoc cref="IEnrichPolicy.MatchField"/>
		public EnrichPolicyDescriptor<TDocument> MatchField(Field matchField) => Assign(matchField, (a, v) => a.MatchField = v);

		/// <inheritdoc cref="IEnrichPolicy.MatchField"/>
		public EnrichPolicyDescriptor<TDocument> MatchField<TValue>(Expression<Func<TDocument, TValue>> objectPath) => Assign(objectPath, (a, v) => a.MatchField = v);

		/// <inheritdoc cref="IEnrichPolicy.EnrichFields"/>
		public EnrichPolicyDescriptor<TDocument> EnrichFields(Fields enrichFields) => Assign(enrichFields, (a, v) => a.EnrichFields = v);

		/// <inheritdoc cref="IEnrichPolicy.EnrichFields"/>
		public EnrichPolicyDescriptor<TDocument> EnrichFields(Func<FieldsDescriptor<TDocument>, IPromise<Fields>> fields) =>
			Assign(fields, (a, v) => a.EnrichFields = v?.Invoke(new FieldsDescriptor<TDocument>())?.Value);

		/// <inheritdoc cref="IEnrichPolicy.Query"/>
		public EnrichPolicyDescriptor<TDocument> Query(string query) => Assign(query, (a, v) => a.Query = v);
	}
}

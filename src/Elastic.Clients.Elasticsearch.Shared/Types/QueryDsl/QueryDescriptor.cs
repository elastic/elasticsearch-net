// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Linq.Expressions;

#if ELASTICSEARCH_SERVERLESS
namespace Elastic.Clients.Elasticsearch.Serverless.QueryDsl;
#else
namespace Elastic.Clients.Elasticsearch.QueryDsl;
#endif

public sealed partial class QueryDescriptor<TDocument>
{
	/// <summary>
	/// A query defined as a raw JSON string. This can be useful when support for a built-in query is not yet available.
	/// </summary>
	public QueryDescriptor<TDocument> RawJson(string rawJson) => Set(new RawJsonQuery(rawJson), "raw_json");

	public QueryDescriptor<TDocument> MatchAll() =>
		Set<MatchAllQueryDescriptor>(_ => { }, "match_all");

	public QueryDescriptor<TDocument> Term<TValue>(Expression<Func<TDocument, TValue>> field, object value, float? boost = null) =>
		Term(t => t.Field(field).Value(FieldValue.Composite(value)).Boost(boost));

	/// <summary>
	/// Used purely for testing (for now).
	/// </summary>
	internal bool TestHasVariant => ContainsVariant;
}

public sealed partial class QueryDescriptor
{
	/// <summary>
	/// A query defined as a raw JSON string. This can be useful when support for a built-in query is not yet available.
	/// </summary>
	public QueryDescriptor RawJson(string rawJson) => Set(new RawJsonQuery(rawJson), "raw_json");

	public QueryDescriptor MatchAll() =>
		Set<MatchAllQueryDescriptor>(_ => { }, "match_all");

	public QueryDescriptor Term<TDocument, TValue>(Expression<Func<TDocument, TValue>> field, object value, float? boost = null) =>
		Term(t => t.Field(field).Value(FieldValue.Composite(value)).Boost(boost));

	/// <summary>
	/// Used purely for testing (for now).
	/// </summary>
	internal bool TestHasVariant => ContainsVariant;
}

// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Linq.Expressions;

namespace Elastic.Clients.Elasticsearch.QueryDsl;

public sealed partial class QueryDescriptor<TDocument>
{
	/// <summary>
	/// A query defined as a raw JSON string. This can be useful when support for a built-in query is not yet available.
	/// </summary>
	public QueryDescriptor<TDocument> RawJson(string rawJson) => Set(new RawJsonQuery(rawJson), "raw_json");

	public void MatchAll() =>
		Set<MatchAllQueryDescriptor>(_ => { }, "match_all");

	public void Term<TValue>(Expression<Func<TDocument, TValue>> field, object value, float? boost = null) =>
		Term(t => t.Field(field).Value(FieldValue.Composite(value)).Boost(boost));
}

public sealed partial class QueryDescriptor
{
	/// <summary>
	/// A query defined as a raw JSON string. This can be useful when support for a built-in query is not yet available.
	/// </summary>
	public QueryDescriptor RawJson(string rawJson) => Set(new RawJsonQuery(rawJson), "raw_json");

	public void MatchAll() =>
		Set<MatchAllQueryDescriptor>(_ => { }, "match_all");

	public void Term<TDocument, TValue>(Expression<Func<TDocument, TValue>> field, object value, float? boost = null) =>
			Term(t => t.Field(field).Value(FieldValue.Composite(value)).Boost(boost));
}

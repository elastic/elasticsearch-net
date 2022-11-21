// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Linq.Expressions;

namespace Elastic.Clients.Elasticsearch.QueryDsl;

public sealed partial class QueryDescriptor<TDocument>
{
	public void MatchAll() =>
		Set<MatchAllQueryDescriptor>(_ => { }, "match_all");

	public void Term<TValue>(Expression<Func<TDocument, TValue>> field, object value, float? boost = null) =>
		Term(t => t.Field(field).Value(FieldValue.Composite(value)).Boost(boost));
}

public sealed partial class QueryDescriptor
{
	public void MatchAll() =>
		Set<MatchAllQueryDescriptor>(_ => { }, "match_all");

	public void Term<TDocument, TValue>(Expression<Func<TDocument, TValue>> field, object value, float? boost = null) =>
			Term(t => t.Field(field).Value(FieldValue.Composite(value)).Boost(boost));
}

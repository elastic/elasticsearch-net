// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

#if ELASTICSEARCH_SERVERLESS
namespace Elastic.Clients.Elasticsearch.Serverless.QueryDsl;
#else
namespace Elastic.Clients.Elasticsearch.QueryDsl;
#endif

internal static class BoolQueryExtensions
{
	internal static Query Self(this Query q) => q;

	internal static bool HasOnlyShouldClauses(this BoolQuery boolQuery) =>
		boolQuery != null &&
		boolQuery.Should.HasAny() &&
		!boolQuery.Must.HasAny() &&
		!boolQuery.MustNot.HasAny() &&
		!boolQuery.Filter.HasAny();

	internal static bool HasOnlyFilterClauses(this BoolQuery boolQuery) =>
		boolQuery != null &&
		!boolQuery.Should.HasAny() &&
		!boolQuery.Must.HasAny() &&
		!boolQuery.MustNot.HasAny() &&
		boolQuery.Filter.HasAny();

	internal static bool HasOnlyMustNotClauses(this BoolQuery boolQuery) =>
		boolQuery != null &&
		!boolQuery.Should.HasAny() &&
		!boolQuery.Must.HasAny() &&
		boolQuery.MustNot.HasAny() &&
		!boolQuery.Filter.HasAny();
}

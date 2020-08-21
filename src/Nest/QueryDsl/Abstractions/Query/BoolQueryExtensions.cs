// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	internal static class BoolQueryExtensions
	{
		internal static IQueryContainer Self(this QueryContainer q) => q;

		internal static bool HasOnlyShouldClauses(this IBoolQuery boolQuery) =>
			boolQuery != null && !boolQuery.IsVerbatim && boolQuery.Should.HasAny() && !boolQuery.Must.HasAny() && !boolQuery.MustNot.HasAny()
			&& !boolQuery.Filter.HasAny();

		internal static bool HasOnlyFilterClauses(this IBoolQuery boolQuery) =>
			boolQuery != null && !boolQuery.IsVerbatim && !boolQuery.Locked && !boolQuery.Should.HasAny() && !boolQuery.Must.HasAny()
			&& !boolQuery.MustNot.HasAny() && boolQuery.Filter.HasAny();

		internal static bool HasOnlyMustNotClauses(this IBoolQuery boolQuery) =>
			boolQuery != null && !boolQuery.IsVerbatim && !boolQuery.Locked && !boolQuery.Should.HasAny() && !boolQuery.Must.HasAny()
			&& boolQuery.MustNot.HasAny() && !boolQuery.Filter.HasAny();
	}
}

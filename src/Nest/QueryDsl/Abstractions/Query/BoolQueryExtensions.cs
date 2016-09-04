using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	internal static class BoolQueryExtensions
	{

		internal static IQueryContainer Self(this QueryContainer q) => q;

		internal static bool HasOnlyShouldClauses(this IBoolQuery boolQuery) =>
			boolQuery != null && boolQuery.IsWritable && (
				boolQuery.Should.HasAny()
				&& !boolQuery.Must.HasAny()
				&& !boolQuery.MustNot.HasAny()
				&& !boolQuery.Filter.HasAny()
			);

		internal static bool HasOnlyFilterClauses(this IBoolQuery boolQuery) =>
			boolQuery != null && boolQuery.IsWritable && !boolQuery.Locked && (
				!boolQuery.Should.HasAny()
				&& !boolQuery.Must.HasAny()
				&& !boolQuery.MustNot.HasAny()
				&& boolQuery.Filter.HasAny()
			);

		internal static bool HasOnlyMustNotClauses(this IBoolQuery boolQuery) =>
			boolQuery != null && boolQuery.IsWritable && !boolQuery.Locked && (
				!boolQuery.Should.HasAny()
				&& !boolQuery.Must.HasAny()
				&& boolQuery.MustNot.HasAny()
			&& !boolQuery.Filter.HasAny()
		);

	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	internal static class QueryCondition
	{
		public static bool IsConditionless(IGeoHashCellQuery query)
		{
			return query.Location.IsNullOrEmpty();
		}

		public static bool IsConditionless(IGeoPolygonQuery query)
		{
			return !query.Points.HasAny() || query.Points.All(p => p.IsNullOrEmpty());
		}

		public static bool IsConditionless(IScriptQuery query)
		{
			return query.Script.IsNullOrEmpty();
		}

		public static bool IsConditionless(IExistsQuery query)
		{
			return query.Field.IsConditionless();
		}

		public static bool IsConditionless(IMissingQuery query)
		{
			return query.Field.IsConditionless();
		}

		public static bool IsConditionless(ITypeQuery query)
		{
			return query.Value.IsConditionless();
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public static class FieldNameExtensions
	{
		internal static bool IsConditionless(this FieldName field)
		{
			return field == null || (field.Name.IsNullOrEmpty() && field.Expression == null && field.Type == null);
		}
	}
}

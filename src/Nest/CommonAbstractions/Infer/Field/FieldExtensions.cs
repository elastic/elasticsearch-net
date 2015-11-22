using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	internal static class FieldExtensions
	{
		internal static bool IsConditionless(this Field field)
		{
			return field == null || (field.Name.IsNullOrEmpty() && field.Expression == null && field.Property == null);
		}
		internal static bool IsConditionless(this Fields field) => field.ListOfFields.All(l => l.IsConditionless());
	}
}

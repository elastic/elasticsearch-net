using System.Linq;

namespace Nest_5_2_0
{
	internal static class FieldExtensions
	{
		internal static bool IsConditionless(this Field field)
		{
			return field == null || (field.Name.IsNullOrEmpty() && field.Expression == null && field.Property == null);
		}
		internal static bool IsConditionless(this Fields field) => field?.ListOfFields == null || field.ListOfFields.All(l => l.IsConditionless());
	}
}

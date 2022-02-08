using Nest.Core;

namespace Nest
{
	internal static class PropertyNameExtensions
	{
		internal static bool IsConditionless(this PropertyName property) =>
			property == null || property.Name.IsNullOrEmpty() && property.Expression == null &&
			property.Property == null;
	}
}

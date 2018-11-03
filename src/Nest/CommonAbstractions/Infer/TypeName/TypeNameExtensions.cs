namespace Nest
{
	internal static class TypeNameExtensions
	{
		internal static bool IsConditionless(this TypeName marker) => marker == null || marker.Name.IsNullOrEmpty() && marker.Type == null;
	}
}

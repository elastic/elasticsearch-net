namespace Nest
{
	internal static class TypeNameExtensions
	{
		internal static bool IsConditionless(this TypeName marker)
		{
			return marker == null || marker.Name.IsNullOrEmpty() && marker.Type == null;
		}
	}
}

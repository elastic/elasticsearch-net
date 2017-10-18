namespace Nest
{
	internal static class RelationNameExtensions
	{
		internal static bool IsConditionless(this RelationName marker)
		{
			return marker == null || marker.Name.IsNullOrEmpty() && marker.Type == null;
		}
	}
}

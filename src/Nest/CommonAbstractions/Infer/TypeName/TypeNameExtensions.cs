namespace Nest
{
	public static class TypeNameExtensions
	{
		
		public static bool IsConditionless(this TypeName marker)
		{
			return marker == null || marker.Name.IsNullOrEmpty() && marker.Type == null;
		}
	}
}
using Nest.Resolvers;

namespace Nest
{
	public static class TypeNameMarkerExtensions
	{
		public static string Resolve(this TypeNameMarker marker, IConnectionSettings connectionSettings)
		{
			if (marker == null) return null;
			connectionSettings.ThrowIfNull("connectionSettings");

			if (marker.Type == null)
				return marker.Name;
			return new TypeNameResolver(connectionSettings).GetTypeNameFor(marker.Type);

		}
		public static bool IsConditionless(this TypeNameMarker marker)
		{
			return marker == null || marker.Name.IsNullOrEmpty() && marker.Type == null;
		}
	}
}
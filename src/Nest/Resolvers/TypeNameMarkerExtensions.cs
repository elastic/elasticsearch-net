using Shared.Extensions;
using Nest.Resolvers;
using Elasticsearch.Net;

namespace Nest
{
	public static class TypeNameMarkerExtensions
	{
		public static bool IsConditionless(this TypeNameMarker marker)
		{
			return marker == null || marker.Name.IsNullOrEmpty() && marker.Type == null;
		}

		public static bool IsNullOrEmpty(this TypeNameMarker value)
		{
			return value == null || value.GetHashCode() == 0;
		}
	}
}
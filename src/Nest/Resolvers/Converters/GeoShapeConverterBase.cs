using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest.Resolvers.Converters
{
	public abstract class GeoShapeConverterBase : JsonConverter
	{
		public virtual T GetCoordinates<T>(JToken shape)
		{
			var coordinates = shape["coordinates"];
			if (coordinates != null)
				return coordinates.ToObject<T>();
			return default(T);
		}
	}
}

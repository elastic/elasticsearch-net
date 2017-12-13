using Newtonsoft.Json;

namespace Nest
{
	internal static class StatefulSerializerFactory
	{
		public static JsonNetSerializer CreateStateful(IConnectionSettingsValues settings, JsonConverter converter) =>
			new JsonNetSerializer(settings, converter);
	}
}

using Newtonsoft.Json;

namespace Nest
{
	internal static class StatefulSerializerFactory
	{
		public static InternalSerializer CreateStateful(IConnectionSettingsValues settings, JsonConverter converter) =>
			new InternalSerializer(settings, converter);
	}
}

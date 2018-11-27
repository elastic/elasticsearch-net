using System.Runtime.Serialization;

namespace Nest
{
	internal static class StatefulSerializerFactory
	{
		public static InternalSerializer CreateStateful(IConnectionSettingsValues settings, JsonConverter converter) =>
			null; //new InternalSerializer(settings, converter);
	}
}
